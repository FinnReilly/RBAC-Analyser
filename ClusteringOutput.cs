using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MathNet.Numerics.LinearAlgebra.Double;

namespace RBACS
{
    public partial class ClusteringOutput : Form
    {
        Type thisFormResultType;
        RBAC parentReference;
        ClusteringAlgo thisAlgo;
        List<QueryResult> clusteringResultList;
        Report thisQR;
        Report subsetThisQR;
        BindingSource thisBindingSource;
        public ClusteringOutput(List<QueryResult> QRList, RBAC RBACRef)
        {
            thisFormResultType = QRList[0].GetType();
            parentReference = RBACRef;
            ConcurrentBag<Tuple<string, DenseVector>> InputList = new ConcurrentBag<Tuple<string, DenseVector>>();
            if (QRList[0].GetType() == typeof(UserQueryResult))
            {
                Parallel.ForEach<QueryResult>(QRList, QR =>
                {
                    UserQueryResult UQR = (UserQueryResult)QR;
                    Tuple<string, DenseVector> TupleIn = new Tuple<string, DenseVector>(UQR.AccountName, (DenseVector)UQR.ReturnAccessVector());
                    InputList.Add(TupleIn);
                });
            }
            else if (QRList[0].GetType() == typeof(GroupingQueryResult))
            {
                Parallel.ForEach<QueryResult>(QRList, QR =>
                {
                    GroupingQueryResult GQR = (GroupingQueryResult)QR;
                    DenseVector VectorA;
                    if (parentReference.ClusterByRelativeCount)
                    {
                        VectorA = (DenseVector)GQR.ReturnAccessVector();
                    }
                    else
                    {
                        VectorA = (DenseVector)GQR.ReturnTF_IDFVector();
                    }
                    Tuple<string, DenseVector> TupleIn = new Tuple<string, DenseVector>(GQR.GroupingName, VectorA);
                    InputList.Add(TupleIn);
                });
            }
            else { }
            //add options on algo configtab on main form later
            if (parentReference.ClusteringAlgoType == typeof(HACAlgo))
            {
                thisAlgo = new HACAlgo(InputList.OrderBy(o => o.Item1).ToList(), parentReference.PreferredDistanceStyle, parentReference.HACStoppingMetric, parentReference.PreferredStoppingStyle);
            }
            else
            {
                thisAlgo = new KMeansPlusPlus(InputList.OrderBy(o => o.Item1).ToList(), parentReference.KMeansValue, parentReference.PreferredKMeansStoppingStyle, parentReference.KMeansMaxIter);
            }
            parentReference.statusLabelChanger($"Initialising {thisAlgo.GetType().ToString().Split('.')[1]}, please be patient");
            thisAlgo.InitialiseClusters();
            
            while (!thisAlgo.Stopped)
            {
          
                thisAlgo.IterateOnce();
                if (!thisAlgo.Stopped)
                {
                    parentReference.statusLabelChanger($"Running {thisAlgo.GetType().ToString().Split('.')[1]}, iteration {thisAlgo.Iterator}");
                }
            }
            //set all centroids as means in case mapping of clusters is required further
            //down the line:

            if (QRList[0].ReturnAccessVector().Count > 500)
            {
                thisAlgo.SetCentroidsAsMeansHighDimensionality();
            }
            else
            {
                thisAlgo.SetCentroidsAsMeans();
            }
            parentReference.statusLabelChanger("Creating Data View");

            ConcurrentBag<QueryResult> ResultsBag = new ConcurrentBag<QueryResult>();
            if (thisFormResultType == typeof(UserQueryResult))
            {
                foreach (Cluster Clust in thisAlgo.Clusters)
                {
                    Parallel.ForEach<Tuple<string, DenseVector>>(Clust.MemberList, Member => {
                        UserQueryResult Target = (UserQueryResult)(from UQR in QRList.Cast<UserQueryResult>() where UQR.AccountName == Member.Item1 select UQR).ToList()[0];
                        ResultsBag.Add(new UserClusteringResult(Target, Clust.ClusterID, Clust.ListPosition));
                    });
                }
                clusteringResultList = ResultsBag.Cast<UserClusteringResult>().OrderBy(o => o.ClusterIndex).ToList<QueryResult>();
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                foreach (Cluster Clust in thisAlgo.Clusters)
                {
                    Parallel.ForEach<Tuple<string, DenseVector>>(Clust.MemberList, Member => {
                        GroupingQueryResult Target = (GroupingQueryResult)(from GQR in QRList.Cast<GroupingQueryResult>().ToList() where GQR.GroupingName == Member.Item1 select GQR).ToList<GroupingQueryResult>()[0];
                        ResultsBag.Add(new GroupingClusteringResult(Target, Clust.ClusterID, Clust.ListPosition));
                    });
                }
                clusteringResultList = ResultsBag.Cast<GroupingClusteringResult>().OrderBy(o => o.ClusterIndex).ToList<QueryResult>();
            }
            else { }
            parentReference.statusLabelChanger("Idle");

            InitializeComponent();
            this.Text = $"Clustering Results from {thisAlgo.Iterator} Iterations, using {thisAlgo.GetType().ToString().Split('.')[1]}, {thisAlgo.Clusters.Count} Clusters";

            
            thisBindingSource = new BindingSource();
            if (thisFormResultType == typeof(UserQueryResult))
            {
                //needs a bit of casting to allow datagridview to access type-specific public properties
                thisQR = new UserClusteringReport(clusteringResultList.Cast<UserClusteringResult>().ToList(), Ordering.Ascending);
                UserClusteringReport ReportReference = (UserClusteringReport)thisQR;
                thisBindingSource.DataSource =  ReportReference.QRList;
                clustersDataGridView.DataSource = thisBindingSource;
            }
            else
            {
                thisQR = new GroupingClusteringReport(clusteringResultList.Cast<GroupingClusteringResult>().ToList(), Ordering.Ascending);
                GroupingClusteringReport ReportReference = (GroupingClusteringReport)thisQR;
                thisBindingSource.DataSource = ReportReference.QRList;
                clustersDataGridView.DataSource = thisBindingSource;
            }
        }

