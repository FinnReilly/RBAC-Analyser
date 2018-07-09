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
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace RBACS
{
    public partial class QueryDetailsForm : Form
    {
        string qRName;
        QueryResult thisFormResult;
        RBAC parentReference;
        Type thisFormResultType;
        List<iKNNResult> AllKNNResults;
        Report thisQueryReport;
        Report thisGroupsReport;
        BindingSource thisBindingSource;
        BindingSource thisBindingSourceGroups;
        public QueryDetailsForm()
        {
            InitializeComponent();
        }

        public QueryDetailsForm(QueryResult InputQR, RBAC SourceRBAC) : this()
        {           
            thisFormResult = InputQR;
            thisBindingSourceGroups = new BindingSource();
            parentReference = SourceRBAC;
            if (InputQR.GetType() == typeof(RBACS.UserQueryResult))
            {
                UserQueryResult UQR = (UserQueryResult)InputQR;
                qRName = UQR.AccountName;
                string QRScrip = UQR.Description;
                string QRTitle = UQR.Title;
                Text = $"{qRName} Query Details";
                titlingRichTextBox.Text = $"Name:\t{qRName}\r\nTitle:\t{QRTitle}\r\nDescription:\t{QRScrip}";
                thisGroupsReport = new GroupReport(HelperFunctions.QueryResultToGroupResults(UQR, parentReference.GroupNamesAndDescriptionsAll), Ordering.Ascending);
                GroupReport GR2 = (GroupReport)thisGroupsReport;
                thisBindingSourceGroups.DataSource = GR2.QRList;
                groupsDataGridView.DataSource = thisBindingSourceGroups;
                //summaryTextBox.Text = FileHelperFuctions.ReturnFormattedPersonInfo((UserQueryResult)InputQR, SourceRBAC.GroupNamesAndDescriptionsAll, SourceRBAC.ByTitle);
            }
            else if (InputQR.GetType() == typeof(RBACS.GroupingQueryResult))
            {
                GroupingQueryResult GQR = (GroupingQueryResult)InputQR;
                qRName = GQR.GroupingName;
                Text = $"{qRName} Query Details";
                titlingRichTextBox.Text = $"{GQR.GroupingType}:\t{GQR.GroupingName}\r\nMember Count:\t{GQR.MemberCount}";
                List<UserQueryResult> TempUQRList = new List<UserQueryResult>();
                foreach (string Mem in GQR.Members.Split(',')) {
                    if (Mem != null)
                    {
                        string MemTrim = Mem.Trim();
                        Parallel.ForEach(parentReference.UserQueryResults, UQR =>
                        {
                            if (UQR.AccountName == MemTrim)
                            {
                                TempUQRList.Add(UQR);
                            }
                        });
                    }
                }

                thisGroupsReport = new GroupRepresentationTFIDFReport(HelperFunctions.QueryListToGroupRepresentationTFIDFList(GQR, parentReference.GroupingQueryResults, parentReference.GroupNamesAndDescriptionsAll), Ordering.Ascending);
                GroupRepresentationTFIDFReport GRR2 = (GroupRepresentationTFIDFReport)thisGroupsReport;
                thisBindingSourceGroups.DataSource = GRR2.QRList;
                groupsDataGridView.DataSource = thisBindingSourceGroups;
                //summaryTextBox.Text = FileHelperFuctions.ReturnFormattedGroupSummary((GroupingQueryResult)InputQR, SourceRBAC.GroupNamesAndDescriptionsAll);
            }
            else
            {

            }
            thisFormResultType = InputQR.GetType();
            thisBindingSource = new BindingSource();
            parentReference = SourceRBAC;
            //InitializeComponent();
        }

        private void QueryDetailsForm_Load(object sender, EventArgs e)
        {

        }

        private void exportQueryAsFileButton_Click(object sender, EventArgs e)
        {
            string Filename;
            string Outstring;
            if (thisFormResult.GetType() == typeof(RBACS.UserQueryResult))
            {
                UserQueryResult UQR = (UserQueryResult)thisFormResult;
                Filename = parentReference.PreferredExportFilepath + "\\" + UQR.AccountName + ".txt";
                Outstring = FileHelperFuctions.ReturnFormattedPersonInfo(UQR, parentReference.GroupNamesAndDescriptionsAll, parentReference.ByTitle);
            }
            else if (thisFormResult.GetType() == typeof(RBACS.GroupingQueryResult))
            {
                GroupingQueryResult GQR = (GroupingQueryResult)thisFormResult;
                Filename = parentReference.PreferredExportFilepath + "\\" + FileHelperFuctions.ReturnAcceptableFileName(GQR.GroupingName) + ".txt";
                Outstring = FileHelperFuctions.ReturnFormattedGroupInfo(parentReference.UserQueryResults, GQR, parentReference.GroupNamesAndDescriptionsAll);
            }
            else { Filename = "";Outstring = ""; }

            TextWriter QueryText = new StreamWriter(Filename);
            QueryText.Write(Outstring);
            QueryText.Dispose();
        }

        private void nearestNeighboursButton_Click(object sender, EventArgs e)
        {
            nNDataGridView.AutoGenerateColumns = true;
            bool KSpecified = true;
            int K;
            List<iKNNResult> AllResults = new List<iKNNResult>();
            ConcurrentBag<iKNNResult> ResultBag = new ConcurrentBag<iKNNResult>();
            try { K = Convert.ToInt32(kTextBox.Text); }
            catch
            {
                KSpecified = false;
                kTextBox.Text = "All";
                Application.DoEvents();
                K = -1;
            }
            if (thisFormResultType == typeof(UserQueryResult))
            {
                
                UserQueryResult UQR = (UserQueryResult)thisFormResult;
                List<UserKNNResult> UKRList = new List<UserKNNResult>();
                Parallel.ForEach<UserQueryResult>(parentReference.UserQueryResults, CurrentUQR => {
                    if(CurrentUQR.AccountName != UQR.AccountName)
                    {
                        UserKNNResult UKR = new UserKNNResult(CurrentUQR);
                        double CurrentDistance = HelperFunctions.GetEuclideanDistance(UQR.ReturnAccessVector(), CurrentUQR.ReturnAccessVector());
                        UKR.AssignKNNDistanceFromX(CurrentDistance);
                        ResultBag.Add(UKR);
                    }
                });
                UKRList = ResultBag.Cast<UserKNNResult>().ToList<UserKNNResult>();
                AllResults = UKRList.OrderBy(o => o.Distance).ToList().Cast<iKNNResult>().ToList();
                thisQueryReport = new UserKNNReport(AllResults.Cast<UserKNNResult>().ToList(), Ordering.Ascending);
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                GroupingQueryResult GQR = (GroupingQueryResult)thisFormResult;
                List<GroupingKNNResult> GKRList = new List<GroupingKNNResult>();
                Parallel.ForEach<GroupingQueryResult>(parentReference.GroupingQueryResults, CurrentGQR => {
                    if(CurrentGQR.GroupingName != GQR.GroupingName)
                    {
                        GroupingKNNResult GKR = new GroupingKNNResult(CurrentGQR);
                        double CurrentDistance;
                        if (parentReference.ClusterByRelativeCount)
                        {
                            CurrentDistance = HelperFunctions.GetEuclideanDistance(GQR.ReturnAccessVector(), CurrentGQR.ReturnAccessVector());
                        }
                        else
                        {
                            CurrentDistance = HelperFunctions.GetEuclideanDistance(GQR.ReturnTF_IDFVector(), CurrentGQR.ReturnTF_IDFVector());
                        }
                        GKR.AssignKNNDistanceFromX(CurrentDistance);
                        ResultBag.Add(GKR);
                    }
                });
                GKRList = ResultBag.Cast<GroupingKNNResult>().ToList();
                AllResults = GKRList.OrderBy(o => o.Distance).ToList().Cast<iKNNResult>().ToList();
                thisQueryReport = new GroupingKNNReport(AllResults.Cast<GroupingKNNResult>().ToList(), Ordering.Ascending);
            }
            else { }
           
            if (KSpecified && K<=AllResults.Count)
            {
                List<iKNNResult> Outlist = new List<iKNNResult>();
                for (int i = 0; i < K; i++)
                {
                    Outlist.Add(AllResults[i]);
                }
                //AllKNNResults = Outlist;
                if(thisFormResultType == typeof(UserQueryResult)) {
                    thisQueryReport = new UserKNNReport(Outlist.Cast<UserKNNResult>().ToList(), Ordering.Ascending);
                }
                else if (thisFormResultType == typeof(GroupingQueryResult)) {
                    thisQueryReport = new GroupingKNNReport(Outlist.Cast<GroupingKNNResult>().ToList(), Ordering.Ascending);
                }
                else { }
            }
            else
            {
                AllKNNResults = AllResults;
            }
            if(thisFormResultType == typeof(UserQueryResult))
            {
                UserKNNReport ReportPointer = (UserKNNReport)thisQueryReport;
                thisBindingSource.DataSource = ReportPointer.QRList;
                //thisBindingSource.DataSource = (UserKNNReport)thisQueryReport..Cast<UserKNNResult>().ToList();
                nNDataGridView.DataSource = thisBindingSource;
            }
            else if(thisFormResultType == typeof(GroupingQueryResult))
            {
                GroupingKNNReport ReportPointer = (GroupingKNNReport)thisQueryReport;
                thisBindingSource.DataSource = ReportPointer.QRList;
                
                nNDataGridView.DataSource = thisBindingSource;
            }
            else { }
            
        }

        private void kNNAsCSVButton_Click(object sender, EventArgs e)
        {
            string CSVString = FileHelperFuctions.ReturnCSVString(nNDataGridView);
            string FileName;
            if (thisFormResultType == typeof(UserQueryResult))
            {
                UserQueryResult UQR = (UserQueryResult)thisFormResult;
                FileName = $"{parentReference.PreferredExportFilepath}\\{UQR.Name} {kTextBox.Text} Nearest Neighbours.csv";
                File.WriteAllText(FileName, CSVString);
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                GroupingQueryResult GQR = (GroupingQueryResult)thisFormResult;
                FileName = $"{parentReference.PreferredExportFilepath}\\{FileHelperFuctions.ReturnAcceptableFileName(GQR.GroupingName)} {kTextBox.Text} Nearest Neighbours.csv";
                File.WriteAllText(FileName, CSVString);
            }
            else { }
        }

        private void kNNAsFileButton_Click(object sender, EventArgs e)
        {
            string FileName;
            string FileContent = "";
            if (thisFormResultType == typeof(UserQueryResult))
            {
                UserQueryResult UQR = (UserQueryResult)thisFormResult;
                List<UserKNNResult> UKRList = AllKNNResults.Cast<UserKNNResult>().ToList();
                FileName = $"{parentReference.PreferredExportFilepath}\\{UQR.Name} {kTextBox.Text} Nearest Neighbours.txt";
                int i = 1;
                foreach(UserKNNResult UKR in UKRList)
                {
                    FileContent = FileContent + FileHelperFuctions.FirstSecondEtc(i) + $" Nearest Neighbour to {UQR.AccountName}, Distance of {UKR.Distance}:\r\n\r\n#########################################################\r\n" 
                        + FileHelperFuctions.ReturnFormattedPersonInfo(new UserQueryResult(UKR.Title, UKR.Description, UKR.Name, UKR.AccountName, UKR.DistinguishedName, UKR.ReturnAccessVector()), parentReference.GroupNamesAndDescriptionsAll, parentReference.ByTitle);
                    i++;
                }

                TextWriter UTextWr = new StreamWriter(FileName);
                UTextWr.Write(FileContent);
                UTextWr.Dispose();
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                //only puts out single file at this point
                //don't see why multiple files would even be desirable for KNN Results

                GroupingQueryResult GQR = (GroupingQueryResult)thisFormResult;
                List<GroupingKNNResult> GKRList = AllKNNResults.Cast<GroupingKNNResult>().ToList();
                FileName = $"{parentReference.PreferredExportFilepath}\\{FileHelperFuctions.ReturnAcceptableFileName(GQR.GroupingName)} {kTextBox.Text} Nearest Neighbours.txt";
                int i2 = 1;
                foreach(GroupingKNNResult GKR in GKRList)
                {
                    FileContent = FileContent + FileHelperFuctions.FirstSecondEtc(i2) + $" Nearest Neighbour to {GQR.GroupingName}, Distance of {GKR.Distance}:\r\n\r\n#########################################################\r\n"
                        + FileHelperFuctions.ReturnFormattedGroupSummary(new GroupingQueryResult(GKR), parentReference.GroupNamesAndDescriptionsAll);
                    i2++;
                }

                TextWriter GTextWr = new StreamWriter(FileName);
                GTextWr.Write(FileContent);
                GTextWr.Dispose();
            }
            else { }
        }

        private void nNDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (thisFormResultType == typeof(UserQueryResult))
            {
                int RowNum = e.RowIndex;
                if (RowNum >= 0) { 
                var KeyResult = from UQR in parentReference.UserQueryResults where UQR.AccountName == nNDataGridView.Rows[RowNum].Cells[4].Value.ToString() select UQR;
                UserQueryResult Result = KeyResult.ToList()[0];
                QueryDetailsForm QDF = new QueryDetailsForm(Result, parentReference);
                QDF.Show();}
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                int RowNum = e.RowIndex;
                if (RowNum >= 0)
                {
                    var KeyResult = from UQR in parentReference.GroupingQueryResults where UQR.GroupingName == nNDataGridView.Rows[RowNum].Cells[1].Value.ToString() select UQR;
                    GroupingQueryResult Result = KeyResult.ToList()[0];
                    QueryDetailsForm QDF = new QueryDetailsForm(Result, parentReference);
                    QDF.Show();
                }
            }
        }
        private QueryResult uQtoQR(UserQueryResult QR) {
            return (QueryResult)QR;
        }
        private QueryResult gQtoQR(GroupingQueryResult QR)
        {
            return (QueryResult)QR;
        }
        private void recommendTemplateButton_Click(object sender, EventArgs e)
        {
            List<QueryResult> QRList = new List<QueryResult>();
            string Title;
            if (nNDataGridView.SelectedRows.Count > 0)
            {
                Title = $"User Selection from {nNDataGridView.Rows.Count} Nearest Neighbours, {qRName}";
            }
            else
            {
                Title = $"{nNDataGridView.Rows.Count} Nearest Neighbours, {qRName}";
            }
            if(thisFormResultType == typeof(UserQueryResult))
            {

                Converter<UserQueryResult, QueryResult> Converter = new Converter<UserQueryResult, QueryResult>(uQtoQR);
                QRList = HelperFunctions.DatagridViewToQueryResultList(nNDataGridView, 4, parentReference.UserQueryResults.ConvertAll<QueryResult>(Converter));
                QRList.Add(thisFormResult);
                string InputString = FileHelperFuctions.ReturnRecommendationString(QRList, parentReference.GroupNamesAndDescriptionsAll, parentReference.Threshold);
                TemplateForm ResultantForm = new TemplateForm(InputString, Title);
                ResultantForm.Show();
            }
            else
            {
                Converter<GroupingQueryResult, QueryResult> Converter = new Converter<GroupingQueryResult, QueryResult>(gQtoQR);
                QRList = HelperFunctions.DatagridViewToQueryResultList(nNDataGridView, 1, parentReference.GroupingQueryResults.ConvertAll<QueryResult>(Converter));
                QRList.Add(thisFormResult);
                string InputString = FileHelperFuctions.ReturnRecommendationString(QRList, parentReference.GroupNamesAndDescriptionsAll, parentReference.Threshold);
                TemplateForm ResultantForm = new TemplateForm(InputString,Title);
                ResultantForm.Show();
            }
        }

        private void nNDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string Property = nNDataGridView.Columns[e.ColumnIndex].HeaderText;
            if (thisFormResultType == typeof(UserQueryResult))
            {
                UserKNNReport ReportPointer = (UserKNNReport)thisQueryReport;
                ReportPointer.SortByProperty(Property);
                thisBindingSource.DataSource = ReportPointer.QRList;
            }
            else if (thisFormResultType == typeof(GroupingQueryResult))
            {
                GroupingKNNReport ReportPointer = (GroupingKNNReport)thisQueryReport;
                ReportPointer.SortByProperty(Property);
                thisBindingSource.DataSource = ReportPointer.QRList;
            }
            else { }
        }

        private void groupsDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (thisGroupsReport.GetType() == typeof(RBACS.GroupReport))
            {
                GroupReport Shadow = (GroupReport)thisGroupsReport;
                Shadow.SortByProperty(groupsDataGridView.Columns[e.ColumnIndex].HeaderText);
                thisBindingSourceGroups.DataSource = Shadow.QRList;
                groupsDataGridView.DataSource = thisBindingSourceGroups;
            }
            else if (thisGroupsReport.GetType() == typeof(RBACS.GroupRepresentationTFIDFReport))
            {
                GroupRepresentationTFIDFReport Shadow = (GroupRepresentationTFIDFReport)thisGroupsReport;
                Shadow.SortByProperty(groupsDataGridView.Columns[e.ColumnIndex].HeaderText);
                thisBindingSourceGroups.DataSource = Shadow.QRList;
                groupsDataGridView.DataSource = thisBindingSourceGroups;
            }
        }
    }
}
