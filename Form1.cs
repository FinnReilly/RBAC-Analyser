using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.DirectoryServices.AccountManagement;
using MathNet.Numerics.LinearAlgebra.Double;


namespace RBACS
{
    public partial class RBAC : Form
    {
        string domainName;
        string exportPath;
        //string importPath;
        bool allUsersDisplayed;
        bool allGroupsDisplayed;
        bool byTitle;
        bool byRelativeCountCluster;
        bool inclusionsIsLogicalAnd;
        bool exclusionsByWholeName;
        bool groupRecursionDetection;
        int sizeLimit = 10000;
        int mapUsersDimensions;
        int mapGroupingsDimensions;
        int mapEpochs;
        string[] Exclusions;
        string[] Inclusions;
        HACDistanceStyle preferredDistanceStyle;
        HACStoppingStyle preferredStoppingStyle;
        KMeansStoppingStyle preferredKMeansStoppingStyle;
        Type clusteringAlgoType;
        double hACStoppingMetric;

        List<UserQueryResult> userQueryResults;
        List<GroupingQueryResult> groupingQueryResults;
        List<string> groupNamesAll;
        List<Tuple<string, string>> groupNamesAndDescriptionsAll;
        List<string> descriptionsAll;
        List<string> titlesAll;
        BindingSource RBACBindingSource = new BindingSource();

        UserQueryReport uQRReport;
        UserQueryReport subSetUQReport;
        GroupingQueryReport gQRReport;
        GroupingQueryReport subSetGQReport;

        public List<UserQueryResult> UserQueryResults => uQRReport.QRList;
        public List<GroupingQueryResult> GroupingQueryResults => gQRReport.QRList;
        public List<Tuple<string, string>> GroupNamesAndDescriptionsAll => groupNamesAndDescriptionsAll;
        public bool ByTitle => byTitle;
        public bool ClusterByRelativeCount => byRelativeCountCluster;
        
        public string PreferredExportFilepath => exportPath;
        public string PreferredImportFilepath => importFileTextBox.Text;
        public string AdditionalPermissionsType => permissionsTypeTextBox.Text;
        public double Threshold => Convert.ToDouble(recommendThresholdTextBox.Text);
        public double HACStoppingMetric => hACStoppingMetric;
        public int KMeansValue => Convert.ToInt32(textBoxValidate(kValueTextBox, 2));
        public int KMeansMaxIter => Convert.ToInt32(textBoxValidate(kMIterationsTextBox, 100));
        public int MapEpochs => mapEpochs;
        public HACDistanceStyle PreferredDistanceStyle => preferredDistanceStyle;
        public HACStoppingStyle PreferredStoppingStyle => preferredStoppingStyle;
        public KMeansStoppingStyle PreferredKMeansStoppingStyle => preferredKMeansStoppingStyle;
        public Type ClusteringAlgoType => clusteringAlgoType;

        public RBAC()
        {
            InitializeComponent();
            groupNamesAll = new List<string>();
            descriptionsAll = new List<string>();
            titlesAll = new List<string>();
            exportPath = System.IO.Directory.GetCurrentDirectory();
            //importPath = importFileTextBox.Text;
            textBox2.Text = exportPath;
            byTitle = true;
            byRelativeCountCluster = true;
            preferredDistanceStyle = HACDistanceStyle.Centroid;
            hACStoppingMetric = Convert.ToDouble(p1DValueTextBox.Text);
            preferredKMeansStoppingStyle = KMeansStoppingStyle.NoChangeInCentroids;
            clusteringAlgoType = Type.GetType("RBACS.HACAlgo");
            mapUsersDimensions = 5;
            mapGroupingsDimensions = 5;
            mapEpochs = 5;
            textBox1.Text = (Domain.GetComputerDomain()).Name;
            button1.Text = $"Generate Data \n{Domain.GetComputerDomain().Name}";
            domainName = Domain.GetComputerDomain().Name;
            dataGridView1.AutoGenerateColumns = true;
            exclusionsByWholeName = false;
            inclusionsIsLogicalAnd = false;
            allGroupsDisplayed = true;
            allUsersDisplayed = true;
        }

