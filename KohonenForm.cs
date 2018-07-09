using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.LinearAlgebra.Double;

namespace RBACS
{
    public partial class KohonenForm : Form
    {
        RBAC parentReference;
        int dimensions;
        List<Tuple<int,int>> coordList;
        List<Cluster> initialClusterList;
        GroupRepresentationReport groupReps;
        GroupRepresentationTFIDFReport groupRepsTFIDF;
        Ordering groupRepsOrdering = Ordering.Ascending;
       

        Type thisQRType;
        KohonenMap thisMap;
        ClusteringAlgo secondaryAlgo;
        Report thisQR;
        Report subsetThisQR;
        Report membersSubsetThisQR;
        BindingSource thisBindingSource;
        ToolTip toolTip;
        bool secondaryCLusteringInEffect;
        //? prevPosition = null;

        // Colour Placeholders for returning points' colour to normal after
        // find has turned them black
        Color standardPointColour;
        Color[] allPointsColourPlaceholders;

        //datagridview binding sources
        BindingSource GRBinding;
        BindingSource MembersBinding;

        //options properties
        //bool showLabels => checkBox1.Checked;
        bool hACNotKMeans => hACRadioButton.Checked;
        bool kMeansIsIterations => kMIterationsRadioButton.Checked;
        HACStoppingStyle hACStoppingStyle = HACStoppingStyle.ProportionOfFirstDistance;
        HACDistanceStyle hACDistanceStyle = HACDistanceStyle.Centroid;
        int kMeansKValue => textBoxValidate(kValueTextBox, 2);
        int kMeansIterations => textBoxValidate(kMIterationsTextBox, 100);
        int hACIterations => textBoxValidate(iterationsTextBox, 100);
        double hAC1stDistanceProportion => textBoxValidateDouble(p1DValueTextBox, 1.5);
        double hACLastDistanceProportion => textBoxValidateDouble(pLDValueTextBox, 1.5);

        public List<GroupRepresentationResult> GroupReps => groupReps.QRList;
        public List<GroupRepresentationTFIDFResult> GroupRepsTFIDF => groupRepsTFIDF.QRList;
        public KohonenForm(RBAC Parent, int Dimensions, List<QueryResult>QRList, int Epochs = 5, List<Cluster> ClusterList = null)
        {
            InitializeComponent();
          
            dimensions = Dimensions;
            parentReference = Parent;
            toolTip = new ToolTip();
            GRBinding = new BindingSource();
            MembersBinding = new BindingSource();
            if(ClusterList != null)
            {
                initialClusterList = ClusterList;
            }
            thisQRType = QRList[0].GetType();
            //setting iterations to count of dataset * epochs guarantees full passes over dataset
            //can be updated to allow for tfidf
            thisMap = new KohonenMap(returnInputListFromQRList(QRList, !parentReference.ClusterByRelativeCount),dimensions,dimensions,QRList.Count*Epochs);
            parentReference.statusLabelChanger($"Initialising {dimensions}x{dimensions} Map");
            thisMap.InitialiseClusters();
            while (!thisMap.Stopped)
            {
                thisMap.IterateOnce();
                parentReference.statusLabelChanger($"Calculating Kohonen Map, Iteration {thisMap.Iterator}, {FileHelperFuctions.FirstSecondEtc(thisMap.Epoch)} Pass over Dataset");
            }
            parentReference.statusLabelChanger("Idle");
            thisBindingSource = new BindingSource();
            coordList = new List<Tuple<int, int>>();
            List<string> MembersList = new List<string>();

            foreach (Cluster Clust in thisMap.Clusters)
            {
                KohonenNeuron CurrentKoho = (KohonenNeuron)Clust;
                coordList.Add(new Tuple<int, int>((int)CurrentKoho.Coordinates[0], (int)CurrentKoho.Coordinates[1]));
                double CurrentX = CurrentKoho.Coordinates[0];
                double CurrentY = CurrentKoho.Coordinates[1];
                
                chart1.Series[0].Points.AddXY(CurrentX, CurrentY);
                chart1.Series[0].Points[chart1.Series[0].Points.Count - 1].YValues[1] = (double)Decimal.Divide(CurrentKoho.MemberCount, QRList.Count);
                if(chart1.Series[0].Points[chart1.Series[0].Points.Count -1].YValues[1] == 0)
                {
                    chart1.Series[0].Points.RemoveAt(chart1.Series[0].Points.Count - 1);
                }
                //List<string> ThisStringList = from 
                MembersList.AddRange((from Tuple<string, DenseVector> Memb in Clust.MemberList select Memb.Item1).ToList());
            }

            if (thisQRType == typeof(UserQueryResult))
            {
                thisQR = new UserQueryReport(QRList.ConvertAll(c => (UserQueryResult)c).ToList(), Ordering.Ascending);
                chart1.Series[0].Name = "Users";
            }
            else if (thisQRType == typeof(GroupingQueryResult))
            {
                thisQR = new GroupingQueryReport(QRList.ConvertAll(c => (GroupingQueryResult)c).ToList(), Ordering.Ascending);
                chart1.Series[0].Name = "Groupings";
            }
            else if (thisQRType == typeof(UserClusteringResult))
            {
                thisQR = new UserClusteringReport(QRList.ConvertAll(c => (UserClusteringResult)c), Ordering.Ascending);
                chart1.Series[0].Name = "Clusters";
            }
            else if (thisQRType == typeof(GroupingClusteringResult))
            {
                thisQR = new GroupingClusteringReport(QRList.ConvertAll(c => (GroupingClusteringResult)c), Ordering.Ascending);
                chart1.Series[0].Name = "Clusters";
            }

            standardPointColour = chart1.Series[0].Points[0].Color;
        }