        private void exportAsCSVButton_Click(object sender, EventArgs e)
        {
            string CSVString = FileHelperFuctions.ReturnCSVString(clustersDataGridView);
            string FileName;
            if (thisFormResultType == typeof(UserQueryResult))
            {              
                FileName = $"{parentReference.PreferredExportFilepath}\\ClusterResultsByUser.csv";
                File.WriteAllText(FileName, CSVString);
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                string TypeOfGroup;
                if (parentReference.ByTitle) {
                    TypeOfGroup = "Title";
                }
                else
                {
                    TypeOfGroup = "Description";
                }
                FileName = $"{parentReference.PreferredExportFilepath}\\ClusterResultsBy{TypeOfGroup}.csv";
                File.WriteAllText(FileName, CSVString);
            }
            else { }
        }

        private QueryResult uQtoQR(UserQueryResult QR)
        {
            return (QueryResult)QR;
        }
        private QueryResult gQtoQR(GroupingQueryResult QR)
        {
            return (QueryResult)QR;
        }
        private void recommendTemplateButton_Click(object sender, EventArgs e)
        {
            List<QueryResult> QRList = new List<QueryResult>();
            
            foreach (Cluster C in thisAlgo.Clusters)
            {
                string Title = $"Based on Cluster {C.ListPosition}, clustered by {thisAlgo.GetType().ToString().Split('.')[1]}";
                if (thisFormResultType == typeof(UserQueryResult))
                {

                    Converter<UserQueryResult, QueryResult> Converter = new Converter<UserQueryResult, QueryResult>(uQtoQR);
                    QRList = HelperFunctions.DatagridViewSubsetToQueryResultList(clustersDataGridView, 4, 0, C.ListPosition.ToString(), parentReference.UserQueryResults.ConvertAll<QueryResult>(Converter));

                    string InputString = FileHelperFuctions.ReturnRecommendationString(QRList, parentReference.GroupNamesAndDescriptionsAll, parentReference.Threshold);
                    TemplateForm ResultantForm = new TemplateForm(InputString, Title);
                    ResultantForm.Show();
                }
                else
                {
                    Converter<GroupingQueryResult, QueryResult> Converter = new Converter<GroupingQueryResult, QueryResult>(gQtoQR);
                    QRList = HelperFunctions.DatagridViewSubsetToQueryResultList(clustersDataGridView, 1, 0, C.ListPosition.ToString(), parentReference.GroupingQueryResults.ConvertAll<QueryResult>(Converter));

                    string InputString = FileHelperFuctions.ReturnRecommendationString(QRList, parentReference.GroupNamesAndDescriptionsAll, parentReference.Threshold);
                    TemplateForm ResultantForm = new TemplateForm(InputString, Title);
                    ResultantForm.Show();
                }
            }
        }