        public void SortDataGridViewByColumnTitle(DataGridView DGV, int ColumnIndex)
        {
            if (DGV.SortOrder == SortOrder.Descending || DGV.SortOrder == SortOrder.None)
            {
                DGV.Sort(DGV.Columns[ColumnIndex], ListSortDirection.Ascending);
            }
            else
            {
                DGV.Sort(DGV.Columns[ColumnIndex], ListSortDirection.Descending);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //event handler for main "GetData" button
            //conducts search of AD for all users in search term
            checkInclusionsExclusions();
            PrincipalContext CurrentContext = new PrincipalContext(ContextType.Domain, domainName);
            string SearchFilter = "(&(objectClass=user)";
            if ((Exclusions == null) && (Inclusions == null))
            {
                SearchFilter = SearchFilter + ")";
            }
            else
            {
                if (Exclusions != null)
                {
                    foreach (string member in Exclusions)
                    {
                        string Addition = "(!distinguishedname=*" + member + "*)";
                        SearchFilter = SearchFilter + Addition;
                    }
                }
                if (Inclusions != null)
                {
                    foreach (string member2 in Inclusions)
                    {
                        string Addition2 = "(distinguishedname=*" + member2 + "*)";
                        SearchFilter = SearchFilter + Addition2;
                    }
                }
                SearchFilter = SearchFilter + ")";
            }
            queryRichTextBox.Text = SearchFilter;
            if (domainName.Contains("."))
            {
                domainName = domainName.Split('.')[0];
            }
            string DomainPath = "LDAP://DC=" + domainName + ",DC=com";
            DirectoryEntry NewSearchRoot = new DirectoryEntry(DomainPath);
            DirectorySearcher NewSearch = new DirectorySearcher(NewSearchRoot);
            //Select all properties required to build user query objects with binary vector on backend
            NewSearch.Filter = "(objectClass=user)";
            NewSearch.PropertiesToLoad.Add("samaccountname");
            NewSearch.PropertiesToLoad.Add("memberof");
            NewSearch.PropertiesToLoad.Add("displayname");
            NewSearch.PropertiesToLoad.Add("mail");
            NewSearch.PropertiesToLoad.Add("title");
            NewSearch.PropertiesToLoad.Add("description");
            NewSearch.PropertiesToLoad.Add("distinguishedname");
            NewSearch.PropertiesToLoad.Add("objectCategory");
            NewSearch.SizeLimit = sizeLimit;
            NewSearch.PageSize = 1000;

            statusLabelChanger("Retrieving and Filtering AD User Info");
            SearchResultCollection ResultCollection = NewSearch.FindAll();
            List<SearchResult> FilteredResultList = new List<SearchResult>();

            //set up some variables before all the gnarly stuff
            groupNamesAll = new List<string>();
            groupNamesAndDescriptionsAll = new List<Tuple<string, string>>();
            groupNamesAll.Add("Domain Users");
            titlesAll = new List<string>();
            descriptionsAll = new List<string>();
            userQueryResults = new List<UserQueryResult>();
            foreach (SearchResult Result in ResultCollection)
            {
                bool DropResult = false;
                //get full DN and DN from first comma onwards
                string Dist = Result.Properties["distinguishedname"][0].ToString();
                string DNameContainerString = Result.Properties["distinguishedname"][0].ToString().Substring(Result.Properties["distinguishedname"][0].ToString().IndexOf(','));
                if (Exclusions != null)
                {
                    foreach (string Exclusion in Exclusions)
                    {
                        if (exclusionsByWholeName)
                        {
                            if (Dist.Contains(Exclusion))
                            {
                                DropResult = true;
                            }
                        }
                        //complicated syntax below avoids excluding based on the name of the object itself
                        else
                        {
                            if (DNameContainerString.Contains(Exclusion))
                            {
                                DropResult = true;
                            }
                        }
                    }
                }
                if (Inclusions != null)
                {
                    
                    if (inclusionsIsLogicalAnd)
                    {
                        //container name must contain all inclusions
                        bool ContainsAllInclusions = true;
                        foreach (string Inclusion in Inclusions)
                        {
                            if (!DNameContainerString.Contains(Inclusion))
                            {
                                ContainsAllInclusions = false;
                            }
                        }
                        if (!ContainsAllInclusions)
                        {
                            DropResult = true;
                        }
                    }
                    else
                    {
                        //else if inclusions work on logical OR:
                        bool NoInclusionsPresent = true;
                        foreach (string Inclusion in Inclusions)
                        {
                            //complicated syntax below avoids excluding based on the name of the object itself
                            if (DNameContainerString.Contains(Inclusion))
                            {
                                NoInclusionsPresent = false;
                            }
                        }
                        if (NoInclusionsPresent)
                        {
                            DropResult = true;
                        }
                    }
                }
                if (!(Result.Properties["objectCategory"][0].ToString().StartsWith("CN=Person")))
                {
                    DropResult = true;
                }
                if (!(DropResult))
                {
                    //string name = (string)Result.Properties["displayname"][0];
                    string descrip = null;
                    string title = null;
                    int GroupCount = Result.Properties["memberof"].Count;
                    if (Result.Properties["memberof"].Count > 0)
                    {
                        foreach (string Group in Result.Properties["memberof"])
                        {
                            string nextString = Group.Split(',')[0].Substring(3);
                            groupNamesAll.Add(nextString);
                        }
                    }
                    try { descrip = Result.Properties["description"][0].ToString(); } catch { descrip = ""; }
                    try { title = Result.Properties["title"][0].ToString(); } catch { title = ""; }
                    titlesAll.Add(title);
                    descriptionsAll.Add(descrip);
                    FilteredResultList.Add(Result);
                    string Message = "Retrieved AD Information for AD User " + Result.Properties["samaccountname"][0].ToString();
                    statusLabelChanger(Message);
                }
            }
            //disposal prevents memory leaks
            NewSearch.Dispose();
            ResultCollection.Dispose();
            groupNamesAll.Sort();
            groupNamesAll = groupNamesAll.Distinct<string>().ToList<string>();
            titlesAll.Sort();
            titlesAll = titlesAll.Distinct<string>().ToList<string>();
            descriptionsAll.Sort();
            descriptionsAll = descriptionsAll.Distinct<string>().ToList<string>();

            statusLabelChanger("Creating Binary Vectors");

            foreach (SearchResult Res in FilteredResultList)
            {
                List<string> GroupsListforRes = new List<string>();
                GroupsListforRes.Add("Domain Users");
                if (Res.Properties["memberof"].Count > 0)
                {
                    foreach (string Group in Res.Properties["memberof"])
                    {
                        string groupString = Group.Split(',')[0].Substring(3);
                        GroupsListforRes.Add(groupString);
                    }
                }
                string title;
                try { title = (string)Res.Properties["title"][0]; } catch { title = ""; }

                string description;
                try { description = (string)Res.Properties["description"][0]; } catch { description = ""; }
                if (description == null) { description = ""; }
                string accountname = (string)Res.Properties["samaccountname"][0];
                string name = "";
                if (Res.Properties.Contains("displayname"))
                {
                    name = (string)Res.Properties["displayname"][0];
                }

                string dN = (string)Res.Properties["distinguishedname"][0];
                Vector GroupsVector = HelperFunctions.StringListsToVector(groupNamesAll, GroupsListforRes);
                UserQueryResult CurrentUQR = new UserQueryResult(title, description, name, accountname, dN, (Vector)GroupsVector.Clone());
                userQueryResults.Add(CurrentUQR);
            }
            userQueryResults = userQueryResults.OrderBy(uqr => uqr.AccountName).ToList();          
            
            //dataGridView1.DataSource = UserQueryResults;
            GroupNumberLabel.Text = GroupNumberLabel.Text.Split(':')[0] + ":" + groupNamesAll.Count.ToString();
            userNumberLabel.Text = userNumberLabel.Text.Split(':')[0] + ":" + userQueryResults.Count.ToString();
            descriptionsNumberLabel.Text = descriptionsNumberLabel.Text.Split(':')[0] + ":" + descriptionsAll.Count.ToString();
            titlesNumberLabel.Text = titlesNumberLabel.Text.Split(':')[0] + ":" + titlesAll.Count.ToString();

            //get group descriptions
            statusLabelChanger("Retrieving Group Descriptions");
            bool AllGroupsAccountedFor = false;
            List<string> GroupsFullyUpdated = new List<string>();
            int DebugLayerCount = 0;
            while (!AllGroupsAccountedFor)
            {
                int DebugThisLayerChildGroupsCount = 0;
                int DebugThisLayerNewGroupsAdded = 0;
                bool InnerGroupsBool = true;
                foreach (string Group in groupNamesAll)
                {
                    //this is where any code to deal with nested groups needs to go

                    if (!GroupsFullyUpdated.Contains(Group))
                    {
                        Principal CurrentGroup = new GroupPrincipal(CurrentContext, Group);
                        PrincipalSearcher ThisGroupSearcher = new PrincipalSearcher(CurrentGroup);
                        string GroupScrip;

                        try { GroupScrip = ThisGroupSearcher.FindOne().Description; } catch { GroupScrip = ""; }
                        Tuple<string, string> CurrentTuple = new Tuple<string, string>(Group, GroupScrip);
                        groupNamesAndDescriptionsAll.Add(CurrentTuple);

                        //check if Group is itself a member of any other groups
                        //If it is, update the groupslist and dataset accordingly

                        bool GroupIsMemberOfOther = true;
                        try
                        {
                            ThisGroupSearcher.FindOne().GetGroups();
                        }
                        catch
                        {
                            GroupIsMemberOfOther = false;
                        }
                                               
                        if (GroupIsMemberOfOther)
                        {
                            PrincipalSearchResult<Principal> Prince = ThisGroupSearcher.FindOne().GetGroups();
                            if (Prince.Count() > 0)
                            {
                                DebugThisLayerChildGroupsCount++;
                                int DebugParentGroupsCounter = 0;
                                int DebugNewParentGroupsCounter = 0;
                                //InnerGroupsBool = false;
                                List<string> NewGroupsAdded = new List<string>();
                                foreach (Principal ParentGroup in Prince)
                                {
                                    DebugParentGroupsCounter++;
                                    //deals with parent groups
                                    if (groupNamesAll.Contains(ParentGroup.Name))
                                    {
                                        statusLabelChanger($"{Group} is a member of {ParentGroup.Name}, updating user permissions");
                                        userQueryResults = DatasetManagement.UpdatePermissionsByCondition(groupNamesAll.IndexOf(ParentGroup.Name), groupNamesAll.IndexOf(Group), userQueryResults);
                                    }
                                    else
                                    {
                                        DebugThisLayerNewGroupsAdded++;
                                        DebugNewParentGroupsCounter++;
                                        //loop needs to go around again to get group description and check for further nested 
                                        //group membership
                                        InnerGroupsBool = false;

                                        statusLabelChanger($"{Group} is a member of {ParentGroup.Name}, adding to group list and updating user permissions");
                                        groupNamesAll = DatasetManagement.AddPermissionToList(ParentGroup.Name, groupNamesAll);
                                        NewGroupsAdded.Add(ParentGroup.Name); //this was used for debugging
                                        userQueryResults = DatasetManagement.AddPermissionToAllConditionalOnIndex(userQueryResults, groupNamesAll, ParentGroup.Name, new List<int>(), groupNamesAll.IndexOf(Group));
                                     
                                    }
                                }
                                
                            }
                        }
                        ThisGroupSearcher.Dispose();
                        GroupsFullyUpdated.Add(Group);
                    }
                }
                if (InnerGroupsBool)
                {
                    AllGroupsAccountedFor = true;
                }
                DebugLayerCount++;
            }

            groupNamesAndDescriptionsAll = groupNamesAndDescriptionsAll.Distinct().OrderBy(o => o.Item1).ToList();
            GroupsListBox.Items.Clear();
            GroupsListBox.Items.AddRange((object[])groupNamesAll.ToArray());
            GroupNumberLabel.Text = GroupNumberLabel.Text.Split(':')[0] + ":" + groupNamesAll.Count.ToString();

            uQRReport = new UserQueryReport(userQueryResults, Ordering.Ascending);
            RBACBindingSource.DataSource = uQRReport.QRList;
            dataGridView1.DataSource = RBACBindingSource;

            statusLabelChanger("Idle");
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Tab))
            {
                domainName = textBox1.Text;
                button1.Text = $"Generate Data \n{domainName}";
            }
        }