        private List<Tuple<string, DenseVector>> returnInputListFromQRList (List<QueryResult>QRList, bool TFIDF=false)
        {
            ConcurrentBag<Tuple<string, DenseVector>> InputList = new ConcurrentBag<Tuple<string, DenseVector>>();
            if (thisQRType == typeof(UserQueryResult))
            {
                Parallel.ForEach<QueryResult>(QRList, QR =>
                {
                    UserQueryResult UQR = (UserQueryResult)QR;
                    Tuple<string, DenseVector> TupleIn = new Tuple<string, DenseVector>(UQR.AccountName, (DenseVector)UQR.ReturnAccessVector());
                    InputList.Add(TupleIn);
                });
            }
            else if (thisQRType == typeof(GroupingQueryResult))
            {
                Parallel.ForEach<QueryResult>(QRList, QR =>
                {
                    GroupingQueryResult GQR = (GroupingQueryResult)QR;
                    DenseVector VectorA;
                    //could use tfidf switch here
                    if (!TFIDF)
                    {
                        VectorA = (DenseVector) GQR.ReturnAccessVector();
                    }
                    else
                    {
                        VectorA = (DenseVector) GQR.ReturnTF_IDFVector();
                    }
                    Tuple<string, DenseVector> TupleIn = new Tuple<string, DenseVector>(GQR.GroupingName, VectorA);
                    InputList.Add(TupleIn);
                });
            }
            else if (thisQRType == typeof(UserClusteringResult)||thisQRType == typeof(GroupingClusteringResult))
            {
                DenseVector VectorA;
                Parallel.ForEach(initialClusterList, Clust => {
                    VectorA = Clust.GetCentroid();
                    Tuple<string, DenseVector> TupleIn = new Tuple<string, DenseVector>(Clust.ListPosition.ToString(), VectorA);
                    InputList.Add(TupleIn);
                });
                
            }
            else
            {
                throw new NotSupportedException();
            }
            return InputList.OrderBy(o => o.Item1).ToList();
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip.RemoveAll();
        }
        private string kohonenNeuronToToolTipText(KohonenNeuron KN) {
            string OutString = "";
            int i = 0;
            foreach(Tuple<string,DenseVector> Member in KN.MemberList)
            {
                if (Member.Item1 == "")
                {
                    OutString += "(Empty Field)";
                }
                if (i == KN.MemberCount - 1)
                {
                    OutString += $"{Member.Item1}";
                }
                else
                {
                    OutString += $"{Member.Item1} || ";
                }
                i++;
            }
            return OutString;
        }
        private string kohonenNeuronToLabel(KohonenNeuron KN, int MaxLines)
        {
            string ReturnString = "";
            if (KN.MemberCount >= MaxLines)
            {
                for (int i=0; i<MaxLines; i++)
                {
                    ReturnString = ReturnString + $"{KN.MemberList[i].Item1}\r\n";
                }
                if (KN.MemberCount > MaxLines)
                {
                    ReturnString += $"+ {KN.MemberCount - MaxLines} more...";
                }
            }
            else
            {
                for (int i = 0; i < KN.MemberCount; i++)
                {
                    ReturnString = ReturnString + $"{KN.MemberList[i].Item1}\r\n";
                }
            }
            return ReturnString;
        }
        private string kohonenNeuronsToLabel(List<KohonenNeuron>KNsIn, int MaxLines)
        {
            string OutString = "";
            List<string> Strings = new List<string>();
            foreach(KohonenNeuron KN in KNsIn)
            {
                for(int i = 0; i < KN.MemberCount; i++)
                {
                    Strings.Add(KN.MemberList[i].Item1);
                }
            }
            Strings.Sort();
            if(Strings.Count >= MaxLines)
            {
                for(int i=0; i<MaxLines; i++)
                {
                    OutString = OutString + $"{Strings[i]}\r\n";
                }
                OutString += $"+ {Strings.Count - MaxLines} more...";
            }
            else
            {
                foreach(string Str in Strings)
                {
                    OutString += $"{Str}\r\n";
                }
            }
            return OutString;
        }

        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            if(findTextBox.Text!=null && findTextBox.Text != "")
            {
                ConcurrentBag<KohonenNeuron> FoundNeurons = new ConcurrentBag<KohonenNeuron>();
                if (thisQRType == typeof(UserQueryResult))
                {
                    UserQueryReport ReportRef = (UserQueryReport)thisQR;
                    subsetThisQR = ReportRef.Search(findTextBox.Text);
                    UserQueryReport SubReportRef = (UserQueryReport)subsetThisQR;
                    Parallel.ForEach(SubReportRef.QRList, Member => {
                        List<KohonenNeuron> Found = (from KohonenNeuron KN in thisMap.Clusters
                                                     where KN.MemberList.ConvertAll(c => c.Item1).Contains(Member.AccountName)
                                                     select KN).ToList();
                        FoundNeurons.Add(Found[0]);
                    });
                }
                else if (thisQRType == typeof(GroupingQueryResult))
                {
                    GroupingQueryReport ReportRef = (GroupingQueryReport)thisQR;
                    subsetThisQR = ReportRef.Search(findTextBox.Text);
                    GroupingQueryReport SubReportRef = (GroupingQueryReport)subsetThisQR;
                    Parallel.ForEach(SubReportRef.QRList, Member => {
                        List<KohonenNeuron> Found = (from KohonenNeuron KN in thisMap.Clusters
                                                     where KN.MemberList.ConvertAll(c => c.Item1).Contains(Member.GroupingName)
                                                     select KN).ToList();
                        FoundNeurons.Add(Found[0]);
                    });
                }
                else if (thisQRType == typeof(UserClusteringResult))
                {
                    UserClusteringReport ReportRef = (UserClusteringReport)thisQR;
                    subsetThisQR = ReportRef.Search(findTextBox.Text);
                    UserClusteringReport SubReportRef = (UserClusteringReport)subsetThisQR;
                    Parallel.ForEach(SubReportRef.QRList, Member => {
                        List<KohonenNeuron> Found = (from KohonenNeuron KN in thisMap.Clusters
                                                     where KN.MemberList.ConvertAll(c => c.Item1).Contains(Member.ClusterIndex.ToString())
                                                     select KN).ToList();
                        FoundNeurons.Add(Found[0]);
                    });
                }
                else if (thisQRType == typeof(GroupingClusteringResult))
                {
                    GroupingClusteringReport ReportRef = (GroupingClusteringReport)thisQR;
                    subsetThisQR = ReportRef.Search(findTextBox.Text);
                    GroupingClusteringReport SubReportRef = (GroupingClusteringReport)subsetThisQR;
                    Parallel.ForEach(SubReportRef.QRList, Member => {
                        List<KohonenNeuron> Found = (from KohonenNeuron KN in thisMap.Clusters
                                                     where KN.MemberList.ConvertAll(c => c.Item1).Contains(Member.ClusterIndex.ToString())
                                                     select KN).ToList();
                        FoundNeurons.Add(Found[0]);
                    });
                }
                List<KohonenNeuron> FoundList = FoundNeurons.Distinct().ToList();
                
                foreach(KohonenNeuron Neuron in thisMap.Clusters)
                {
                    if (Neuron.MemberCount > 0)
                    {
                        //get each datapoint from chart ready for editing.
                        DataPoint Datum = (from DataPoint D in chart1.Series[0].Points
                                           where D.XValue == Neuron.Coordinates[0] && D.YValues[0] == Neuron.Coordinates[1]
                                           select D).ToArray()[0];
                        if (FoundList.Contains(Neuron))
                        {
                            Datum.Color = Color.Black;
                        }
                        else
                        {
                            if (secondaryCLusteringInEffect)
                            {
                                Datum.Color = allPointsColourPlaceholders[chart1.Series[0].Points.IndexOf(Datum)];
                            }
                            else
                            {
                                Datum.Color = standardPointColour;
                            }
                        }
                    }
                }
                chart1.Palette = ChartColorPalette.EarthTones;
            }
            else
            {
                subsetThisQR = null;
                foreach(DataPoint DP in chart1.Series[0].Points)
                {
                    if (secondaryCLusteringInEffect)
                    {
                        DP.Color = allPointsColourPlaceholders[chart1.Series[0].Points.IndexOf(DP)];
                    }
                    else
                    {
                        DP.Color = standardPointColour;
                    }
                }
                chart1.Palette = ChartColorPalette.EarthTones;
            }
        }