        private void clustersDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (thisFormResultType == typeof(UserQueryResult))
            {
                int RowNum = e.RowIndex;
                if (RowNum >= 0)
                {
                    var KeyResult = from UQR in parentReference.UserQueryResults where UQR.AccountName == clustersDataGridView.Rows[RowNum].Cells[4].Value.ToString() select UQR;
                    UserQueryResult Result = KeyResult.ToList()[0];
                    QueryDetailsForm QDF = new QueryDetailsForm(Result, parentReference);
                    QDF.Show();
                }
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                int RowNum = e.RowIndex;
                if (RowNum >= 0)
                {
                    var KeyResult = from UQR in parentReference.GroupingQueryResults where UQR.GroupingName == clustersDataGridView.Rows[RowNum].Cells[1].Value.ToString() select UQR;
                    GroupingQueryResult Result = KeyResult.ToList()[0];
                    QueryDetailsForm QDF = new QueryDetailsForm(Result, parentReference);
                    QDF.Show();
                }
            }
        }

        private void clustersDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string Prop = clustersDataGridView.Columns[e.ColumnIndex].HeaderText;
            if (thisFormResultType == typeof(UserQueryResult))
            {
                UserClusteringReport ReportRef = (UserClusteringReport)thisQR;
                ReportRef.SortByProperty(Prop);
                thisBindingSource.DataSource = ReportRef.QRList;
                clustersDataGridView.DataSource = thisBindingSource;
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                GroupingClusteringReport ReportRef = (GroupingClusteringReport)thisQR;
                ReportRef.SortByProperty(Prop);
                thisBindingSource.DataSource = ReportRef.QRList;
                clustersDataGridView.DataSource = thisBindingSource;
            }
            else { }
        }

        private void clusteringFindTextBox_TextChanged(object sender, EventArgs e)
        {
            if (clusteringFindTextBox.Text == null || clusteringFindTextBox.Text == "")
            {
                //allUsersDisplayed = true;
                if (thisFormResultType == typeof(UserQueryResult))
                {
                    UserClusteringReport ReportReference = (UserClusteringReport)thisQR;
                    thisBindingSource.DataSource = ReportReference.QRList;
                    clustersDataGridView.DataSource = thisBindingSource;
                }
                else if (thisFormResultType == typeof(GroupingQueryResult))
                {
                    GroupingClusteringReport ReportReference = (GroupingClusteringReport)thisQR;
                    thisBindingSource.DataSource = ReportReference.QRList;
                    clustersDataGridView.DataSource = thisBindingSource;
                }

            }
            else
            {
                //allUsersDisplayed = false;
                if (thisFormResultType == typeof(UserQueryResult))
                {
                    UserClusteringReport ReportReference = (UserClusteringReport)thisQR;
                    subsetThisQR = ReportReference.Search(clusteringFindTextBox.Text);
                    ReportReference = (UserClusteringReport)subsetThisQR;
                    thisBindingSource.DataSource = ReportReference.QRList;
                    clustersDataGridView.DataSource = thisBindingSource;
                }
                else if (thisFormResultType == typeof(GroupingQueryResult))
                {
                    GroupingClusteringReport ReportReference = (GroupingClusteringReport)thisQR;
                    subsetThisQR = ReportReference.Search(clusteringFindTextBox.Text);
                    ReportReference = (GroupingClusteringReport)subsetThisQR;
                    thisBindingSource.DataSource = ReportReference.QRList;
                    clustersDataGridView.DataSource = thisBindingSource;
                }
            }
        }

        private int textBoxValidate(TextBox TB, int Default) {
            bool Good = true;
            try { Convert.ToInt16(TB.Text); } catch { Good = false; }
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

        private void mapButton_Click(object sender, EventArgs e)
        {
            if (thisFormResultType == typeof(UserQueryResult))
            {
                UserClusteringReport ReportRef = (UserClusteringReport)thisQR;
                KohonenForm KF = new KohonenForm(parentReference, 
                    textBoxValidate(textBox1, 3), 
                    ReportRef.QRList.ConvertAll(c => (QueryResult)c), 
                    parentReference.MapEpochs, 
                    thisAlgo.Clusters);
                KF.Show();
            }
            else if(thisFormResultType == typeof(GroupingQueryResult))
            {
                GroupingClusteringReport ReportRef = (GroupingClusteringReport)thisQR;
                KohonenForm KF = new KohonenForm(parentReference,
                    textBoxValidate(textBox1, 3),
                    ReportRef.QRList.ConvertAll(c => (QueryResult)c),
                    parentReference.MapEpochs,
                    thisAlgo.Clusters);
                KF.Show();
            }
        }
    }
}