        private void richTextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            //imports exclusions list for use in LDAP search filter
            if ((e.KeyCode == Keys.Tab) || (e.KeyCode == Keys.Enter))
            {
                Exclusions = richTextBox2.Text.Split('\n');
                List<string> InterimList = new List<string>();
                foreach (string Entry in Exclusions)
                {
                    if (Entry != "")
                    {
                        if (Entry.StartsWith(" "))
                        {
                            InterimList.Add(Entry.Substring(1).Replace(",", ""));
                        }
                        else
                        {
                            InterimList.Add(Entry.Replace(",", ""));
                        }
                    }
                }
                if (InterimList.Count > 0)
                {
                    Exclusions = InterimList.ToArray<string>();
                }
                else
                {
                    Exclusions = null;
                }
            }
        }

        private void searchSizeLimitBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                sizeLimit = Convert.ToInt32(searchSizeLimitBox.Text);
            }
            catch
            {
                richTextBox2.Text = sizeLimit.ToString();
            }
        }

        private void inclusionsBox_TextChanged(object sender, EventArgs e)
        {
            string[] initialInclusionsList = inclusionsBox.Text.Split('\n');
            List<string> InterimInclusionsList = new List<string>();
            bool AllNull = true;
            foreach (string IncString in initialInclusionsList)
            {
                if (IncString != "")
                {
                    InterimInclusionsList.Add(IncString.Replace(",", ""));
                    AllNull = false;
                }
            }
            if (AllNull)
            {
                Inclusions = null;
            }
            else
            {
                Inclusions = InterimInclusionsList.ToArray<string>();
            }
        }

        //?
        private void checkInclusionsExclusions()
        {
            bool IncAllNull = true;
            bool ExcAllNull = true;

            foreach (string Inc in inclusionsBox.Text.Split('\n'))
            {
                if (Inc != "")
                {
                    IncAllNull = false;
                }
            }
            foreach (string Exc in richTextBox2.Text.Split('\n'))
            {
                if (Exc != "")
                {
                    ExcAllNull = false;
                }
            }
            if (IncAllNull)
            {
                Inclusions = null;
            }
            if (ExcAllNull)
            {
                Exclusions = null;
            }
        }

        private void dataGroupingButton_Click(object sender, EventArgs e)
        {
            if (userQueryResults.Count < 1)
            {
                throw new ArgumentOutOfRangeException();
            }
            groupingQueryResults = new List<GroupingQueryResult>();

            if (titleRadioButtion.Checked)
            {
                statusLabelChanger("Grouping Users By Title");
                foreach (string titlename in titlesAll)
                {
                    ConcurrentBag<UserQueryResult> UQBag = new ConcurrentBag<UserQueryResult>();

                    Parallel.ForEach<UserQueryResult>(userQueryResults, UQR =>
                    {
                        if (UQR.Title == titlename)
                        {
                            UQBag.Add(UQR);
                        }
                    });
                    List<UserQueryResult> CurrentTitleList = UQBag.ToList();
                    groupingQueryResults.Add(new GroupingQueryResult(CurrentTitleList, groupNamesAll, titlename, "Title"));
                }
            }
            else
            {
                statusLabelChanger("Grouping Users By Description");
                foreach (string descriptionname in descriptionsAll)
                {
                    ConcurrentBag<UserQueryResult> UQBag = new ConcurrentBag<UserQueryResult>();

                    Parallel.ForEach<UserQueryResult>(userQueryResults, UQR =>
                    {
                        if (UQR.Description == descriptionname)
                        {
                            UQBag.Add(UQR);
                        }
                    });
                    List<UserQueryResult> CurrentDescriptionList = UQBag.ToList();
                    groupingQueryResults.Add(new GroupingQueryResult(CurrentDescriptionList, groupNamesAll, descriptionname, "Description"));
                }
            }

            groupingQueryResults = groupingQueryResults.OrderBy(gqr => gqr.GroupingName).ToList<GroupingQueryResult>();

            string TFIDFType;
            if (relativeCountRadioButton.Checked)
            {
                TFIDFType = "Relative Count";
            }
            else
            {
                TFIDFType = "Raw Count";
            }
            string Message = "Generating TF-IDF Vectors from " + TFIDFType + " of AD Groups";
            statusLabelChanger(Message);
            foreach (GroupingQueryResult GQR in groupingQueryResults)
            {
                GQR.WriteTF_IDF(HelperFunctions.CalculateTFIDFVector(GQR, groupingQueryResults, ByRawCount: rawCountRadioButton.Checked));
            }
            gQRReport = new GroupingQueryReport(groupingQueryResults, Ordering.Ascending);
            groupingDataGridView.DataSource = GroupingQueryResults;
            statusLabelChanger("Idle");
        }

        public void statusLabelChanger(string Message)
        {
            statusLabel.Text = statusLabel.Text.Split(':')[0] + ": " + Message;
            Application.DoEvents();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            exportPath = textBox2.Text;
        }

        private void exportGroupingsAsFilesButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(exportPath))
            {
                exportPath = Directory.GetCurrentDirectory();
                textBox2.Text = exportPath;
                Application.DoEvents();
            }
            if (singleFileGroupingsExportRadioButton.Checked)
            {
                string SingleFileString = "";
                string FileNameString;
                statusLabelChanger("Creating Single Output File...");
                int i = 0;
                foreach (GroupingQueryResult GQR in groupingQueryResults)
                {
                    if (i == 0)
                    {
                        SingleFileString = SingleFileString + FileHelperFuctions.ReturnFormattedGroupInfo(userQueryResults, GQR, groupNamesAndDescriptionsAll);

                    }
                    else
                    {
                        string[] GQRStrings = FileHelperFuctions.ReturnFormattedGroupInfo(userQueryResults, GQR, groupNamesAndDescriptionsAll).Split('\n');
                        for (int i2 = 1; i2 < GQRStrings.Length; i2++)
                        {
                            SingleFileString = SingleFileString + GQRStrings[i2] + "\n";
                        }
                    }
                    SingleFileString = SingleFileString + "*************************************************************************************************\r\n\r\n";
                    i++;
                }
                if (descriptionRadioButton.Checked)
                {
                    FileNameString = "All Descriptions.txt";
                }
                else
                {
                    FileNameString = "All Titles.txt";
                }

                string PathAndFile = exportPath + "\\" + FileNameString;
                string Message = $"Exporting Single File {PathAndFile}";
                statusLabelChanger(Message);
                TextWriter Text = new StreamWriter(PathAndFile);
                Text.Write(SingleFileString);
                Text.Dispose();
            }
            else
            {
                foreach (GroupingQueryResult GQR in groupingQueryResults)
                {
                    string FileName = FileHelperFuctions.ReturnAcceptableFileName(GQR.GroupingName) + ".txt";
                    string FileString = FileHelperFuctions.ReturnFormattedGroupInfo(userQueryResults, GQR, groupNamesAndDescriptionsAll);
                    string PathAndFile = exportPath + "\\" + FileName;
                    string Message = $"Exporting Single File {PathAndFile}";
                    statusLabelChanger(Message);
                    TextWriter Text = new StreamWriter(PathAndFile);
                    Text.Write(FileString);
                    Text.Dispose();
                }
            }
            statusLabelChanger("Idle");
        }

        private void exportGroupingAsCSVButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(exportPath))
            {
                exportPath = Directory.GetCurrentDirectory();
                textBox2.Text = exportPath;
            }
            string PathAndFile;
            string Filename;
            if (descriptionRadioButton.Checked)
            {
                Filename = "Descriptions.csv";
            }
            else
            {
                Filename = "Titles.csv";
            }
            PathAndFile = exportPath + "\\" + Filename;
            string Message = $"Exporting {PathAndFile}";
            statusLabelChanger(Message);
            string CSVContent = FileHelperFuctions.ReturnCSVString(groupingDataGridView);
            File.WriteAllText(PathAndFile, CSVContent);
            statusLabelChanger("Idle");
        }

        private void exportDataAsCSVButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(exportPath))
            {
                exportPath = Directory.GetCurrentDirectory();
                textBox2.Text = exportPath;
            }
            string PathAndFile;
            string Filename = "Users.csv";

            PathAndFile = exportPath + "\\" + Filename;
            string Message = $"Exporting {PathAndFile}";
            statusLabelChanger(Message);
            string CSVContent = FileHelperFuctions.ReturnCSVString(dataGridView1);
            File.WriteAllText(PathAndFile, CSVContent);
            statusLabelChanger("Idle");
        }

        private void RBAC_Load(object sender, EventArgs e)
        {

        }

        private void titleRadioButtion_CheckedChanged(object sender, EventArgs e)
        {
            if (titleRadioButtion.Checked)
            {
                byTitle = true;
            }
            else
            {
                byTitle = false;
            }
        }

        private void clusterByRelativeCountRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (clusterByRelativeCountRadioButton.Checked)
            {
                byRelativeCountCluster = true;
            }
            else
            {
                byRelativeCountCluster = false;
            }
        }

        private void groupingDataGridView_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowNum = e.RowIndex;
            if (RowNum >= 0) {
                var KeyResult = from GQR in groupingQueryResults where GQR.GroupingName == (string)groupingDataGridView.Rows[RowNum].Cells[0].FormattedValue select GQR;
                GroupingQueryResult Result = KeyResult.ToArray<GroupingQueryResult>()[0];
                //is writing my own paralell loop faster than using linq?
                //to test
                QueryDetailsForm QDF = new QueryDetailsForm(Result, this);
                QDF.Show();
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowNum = e.RowIndex;
            if (RowNum >= 0)
            {
                var KeyResult = from UQR in userQueryResults where UQR.AccountName == dataGridView1.Rows[RowNum].Cells[3].Value.ToString() select UQR;
                UserQueryResult Result = KeyResult.ToList()[0];
                QueryDetailsForm QDF = new QueryDetailsForm(Result, this);
                QDF.Show();
            }
        }

        private void clusterUsersButton_Click(object sender, EventArgs e)
        {
            List<QueryResult> QRList = userQueryResults.ConvertAll(x => (QueryResult)x);
            ClusteringOutput UsersCluster = new ClusteringOutput(QRList, this);
            UsersCluster.Show();
        }

        private void clusterGroupingsButton_Click(object sender, EventArgs e)
        {
            List<QueryResult> QRList = groupingQueryResults.ConvertAll(x => (QueryResult)x);
            ClusteringOutput GroupingsCluster = new ClusteringOutput(QRList, this);
            GroupingsCluster.Show();
        }

        private void centroidRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (centroidRadioButton.Checked)
            {
                preferredDistanceStyle = HACDistanceStyle.Centroid;
            }
        }

        private void wardRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (wardRadioButton.Checked)
            {
                preferredDistanceStyle = HACDistanceStyle.Ward;
            }
        }

        private void averageDistanceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (averageDistanceRadioButton.Checked)
            {
                preferredDistanceStyle = HACDistanceStyle.MeanDist;
            }
        }

        private void completeLinkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (completeLinkRadioButton.Checked)
            {
                preferredDistanceStyle = HACDistanceStyle.CLink;
            }
        }

        private void singleLinkRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (singleLinkRadioButton.Checked)
            {
                preferredDistanceStyle = HACDistanceStyle.SLink;
            }
        }

        private void firstDistanceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (firstDistanceRadioButton.Checked)
            {
                preferredStoppingStyle = HACStoppingStyle.ProportionOfFirstDistance;
                bool NoIssues = true;
                try
                {
                    hACStoppingMetric = Convert.ToDouble(p1DValueTextBox.Text);
                }
                catch
                {
                    hACStoppingMetric = 1.5;
                    p1DValueTextBox.Text = "1.5";
                    NoIssues = false;
                }
                if (NoIssues)
                {
                    hACStoppingMetric = Convert.ToDouble(p1DValueTextBox.Text);
                }
            }
        }

        private void proportionLastDistanceRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (proportionLastDistanceRadioButton.Checked)
            {
                preferredStoppingStyle = HACStoppingStyle.ProportionOfLastDistance;
                bool NoIssues = true;
                try
                {
                    hACStoppingMetric = Convert.ToDouble(pLDValueTextBox.Text);
                }
                catch
                {
                    hACStoppingMetric = 1.5;
                    pLDValueTextBox.Text = "1.5";
                    NoIssues = false;
                }
                if (NoIssues)
                {
                    hACStoppingMetric = Convert.ToDouble(pLDValueTextBox.Text);
                }
            }
        }

        private void iterationsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (iterationsRadioButton.Checked)
            {
                preferredStoppingStyle = HACStoppingStyle.Iterations;
                bool NoIssues = true;
                try
                {
                    hACStoppingMetric = Convert.ToDouble(iterationsTextBox.Text);
                }
                catch
                {
                    hACStoppingMetric = 100;
                    iterationsTextBox.Text = "100";
                    NoIssues = false;
                }
                if (NoIssues)
                {
                    hACStoppingMetric = Math.Round(Convert.ToDouble(iterationsTextBox.Text));
                    iterationsTextBox.Text = $"{hACStoppingMetric}";
                }
            }
        }

        private void noneButton_CheckedChanged(object sender, EventArgs e)
        {
            preferredStoppingStyle = HACStoppingStyle.None;
        }

        private void RBAC_TextChanged(object sender, EventArgs e)
        {

        }

        private double textBoxValidate(TextBox TB, double DefaultVal)
        {
            bool NoIssues = true;
            try
            {
                Convert.ToDouble(TB.Text);
            }
            catch
            {
                NoIssues = false;
            }
            if (NoIssues)
            {
                return Convert.ToDouble(TB.Text);
            }
            else
            {
                return DefaultVal;
            }
        }

        private void p1DValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (firstDistanceRadioButton.Checked)
            {
                hACStoppingMetric = textBoxValidate(p1DValueTextBox, 1.5);
            }
        }

        private void pLDValueTextBox_TextChanged(object sender, EventArgs e)
        {
            if (proportionLastDistanceRadioButton.Checked)
            {
                hACStoppingMetric = textBoxValidate(pLDValueTextBox, 1.5);
            }
        }

        private void iterationsTextBox_TextChanged(object sender, EventArgs e)
        {
            if (iterationsRadioButton.Checked)
            {
                hACStoppingMetric = textBoxValidate(iterationsTextBox, 100);
            }
        }

        private void hACRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (hACRadioButton.Checked)
            {
                clusteringAlgoType = Type.GetType("RBACS.HACAlgo");
            }
            else
            {
                clusteringAlgoType = Type.GetType("RBACS.KMeansPlusPlus");
            }
        }

        private void meanDistancesToCentroidsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (meanDistancesToCentroidsRadioButton.Checked)
            {
                preferredKMeansStoppingStyle = KMeansStoppingStyle.NoChangeInCentroids;
            }
            else
            {
                preferredKMeansStoppingStyle = KMeansStoppingStyle.Iterations;
            }
        }

        private void mapGroupingsButton_Click(object sender, EventArgs e)
        {
            List<QueryResult> QRList = GroupingQueryResults.ConvertAll(x => (QueryResult)x);
            KohonenForm KF = new KohonenForm(this, mapGroupingsDimensions, QRList, mapEpochs);
            KF.Show();
        }

        private int testInput(TextBox TB, int Default)
        {
            //return either conversion or default
            bool NoIssues = true;
            try
            {
                Convert.ToInt32(TB.Text);
            }
            catch
            {
                NoIssues = false;
            }
            if (NoIssues)
            {
                return Convert.ToInt32(TB.Text);
            }
            else
            {
                return Default;
            }
        }
        private void mapUsersTextBox_TextChanged(object sender, EventArgs e)
        {
            mapUsersDimensions = testInput(mapUsersTextBox, mapUsersDimensions);
        }

        private void mapGroupingSizeTextBox_TextChanged(object sender, EventArgs e)
        {
            mapGroupingsDimensions = testInput(mapGroupingSizeTextBox, mapGroupingsDimensions);
        }

        private void mapUsersButton_Click(object sender, EventArgs e)
        {
            List<QueryResult> QRList = UserQueryResults.ConvertAll(x => (QueryResult)x);
            KohonenForm KF = new KohonenForm(this, mapUsersDimensions, QRList, mapEpochs);
            KF.Show();
        }

        private void epochsTextBox_TextChanged(object sender, EventArgs e)
        {
            mapEpochs = testInput(epochsTextBox, mapEpochs);
        }

        private void groupingDataGridView_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string Property = groupingDataGridView.Columns[e.ColumnIndex].HeaderText;
            if (allGroupsDisplayed)
            {
                gQRReport.SortByProperty(Property);
                groupingDataGridView.DataSource = GroupingQueryResults;
            }
            else
            {
                if (subSetGQReport.QRList.Count > 0)
                {
                    subSetGQReport.SortByProperty(Property);
                    gQRReport.SortByProperty(Property);
                    groupingDataGridView.DataSource = subSetGQReport.QRList;
                }
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string Property = dataGridView1.Columns[e.ColumnIndex].HeaderText;
            if (allUsersDisplayed)
            {
                uQRReport.SortByProperty(Property);
                dataGridView1.DataSource = UserQueryResults;
            }
            else
            {
                if (subSetUQReport.QRList.Count > 0)
                {
                    subSetUQReport.SortByProperty(Property);
                    uQRReport.SortByProperty(Property);
                    dataGridView1.DataSource = subSetUQReport.QRList;
                }
            }
            //uQRReport.SortByProperty(Property);

            // dataGridView1.DataSource = UserQueryResults;
        }

        private void andRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (andRadioButton.Checked)
            {
                inclusionsIsLogicalAnd = true;
            }
            else
            {
                inclusionsIsLogicalAnd = false;
            }
        }

        private void dNRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (dNRadioButton.Checked)
            {
                exclusionsByWholeName = true;
            }
            else
            {
                exclusionsByWholeName = false;
            }
        }

        private void findTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ConfigTab.SelectedTab == ConfigTab.TabPages["OutputTab"])
            {
                if (findTextBox.Text == null || findTextBox.Text == "")
                {
                    allUsersDisplayed = true;
                    RBACBindingSource.DataSource = uQRReport.QRList;
                    dataGridView1.DataSource = RBACBindingSource;
                }
                else
                {
                    allUsersDisplayed = false;
                    subSetUQReport = uQRReport.Search(findTextBox.Text);
                    RBACBindingSource.DataSource = subSetUQReport.QRList;
                    dataGridView1.DataSource = RBACBindingSource;
                }
            }
            else if (ConfigTab.SelectedTab == ConfigTab.TabPages["Grouping"])
            {
                if (findTextBox.Text == null || findTextBox.Text == "")
                {
                    allGroupsDisplayed = true;
                    groupingDataGridView.DataSource = gQRReport.QRList;
                }
                else
                {
                    allGroupsDisplayed = false;
                    subSetGQReport = gQRReport.Search(findTextBox.Text);
                    groupingDataGridView.DataSource = subSetGQReport.QRList;
                }
            }
            else
            {
                //do nothing
            }
        }

        private void csvImportButton_Click(object sender, EventArgs e)
        {
            if (PreferredImportFilepath.Contains(".csv"))
            {
                string PathToUse;
                if (PreferredImportFilepath.Contains("\\"))
                {
                    PathToUse = PreferredImportFilepath;
                }
                else
                {
                    PathToUse = $"{PreferredExportFilepath}\\{PreferredImportFilepath}";
                }
                bool Successful = true;
                try
                {
                    new StreamReader(PathToUse);
                }
                catch
                {
                    statusLabelChanger($"Unable to import CSV at {PathToUse}");
                    Successful = false;
                }
                if (Successful)
                {
                    List<Tuple<string, string>> PermissionsList = new List<Tuple<string, string>>();
                    StreamReader Reader = new StreamReader(PathToUse);
                    while (!Reader.EndOfStream)
                    {
                        var Line = Reader.ReadLine();
                        var vals = Line.Split(',');
                        if (vals.Count() < 2)
                        {
                            statusLabelChanger("Import CSV must contain a Principals column and a Permissions column.");
                        }
                        else
                        {
                            PermissionsList.Add(new Tuple<string, string>(vals[0], $"{AdditionalPermissionsType}:{vals[1]}"));
                        }
                    }
                    if (PermissionsList.Count > 0)
                    {
                        List<Tuple<int, string>> PrincipalList = new List<Tuple<int, string>>();
                        List<string> DistinctUserPermissions = new List<string>();
                        
                        foreach (Tuple<string,string> Permission in PermissionsList)
                        {
                            //designed to discard permissions not belonging to a principal or group in the dataset
                            if (groupNamesAll.Contains(Permission.Item1))
                            {
                                statusLabelChanger($"Adding {Permission.Item2} to members of {Permission.Item1}");
                                if (groupNamesAll.Contains(Permission.Item2))
                                {
                                    userQueryResults = DatasetManagement.UpdatePermissionsByCondition(groupNamesAll.IndexOf(Permission.Item2), groupNamesAll.IndexOf(Permission.Item1), userQueryResults);
                                }
                                else
                                {
                                    groupNamesAll = DatasetManagement.AddPermissionToList(Permission.Item2, groupNamesAll);
                                    groupNamesAndDescriptionsAll.Add(new Tuple<string, string>(Permission.Item2, ""));

                                    //membership of new permission is conditional on membership of group listed as principal ie. item 1
                                    userQueryResults = DatasetManagement.AddPermissionToAllConditionalOnIndex(userQueryResults, groupNamesAll, Permission.Item2, new List<int>(), groupNamesAll.IndexOf(Permission.Item1));
                                }
                            }
                            else
                            {
                                int IndexOfPrincipal = -1;
                                Parallel.For(0, userQueryResults.Count, i => {
                                    if (userQueryResults[i].AccountName == Permission.Item1)
                                    {
                                        IndexOfPrincipal = i;
                                        //there will never be two occurrences of the same account name, so this is safe
                                    }
                                });
                                if (IndexOfPrincipal>=0)
                                {
                                    PrincipalList.Add(new Tuple<int, string>(IndexOfPrincipal, Permission.Item2));
                                    DistinctUserPermissions.Add(Permission.Item2);
                                    statusLabelChanger($"Adding {Permission.Item2} for {Permission.Item1}");
                                    
                                    
                                }
                            }
                            
                        }
                        if (DistinctUserPermissions.Count > 0)
                        {
                            DistinctUserPermissions = DistinctUserPermissions.Distinct().ToList();
                            foreach (string UserPermission in DistinctUserPermissions)
                            {
                                List<int> Indices = (from PrincipalListed in PrincipalList where PrincipalListed.Item2 == UserPermission select PrincipalListed.Item1).ToList();
                                if (!groupNamesAll.Contains(UserPermission))
                                {
                                    groupNamesAll = DatasetManagement.AddPermissionToList(UserPermission, groupNamesAll);
                                    groupNamesAndDescriptionsAll.Add(new Tuple<string, string>(UserPermission, ""));
                                    userQueryResults = DatasetManagement.AddPermissionToAllConditionalOnIndex(userQueryResults, groupNamesAll, UserPermission, Indices);
                                }
                                else
                                {
                                    Parallel.ForEach(Indices, Index => {
                                        DenseVector CurrentVector = (DenseVector)userQueryResults[Index].ReturnAccessVector();
                                        CurrentVector[groupNamesAll.IndexOf(UserPermission)] = 1;
                                        UserQueryResult ReferenceUQR = userQueryResults[Index];
                                        userQueryResults[Index] = new UserQueryResult(ReferenceUQR.Title, ReferenceUQR.Description, ReferenceUQR.Name, ReferenceUQR.AccountName, ReferenceUQR.DistinguishedName, CurrentVector);
                                    });
                                }
                            }
                            //userQueryResults = DatasetManagement.AddPermissionToAllConditionalOnIndex(userQueryResults, groupNamesAll, Permission.Item2, PrincipalList);
                        }

                        statusLabelChanger("Idle.");

                        groupNamesAndDescriptionsAll = groupNamesAndDescriptionsAll.Distinct().OrderBy(o => o.Item1).ToList();
                        GroupsListBox.Items.Clear();
                        GroupsListBox.Items.AddRange((object[])groupNamesAll.ToArray());
                        GroupNumberLabel.Text = GroupNumberLabel.Text.Split(':')[0] + ":" + groupNamesAll.Count.ToString();

                        uQRReport = new UserQueryReport(userQueryResults, Ordering.Ascending);
                        RBACBindingSource.DataSource = uQRReport.QRList;
                        dataGridView1.DataSource = RBACBindingSource;
                    }
                }
            }
            else
            {
                statusLabelChanger("Import File must be a CSV (Comma Separated)");
            }
        }

        private void xMLExportButton_Click(object sender, EventArgs e)
        {
            //Check Filename
            bool ValidFilename = true;
            //Check Export Directory, change to working Directory if invalid
            if (!Directory.Exists(exportPath))
            {
                exportPath = Directory.GetCurrentDirectory();
                textBox2.Text = exportPath;
            }
            //check user has enteredfilename
            string ExportFilename = "";
            if (xMLFileNameTextBox.Text == null||xMLFileNameTextBox.Text=="") {
                statusLabelChanger("You must enter a filename for XML Exports");
                ValidFilename = false;
            }
            else
            {
                if (xMLFileNameTextBox.Text.Contains("\\"))
                {
                    var WithoutFile = xMLFileNameTextBox.Text.Substring(0, xMLFileNameTextBox.Text.LastIndexOf('\\'));
                    if (!Directory.Exists(xMLFileNameTextBox.Text.Substring(0, xMLFileNameTextBox.Text.LastIndexOf('\\')))){

                        statusLabelChanger($"Could not use the path {WithoutFile}");
                        string[] Strings = xMLFileNameTextBox.Text.Split('\\');
                       
                        ExportFilename = $"{exportPath}\\{Strings[Strings.Length-1]}";
                    }
                    else
                    {
                        ExportFilename = $"{xMLFileNameTextBox.Text}";
                    }
                }
                else
                {
                    ExportFilename = $"{exportPath}\\{xMLFileNameTextBox.Text}";
                }
                //check has .xml extension
                if (!(ExportFilename.Substring(ExportFilename.Length - 4) == ".xml"))
                {
                    ExportFilename += ".xml";
                }
                
            }
            

            if (ValidFilename)
            {
                //build output XML Document
                statusLabelChanger($"Creating and exporting {ExportFilename}");

                XDocument OutXML = new XDocument(
                    new XElement("Dataset",
                        new XElement(
                            "Groups",
                            from GroupScrip in groupNamesAndDescriptionsAll
                            select new XElement("Group",
                                new XElement("Name", FileHelperFuctions.RemoveXMLIllegalCharacters(GroupScrip.Item1)),
                                new XElement("Description", FileHelperFuctions.RemoveXMLIllegalCharacters(GroupScrip.Item2)))
                        ),
                        new XElement(
                            "Users",
                            from UQR in uQRReport.QRList
                            select new XElement("User",
                                new XElement("Name", FileHelperFuctions.RemoveXMLIllegalCharacters(UQR.Name)),
                                new XElement("Account_Name", FileHelperFuctions.RemoveXMLIllegalCharacters(UQR.AccountName)),
                                new XElement("Title", FileHelperFuctions.RemoveXMLIllegalCharacters(UQR.Title)),
                                new XElement("Description", FileHelperFuctions.RemoveXMLIllegalCharacters(UQR.Description)),
                                new XElement("DistinguishedName", FileHelperFuctions.RemoveXMLIllegalCharacters(UQR.DistinguishedName)),
                                new XElement("Access_Vector", FileHelperFuctions.RemoveXMLIllegalCharacters(DatasetManagement.VectorToString(UQR.ReturnAccessVector())))
                            )
                        )
                    )
                );
                OutXML.Save(ExportFilename);
                statusLabelChanger("Idle.");
            }
            
        }

        private void xMLImportButton_Click(object sender, EventArgs e)
        {
            bool IsValidImportPath = true;

            //Filepath Validation code

            if (PreferredImportFilepath == null || PreferredImportFilepath == "")
            {
                statusLabelChanger("Please Enter Valid Filepath for XML Dataset Import");
                IsValidImportPath = false;
            }
            else
            {
                if (PreferredImportFilepath.Contains("\\"))
                {
                    if (!Directory.Exists(PreferredImportFilepath.Substring(0, PreferredImportFilepath.LastIndexOf('\\'))))
                    {
                        string[] AllSegments = PreferredImportFilepath.Split('\\');
                        importFileTextBox.Text = Directory.GetCurrentDirectory() + "\\" + AllSegments[AllSegments.Length - 1];
                    }
                }
                else
                {
                    importFileTextBox.Text = Directory.GetCurrentDirectory() + "\\" + PreferredImportFilepath;
                }
                if (PreferredImportFilepath.Substring(PreferredImportFilepath.Length - 4) != ".xml")
                {
                    importFileTextBox.Text += ".xml";
                }
                try { XElement.Load(PreferredImportFilepath); } catch
                {
                    statusLabelChanger("Not A Valid XML");
                    IsValidImportPath = false;
                }
            }
            if (IsValidImportPath)
            {
                //validate xml document is correctly structured
                bool IsCorrectlyStructured = true;
                XmlDocument Doc = new XmlDocument();
                Doc.Load(PreferredImportFilepath);
                try { Doc.DocumentElement.SelectNodes("Groups/Group")[0].SelectSingleNode("Name"); } catch { IsCorrectlyStructured = false; }
                try { Doc.DocumentElement.SelectNodes("Users/User")[0].SelectSingleNode("Account_Name"); } catch { IsCorrectlyStructured = false; }
                if (IsCorrectlyStructured)
                {
                    XmlNodeList Groups = Doc.DocumentElement.SelectNodes("Groups/Group");
                    XmlNodeList Users = Doc.DocumentElement.SelectNodes("Users/User");

                    statusLabelChanger("Replacing Existing Data...");

                    groupNamesAll = new List<string>();
                    groupNamesAndDescriptionsAll = new List<Tuple<string, string>>();
                    uQRReport = null;
                    gQRReport = null;
                    userQueryResults = new List<UserQueryResult>();
                    groupingQueryResults = null;
                    titlesAll = new List<string>();
                    descriptionsAll = new List<string>();

                    foreach (XmlNode Group in Groups)
                    {
                        statusLabelChanger($"Importing AD Group {Group.SelectSingleNode("Name").InnerText}");
                        groupNamesAll.Add(Group.SelectSingleNode("Name").InnerText);
                        groupNamesAndDescriptionsAll.Add(
                            new Tuple<string, string>(
                                Group.SelectSingleNode("Name").InnerText,
                                Group.SelectSingleNode("Description").InnerText
                            )
                        );
                    }
                    foreach (XmlNode User in Users)
                    {
                        //add further validation here?
                        statusLabelChanger($"Importing User {User.SelectSingleNode("Account_Name").InnerText}");
                        UserQueryResult UQR = new UserQueryResult(
                            User.SelectSingleNode("Title").InnerText,
                            User.SelectSingleNode("Description").InnerText,
                            User.SelectSingleNode("Name").InnerText,
                            User.SelectSingleNode("Account_Name").InnerText,
                            User.SelectSingleNode("DistinguishedName").InnerText,
                            DatasetManagement.StringToDenseVector(
                                User.SelectSingleNode("Access_Vector").InnerText
                            )
                        );
                        titlesAll.Add(UQR.Title);
                        descriptionsAll.Add(UQR.Description);
                        userQueryResults.Add(UQR);
                    }

                    titlesAll = titlesAll.Distinct().ToList();
                    titlesAll.Sort();
                    descriptionsAll = descriptionsAll.Distinct().ToList();
                    descriptionsAll.Sort();

                    GroupNumberLabel.Text = GroupNumberLabel.Text.Split(':')[0] + $":{groupNamesAndDescriptionsAll.Count}";
                    userNumberLabel.Text = userNumberLabel.Text.Split(':')[0] + $":{userQueryResults.Count}";
                    descriptionsNumberLabel.Text = descriptionsNumberLabel.Text.Split(':')[0] + $":{descriptionsAll.Count}";
                    titlesNumberLabel.Text = titlesNumberLabel.Text.Split(':')[0] + $":{titlesAll.Count}";

                    uQRReport = new UserQueryReport(userQueryResults, Ordering.Ascending);
                    userQueryResultBindingSource.DataSource = uQRReport.QRList;
                    dataGridView1.DataSource = userQueryResultBindingSource;
                    statusLabelChanger("Idle.");
                }
                else statusLabelChanger("Not a correctly structured XML");
            }
           
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            this.richTextBox2_KeyDown(richTextBox2, new KeyEventArgs(Keys.Enter));
        }
    }


}