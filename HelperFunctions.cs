using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;


namespace RBACS
{
    public class HelperFunctions
    {
        public static List<GroupRepresentationResult> QueryListToGroupRepresentationList(List<QueryResult> QRList, List<Tuple<string,string>> GroupNames)
        {
            ConcurrentBag<GroupRepresentationResult> GRBag = new ConcurrentBag<GroupRepresentationResult>();
           
            DenseVector SummedVector = new DenseVector(QRList[0].ReturnAccessVector().Count);
            Parallel.ForEach(QRList, QR => {
                SummedVector = SummedVector + (DenseVector)QR.ReturnAccessVector();
            });

            Parallel.For(0, GroupNames.Count, i =>
            {
                GroupRepresentationResult CurrentGR = new GroupRepresentationResult(GroupNames[i].Item1, GroupNames[i].Item2,(double)Decimal.Divide((decimal)SummedVector[i], QRList.Count));
                GRBag.Add(CurrentGR);
            });
            return (from GR in GRBag
                    where Convert.ToDouble(GR.Percent.Substring(0,GR.Percent.Length-1)) > 0
                    select GR).Distinct().OrderBy(o => o.Name).ToList();         
        }
        public static List<GroupRepresentationTFIDFResult> QueryListToGroupRepresentationTFIDFList(GroupingQueryResult QR, List<GroupingQueryResult> AllQueries, List<Tuple<string,string>> GroupNames)
        {
            ConcurrentBag<GroupRepresentationTFIDFResult> GRTBag = new ConcurrentBag<GroupRepresentationTFIDFResult>();
            DenseVector TFIDFVector = (DenseVector)CalculateTFIDFVector(QR, AllQueries);
             
            /*DenseVector SummedVector = new DenseVector(QRList[0].ReturnAccessVector().Count);
            //DenseVector AllQRSummedVector = new DenseVector(QRList[0].ReturnAccessVector().Count);
            Parallel.ForEach(QRList, QR => {
                SummedVector = SummedVector + (DenseVector)QR.ReturnAccessVector();
            });
            DenseVector IDFVector = new DenseVector(GroupNames.Count);
            Parallel.ForEach(AllQueries, QR => {
                IDFVector = IDFVector + (DenseVector)QR.ReturnAccessVector();
            });
            Parallel.For(0, IDFVector.Count, i => {
                IDFVector[i] = Math.Log((double)Decimal.Divide(AllQueries.Count, (decimal)IDFVector[i]));
            });
            SummedVector = (DenseVector)SummedVector.Divide((double)QRList.Count);
            DenseVector TFIDFVector = (DenseVector)SummedVector.PointwiseMultiply(IDFVector);*/
            Parallel.For(0, GroupNames.Count, i => {
                GroupRepresentationTFIDFResult CurrentGRT = new GroupRepresentationTFIDFResult(GroupNames[i].Item1, GroupNames[i].Item2, QR.ReturnAccessVector()[i], TFIDFVector[i]);
                GRTBag.Add(CurrentGRT);
            });
            return (from GR in GRTBag
                    where Convert.ToDouble(GR.Percent.Substring(0, GR.Percent.Length - 1)) > 0
                    select GR).Distinct().OrderBy(o => o.Name).ToList();
        }
        public static List<GroupResult> QueryResultToGroupResults(QueryResult UQR, List<Tuple<string, string>> GroupNames) {
            ConcurrentBag<GroupResult> ReturnBag = new ConcurrentBag<GroupResult>();
            Parallel.For(0, GroupNames.Count, i => {
                ReturnBag.Add(new GroupResult(GroupNames[i].Item1, GroupNames[i].Item2));
            });
            return ReturnBag.OrderBy(o => o.Name).ToList();
        }
        public static Vector StringListsToVector(List<string> RefererallList, List<string> TargetList)
        {
            //untested
            Vector OutputVector = new DenseVector(RefererallList.Count);
            for (int i = 0; i < RefererallList.Count; i++)
            {
                if (TargetList.Contains(RefererallList[i]))
                {
                    OutputVector[i] = 1;
                }
            }
            return OutputVector;
        }

