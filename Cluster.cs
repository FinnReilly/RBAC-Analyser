using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace RBACS
{
    public class Cluster:ICloneable
    {
        protected List<Tuple<string, DenseVector>> memberList;
        
        protected DenseVector centroid;
        protected string clusterID;
        double xValue;

        public List<Tuple<string, DenseVector>> MemberList => memberList;
        public string ClusterID => clusterID;
        public int MemberCount => memberList.Count;
        public int ListPosition;
        public double XValue => xValue;

        public Cluster(List<Tuple<string, DenseVector>> Members, string ID, double XValue=0)
        {
            memberList = Members;
            clusterID = ID;
            xValue = XValue;
        }
        public void Merge(Cluster SubsumedCluster)
        {
            memberList.AddRange(SubsumedCluster.MemberList);
            memberList.OrderBy(o => o.Item1);
            clusterID = clusterID + "_" + SubsumedCluster.ClusterID;
            xValue = (Math.Abs((this.xValue - SubsumedCluster.xValue)) / 2) + Math.Min(this.xValue, SubsumedCluster.xValue);
            //SubsumedCluster = null;
        }
        public void ResetClusterID(string Input)
        {
            clusterID = Input;
        }
        public void SetCentroidAsMean(bool UseParallel=false)
        {
            if (MemberCount > 0)
            {
                DenseVector MeanVector = new DenseVector(memberList[0].Item2.Count);
                foreach(Tuple<string, DenseVector>Member in memberList)
                {
                    MeanVector += Member.Item2;
                }
                //parallel can be used if required, good for high-dimensional problems
                //not recommended for use inside another parallel loop
                if (UseParallel)
                {
                    Parallel.For(0, MeanVector.Count, i => {
                        MeanVector[i] = (double)Decimal.Divide((decimal)MeanVector[i], MemberCount);
                    });
                }
                else
                {
                    for(int i = 0; i < MeanVector.Count; i++)
                    {
                        MeanVector[i] = (double)Decimal.Divide((decimal)MeanVector[i], MemberCount);
                    }
                }
                centroid = MeanVector;
            }
        }
        public void SetCentroid(DenseVector Replacement)
        {
            centroid = Replacement;
        }
        public DenseVector GetCentroid()
        {
            //returns reference not value type
            return centroid;
        }
        public void AddMembers(List<Tuple<string, DenseVector>>InputMembers)
        {
            memberList.AddRange(InputMembers);
            memberList.OrderBy(o => o.Item1);
        }
        public void FlushMembers()
        {
            memberList = new List<Tuple<string, DenseVector>>();
        }
        public void RemoveMember(string UID)
        {
            Tuple<string, DenseVector> Chosen = (from Tup in memberList where Tup.Item1 == UID select Tup).ToList()[0];
        }
        public List<Tuple<string,DenseVector>> StealMembers(List<string> UIDs)
        {
            ConcurrentBag<Tuple<string, DenseVector>> ReturnBag = new ConcurrentBag<Tuple<string, DenseVector>>();
            //ConcurrentBag<Tuple<string, DenseVector>> ReplaceBag = new ConcurrentBag<Tuple<string, DenseVector>>(memberList);
            Parallel.ForEach(UIDs, UID => {
                Tuple<string, DenseVector> CorrespondingTuple = (from Tup in memberList where Tup.Item1 == UID select Tup).ToList()[0];
                ReturnBag.Add(CorrespondingTuple);    
            });
            List<Tuple<string, DenseVector>> ReturnList = ReturnBag.ToList();
            foreach(Tuple<string, DenseVector> ChosenMember in ReturnList)
            {
                memberList.Remove(ChosenMember);
            }
            memberList.OrderBy(o => o.Item1);
            return ReturnList;
        }

        public object Clone()
        {
            List<Tuple<string, DenseVector>> ClonedMembers = new List<Tuple<string, DenseVector>>(memberList);
            Cluster ClonedCluster = new Cluster(ClonedMembers, clusterID, xValue);
            return ClonedCluster;
        }
    }

    public class KohonenNeuron : Cluster
    {
        private DenseVector coordinates;
        public DenseVector Coordinates => coordinates;

        public KohonenNeuron(List<Tuple<string, DenseVector>> Members, string ID, Tuple<int,int>Coordinates):base(Members,ID)
        {
            coordinates = new DenseVector(2);
            coordinates[0] = Coordinates.Item1;
            coordinates[1] = Coordinates.Item2;
        }
    }

    public abstract class ClusteringAlgo
    {
        protected List<Cluster> clusters;
        protected List<Tuple<string, DenseVector>> data;
        protected int iterator;
        protected bool stopped;
        public List<Cluster> Clusters => clusters;
        public int Iterator => iterator;
        public bool Stopped => stopped;

        protected void updateClusterIndexes(List<Cluster> ClustersIn)
        {
            Parallel.For(0, ClustersIn.Count, i => {
                ClustersIn[i].ListPosition = i;
            });
        }
        public abstract bool StoppingConditionMet();
        public abstract void IterateOnce();
        public abstract void InitialiseClusters();
        public abstract void IterateUntilStopped();

        public void SetCentroidsAsMeans()
        {
            foreach (Cluster Clust in clusters)
            {
                Clust.SetCentroidAsMean();
            }
        }
        public void SetCentroidsAsMeansHighDimensionality()
        {
            foreach (Cluster Clust in clusters)
            {
                Clust.SetCentroidAsMean(true);
            }
        }


        public ClusteringAlgo(List<Tuple<string, DenseVector>> InputData) {
            data = InputData.OrderBy(o=>o.Item1).ToList();
            clusters = new List<Cluster>();
            iterator = 0;
            stopped = false;
        }
    }

    public enum HACDistanceStyle
    {
        SLink,
        CLink,
        MeanDist,
        Centroid,
        Ward
    }

    public enum HACStoppingStyle
    {
        ProportionOfFirstDistance,
        ProportionOfLastDistance,
        CustomDistance,
        Iterations,
        None
    }

    public enum KMeansStoppingStyle
    {
        NoChangeInCentroids,
        Iterations
    }

    public class HACAlgo : ClusteringAlgo
    {
        //strings in tuple are clusterids, doubles are height and x-axis values respectively
        List<Tuple<string, string, double, double>> dendrogram;
        DenseMatrix currentDistanceMatrix;
        HACDistanceStyle style;
        HACStoppingStyle stoppingStyle;
        double stoppingMetric;
        double maxDistance;
        double nextDistance;
        double lastDistance;
        double artificialMax;

        public double CustomMaxDistance {
            get { return maxDistance; }
            set
            {
                if (stoppingStyle == HACStoppingStyle.CustomDistance)
                {
                    maxDistance = value;
                }
            }
        }
        
        public List<Tuple<string, string, double, double>> Dendrogram => dendrogram;

        double runLanceWilliamsEquation(double DistanceKI, double DistanceKJ, double DistanceIJ, double AI, double AJ, double B, double G)
        {
            return (AI * DistanceKI) + (AJ * DistanceKJ) + (B * DistanceIJ) + (G * (Math.Abs(DistanceKI - DistanceKJ)));
        }

        double calculateMeanAlpha(Cluster ClusterI, Cluster ClusterJ)
        {
            return (double)Decimal.Divide(ClusterI.MemberCount, (ClusterI.MemberCount + ClusterJ.MemberCount));
        }
        double calculateCentroidBeta(Cluster ClusterI, Cluster ClusterJ)
        {
            return (double)Decimal.Divide((decimal)(-(decimal)ClusterI.MemberCount * (decimal)ClusterJ.MemberCount) , (decimal)Math.Pow((ClusterI.MemberCount + ClusterJ.MemberCount),2));
        }
        double calculateWardAlpha(Cluster ClusterI, Cluster ClusterJ, Cluster ClusterK)
        {
            return (double)Decimal.Divide((ClusterI.MemberCount + ClusterK.MemberCount), (ClusterI.MemberCount + ClusterJ.MemberCount + ClusterK.MemberCount));
        }
        double calculateWardBeta(Cluster ClusterI, Cluster ClusterJ, Cluster ClusterK)
        {
            return (double)Decimal.Divide((-ClusterK.MemberCount), (ClusterI.MemberCount + ClusterJ.MemberCount + ClusterK.MemberCount));
        }

        public override void InitialiseClusters()
        {
            ConcurrentBag<Cluster> ClusterBag = new ConcurrentBag<Cluster>();
            Parallel.For(0, data.Count, i => {
                List<Tuple<string, DenseVector>> CurrentList = new List<Tuple<string, DenseVector>>();
                CurrentList.Add(data[i]);
                Cluster CurrentCluster = new Cluster(CurrentList, i.ToString(), Convert.ToDouble(i));
                CurrentCluster.ListPosition = i;
                ClusterBag.Add(CurrentCluster);
            });
            clusters = ClusterBag.OrderBy(c=>Convert.ToInt32(c.ClusterID)).ToList();
            updateClusterIndexes(clusters);
            /*Parallel.ForEach<Cluster>(clusters, Clust => {
                Clust.ResetClusterID(Clust.ListPosition.ToString());
            });*/

            for(int i = 0; i < currentDistanceMatrix.RowCount; i++)
            {
                Parallel.For(0, currentDistanceMatrix.ColumnCount, j => {
                    if (i <= j)
                    {
                        if (i == j)
                        {
                            currentDistanceMatrix[i, j] = 0;
                        }
                        else
                        {
                            currentDistanceMatrix[i, j] = HelperFunctions.GetEuclideanDistance(data[i].Item2, data[j].Item2);
                        }
                    }
                    else
                    {
                        currentDistanceMatrix[i, j] = currentDistanceMatrix[j, i];
                    }
                });
            }

            artificialMax = currentDistanceMatrix.Values.Max() + 1;
            Parallel.For(0, currentDistanceMatrix.RowCount, i => {
                currentDistanceMatrix[i, i] = artificialMax;
            });

            if (stoppingStyle == HACStoppingStyle.ProportionOfFirstDistance)
            {
                double NextNonZeroDistance = currentDistanceMatrix.Enumerate(Zeros.AllowSkip).ToList().Min();
                maxDistance = stoppingMetric * NextNonZeroDistance;
                nextDistance = currentDistanceMatrix.Values.Min();
            }
            
            else
            {
                nextDistance = currentDistanceMatrix.Values.Min();
            }
        }

        public override void IterateOnce()
        {
            double AlphaI = 0;
            double AlphaJ = 0;
            double Beta = 0;
            double Gamma = 0;
            int Winner;
            if (!stopped)
            {
                if (clusters.Count > 1)
                {
                    DenseMatrix InterimMatrix = new DenseMatrix(currentDistanceMatrix.RowCount);
                    currentDistanceMatrix.CopyTo(InterimMatrix);
                    Cluster[] InterimClusters = new Cluster[clusters.Count];
                    Parallel.For(0, clusters.Count, i => {
                        InterimClusters[i] = (Cluster)clusters[i].Clone();
                    });
                    List<Cluster> InterimClusterList = InterimClusters.ToList<Cluster>();

                    Tuple<int, int, double> CurrentMinimumDistance = new Tuple<int, int, double>(0, 0, 0);
                    Parallel.ForEach<Tuple<int, int, double>>(currentDistanceMatrix.EnumerateIndexed(), CurrentTuple =>
                    {
                        if (CurrentTuple.Item3 == nextDistance)
                        {
                            CurrentMinimumDistance = CurrentTuple;
                        }
                    });
                    switch (style)
                    //this loops initialises coefficient values for the lance williams algorithm - this is within the iterate once method
                    //as some styles require iterative recalculation
                    {
                        case HACDistanceStyle.Centroid:
                            AlphaI = calculateMeanAlpha(clusters[CurrentMinimumDistance.Item1], clusters[CurrentMinimumDistance.Item2]);
                            AlphaJ = calculateMeanAlpha(clusters[CurrentMinimumDistance.Item2], clusters[CurrentMinimumDistance.Item1]);
                            Beta = calculateCentroidBeta(clusters[CurrentMinimumDistance.Item1], clusters[CurrentMinimumDistance.Item2]);
                            Gamma = 0;
                            break;
                        case HACDistanceStyle.CLink:
                            AlphaI = 0.5;
                            AlphaJ = 0.5;
                            Beta = 0;
                            Gamma = 0.5;
                            break;
                        case HACDistanceStyle.MeanDist:
                            AlphaI = calculateMeanAlpha(clusters[CurrentMinimumDistance.Item1], clusters[CurrentMinimumDistance.Item2]);
                            AlphaJ = calculateMeanAlpha(clusters[CurrentMinimumDistance.Item2], clusters[CurrentMinimumDistance.Item1]);
                            Beta = 0;
                            Gamma = 0;
                            break;
                        case HACDistanceStyle.SLink:
                            AlphaI = 0.5;
                            AlphaJ = 0.5;
                            Beta = 0;
                            Gamma = -0.5;
                            break;
                        case HACDistanceStyle.Ward:
                            //has to be implemented inside matrix update loop
                            //as requires values for cluster k
                            break;
                    }
                    //handle cluster merging in list
                    InterimClusterList[CurrentMinimumDistance.Item1].Merge(InterimClusterList[CurrentMinimumDistance.Item2]);
                    InterimClusterList.RemoveAt(CurrentMinimumDistance.Item2);
                    updateClusterIndexes(InterimClusterList);
                    //handles removal of subsumed cluster's distances from distance matrix
                    if (CurrentMinimumDistance.Item1 < CurrentMinimumDistance.Item2)
                    {
                        Winner = CurrentMinimumDistance.Item1;
                    }
                    else
                    {
                        //if the item 1 value is further down the distances matrix than the row/column to be removed
                        Winner = CurrentMinimumDistance.Item1 - 1;
                    }
                    InterimMatrix = (DenseMatrix)InterimMatrix.RemoveColumn(CurrentMinimumDistance.Item2).RemoveRow(CurrentMinimumDistance.Item2);
                    Parallel.For(0, InterimMatrix.RowCount, i =>
                    {
                        //recalculate distances from new cluster to all other clusters
                        if (style == HACDistanceStyle.Ward)
                        {
                            AlphaI = calculateWardAlpha(clusters[CurrentMinimumDistance.Item1], clusters[CurrentMinimumDistance.Item2], clusters[i]);
                            AlphaJ = calculateWardAlpha(clusters[CurrentMinimumDistance.Item2], clusters[CurrentMinimumDistance.Item1], clusters[i]);
                            Beta = calculateWardBeta(clusters[CurrentMinimumDistance.Item1], clusters[CurrentMinimumDistance.Item2], clusters[i]);
                            Gamma = 0;
                        }
                        double DistanceIJ = nextDistance;
                        double DistanceIK = currentDistanceMatrix[CurrentMinimumDistance.Item1, i];
                        double DistanceJK = currentDistanceMatrix[CurrentMinimumDistance.Item2, i];
                        InterimMatrix[Winner, i] = runLanceWilliamsEquation(DistanceIK, DistanceJK, DistanceIJ, AlphaI, AlphaJ, Beta, Gamma);
                        InterimMatrix[i, Winner] = InterimMatrix[Winner, i];
                        InterimMatrix[Winner, Winner] = artificialMax;
                    });
                    lastDistance = nextDistance;
                    nextDistance = InterimMatrix.Values.Min();
                    if (!stopped)
                    {
                        currentDistanceMatrix = (DenseMatrix)InterimMatrix.Clone();
                        double XValue = InterimClusterList[Winner].XValue;
                        
                        Tuple<string, string, double, double> ThisDendroEntry = new Tuple<string, string, double, double>(clusters[CurrentMinimumDistance.Item1].ClusterID, clusters[CurrentMinimumDistance.Item2].ClusterID, lastDistance, XValue);
                        dendrogram.Add(ThisDendroEntry);
                        clusters = InterimClusterList;
                        
                        iterator++;
                        stopped = StoppingConditionMet();
                    }
                    else
                    {
                        Parallel.ForEach(clusters, Clust => {
                            Clust.SetCentroidAsMean();
                        });
                        stopped = true;
                    }
                }
                else stopped = true;
            }
            else
            {
                Parallel.ForEach(clusters, Clust => {
                    Clust.SetCentroidAsMean();
                });
            }
        }
        

        public override bool StoppingConditionMet()
        {
            if (stoppingStyle == HACStoppingStyle.ProportionOfFirstDistance)
            {
                if (nextDistance > maxDistance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (stoppingStyle == HACStoppingStyle.ProportionOfLastDistance)
            {
                if (lastDistance != 0)
                {
                    if (nextDistance > lastDistance * stoppingMetric)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else if (stoppingStyle == HACStoppingStyle.Iterations)
            {
                int Limit = 0;
                try { int Test = Convert.ToInt32(stoppingMetric); } catch { Limit = (int)Math.Round(stoppingMetric); }
                if (Limit == 0) { Limit = (int)stoppingMetric; }
                if (iterator >= Limit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (stoppingStyle == HACStoppingStyle.CustomDistance)
            {
                if (nextDistance >= maxDistance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override void IterateUntilStopped()
        {
            while (!stopped)
            {
                IterateOnce();
            }
        }

        public HACAlgo(List<Tuple<string,DenseVector>> Input, HACDistanceStyle Style = HACDistanceStyle.Centroid, double StoppingMetric=-1, HACStoppingStyle StoppingType=HACStoppingStyle.ProportionOfFirstDistance) : base(Input)
        {
            dendrogram = new List<Tuple<string, string, double, double>>();
            currentDistanceMatrix = new DenseMatrix(Input.Count);
            style = Style;
            stoppingMetric = StoppingMetric;
            stoppingStyle = StoppingType;
            
        }
    }

    public class KMeansPlusPlus : ClusteringAlgo
    {
        int k;
        int maxIter;
        int[] assignmentIndices;
        KMeansStoppingStyle stopStyle;
        DenseVector[] centroids;
        DenseVector[] lastCentroids;
        
        public override void InitialiseClusters()
        {
            Tuple<string, DenseVector>[] Copy = data.ToArray();
            List<Tuple<string, DenseVector>> ShuffleList = Copy.ToList();
            double[] CombinedProbabilities = new double[ShuffleList.Count];
            for (int i = 0; i < k; i++)
            {
                
                //two iterations will pass before this array is relevant
                //only initialising at this point to stop compiler errors anyway
                if(i == 0)
                {
                    int random = (new Random().Next(data.Count));
                    Tuple<string, DenseVector> Pick = data[random];
                    Cluster thisCluster = new Cluster(new List<Tuple<string, DenseVector>>(), i.ToString());
                    thisCluster.SetCentroid((DenseVector)Pick.Item2.Clone());
                    clusters.Add(thisCluster);
                    ShuffleList.RemoveAt(random);
                }
                else
                {
                    Cluster thisCluster = new Cluster(new List<Tuple<string, DenseVector>>(), i.ToString());
                    double[] ProbabilitiesList = new double[ShuffleList.Count];
                    Parallel.For(0, ShuffleList.Count, j => {
                        //first pass to calculate all distances squared
                        ProbabilitiesList[j] = HelperFunctions.GetEuclideanDistanceSquared(clusters[i - 1].GetCentroid(), ShuffleList[j].Item2);
                    });
                    double Divisor = ProbabilitiesList.Sum();
                    Parallel.For(0, ShuffleList.Count, l =>
                    {
                        //normalise distances from last winner
                        ProbabilitiesList[l] = (double)Decimal.Divide((decimal)ProbabilitiesList[l], (decimal)Divisor);
                    });
                    if (i > 1)
                    {
                        //IE if more than one centroid is already selected
                        Parallel.For(0, ProbabilitiesList.Length, innerI => {
                            //combine probabilities
                            //create running product
                            CombinedProbabilities[innerI] = ProbabilitiesList[innerI] * CombinedProbabilities[innerI];
                        });
                        //normalise combined probabilities to prevent disappearing values
                        double SecondDivisor = CombinedProbabilities.Sum();
                        Parallel.For(0, ProbabilitiesList.Length, innerJ => {
                            ProbabilitiesList[innerJ] = (double)Decimal.Divide((decimal)CombinedProbabilities[innerJ], (decimal)SecondDivisor);
                        });
                    }
                    int Winner = -1;
                    bool WinnerSelected = false;
                    while (!WinnerSelected)
                    {
                        //select randomly from weighted probability distribution proportional to squared distance(s)
                        double RandDoub = new Random().NextDouble();

                        Parallel.For(0, ShuffleList.Count, (m, ParallelLoopState) =>
                        {
                            if (ProbabilitiesList[m] > RandDoub)
                            {
                                Winner = m;
                                WinnerSelected = true;
                                ParallelLoopState.Break();
                            }
                        });
                    }
                    thisCluster.SetCentroid((DenseVector)ShuffleList[Winner].Item2.Clone());
                    clusters.Add(thisCluster);
                    
                    ShuffleList.RemoveAt(Winner);
                    //save current set of probabilities, minus selected datapoint, for use in later iterations
                    //over multiple iterations this should save resources vs a per centroid search for each datapoint
                    double[] LastProbabilities = new double[CombinedProbabilities.Length];
                    CombinedProbabilities.CopyTo(LastProbabilities,0);
                    //LastProbabilities = new double[ShuffleList.Count];
                    Parallel.For(0, ProbabilitiesList.Length, n => {
                        if (n < Winner)
                        {
                            CombinedProbabilities[n] = ProbabilitiesList[n];
                        }
                        else if (n > Winner)
                        {
                            CombinedProbabilities[n - 1] = ProbabilitiesList[n];
                        }
                        else
                        {
                            //do not copy winning data point
                        }
                    });
                    updateClusterIndexes(clusters);
                }
                
            }
            Parallel.For(0, clusters.Count, i2 => {
                //assign references to centroids list to make updating simpler
                centroids[i2] = clusters[i2].GetCentroid();
            });
            lastCentroids = new DenseVector[centroids.Length];
            //initialise lastCentroids members to prevent errors with densevector.copyto method
            Parallel.For(0, lastCentroids.Length, LC => {
                lastCentroids[LC] = new DenseVector(centroids[0].Count);
            });
            Parallel.For(0, data.Count, i3 => {
                //intialclusterassignments
                List<double> Distances = new List<double>();
                Distances.Add(HelperFunctions.GetEuclideanDistanceSquared(data[i3].Item2, centroids[0]));
                double CurrentBest = Distances[0];
                for(int j3 = 1; j3 < centroids.Length; j3++)
                {
                    Distances.Add(HelperFunctions.GetEuclideanDistanceSquared(data[i3].Item2, centroids[j3]));
                    if (Distances[j3] < CurrentBest)
                    {
                        CurrentBest = Distances[j3];
                    }
                }
                //add to quick indexing record
                assignmentIndices[i3] = Distances.IndexOf(CurrentBest);
            });
            //update cluster objects
            //necessary as it might be faster/easier to update cluster centroids with their own memberlists
            Parallel.For(0, clusters.Count, i4 => {
                List<Tuple<string, DenseVector>> SelectedMembers = new List<Tuple<string, DenseVector>>();
                for(int j4 = 0; j4 < data.Count; j4++)
                {
                    if (assignmentIndices[j4] == i4)
                    {
                        SelectedMembers.Add(data[j4]);
                    }
                }
                clusters[i4].AddMembers(SelectedMembers);
            });
        }

        private int[] returnIndicesOfValue(int[] Assignments, int Val)
        {
            return Assignments.Select((b, i) => Equals(b, Val) ? i : -1).Where(i => i != -1).ToArray();
        }

        public override void IterateOnce()
        {
            if (!stopped)
            {
                //keep track of centroid movement
                Parallel.For(0, centroids.Length, i =>
                {
                    centroids[i].CopyTo(lastCentroids[i]);
                });
                //recalculate centroids
                //should automatically update in centroids array as array of references
                for (int i = 0;i< clusters.Count; i++)
                {
                    //get means of all members' vectors
                    DenseVector MeanVector = new DenseVector(clusters[i].GetCentroid().Count);
                    Parallel.For(0, clusters[i].MemberCount, j => {
                        MeanVector = MeanVector + clusters[i].MemberList[j].Item2;
                    });
                    if (clusters[i].MemberCount > 0)
                    {
                        Parallel.For(0, MeanVector.Count, j2 =>
                        {
                            MeanVector[j2] = (double)Decimal.Divide((decimal)MeanVector[j2], (decimal)clusters[i].MemberCount);
                        });
                    }
                    clusters[i].SetCentroid(MeanVector);
                    centroids[i] = clusters[i].GetCentroid();
                    //flush current memberlist
                    clusters[i].FlushMembers();
                }
                //reassign datapoints to clusters
                Parallel.For(0, data.Count, i => {
                    //cluster assignments
                    List<double> Distances = new List<double>();
                    Distances.Add(HelperFunctions.GetEuclideanDistanceSquared(data[i].Item2, centroids[0]));
                    double CurrentBest = Distances[0];
                    for (int j = 1; j < centroids.Length; j++)
                    {
                        Distances.Add(HelperFunctions.GetEuclideanDistanceSquared(data[i].Item2, centroids[j]));
                        if (Distances[j] < CurrentBest)
                        {
                            CurrentBest = Distances[j];
                        }
                    }
                    //add to quick indexing record
                    assignmentIndices[i] = Distances.IndexOf(CurrentBest);
                });
                Parallel.For(0, clusters.Count, i => {
                    List<Tuple<string, DenseVector>> SelectedMembers = new List<Tuple<string, DenseVector>>();
                    for (int j = 0; j < data.Count; j++)
                    {
                        if (assignmentIndices[j] == i)
                        {
                            SelectedMembers.Add(data[j]);
                        }
                    }
                    clusters[i].AddMembers(SelectedMembers);
                });

                iterator++;
                stopped = StoppingConditionMet();
            }
        }

        public override void IterateUntilStopped()
        {
            while (!stopped)
            {
                IterateOnce();
            }
        }

        public override bool StoppingConditionMet()
        {
            if (stopStyle == KMeansStoppingStyle.Iterations)
            {
                if (iterator >= maxIter)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                double SummedCentroidDifference = 0;
                Parallel.For(0, centroids.Length, i => {
                    SummedCentroidDifference = SummedCentroidDifference + HelperFunctions.GetEuclideanDistanceSquared(centroids[i], lastCentroids[i]);
                });
                if (SummedCentroidDifference == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public KMeansPlusPlus(List<Tuple<string, DenseVector>>InputData, int K, KMeansStoppingStyle StopStyle, int MaxIter = 100):base(InputData)
        {
            stopStyle = StopStyle;
            k = K;
            centroids = new DenseVector[k];
            maxIter = MaxIter;
            assignmentIndices = new int[InputData.Count];
        }
    }

    public class KohonenMap : ClusteringAlgo
    {
        int x;
        int y;
        double radius;
        double timeConstant;
        double learningRate;
        KohonenNeuron[,] map;
        int maxIter;
        int epochCounter;
        List<Tuple<string, DenseVector>> shuffleList;
        Tuple<int, int>[] currentDataCoordinates;

        public int Epoch => epochCounter;

        public override void InitialiseClusters()
        {
            epochCounter = 1;
            iterator = 1;
            Random Randy = new Random();
            map = new KohonenNeuron[x, y];
            for(int i=0; i<x; i++)
            {
                for(int j=0; j<y; j++)
                {
                    //randomly initialise weights (in "Centroid" field) of each neuron
                    DenseVector InitialCentroid = new DenseVector(data[0].Item2.Count);
                    Parallel.For(0, InitialCentroid.Count, iter => {
                        InitialCentroid[iter] = Randy.NextDouble();
                    });
                    List<Tuple<string, DenseVector>> EmptyList = new List<Tuple<string, DenseVector>>();
                    map[i, j] = new KohonenNeuron(EmptyList, $"{i}, {j}", new Tuple<int, int>(i, j));
                    map[i, j].SetCentroid(InitialCentroid);
                }
            }
            Tuple < string, DenseVector >[] ShuffleList = new Tuple<string, DenseVector>[data.Count];
            data.CopyTo(ShuffleList);
            shuffleList = ShuffleList.ToList();
        }

        

        public override void IterateOnce()
        {
            if (!stopped)
            {
                if (Decimal.Divide(Decimal.Divide(iterator,epochCounter), data.Count) == 1)
                {
                    //maintaining separate list allows sampling without replacement at random throughout each epoch
                    //of training.  This needs to be reset to match all data at start of each epoch.
                    // 1 epoch = 1 pass over all datapoints

                    epochCounter++;
                    Tuple<string, DenseVector>[] ShuffleList = new Tuple<string, DenseVector>[data.Count];
                    data.CopyTo(ShuffleList);
                    shuffleList = ShuffleList.ToList();
                }
                //randomly select datapoint with replacement
                Random CurrentRand = new Random();
                int PickIndex = CurrentRand.Next(shuffleList.Count);
                Tuple<string, DenseVector> Picked = shuffleList[PickIndex];
                shuffleList.RemoveAt(PickIndex);
                //select most appropriate neuron by distance (distance squared to reduce overhead)
                double BestDistance = HelperFunctions.GetEuclideanDistanceSquared(Picked.Item2, map[0, 0].GetCentroid());
                Tuple<int, int> BestCoordinate = new Tuple<int, int>(0, 0);
                KohonenNeuron Best = map[0, 0];
                for (int i = 1; i < x; i++)
                {
                    Parallel.For(0, y, j =>
                    {
                        double TestDistance = HelperFunctions.GetEuclideanDistanceSquared(Picked.Item2, map[i, j].GetCentroid());
                        if (BestDistance > TestDistance)
                        {
                            BestDistance = TestDistance;
                            BestCoordinate = new Tuple<int, int>(i, j);
                            Best = map[i, j];
                        }
                    });
                }
                //update data coordinates list
                currentDataCoordinates[data.IndexOf(Picked)] = BestCoordinate;
                //select neighbourhood and adjust neighbours' weights accordingly
                //exponential decay formula ensures neighbourhood keeps shrinking
                double CurrentRadius = radius * Math.Exp(-(double)Decimal.Divide(iterator, (decimal)timeConstant));
                double CurrentLearningRate = learningRate * Math.Exp(-(double)Decimal.Divide(iterator, (decimal)timeConstant));
                for (int i = 0; i < x; i++)
                {
                    Parallel.For(0, y, j =>
                    {
                        double DistanceFromBest = HelperFunctions.GetEuclideanDistanceSquared(Best.Coordinates, map[i, j].Coordinates);
                        if (DistanceFromBest < Math.Pow(CurrentRadius, 2))
                        {
                            double NeighbourhoodWeighting = Math.Exp(-(double)Decimal.Divide((decimal)DistanceFromBest, (decimal)(2 * Math.Pow(CurrentRadius, 2))));
                            DenseVector CentroidReplacement = map[i, j].GetCentroid() + (NeighbourhoodWeighting * CurrentLearningRate) * (Picked.Item2 - map[i, j].GetCentroid());
                            map[i, j].SetCentroid(CentroidReplacement);
                        }
                    });
                }
                //housekeeping
                iterator++;
                stopped = StoppingConditionMet();
                if (stopped)
                {
                    //add all datapoints to clusters by coordinates
                    //add all neurons to cluster list
                    for (int i = 0; i < x; i++)
                    {
                        for (int j = 0; j < y; j++)
                        {
                            ConcurrentBag<Tuple<string, DenseVector>> FinalMemberList = new ConcurrentBag<Tuple<string, DenseVector>>();
                            Parallel.For(0, data.Count, datum =>
                            {
                                if (currentDataCoordinates[datum].Item1 == map[i, j].Coordinates[0] && currentDataCoordinates[datum].Item2 == map[i, j].Coordinates[1])
                                {
                                    FinalMemberList.Add(data[datum]);
                                }
                            });
                            map[i, j].AddMembers(FinalMemberList.OrderBy(o=>o.Item1).ToList());
                            clusters.Add(map[i, j]);
                        }
                    }
                }
            }
        }

        public override void IterateUntilStopped()
        {
            while (!stopped)
            {
                IterateOnce();
            }
        }

        public override bool StoppingConditionMet()
        {
            if (iterator > maxIter)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public KohonenMap(List<Tuple<string, DenseVector>> InputData, int X, int Y, int IterationsMax):base(InputData)
        {
            x = X;
            y = Y;
            epochCounter = 1;
            if (IterationsMax < InputData.Count)
            {
                maxIter = InputData.Count;
            }
            else
            {
                maxIter = IterationsMax;
            }
            if (x >= y)
            {
                radius = x;
            }
            else
            {
                radius = y;
            }
            currentDataCoordinates = new Tuple<int, int>[data.Count];
            timeConstant = (double)Decimal.Divide(maxIter, (decimal)Math.Log(Convert.ToDouble(radius)));
            learningRate = 0.1;
        }
    }
}