        private void chart1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            var results = chart1.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        if (!secondaryCLusteringInEffect || !displayAsClustersCheckbox.Checked)
                        {

                            KohonenNeuron SelectedKN = (from KohonenNeuron KN in thisMap.Clusters
                                                        where KN.Coordinates[0] == prop.XValue && KN.Coordinates[1] == prop.YValues[0]
                                                        select KN).ToList()[0];
                            // check if the cursor is really close to the point (2 pixels around the point)
                            if (Math.Abs(pos.X - pointXPixel) < 50 &&
                                Math.Abs(pos.Y - pointYPixel) < 50)
                            {
                                //handle group representations first
                                List<QueryResult> QRList = new List<QueryResult>();
                                if (thisQRType == typeof(UserQueryResult))
                                {
                                    UserQueryReport ReferenceReport = (UserQueryReport)thisQR;
                                    List<UserQueryResult> CurrentQRList = (from UserQueryResult QR in ReferenceReport.QRList
                                                                           where SelectedKN.MemberList.ConvertAll(c => c.Item1).Contains(QR.AccountName)
                                                                           select QR).OrderBy(o => o.AccountName).ToList();
                                    membersSubsetThisQR = new UserQueryReport(CurrentQRList, Ordering.Ascending);
                                    UserQueryReport ReferenceSubset = (UserQueryReport)membersSubsetThisQR;
                                    MembersBinding.DataSource = ReferenceSubset.QRList;
                                    membersDataGridView.DataSource = MembersBinding;
                                    QRList = CurrentQRList.ConvertAll(c => (QueryResult)c);
                                }
                                else if (thisQRType == typeof(GroupingQueryResult))
                                {
                                    GroupingQueryReport ReferenceReport = (GroupingQueryReport)thisQR;
                                    List<GroupingQueryResult> CurrentQRList = (from GroupingQueryResult QR in ReferenceReport.QRList
                                                                               where SelectedKN.MemberList.ConvertAll(c => c.Item1).Contains(QR.GroupingName)
                                                                               select QR).OrderBy(o => o.GroupingName).ToList();
                                    membersSubsetThisQR = new GroupingQueryReport(CurrentQRList, Ordering.Ascending);
                                    GroupingQueryReport ReferenceSubset = (GroupingQueryReport)membersSubsetThisQR;
                                    MembersBinding.DataSource = ReferenceSubset.QRList;
                                    membersDataGridView.DataSource = MembersBinding;
                                    QRList = CurrentQRList.ConvertAll(c => (QueryResult)c);
                                }
                                else if (thisQRType == typeof(UserClusteringResult))
                                {
                                    UserClusteringReport ReferenceReport = (UserClusteringReport)thisQR;
                                    List<UserClusteringResult> CurrentQRList = (from UserClusteringResult QR in ReferenceReport.QRList
                                                                                where SelectedKN.MemberList.ConvertAll(c => c.Item1).Contains(QR.ClusterIndex.ToString())
                                                                                select QR).OrderBy(o => o.ClusterIndex).ToList();
                                    membersSubsetThisQR = new UserClusteringReport(CurrentQRList, Ordering.Ascending);
                                    UserClusteringReport ReferenceSubset = (UserClusteringReport)membersSubsetThisQR;
                                    MembersBinding.DataSource = ReferenceSubset.QRList;
                                    membersDataGridView.DataSource = MembersBinding;
                                    QRList = CurrentQRList.ConvertAll(c => (QueryResult)c);
                                }
                                else if (thisQRType == typeof(GroupingClusteringResult))
                                {
                                    GroupingClusteringReport ReferenceReport = (GroupingClusteringReport)thisQR;
                                    List<GroupingClusteringResult> CurrentQRList = (from GroupingClusteringResult QR in ReferenceReport.QRList
                                                                                    where SelectedKN.MemberList.ConvertAll(c => c.Item1).Contains(QR.ClusterIndex.ToString())
                                                                                    select QR).OrderBy(o => o.ClusterIndex).ToList();
                                    membersSubsetThisQR = new GroupingClusteringReport(CurrentQRList, Ordering.Ascending);
                                    GroupingClusteringReport ReferenceSubset = (GroupingClusteringReport)membersSubsetThisQR;
                                    MembersBinding.DataSource = ReferenceSubset.QRList;
                                    membersDataGridView.DataSource = MembersBinding;
                                    QRList = CurrentQRList.ConvertAll(c => (QueryResult)c);
                                }
                                groupReps = new GroupRepresentationReport(HelperFunctions.QueryListToGroupRepresentationList(QRList, parentReference.GroupNamesAndDescriptionsAll),Ordering.Ascending);
                                GRBinding.DataSource = GroupReps;
                                groupsOverviewDataGridView.DataSource = GRBinding;
                                groupsOverviewLabel.Text = groupsOverviewLabel.Text.Split(':')[0] + $":{{{SelectedKN.Coordinates[0]},{SelectedKN.Coordinates[1]}}}";
                            }
                        }
                        else
                        {
                            //get cluster selected node is in
                            //and all member tuples of other nodes in cluster

                            Cluster SecondaryCluster = secondaryAlgo.Clusters.Where(
                                clust => clust.MemberList.ConvertAll(
                                    c => c.Item1
                                    ).Contains($"{prop.XValue},{prop.YValues[0]}")
                                    ).ToList()[0];
                            
                            List<Tuple<string, DenseVector>> MemberInputs = new List<Tuple<string, DenseVector>>();
                            foreach(string Id in SecondaryCluster.MemberList.ConvertAll(c => c.Item1))
                            {
                                int X = Convert.ToInt32(Id.Split(',')[0]);
                                int Y = Convert.ToInt32(Id.Split(',')[1]);
                                List<KohonenNeuron> Kohos = (from Cl in thisMap.Clusters.ConvertAll(c => (KohonenNeuron)c)
                                                             where Cl.Coordinates[0] == X && Cl.Coordinates[1] == Y
                                                             select Cl).ToList();
                                foreach (KohonenNeuron Koho in Kohos)
                                {
                                    MemberInputs.AddRange(Koho.MemberList);
                                }
                            }
                            MemberInputs = MemberInputs.OrderBy(o => o.Item1).ToList();

                            if (Math.Abs(pos.X - pointXPixel) < 50 &&
                                Math.Abs(pos.Y - pointYPixel) < 50)
                            {
                                //handle group representations first
                                List<QueryResult> QRList = new List<QueryResult>();
                                if (thisQRType == typeof(UserQueryResult))
                                {
                                    UserQueryReport ReferenceReport = (UserQueryReport)thisQR;
                                    List<UserQueryResult> CurrentQRList = (from UserQueryResult QR in ReferenceReport.QRList
                                                                           where MemberInputs.ConvertAll(c=>c.Item1).Contains(QR.AccountName)
                                                                           select QR).OrderBy(o => o.AccountName).ToList();
                                    membersSubsetThisQR = new UserQueryReport(CurrentQRList, Ordering.Ascending);
                                    UserQueryReport ReferenceSubset = (UserQueryReport)membersSubsetThisQR;
                                    MembersBinding.DataSource = ReferenceSubset.QRList;
                                    membersDataGridView.DataSource = MembersBinding;
                                    QRList = CurrentQRList.ConvertAll(c => (QueryResult)c);
                                }
                                else if (thisQRType == typeof(GroupingQueryResult))
                                {
                                    GroupingQueryReport ReferenceReport = (GroupingQueryReport)thisQR;
                                    List<GroupingQueryResult> CurrentQRList = (from GroupingQueryResult QR in ReferenceReport.QRList
                                                                               where MemberInputs.ConvertAll(c => c.Item1).Contains(QR.GroupingName)
                                                                               select QR).OrderBy(o => o.GroupingName).ToList();
                                    membersSubsetThisQR = new GroupingQueryReport(CurrentQRList, Ordering.Ascending);
                                    GroupingQueryReport ReferenceSubset = (GroupingQueryReport)membersSubsetThisQR;
                                    MembersBinding.DataSource = ReferenceSubset.QRList;
                                    membersDataGridView.DataSource = MembersBinding;
                                    QRList = CurrentQRList.ConvertAll(c => (QueryResult)c);
                                }
                                else if (thisQRType == typeof(UserClusteringResult))
                                {
                                    UserClusteringReport ReferenceReport = (UserClusteringReport)thisQR;
                                    List<UserClusteringResult> CurrentQRList = (from UserClusteringResult QR in ReferenceReport.QRList
                                                                                where MemberInputs.ConvertAll(c => c.Item1).Contains(QR.ClusterIndex.ToString())
                                                                                select QR).OrderBy(o => o.ClusterIndex).ToList();
                                    membersSubsetThisQR = new UserClusteringReport(CurrentQRList, Ordering.Ascending);
                                    UserClusteringReport ReferenceSubset = (UserClusteringReport)membersSubsetThisQR;
                                    MembersBinding.DataSource = ReferenceSubset.QRList;
                                    membersDataGridView.DataSource = MembersBinding;
                                    QRList = CurrentQRList.ConvertAll(c => (QueryResult)c);
                                }
                                else if (thisQRType == typeof(GroupingClusteringResult))
                                {
                                    GroupingClusteringReport ReferenceReport = (GroupingClusteringReport)thisQR;
                                    List<GroupingClusteringResult> CurrentQRList = (from GroupingClusteringResult QR in ReferenceReport.QRList
                                                                                    where MemberInputs.ConvertAll(c => c.Item1).Contains(QR.ClusterIndex.ToString())
                                                                                    select QR).OrderBy(o => o.ClusterIndex).ToList();
                                    membersSubsetThisQR = new GroupingClusteringReport(CurrentQRList, Ordering.Ascending);
                                    GroupingClusteringReport ReferenceSubset = (GroupingClusteringReport)membersSubsetThisQR;
                                    MembersBinding.DataSource = ReferenceSubset.QRList;
                                    membersDataGridView.DataSource = MembersBinding;
                                    QRList = CurrentQRList.ConvertAll(c => (QueryResult)c);
                                }
                                groupReps = new GroupRepresentationReport(HelperFunctions.QueryListToGroupRepresentationList(QRList, parentReference.GroupNamesAndDescriptionsAll),groupRepsOrdering);
                                GRBinding.DataSource = GroupReps;
                                groupsOverviewDataGridView.DataSource = GRBinding;
                                groupsOverviewLabel.Text = groupsOverviewLabel.Text.Split(':')[0] + $": Cluster {secondaryAlgo.Clusters.IndexOf(SecondaryCluster)}";
                            }
                        }
                        
                    }
                }
            }
            Application.DoEvents();
        }

        private void chart1_MouseClick(object sender, MouseEventArgs e)
        {
            var pos = e.Location;
            
            
            var results = chart1.HitTest(pos.X, pos.Y, false,
                                            ChartElementType.DataPoint);
            List<KohonenNeuron> KNList = thisMap.Clusters.ConvertAll<KohonenNeuron>(cl => (KohonenNeuron)cl);
            //KohonenNeuron SelectedKN = (from KN in KNList where KN.Coordinates[0] == pos.X && KN.Coordinates[1] == pos.Y select KN).ToList()[0];
            foreach (var result in results)
            {
                if (result.ChartElementType == ChartElementType.DataPoint)
                {
                    var prop = result.Object as DataPoint;
                    if (prop != null)
                    {
                        var pointXPixel = result.ChartArea.AxisX.ValueToPixelPosition(prop.XValue);
                        var pointYPixel = result.ChartArea.AxisY.ValueToPixelPosition(prop.YValues[0]);

                        if ((!secondaryCLusteringInEffect) || (!displayAsClustersCheckbox.Checked)) {
                            KohonenNeuron SelectedKN = (from KN in KNList
                                                        where KN.Coordinates[0] == prop.XValue && KN.Coordinates[1] == prop.YValues[0]
                                                        select KN).ToList()[0];
                            // check if the cursor is really close to the point (2 pixels around the point)
                            if (Math.Abs(pos.X - pointXPixel) < 50 &&
                                Math.Abs(pos.Y - pointYPixel) < 50)

                            {
                                explorerRichTextBox.Text = null;
                                explorerRichTextBox.Text = kohonenNeuronToLabel(SelectedKN, 20);
                                currentNeuronLabel.Text = $"{{{prop.XValue},{prop.YValues[0]}}}:";
                                currentNeuronLabel.ForeColor = Color.Black;
                            }
                        }
                        else
                        {
                            List<KohonenNeuron> SelectedKNs = new List<KohonenNeuron>();
                            
                            Cluster SecondaryCluster = secondaryAlgo.Clusters.Where(
                                clust => clust.MemberList.ConvertAll(
                                    c => c.Item1
                                    ).ToList().Contains($"{prop.XValue},{prop.YValues[0]}"
                                )
                            ).ToList()[0];
                            foreach(string Coords in SecondaryCluster.MemberList.ConvertAll(c => c.Item1))
                            {
                                int Xx = Convert.ToInt32(Coords.Split(',')[0]);
                                int Yy = Convert.ToInt32(Coords.Split(',')[1]);
                                SelectedKNs.Add(
                                    thisMap.Clusters.ConvertAll(c => (KohonenNeuron)c).Where(
                                        kn => kn.Coordinates[0] == Xx && kn.Coordinates[1] == Yy
                                        ).ToList()[0]
                                    );
                            }
                            if(Math.Abs(pos.X - pointXPixel) < 50 &&
                                Math.Abs(pos.Y - pointYPixel) < 50)

                            {
                                explorerRichTextBox.Text = null;
                                explorerRichTextBox.Text = kohonenNeuronsToLabel(SelectedKNs, 20);
                                currentNeuronLabel.Text = $"Cluster {secondaryAlgo.Clusters.IndexOf(SecondaryCluster)}";
                                currentNeuronLabel.ForeColor = allPointsColourPlaceholders[chart1.Series[0].Points.IndexOf(prop)];
                            }

                            
                        }

                    }
                    else
                    {
                        explorerRichTextBox.Text = null;
                        currentNeuronLabel.Text = null;
                    }
                }
            }
        }

        private int textBoxValidate(TextBox TB, int Default)
        {
            bool Good = true;
            try { Convert.ToInt32(TB.Text); } catch { Good = false; }
            if (Good)
            {
                return Convert.ToInt32(TB.Text);
            }
            else
            {
                TB.Text = Default.ToString();
                return Default;
            }
        }
        private double textBoxValidateDouble(TextBox TB, double Default)
        {
            bool Good = true;
            try { Convert.ToDouble(TB.Text); } catch { Good = false; }
            if (Good)
            {
                return Convert.ToDouble(TB.Text);
            }
            else
            {
                TB.Text = Default.ToString();
                return Default;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void membersDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (thisQRType == typeof(UserQueryResult)||thisQRType==typeof(UserClusteringResult))
            {
                int TargetIndex = (from DataGridViewColumn Col in membersDataGridView.Columns
                                   where Col.HeaderText == "AccountName"
                                   select Col.Index).ToList()[0];
                int RowNum = e.RowIndex;
                if (RowNum >= 0)
                {
                    var KeyResult = from UQR in parentReference.UserQueryResults
                                    where UQR.AccountName == membersDataGridView.Rows[RowNum].Cells[TargetIndex].Value.ToString()
                                    select UQR;
                    UserQueryResult Result = KeyResult.ToList()[0];
                    QueryDetailsForm QDF = new QueryDetailsForm(Result, parentReference);
                    QDF.Show();
                }
            }
            else if (thisQRType == typeof(GroupingQueryResult)||thisQRType==typeof(GroupingClusteringResult))
            {
                int TargetIndex = (from DataGridViewColumn Col in membersDataGridView.Columns
                                   where Col.HeaderText == "GroupingName"
                                   select Col.Index).ToList()[0];
                int RowNum = e.RowIndex;
                if (RowNum >= 0)
                {
                    var KeyResult = from UQR in parentReference.GroupingQueryResults
                                    where UQR.GroupingName == membersDataGridView.Rows[RowNum].Cells[TargetIndex].Value.ToString()
                                    select UQR;
                    GroupingQueryResult Result = KeyResult.ToList()[0];
                    QueryDetailsForm QDF = new QueryDetailsForm(Result, parentReference);
                    QDF.Show();
                }
            }
        }

        private void membersDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (MembersBinding.DataSource != null)
            {
                string Prop = membersDataGridView.Columns[e.ColumnIndex].HeaderText;
                if (thisQRType == typeof(UserQueryResult)) {
                    UserQueryReport UQR = (UserQueryReport)membersSubsetThisQR;
                    UQR.SortByProperty(Prop);
                    MembersBinding.DataSource = UQR.QRList;                   
                }
                else if(thisQRType == typeof(GroupingQueryResult)) {
                    GroupingQueryReport GQR = (GroupingQueryReport)membersSubsetThisQR;
                    GQR.SortByProperty(Prop);
                    MembersBinding.DataSource = GQR.QRList;                    
                }
                else if (thisQRType == typeof(UserClusteringResult)) {
                    UserClusteringReport UCR = (UserClusteringReport)membersSubsetThisQR;
                    UCR.SortByProperty(Prop);
                    MembersBinding.DataSource = UCR.QRList;                    
                }
                else if(thisQRType == typeof(GroupingClusteringResult)){
                    GroupingClusteringReport GCR = (GroupingClusteringReport)membersSubsetThisQR;
                    GCR.SortByProperty(Prop);
                    MembersBinding.DataSource = GCR.QRList;
                }
                membersDataGridView.DataSource = MembersBinding;
            }
        }

        private string trimEnd(string In, int Trim)
        {
            //trim off last N characters

            return In.Substring(0, In.Length-Trim);
        }
        private void groupsOverviewDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(GRBinding.DataSource != null)
            {
                string ColumnHeader = groupsOverviewDataGridView.Columns[e.ColumnIndex].HeaderText;
                             
                
                var Prop = typeof(GroupRepresentationResult).GetProperty(ColumnHeader);
                string TestString = Prop.GetValue(GroupReps[0], null).ToString();

                groupReps.SortByProperty(ColumnHeader);
                
                GRBinding.DataSource = GroupReps;
                groupsOverviewDataGridView.DataSource = GRBinding;
            }
        }

        private List<Tuple<string, DenseVector>> returnInputListFromKohonenNeurons(List<KohonenNeuron> KNList)
        {
            List<Tuple<string, DenseVector>> Output = new List<Tuple<string, DenseVector>>();
            foreach(KohonenNeuron KN in KNList)
            {
                if (KN.MemberCount > 0)
                {
                    Tuple<string, DenseVector> CurrentTuple = new Tuple<string, DenseVector>($"{KN.Coordinates[0]},{KN.Coordinates[1]}", KN.GetCentroid());
                    Output.Add(CurrentTuple);
                }
            }
            return Output;
        }

        private void clusterButton_Click(object sender, EventArgs e)
        {
            //returnInputListFromQRList does not work with TFIDF yet

            List<Tuple<string, DenseVector>> Inputs = returnInputListFromKohonenNeurons(thisMap.Clusters.ConvertAll(c => (KohonenNeuron)c));
            
            if (hACNotKMeans)
            {
                double Metric=-1;
                if(hACStoppingStyle == HACStoppingStyle.Iterations)
                {
                    Metric = Convert.ToDouble(hACIterations);
                }
                else if (hACStoppingStyle == HACStoppingStyle.ProportionOfFirstDistance)
                {
                    Metric = hAC1stDistanceProportion;
                }
                else if(hACStoppingStyle == HACStoppingStyle.ProportionOfLastDistance)
                {
                    Metric = hACLastDistanceProportion;
                }
                else
                {
                    //fill in when dendrogram implemented
                }
                secondaryAlgo = new HACAlgo(Inputs, hACDistanceStyle, Metric, hACStoppingStyle);
                secondaryAlgo.InitialiseClusters();
                while (!secondaryAlgo.Stopped)
                {
                    secondaryAlgo.IterateOnce();
                    // add status bar stuff here later
                }
            }
            else
            {
                KMeansStoppingStyle StopStyle = KMeansStoppingStyle.NoChangeInCentroids;
                if (kMeansIsIterations)
                {
                    StopStyle = KMeansStoppingStyle.Iterations;
                }
                secondaryAlgo = new KMeansPlusPlus(Inputs, kMeansKValue, StopStyle, kMeansIterations);
                secondaryAlgo.InitialiseClusters();
                while (!secondaryAlgo.Stopped)
                {
                    secondaryAlgo.IterateOnce();
                    // add status bar stuff here later
                }
            }
            Random Rand = new Random();
            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();
            foreach (Cluster Clust in secondaryAlgo.Clusters)
            {
                int r=-1;
                int g=-1;
                int b=-1;
                bool AllSelected = false;
                //randomly select red/blue/green values without replacement (Using int lists above to keep track)
                while (!AllSelected)
                {
                    bool Selected = true;
                    if (r < 0)
                    {
                        int RAttempt = Rand.Next(256);
                        if (R.Contains(RAttempt)){
                            Selected = false;
                        }
                        else
                        {
                            r = RAttempt;
                            R.Add(RAttempt);
                        }
                    }
                    if (g < 0)
                    {
                        int GAttempt = Rand.Next(256);
                        if (G.Contains(GAttempt))
                        {
                            Selected = false;
                        }
                        else
                        {
                            g = GAttempt;
                            G.Add(GAttempt);
                        }
                    }
                    if (b < 0)
                    {
                        int BAttempt = Rand.Next(256);
                        if (B.Contains(BAttempt))
                        {
                            Selected = false;
                        }
                        else
                        {
                            b = BAttempt;
                            B.Add(BAttempt);
                        }
                    }
                    if (Selected) {
                        AllSelected = true;
                    }
                }
                Color CurrentClusterColour = Color.FromArgb(r, g, b);
                if (Clust.MemberCount > 0)
                {

                    foreach (Tuple<string, DenseVector> Member in Clust.MemberList)
                    {
                        int X = Convert.ToInt32(Member.Item1.Split(',')[0]);
                        int Y = Convert.ToInt32(Member.Item1.Split(',')[1]);
                        DataPoint Datum = chart1.Series[0].Points.Where(dp => (dp.XValue == X && dp.YValues[0] == Y)).ToList()[0];
                        Datum.Color = CurrentClusterColour;
                    }
                }
            }
            allPointsColourPlaceholders = new Color[chart1.Series[0].Points.Count];
            for(int i = 0; i < allPointsColourPlaceholders.Length; i++)
            {
                allPointsColourPlaceholders[i] = chart1.Series[0].Points[i].Color;
            }
            secondaryCLusteringInEffect = true;
            //make relevant form controls available
            unClusterButton.Enabled = true;
            displayAsClustersCheckbox.Enabled = true;
        }

        private void unClusterButton_Click(object sender, EventArgs e)
        {
            foreach(DataPoint DP in chart1.Series[0].Points)
            {
                DP.Color = standardPointColour;
            }
            chart1.Palette = ChartColorPalette.EarthTones;
            unClusterButton.Enabled = false;
            displayAsClustersCheckbox.Enabled = false;
            membersDataGridView.DataSource = null;
            groupsOverviewDataGridView.DataSource = null;
            currentNeuronLabel.ForeColor = Color.Black;

            secondaryCLusteringInEffect = false;
        }

        private void csvButton_Click(object sender, EventArgs e)
        {
            string CSVString = FileHelperFuctions.ReturnCSVString(membersDataGridView);
            string FileName = $"{parentReference.PreferredExportFilepath}\\{FileHelperFuctions.ReturnAcceptableFileName(groupsOverviewLabel.Text.Split(':')[1])}.csv";
            File.WriteAllText(FileName, CSVString);         
        }

        private void templatesButton_Click(object sender, EventArgs e)
        {
            if (!secondaryCLusteringInEffect || !displayAsClustersCheckbox.Checked)
            {
                List<KohonenNeuron> KohoList = (from KN in thisMap.Clusters.ConvertAll(c => (KohonenNeuron)c)
                                                where KN.MemberCount>0
                                                select KN).ToList();
                foreach(KohonenNeuron Koho in KohoList)
                {
                    Cluster Koho2 = (Cluster)Koho.Clone();
                    Koho2.SetCentroidAsMean();
                    DenseVector VectIn = Koho2.GetCentroid();
                    Parallel.For(0, VectIn.Count, i => {
                        if (VectIn[i] < parentReference.Threshold)
                        {
                            VectIn[i] = 0;
                        }
                    });
                    //create and show corresponding Template Recommendation
                    TemplateForm TF = new TemplateForm(
                        FileHelperFuctions.RecommendationString(
                            VectIn, 
                            parentReference.GroupNamesAndDescriptionsAll, 
                            $"{{{Koho.Coordinates[0]},{Koho.Coordinates[1]}}}"                           
                        ),
                        $"{{{Koho.Coordinates[0]},{Koho.Coordinates[1]}}} Template Recommendation"
                        
                    );
                    TF.Show();
                }
            }
            else
            {
                //output templates based on clustered kohonen nodes
                
                foreach(Cluster Clust in secondaryAlgo.Clusters)
                {
                    List<string> UniqueIds = new List<string>();
                    List<KohonenNeuron> KNs = (from KN in thisMap.Clusters.ConvertAll(c=>(KohonenNeuron)c)
                                               where Clust.MemberList.ConvertAll(c=>c.Item1).Contains($"{KN.Coordinates[0]},{KN.Coordinates[1]}")
                                               select KN).ToList();
                    foreach(KohonenNeuron KN in KNs)
                    {
                        UniqueIds.AddRange(KN.MemberList.ConvertAll(c => c.Item1));
                    }
                    UniqueIds = UniqueIds.Distinct().ToList();
                    UniqueIds.Sort();
                    List<QueryResult> ThisClusterQRList = new List<QueryResult>();
                    if (thisQRType == typeof(UserQueryResult))
                    {
                        UserQueryReport ReportRef = (UserQueryReport)thisQR;
                        ThisClusterQRList.AddRange((
                            from QR in ReportRef.QRList
                            where UniqueIds.Contains(QR.AccountName)
                            select QR).ToList());
                    }
                    else if (thisQRType == typeof(GroupingQueryResult))
                    {
                        GroupingQueryReport ReportRef = (GroupingQueryReport)thisQR;
                        ThisClusterQRList.AddRange((
                            from QR in ReportRef.QRList
                            where UniqueIds.Contains(QR.GroupingName)
                            select QR).ToList());
                    }
                    else if (thisQRType == typeof(UserClusteringResult))
                    {
                        UserClusteringReport ReportRef = (UserClusteringReport)thisQR;
                        ThisClusterQRList.AddRange((
                            from QR in ReportRef.QRList
                            where UniqueIds.Contains(QR.ClusterIndex.ToString())
                            select QR).ToList());
                    }
                    else if (thisQRType == typeof(GroupingClusteringResult))
                    {
                        GroupingClusteringReport ReportRef = (GroupingClusteringReport)thisQR;
                        ThisClusterQRList.AddRange((
                            from QR in ReportRef.QRList
                            where UniqueIds.Contains(QR.ClusterIndex.ToString())
                            select QR).ToList());
                    }
                    TemplateForm TF = new TemplateForm(
                        FileHelperFuctions.ReturnRecommendationString(
                            ThisClusterQRList, parentReference.GroupNamesAndDescriptionsAll, parentReference.Threshold
                        ), $"Cluster {secondaryAlgo.Clusters.IndexOf(Clust)} Template Recommendation"
                    );
                    TF.Show();
                }
            }
        }

        private void firstDistanceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (firstDistanceRadioButton.Checked)
            {
                hACStoppingStyle = HACStoppingStyle.ProportionOfFirstDistance;
            }
            else if (proportionLastDistanceRadioButton.Checked)
            {
                hACStoppingStyle = HACStoppingStyle.ProportionOfLastDistance;
            }
            else if (iterationsRadioButton.Checked)
            {
                hACStoppingStyle = HACStoppingStyle.Iterations;
            }
            else if (noneButton.Checked)
            {
                hACStoppingStyle = HACStoppingStyle.None;
            }
        }

        private void proportionLastDistanceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            firstDistanceRadioButton_CheckedChanged(sender, e);
        }

        private void iterationsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            firstDistanceRadioButton_CheckedChanged(sender, e);
        }

        private void noneButton_CheckedChanged(object sender, EventArgs e)
        {
            firstDistanceRadioButton_CheckedChanged(sender, e);
        }

        private void singleLinkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (singleLinkRadioButton.Checked)
            {
                hACDistanceStyle = HACDistanceStyle.SLink;
            }
            else if (completeLinkRadioButton.Checked)
            {
                hACDistanceStyle = HACDistanceStyle.CLink;
            }
            else if (centroidRadioButton.Checked)
            {
                hACDistanceStyle = HACDistanceStyle.Centroid;
            }
            else if (averageDistanceRadioButton.Checked)
            {
                hACDistanceStyle = HACDistanceStyle.MeanDist;
            }
            else if (wardRadioButton.Checked)
            {
                hACDistanceStyle = HACDistanceStyle.Ward;
            }
        }

        private void completeLinkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            singleLinkRadioButton_CheckedChanged(sender, e);
        }

        private void averageDistanceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            singleLinkRadioButton_CheckedChanged(sender, e);
        }

        private void centroidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            singleLinkRadioButton_CheckedChanged(sender, e);
        }

        private void wardRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            singleLinkRadioButton_CheckedChanged(sender, e);
        }
    }
}