        public static List<QueryResult> DatagridViewToQueryResultList(DataGridView DGV, int KeyValueColumnIndex, List<QueryResult> ReferencedList){
            List<QueryResult> OutputList = new List<QueryResult>();
            if (DGV.SelectedRows.Count > 0)
            {
                if (ReferencedList[0].GetType() == typeof(UserQueryResult))
                {
                    foreach (DataGridViewRow ThisRow in DGV.SelectedRows)
                    {
                        UserQueryResult UQR = null;
                        string CompareString = ThisRow.Cells[KeyValueColumnIndex].Value.ToString();
                        Parallel.ForEach(ReferencedList, QR => {
                            UserQueryResult CurrentUQR = (UserQueryResult)QR;
                            
                            if (CompareString == CurrentUQR.AccountName)
                            {
                                UQR = CurrentUQR;
                            }
                        });
                        if (UQR != null)
                        {
                            OutputList.Add(UQR);
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow ThisRow in DGV.SelectedRows)
                    {
                        GroupingQueryResult GQR = null;
                        string CompareString = ThisRow.Cells[KeyValueColumnIndex].Value.ToString();
                        Parallel.ForEach(ReferencedList, QR => {
                            GroupingQueryResult CurrentGQR = (GroupingQueryResult)QR;
                            
                            if (CompareString == CurrentGQR.GroupingName)
                            {
                                GQR = CurrentGQR;
                            }
                        });
                        if (GQR != null)
                        {
                            OutputList.Add(GQR);
                        }
                    }
                }
                return OutputList;
            }
            else
            {
                if (ReferencedList[0].GetType() == typeof(UserQueryResult))
                {
                    foreach(DataGridViewRow ThisRow in DGV.Rows)
                    {
                        string CompareString = ThisRow.Cells[KeyValueColumnIndex].Value.ToString();
                        UserQueryResult UQR=null;
                        Parallel.ForEach(ReferencedList, QR => {
                            UserQueryResult CurrentUQR = (UserQueryResult)QR;
                            if (CompareString == CurrentUQR.AccountName)
                            {
                                UQR = CurrentUQR;
                            }
                        });
                        if (UQR != null)
                        {
                            OutputList.Add(UQR);
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow ThisRow in DGV.Rows)
                    {
                        string CompareString = ThisRow.Cells[KeyValueColumnIndex].Value.ToString();
                        GroupingQueryResult GQR = null;
                        Parallel.ForEach(ReferencedList, QR => {
                            GroupingQueryResult CurrentGQR = (GroupingQueryResult)QR;
                            if (CompareString == CurrentGQR.GroupingName)
                            {
                                GQR = CurrentGQR;
                            }
                        });
                        if (GQR != null)
                        {
                            OutputList.Add(GQR);
                        }
                    }
                }
                return OutputList;
            }
        }

        public static List<QueryResult> DatagridViewSubsetToQueryResultList(DataGridView DGV, int KeyValueColumnIndex, int SubsetColumnIndex, string SubsetValue, List<QueryResult> ReferencedList)
        {
            //takes no notice of selected rows
            List<QueryResult> OutputList = new List<QueryResult>();

            List<DataGridViewRow> RowCollection = (from DataGridViewRow DGVR in DGV.Rows where DGVR.Cells[SubsetColumnIndex].Value.ToString() == SubsetValue select DGVR).ToList();

            if (ReferencedList[0].GetType() == typeof(UserQueryResult))
            {
                foreach (DataGridViewRow ThisRow in RowCollection)
                {
                    string CompareString = ThisRow.Cells[KeyValueColumnIndex].Value.ToString();
                    UserQueryResult UQR = null;
                    Parallel.ForEach(ReferencedList, QR => {
                        UserQueryResult CurrentUQR = (UserQueryResult)QR;
                        if (CompareString == CurrentUQR.AccountName)
                        {
                            UQR = CurrentUQR;
                        }
                    });
                    if (UQR != null)
                    {
                        OutputList.Add(UQR);
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow ThisRow in RowCollection)
                {
                    string CompareString = ThisRow.Cells[KeyValueColumnIndex].Value.ToString();
                    GroupingQueryResult GQR = null;
                    Parallel.ForEach(ReferencedList, QR => {
                        GroupingQueryResult CurrentGQR = (GroupingQueryResult)QR;
                        if (CompareString == CurrentGQR.GroupingName)
                        {
                            GQR = CurrentGQR;
                        }
                    });
                    if (GQR != null)
                    {
                        OutputList.Add(GQR);
                    }
                }
            }
            return OutputList;
          
        }

        public static string StringListToCommaSeparatedString(List<string> InputList)
        {
            string OutString = "";
            for (int i = 0; i<InputList.Count; i++)
            {
                if (i < InputList.Count - 1)
                {
                    OutString = OutString + InputList[i] + ", ";
                }
                else
                {
                    OutString = OutString + InputList[i];
                }
            }
            return OutString;
        }

        public static double GetEuclideanDistance(Vector VectorA, Vector VectorB)
        {
            //untested
            Vector SubtractionVector = (Vector)(VectorA - VectorB);
            double ReturnValue = Math.Sqrt((SubtractionVector.PointwisePower(2)).Sum());
            return ReturnValue;
        }
        public static double GetEuclideanDistanceSquared(Vector VectorA, Vector VectorB)
        {
            return (VectorA - VectorB).PointwisePower(2).Sum();
        }
        public static double GetManhattanDistance(Vector VectorA, Vector VectorB)
        {
            //untested
            Vector SubtractionVector = (Vector)(VectorA - VectorB);
            double ReturnValue = SubtractionVector.PointwiseAbs().Sum();
            return ReturnValue;
        }

        public static Vector CalculateTFIDFVector(GroupingQueryResult TargetGroup, List<GroupingQueryResult> Corpus, bool ByRawCount=false)
        {
            Vector IDFSummedVector = new DenseVector(TargetGroup.ReturnAccessVector().Count);
            Vector ReturnVector = new DenseVector(IDFSummedVector.Count);

            Parallel.For(0, IDFSummedVector.Count, i => {
                foreach(GroupingQueryResult CurrentGQR in Corpus)
                {
                    if(CurrentGQR.GroupingName != TargetGroup.GroupingName)
                    {
                        if (CurrentGQR.ReturnAccessVector()[i] > 0)
                        {
                            IDFSummedVector[i] = IDFSummedVector[i] + 1;
                        }
                    }
                }
            });

            for (int i2=0; i2<IDFSummedVector.Count; i2++)
            {
                //0.1 has been added to the denominator to prevent divide by zero issues
                IDFSummedVector[i2] = Math.Log(Corpus.Count/(IDFSummedVector[i2]+0.1));
            }
            if (ByRawCount)
            {
                return (Vector)IDFSummedVector.PointwiseMultiply(TargetGroup.ReturnRawCountVector());
            }
            else
            {
                return (Vector)IDFSummedVector.PointwiseMultiply(TargetGroup.ReturnAccessVector());
            }
        }

        public static Vector RemoveZeroValues(Vector Input)
        {
            Vector ReturnVector = new DenseVector(((Vector)Input.Enumerate(Zeros.AllowSkip)).Count);
            int i = 0;
            foreach(double Member in Input.Enumerate(Zeros.AllowSkip))
            {
                ReturnVector[i] = Member;
                i++;
            }
            return ReturnVector;
        }
    }

    public class FileHelperFuctions
    {
        public static string RecommendationString(Vector DescriptiveVector, List<Tuple<string,string>>AllGroupNamesAndDescriptions, string TitleString)
        {
            string ReturnString = TitleString + "\r\n\r\n";
            ReturnString = ReturnString + "----------------------------------------------------------\r\n\r\n";
            if(DescriptiveVector.Count != AllGroupNamesAndDescriptions.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            for (int i = 0; i < DescriptiveVector.Count; i++)
            {
                if (DescriptiveVector[i] != 0)
                {
                    string ExtraString = AllGroupNamesAndDescriptions[i].Item1;
                    while(ExtraString.Length < 25)
                    {
                        ExtraString = ExtraString + " ";
                    }
                    ExtraString = ExtraString + AllGroupNamesAndDescriptions[i].Item2;
                    while (ExtraString.Length < 90)
                    {
                        ExtraString = ExtraString + " ";
                    }
                    ExtraString = ExtraString + $"Metric = {DescriptiveVector[i].ToString()}\r\n";
                    ReturnString = ReturnString + ExtraString;
                }
            }
            return ReturnString;
        }

        public static string ReturnAcceptableFileName(string Name) => Name.Replace("/", " ").Replace(":", "").Replace("\\", " ").Replace("~", "").Replace(" % ", "").Replace(" & ", "").Replace("#", "").Replace("*", "").Replace("<", "").Replace(">", "").Replace("{", "").Replace("}", "").Replace("\"", "").Replace("?", "").Replace("+", "").Replace("|", "");

        public static string FirstSecondEtc(int Index)
        {
            string IntString = Index.ToString();
            if (IntString[IntString.Length - 1] == '1')
            {
                if (IntString.Length > 1)
                {
                    if (IntString[IntString.Length - 2] != '1')
                    {
                        IntString = IntString + "st";
                    }
                    else
                    {
                        IntString = IntString + "th";
                    }
                }
                else
                {
                    IntString = IntString + "st";
                }
            }
            else if (IntString[IntString.Length - 1] == '2')
            {
                if (IntString.Length > 1)
                {
                    if (IntString[IntString.Length - 2] != '1')
                    {
                        IntString = IntString + "nd";
                    }
                    else
                    {
                        IntString = IntString + "th";
                    }
                }
                else
                {
                    IntString = IntString + "nd";
                }
            }
            else if (IntString[IntString.Length - 1] == '3')
            {
                if (IntString.Length > 1)
                {
                    if (IntString[IntString.Length - 2] != '1')
                    {
                        IntString = IntString + "rd";
                    }
                    else
                    {
                        IntString = IntString + "th";
                    }
                }
                else
                {
                    IntString = IntString + "rd";
                }
            }
            else
            {
                IntString = IntString + "th";
            }
            return IntString;
        }

        public static string ReturnFormattedPersonInfo(UserQueryResult UQR, List<Tuple<string,string>> AllADGroupsList, bool TitleNotDescription=true)
        {
            if(AllADGroupsList.Count != UQR.ReturnAccessVector().Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            string Outstring = UQR.AccountName;
            while (Outstring.Length < 80)
            {
                Outstring = Outstring + " ";
            }
            if (TitleNotDescription)
            {
                Outstring = Outstring + UQR.Title;
            }
            else
            {
                Outstring = Outstring + UQR.Description;
            }
            Outstring = $"{Outstring}\r\n{UQR.DistinguishedName}\r\n------------------------------------------------------------------------------------------------------------------------\r\n\r\n";
            Vector AccVect = UQR.ReturnAccessVector();
            for (int i=0; i < AccVect.Count; i++)
            {
                if (AccVect[i] > 0)
                {
                    string TempString = AllADGroupsList[i].Item1;
                    while(TempString.Length < 80)
                    {
                        TempString = TempString + " ";
                    }
                    TempString = TempString + AllADGroupsList[i].Item2;
                    Outstring = Outstring + TempString + "\r\n";
                }
            }
            Outstring = Outstring + "\r\n";
            return Outstring;
        }

        public static string ReturnFormattedGroupInfo(List<UserQueryResult> UsersList, GroupingQueryResult GQR, List<Tuple<string,string>> AllADGroupsList)
        {
            string Outstring = DateTime.Now.ToLongDateString() + "\r\n" + GQR.GroupingType + " = " + GQR.GroupingName.ToUpper() + "\r\n" +$"{GQR.MemberCount} Members\r\n------------------------------------------------------------------------------------------------------------------------------\r\n\r\nGroup Representations:\r\n\r\n";
            Vector RefVect = GQR.ReturnAccessVector();
            Vector RefTFIDF = GQR.ReturnTF_IDFVector();
            if (RefVect.Count != AllADGroupsList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            //string Tempstring1 = "";
            for(int i = 0; i<RefVect.Count; i++)
            {
                if (RefVect[i] > 0)
                {
                    string Tempstring1 = AllADGroupsList[i].Item1;
                    while (Tempstring1.Length < 80)
                    {
                        Tempstring1 = Tempstring1 + " ";
                    }
                    Tempstring1 = Tempstring1 + (RefVect[i] * 100).ToString("F2") + "%";
                    while (Tempstring1.Length < 88)
                    {
                        Tempstring1 = Tempstring1 + " ";
                    }
                    Tempstring1 = Tempstring1 + $"(Weighted for rarity {RefTFIDF[i].ToString("F2")})\r\n";
                    Outstring = Outstring + Tempstring1;
                }
            }
            Outstring = Outstring + "\r\n\r\n";
            //List<string> Names = GQR.Members.Split(',').ToList<string>();
            foreach (string Name in GQR.Members.Split(','))
            {
                string TrueName;
                if (Name.StartsWith(" "))
                {
                    TrueName = Name.Substring(1);
                }
                else
                {
                    TrueName = Name;
                }
                Parallel.ForEach<UserQueryResult>(UsersList, UQR => {
                    if(UQR.AccountName == TrueName)
                    {
                        if(GQR.GroupingType == "Description")
                        {
                            Outstring = Outstring + ReturnFormattedPersonInfo(UQR, AllADGroupsList, true);
                        }
                        else
                        {
                            Outstring = Outstring + ReturnFormattedPersonInfo(UQR, AllADGroupsList, false);
                        }
                    }
                });
            }
            return Outstring;
        }

        public static string ReturnFormattedGroupSummary(GroupingQueryResult GQR, List<Tuple<string,string>> AllADGroupsList )
        {
            string Outstring = DateTime.Now.ToLongDateString() + "\r\n" + GQR.GroupingType + " = " + GQR.GroupingName.ToUpper() + "\r\n" + $"{GQR.MemberCount} Members\r\n------------------------------------------------------------------------------------------------------------------------------\r\n\r\nGroup Representations:\r\n\r\n";
            Vector RefVect = GQR.ReturnAccessVector();
            Vector RefTFIDF = GQR.ReturnTF_IDFVector();
            if (RefVect.Count != AllADGroupsList.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            //string Tempstring1 = "";
            for (int i = 0; i < RefVect.Count; i++)
            {
                if (RefVect[i] > 0)
                {
                    string Tempstring1 = AllADGroupsList[i].Item1;
                    while (Tempstring1.Length < 80)
                    {
                        Tempstring1 = Tempstring1 + " ";
                    }
                    Tempstring1 = Tempstring1 + (RefVect[i] * 100).ToString("F2") + "%";
                    while (Tempstring1.Length < 88)
                    {
                        Tempstring1 = Tempstring1 + " ";
                    }
                    Tempstring1 = Tempstring1 + $"(Weighted for rarity {RefTFIDF[i].ToString("F2")})\r\n";
                    Outstring = Outstring + Tempstring1;
                }
            }
            Outstring = Outstring + "\r\n\r\n";
            return Outstring;
        }

        public static string ReturnRecommendationString(List<QueryResult> AllInputs, List<Tuple<string,string>> AllGroupNamesAndDescriptions, double Threshold)
        {
            Vector CountVector = new DenseVector(AllInputs[0].ReturnAccessVector().Count);
            //bool TFIDFAttempted = false;
            /*if (ByTFIDF)
            {
                TFIDFAttempted = true;
                try
                {
                    GroupingQueryResult GQR = (GroupingQueryResult)AllInputs[0];
                    Vector TestVector = GQR.ReturnTF_IDFVector();
                }
                catch
                {
                    ByTFIDF = false;
                }
                if (ByTFIDF)
                {
                    Parallel.ForEach<QueryResult>(AllInputs, QR => {
                        GroupingQueryResult GQR = (GroupingQueryResult)QR;
                        CountVector = (DenseVector)(CountVector + GQR.ReturnTF_IDFVector());
                    });
                    CountVector = (DenseVector)(CountVector / AllInputs.Count);
                    Parallel.For(0, CountVector.Count, i => {
                        if (CountVector[i] < Threshold)
                        {
                            CountVector[i] = 0;
                        }
                    });

                    string Titlestring = $"Template Recommended By TF-IDF, with Threshold of {Threshold.ToString()}";
                    return RecommendationString(CountVector, AllGroupNamesAndDescriptions, Titlestring);
                }
            }*/

            //the following executes if TFIDF is not being used
            if (AllInputs[0].GetType() == typeof(RBACS.UserQueryResult))
            {
                Parallel.ForEach<QueryResult>(AllInputs, QR =>
                {
                    CountVector = (DenseVector)(CountVector + QR.ReturnAccessVector());
                });
            }
            else if (AllInputs[0].GetType()==typeof(GroupingQueryResult))
            {
                Parallel.ForEach(AllInputs, QR => {
                    GroupingQueryResult GQR = (GroupingQueryResult)QR;
                    CountVector = (DenseVector)(CountVector + GQR.ReturnAccessVector());
                });
            }
            else if (AllInputs[0].GetType() == typeof(UserClusteringResult))
            {
                Parallel.ForEach(AllInputs, QR => {
                    UserClusteringResult UCR = (UserClusteringResult)QR;
                    CountVector = (DenseVector)(CountVector + UCR.ReturnAccessVector());
                });
            }
            else if (AllInputs[0].GetType() == typeof(GroupingClusteringResult))
            {
                Parallel.ForEach(AllInputs, QR => {
                    GroupingClusteringResult GCR = (GroupingClusteringResult)QR;
                    CountVector = (DenseVector)(CountVector + GCR.ReturnAccessVector());
                });
            }
            CountVector = (DenseVector)(CountVector / AllInputs.Count);
            Parallel.For(0, CountVector.Count, i => {
                if (CountVector[i] < Threshold)
                {
                    CountVector[i] = 0;
                }
            });
            string TitleString;
            
            TitleString = $"Template Recommended By Relative Count with Threshold {Threshold.ToString()}";
                     
            return RecommendationString(CountVector, AllGroupNamesAndDescriptions, TitleString);

        }

        public static string ReturnCSVString(DataGridView DGV)
        {
            string CSV = "";
            foreach(DataGridViewColumn DGVC in DGV.Columns)
            {
                CSV = CSV + DGVC.HeaderText + ",";
            }
            CSV = CSV + "\r\n";
            foreach(DataGridViewRow DGVR in DGV.Rows)
            {
                foreach(DataGridViewCell Cell in DGVR.Cells)
                {
                    CSV = CSV + Cell.Value.ToString().Replace(",", ";") + ",";
                }
                CSV = CSV + "\r\n";
            }
            return CSV;
        }

        public static string RemoveXMLIllegalCharacters(string StringIn)
        {
            Regex _invalidXMLChars = new Regex(
                @"(?<![\uD800-\uDBFF])[\uDC00-\uDFFF]|[\uD800-\uDBFF](?![\uDC00-\uDFFF])|[\x00-\x08\x0B\x0C\x0E-\x1F\x7F-\x9F\uFEFF\uFFFE\uFFFF]",
                RegexOptions.Compiled);
            if (StringIn == null)
            {
                return "";
            }
            else
            {
                return _invalidXMLChars.Replace(StringIn, "");
            }
        }

    }

    public class DatasetManagement
    {
        public static List<string> AddPermissionToList(string Permission, List<string> Permissions)
        {
            string[] OutArray = new string[Permissions.Count];
            Permissions.CopyTo(OutArray);
            List<string> Outlist = OutArray.Distinct().ToList();
            Outlist.Add(Permission);
            Outlist.Sort();
            //needs to ensure a list of distinct values is returned
            //to prevent issues with circular child-parent group relationships
            return Outlist.Distinct().ToList();
        }
        public static List<UserQueryResult> AddPermissionToAllConditionalOnIndex(List<UserQueryResult> UQRListIn, List<string> Permissions, string Permission, List<int> Indexes, int ConditionalityIndex=-1) {
            int NewPermissionIndex = Permissions.IndexOf(Permission);
            UserQueryResult[] UQRArrayOut = new UserQueryResult[UQRListIn.Count];
            Parallel.For(0, UQRListIn.Count, i => {
            
                double[] CurrentArray = UQRListIn[i].ReturnAccessVector().ToArray();
                double[] NextArray = new double[CurrentArray.Length + 1];
                for (int a = 0; a < NextArray.Length; a++)
                {
                    if (a < NewPermissionIndex)
                    {
                        NextArray[a] = CurrentArray[a];
                    }
                    else if (a == NewPermissionIndex)
                    {
                        //this is used if a list of indices of users is provided
                        if (Indexes.Contains(i))
                        {
                            NextArray[a] = 1;
                        }
                        else
                        {
                            NextArray[a] = 0;
                        }
                        //this is used where a conditional index is specified (ie index of a child group)
                        if (ConditionalityIndex >= 0)
                        {
                            //checks for conditional index in access vector
                            //to be used for nested groups
                            if (ConditionalityIndex > NewPermissionIndex)
                            {
                                //conditionality index is drawn from an updated list of groups
                                //so may need to be decremented when referencing out of a non-updated vector
                                if (CurrentArray[ConditionalityIndex - 1] == 1)
                                {
                                    NextArray[a] = 1;
                                }
                            }
                            else
                            {
                                if (CurrentArray[ConditionalityIndex] == 1)
                                {
                                    NextArray[a] = 1;
                                }
                            }
                        }
                    }
                    else
                    {
                        NextArray[a] = CurrentArray[a - 1];
                    }
                }
                DenseVector NewVector = DenseVector.OfArray(NextArray);
                UQRArrayOut[i] = new UserQueryResult(UQRListIn[i].Title, UQRListIn[i].Description, UQRListIn[i].Name, UQRListIn[i].AccountName, UQRListIn[i].DistinguishedName, NewVector);
            
            });
            return UQRArrayOut.ToList();
        }
        public static List<UserQueryResult> UpdatePermissionsByCondition(int TargetPermissionIndex, int ConditionalityPermissionIndex, List<UserQueryResult> UQRListIn)
        {
            UserQueryResult[] UQRArrayOut = new UserQueryResult[UQRListIn.Count];
            Parallel.For(0, UQRListIn.Count, i =>
            {
                double[] InterimArray = UQRListIn[i].ReturnAccessVector().ToArray();
                if (InterimArray[ConditionalityPermissionIndex] == 1)
                {
                    InterimArray[TargetPermissionIndex] = 1;
                }
                DenseVector NewVector = DenseVector.OfArray(InterimArray);
                UQRArrayOut[i] = new UserQueryResult(UQRListIn[i].Title, UQRListIn[i].Description, UQRListIn[i].Name, UQRListIn[i].AccountName, UQRListIn[i].DistinguishedName, NewVector);
            });
            return UQRArrayOut.ToList();
        }
        public static string VectorToString(Vector VectorIn)
        {
            string Outstring = "";
            for(int i = 0; i < VectorIn.Count; i++)
            {
                if (i < VectorIn.Count - 1) {
                    Outstring += VectorIn[i].ToString() + " ";
                }
                else
                {
                    Outstring += VectorIn[i].ToString();
                }
            }
            return Outstring;
        }
        public static DenseVector StringToDenseVector(string StringIn)
        {
            //string in must be space-separated numbers
            string[] Strings = StringIn.Split(' ');
            DenseVector OutVector = new DenseVector(Strings.Length);
            Parallel.For(0, Strings.Length, i => {
                OutVector[i] = Convert.ToDouble(Strings[i]);
            });
            return OutVector;
        }
    }
}
