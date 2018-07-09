using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;

namespace RBACS
{
    public class GroupResult:iResult
    {
        protected string name;
        protected string description;

        public string Name => name;
        public string Description {
            get {
                if (description == null)
                {
                    return "";
                }
                else
                {
                    return description;
                }
            }
        }

        public GroupResult(string Name, string Description)
        {
            name = Name;
            description = Description;
        }
    }
    public class GroupRepresentationResult:GroupResult
    {
        //used for neater view of group representation in datagridviews
        protected double rep;
        //double tFIDFRep;
        
        public string Percent => $"{(rep * 100).ToString()}%";
        //public double RarityWeighted => tFIDFRep;
        public GroupRepresentationResult(string Name, string Description, double Rep ):base(Name, Description)
        {
            rep = Rep;
            //tFIDFRep = TFIDF;          
        }
    }

    public class GroupRepresentationTFIDFResult : GroupRepresentationResult
    {
        double tFIDFRep;
        public double RarityWeighting => tFIDFRep;
        public GroupRepresentationTFIDFResult(string Name, string Description, double Rep, double TFIDFRep) : base(Name, Description, Rep)
        {
            tFIDFRep = TFIDFRep;
        }
    }

    public interface iKNNResult
    {
        void AssignKNNDistanceFromX(double Distance);
    }
    public interface iResult
    {
        //abstracts all classes designed to represent objects' public properties
        //in datagridviews
    }

    public abstract class QueryResult:iResult
    {
        protected Vector accessSummaryVector;
        //List<string> AccessSummaryList;
        public List<string> ReturnAccessList(List<string>ReferralList)
        {
            //return list of group names based on access vector
            List<string> ReturnList = new List<string>();
            if(ReferralList.Count != accessSummaryVector.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                for(int i=0; i<ReferralList.Count; i++)
                {
                    if (accessSummaryVector[i] > 0)
                    {
                        ReturnList.Add(ReferralList[i]);
                    }
                }
            }
            return ReturnList;
        }
        public Vector ReturnAccessVector()
        {
            return accessSummaryVector;
        }
        public QueryResult(Vector AccessVector)
        {
            //clones the input vector as vector is a reference variable
            accessSummaryVector = (Vector)AccessVector.Clone();
        }
    }

    public class UserClusteringResult:UserQueryResult
    {
        string clusterID;
        int clusterIndex;
        public int ClusterIndex => clusterIndex;
        public UserClusteringResult(UserQueryResult UQR, string ClusterID, int ClusterIndex):base(UQR.Title,UQR.Description,UQR.Name,UQR.AccountName, UQR.DistinguishedName, UQR.ReturnAccessVector())
        {
            clusterID = ClusterID;
            clusterIndex = ClusterIndex;
        }
    }

    public class GroupingClusteringResult : GroupingQueryResult
    {
        string clusterID;
        int clusterIndex;
        public int ClusterIndex => clusterIndex;
        public GroupingClusteringResult(GroupingQueryResult GQR, string ClusterID, int ClusterIndex) : base(GQR)
        {
            clusterID = ClusterID;
            clusterIndex = ClusterIndex;
        }
    }

    public class UserQueryResult:QueryResult
    {
        string title;
        string description;
        string name;
        string accountName;
        string distinguishedName;

        public string Title => title;
        public string Description => description;
        public string Name => name;
        public string AccountName => accountName;
        public string DistinguishedName => distinguishedName; 

        public UserQueryResult(string title, string description, string name, string accountname, string distinguishedname, Vector Access) : base(Access)
        {
            this.title = title;
            this.description = description;
            this.name = name;
            this.accountName = accountname;
            this.distinguishedName = distinguishedname;
        }
    }

    public class UserKNNResult:UserQueryResult, iKNNResult
    {
        double kNNDistanceFromX;
        public double Distance => kNNDistanceFromX;
        public void AssignKNNDistanceFromX(double Distance) => kNNDistanceFromX = Distance;
        public UserKNNResult(UserQueryResult UQR) : base(UQR.Title, UQR.Description, UQR.Name, UQR.AccountName, UQR.DistinguishedName, UQR.ReturnAccessVector())
        {

        }
    }

    public class GroupingQueryResult:QueryResult
    {
        Vector rawCountVector;
        Vector tF_IDFVector;
        int groupMemberCount;
        string groupingName;
        string groupingType;
        string aDGroupsRepresented;
        string members;
        
        public string GroupingName => groupingName;
        public string GroupingType => groupingType;
        public int MemberCount => groupMemberCount;
        public string ADGroupsRepresented => aDGroupsRepresented;
        public string Members => members;
        public void WriteTF_IDF(Vector Input)
        {
            tF_IDFVector = (Vector)Input.Clone();
        }

        //bit of a fuckaround, but having methods to return vectors rather than public 
        //get accessor makes it easier to control what is shown in DataGridView

        //public Vector ReturnAccessSummaryVector() { return accessSummaryVector; }
        public Vector ReturnTF_IDFVector() { return tF_IDFVector; }
        public Vector ReturnRawCountVector() { return rawCountVector; }
        public GroupingQueryResult(List<UserQueryResult> InputList, List<string>ADGroupNames, string Name, string TypeName):base(new DenseVector(InputList[0].ReturnAccessVector().Count))
        {
            groupingName = Name;
            groupingType = TypeName;
            groupMemberCount = InputList.Count;
            List<string> MembersList = new List<string>();
            
            Vector InterimAccessVector = new DenseVector(InputList[0].ReturnAccessVector().Count);
            foreach(UserQueryResult UQR in InputList)
            {
                if (UQR != null)
                {
                    InterimAccessVector = (Vector)(InterimAccessVector + UQR.ReturnAccessVector());
                    MembersList.Add(UQR.AccountName);
                }
            }
            members = HelperFunctions.StringListToCommaSeparatedString(MembersList);
            accessSummaryVector = (Vector)(InterimAccessVector / groupMemberCount);
            aDGroupsRepresented = HelperFunctions.StringListToCommaSeparatedString(ReturnAccessList(ADGroupNames));
            rawCountVector = (Vector)InterimAccessVector.Clone();
            
        }
        public GroupingQueryResult(GroupingQueryResult GQR):base(GQR.ReturnAccessVector())
        {
            this.accessSummaryVector = GQR.ReturnAccessVector();
            this.rawCountVector = GQR.ReturnRawCountVector();
            this.tF_IDFVector = GQR.ReturnTF_IDFVector();
            this.aDGroupsRepresented = GQR.ADGroupsRepresented;
            this.groupingName = GQR.GroupingName;
            this.groupingType = GQR.GroupingType;
            this.groupMemberCount = GQR.MemberCount;
            this.members = GQR.Members;
            
        }
        public GroupingQueryResult(GroupingKNNResult GKR):base(GKR.ReturnAccessVector())
        {
            this.aDGroupsRepresented = GKR.ADGroupsRepresented;
            this.groupingName = GKR.GroupingName;
            this.groupingType = GKR.GroupingType;
            this.groupMemberCount = GKR.MemberCount;
            this.members = GKR.Members;
            this.accessSummaryVector = GKR.ReturnAccessVector();
            this.rawCountVector = GKR.ReturnRawCountVector();
            this.tF_IDFVector = GKR.ReturnTF_IDFVector();
        }
        
    }

    public class GroupingKNNResult:GroupingQueryResult, iKNNResult
    {
        double kNNDistanceFromX;
        public double Distance => kNNDistanceFromX;
        public void AssignKNNDistanceFromX(double Dist) => kNNDistanceFromX = Dist;
        public GroupingKNNResult(GroupingQueryResult GQR) : base(GQR)
        {

        }
    }

    //ReportClasses

    //A bunch of classes providing sorting and searching capability for Lists of the 
    //Various QueryResult derived types

    public enum Ordering
    {
        Ascending,
        Descending,
        None
    }
    public abstract class Report
    {
        protected Type qRType;
        protected List<iResult> qRList;
        protected string lastPropertyQueried;
        protected Ordering order;
        //public List<QueryResult> QRList=>qRList.ConvertAll(c=>(derivedtype)c);

        protected bool returnAscendingStatus()
        {
            if(order == Ordering.Descending || order == Ordering.None)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected abstract void sortByPropertyValue(string Property);

        protected abstract List<iResult> search(string SearchTerm);
        
        protected Ordering flipOrdering(Ordering Order)
        {
            if (Order == Ordering.Descending || Order == Ordering.None)
            {
                return Ordering.Ascending;
            }
            else
            {
                return Ordering.Descending;
            }
        }

        public Ordering FlipOrdering(Ordering Order) => flipOrdering(Order);

        public Report(List<iResult> Inputs, Ordering Order, string LastProperty=null)
        {
            qRList = new List<iResult>();
            iResult[] qRArray = new iResult[Inputs.Count];
            Inputs.CopyTo(qRArray);
            qRList = qRArray.ToList();
            order = Order;
            lastPropertyQueried = LastProperty;
            if (Inputs.Count > 0)
            {
                qRType = Inputs[0].GetType();
            }
            else
            {
                qRType = null;
            }
        }
       
    }

    public class UserQueryReport : Report
    {
        public List<UserQueryResult> QRList => qRList.ConvertAll(c => (UserQueryResult)c);

        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (UserQueryResult Res in qRList.ConvertAll(c => (UserQueryResult)c))
            {
                var Props = typeof(UserQueryResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                var Prop = typeof(UserQueryResult).GetProperty(Property);
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    qRList = qRList.ConvertAll(o => (UserQueryResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Ascending;
                }
                else
                {
                    qRList = qRList.ConvertAll(o => (UserQueryResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Descending;
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }

        public void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public UserQueryReport Search(string SearchTerm)
        {

            UserQueryReport Output = new UserQueryReport(search(SearchTerm).ConvertAll(c => (UserQueryResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public UserQueryReport(List<UserQueryResult> Inputs, Ordering Order, string LastProperty="AccountName"):base(Inputs.ConvertAll(c=>(iResult)c), Order, LastProperty)
        {

        }
    }

    public class GroupingQueryReport : Report
    {
        public List<GroupingQueryResult> QRList => qRList.ConvertAll(c => (GroupingQueryResult)c);

        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (GroupingQueryResult Res in qRList.ConvertAll(c => (GroupingQueryResult)c))
            {
                var Props = typeof(GroupingQueryResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                var Prop = typeof(GroupingQueryResult).GetProperty(Property);
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    qRList = qRList.ConvertAll(o => (GroupingQueryResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Ascending;
                }
                else
                {
                    qRList = qRList.ConvertAll(o => (GroupingQueryResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Descending;
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }

        

        public void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public GroupingQueryReport Search(string SearchTerm)
        {

            GroupingQueryReport Output = new GroupingQueryReport(search(SearchTerm).ConvertAll(c => (GroupingQueryResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public GroupingQueryReport(List<GroupingQueryResult> Inputs, Ordering Order, string LastProperty="GroupingName") : base(Inputs.ConvertAll(c => (iResult)c), Order, LastProperty)
        {

        }
    }

    public class UserKNNReport : Report
    {
        public List<UserKNNResult> QRList => qRList.ConvertAll(c => (UserKNNResult)c);

        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (UserKNNResult Res in qRList.ConvertAll(c => (UserKNNResult)c))
            {
                var Props = typeof(UserKNNResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                var Prop = typeof(UserKNNResult).GetProperty(Property);
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    qRList = qRList.ConvertAll(o => (UserKNNResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Ascending;
                }
                else
                {
                    qRList = qRList.ConvertAll(o => (UserKNNResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Descending;
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }
        

        public void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public UserKNNReport Search(string SearchTerm)
        {

            UserKNNReport Output = new UserKNNReport(search(SearchTerm).ConvertAll(c => (UserKNNResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public UserKNNReport(List<UserKNNResult> Inputs, Ordering Order, string LastProperty="Distance") : base(Inputs.ConvertAll(c => (iResult)c), Order, LastProperty)
        {

        }
    }

    public class GroupingKNNReport : Report
    {
        public List<GroupingKNNResult> QRList => qRList.ConvertAll(c => (GroupingKNNResult)c);

        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (GroupingKNNResult Res in qRList.ConvertAll(c => (GroupingKNNResult)c))
            {
                var Props = typeof(GroupingKNNResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                var Prop = typeof(GroupingKNNResult).GetProperty(Property);
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    qRList = qRList.ConvertAll(o => (GroupingKNNResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Ascending;
                }
                else
                {
                    qRList = qRList.ConvertAll(o => (GroupingKNNResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Descending;
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }

        public void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public GroupingKNNReport Search(string SearchTerm)
        {

            GroupingKNNReport Output = new GroupingKNNReport(search(SearchTerm).ConvertAll(c => (GroupingKNNResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public GroupingKNNReport(List<GroupingKNNResult> Inputs, Ordering Order, string LastProperty="Distance") : base(Inputs.ConvertAll(c => (iResult)c), Order, LastProperty)
        {

        }
    }

    public class UserClusteringReport : Report
    {
        public List<UserClusteringResult> QRList => qRList.ConvertAll(c => (UserClusteringResult)c);

        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (UserClusteringResult Res in qRList.ConvertAll(c => (UserClusteringResult)c))
            {
                var Props = typeof(UserClusteringResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                var Prop = typeof(UserClusteringResult).GetProperty(Property);
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    qRList = qRList.ConvertAll(o => (UserClusteringResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Ascending;
                }
                else
                {
                    qRList = qRList.ConvertAll(o => (UserClusteringResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Descending;
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }

        public void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public UserClusteringReport Search(string SearchTerm)
        {

            UserClusteringReport Output = new UserClusteringReport(search(SearchTerm).ConvertAll(c => (UserClusteringResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public UserClusteringReport(List<UserClusteringResult> Inputs, Ordering Order, string LastProperty="ClusterIndex") : base(Inputs.ConvertAll(c => (iResult)c), Order, LastProperty)
        {

        }
    }

    public class GroupingClusteringReport : Report
    {
        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (GroupingClusteringResult Res in qRList.ConvertAll(c => (GroupingClusteringResult)c))
            {
                var Props = typeof(GroupingClusteringResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                var Prop = typeof(GroupingClusteringResult).GetProperty(Property);
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    qRList = qRList.ConvertAll(o => (GroupingClusteringResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Ascending;
                }
                else
                {
                    qRList = qRList.ConvertAll(o => (GroupingClusteringResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Descending;
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }
        public List<GroupingClusteringResult> QRList => qRList.ConvertAll(c => (GroupingClusteringResult)c);

        public void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public GroupingClusteringReport Search(string SearchTerm)
        {

            GroupingClusteringReport Output = new GroupingClusteringReport(search(SearchTerm).ConvertAll(c => (GroupingClusteringResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public GroupingClusteringReport(List<GroupingClusteringResult> Inputs, Ordering Order, string LastProperty="ClusterIndex") : base(Inputs.ConvertAll(c => (iResult)c), Order, LastProperty)
        {

        }
    }

    public class GroupReport : Report
    {
        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (GroupResult Res in qRList.ConvertAll(c => (GroupResult)c))
            {
                var Props = typeof(GroupResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                var Prop = typeof(GroupResult).GetProperty(Property);
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    qRList = qRList.ConvertAll(o => (GroupResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Ascending;
                }
                else
                {
                    qRList = qRList.ConvertAll(o => (GroupResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                    order = Ordering.Descending;
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }
        public List<GroupResult> QRList => qRList.ConvertAll(c => (GroupResult)c);

        public void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public GroupReport Search(string SearchTerm)
        {

            GroupReport Output = new GroupReport(search(SearchTerm).ConvertAll(c => (GroupResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public GroupReport(List<GroupResult> Inputs, Ordering Order, string LastProperty = "Name") : base(Inputs.ConvertAll(c => (iResult)c), Order, LastProperty)
        {

        }
    }

    public class GroupRepresentationReport : Report
    {
        protected string trimEnd(string In, int Trim)
        {
            //trim off last N characters

            return In.Substring(0, In.Length - Trim);
        }
        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (GroupRepresentationResult Res in qRList.ConvertAll(c => (GroupRepresentationResult)c))
            {
                var Props = typeof(GroupRepresentationResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                bool Numeric = true;
                var Prop = typeof(GroupRepresentationResult).GetProperty(Property);
                //This will test the string value of the public property to see if it
                //is the percentage representation of the group
                //as this needs to be sorted differently to a normal string
                string TestString = Prop.GetValue(qRList[0], null).ToString();

                try { Convert.ToDouble(TestString.Substring(0, TestString.Length - 1)); } catch { Numeric = false; };
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    if (!Numeric)
                    {
                        qRList = qRList.ConvertAll(o => (GroupRepresentationResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                        order = Ordering.Ascending;
                    }
                    else
                    {
                        qRList = qRList.ConvertAll(o => (GroupRepresentationResult)o).OrderBy(o => Convert.ToDouble(trimEnd(Prop.GetValue(o, null).ToString(), 1))).ToList().ConvertAll(c=>(iResult)c);
                        order = Ordering.Ascending;
                    }
                }
                else
                {
                    if (!Numeric)
                    {
                        qRList = qRList.ConvertAll(o => (GroupRepresentationResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                        order = Ordering.Descending;
                    }
                    else
                    {
                        qRList = qRList.ConvertAll(o => (GroupRepresentationResult)o).OrderByDescending(o => Convert.ToDouble(trimEnd(Prop.GetValue(o, null).ToString(), 1))).ToList().ConvertAll(c => (iResult)c);
                        order = Ordering.Descending;
                    }
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }
        public List<GroupRepresentationResult> QRList => qRList.ConvertAll(c => (GroupRepresentationResult)c);

        public void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public GroupRepresentationReport Search(string SearchTerm)
        {

            GroupRepresentationReport Output = new GroupRepresentationReport(search(SearchTerm).ConvertAll(c => (GroupRepresentationResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public GroupRepresentationReport(List<GroupRepresentationResult> Inputs, Ordering Order, string LastProperty = "Name") : base(Inputs.ConvertAll(c => (iResult)c), Order, LastProperty)
        {

        }
    }

    public class GroupRepresentationTFIDFReport:GroupRepresentationReport
    {
        public new List<GroupRepresentationTFIDFResult> QRList => qRList.ConvertAll(c => (GroupRepresentationTFIDFResult)c);
        protected override List<iResult> search(string SearchTerm)
        {
            List<iResult> ReturnList = new List<iResult>();
            ConcurrentBag<iResult> ReturnBag = new ConcurrentBag<iResult>();
            foreach (GroupRepresentationTFIDFResult Res in qRList.ConvertAll(c => (GroupRepresentationTFIDFResult)c))
            {
                var Props = typeof(GroupRepresentationTFIDFResult).GetProperties();
                Parallel.ForEach(Props, (Prop, loopState) => {
                    if (Prop.GetValue(Res, null).ToString().Contains(SearchTerm))
                    {
                        ReturnBag.Add(Res);
                        loopState.Break();
                    }
                });
            }
            ReturnList = ReturnBag.ToList();
            return ReturnList;
        }
        protected override void sortByPropertyValue(string Property)
        {
            if (qRList.Count > 0)
            {
                bool Numeric = true;
                var Prop = typeof(GroupRepresentationTFIDFResult).GetProperty(Property);
                //This will test the string value of the public property to see if it
                //is the percentage representation of the group
                //as this needs to be sorted differently to a normal string
                string TestString = Prop.GetValue(qRList[0], null).ToString();

                try { Convert.ToDouble(TestString.Substring(0, TestString.Length - 1)); } catch { Numeric = false; };
                if (returnAscendingStatus() || Property != lastPropertyQueried)
                {
                    if (Prop.Name != "Percent")
                    {
                        qRList = qRList.ConvertAll(o => (GroupRepresentationTFIDFResult)o).OrderBy(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                        order = Ordering.Ascending;
                    }
                    else
                    {
                        qRList = qRList.ConvertAll(o => (GroupRepresentationTFIDFResult)o).OrderBy(o => Convert.ToDouble(trimEnd(Prop.GetValue(o, null).ToString(), 1))).ToList().ConvertAll(c => (iResult)c);
                        order = Ordering.Ascending;
                    }
                }
                else
                {
                    if (Prop.Name != "Percent")
                    {
                        qRList = qRList.ConvertAll(o => (GroupRepresentationTFIDFResult)o).OrderByDescending(o => Prop.GetValue(o, null)).ToList().ConvertAll(c => (iResult)c);
                        order = Ordering.Descending;
                    }
                    else
                    {
                        qRList = qRList.ConvertAll(o => (GroupRepresentationTFIDFResult)o).OrderByDescending(o => Convert.ToDouble(trimEnd(Prop.GetValue(o, null).ToString(), 1))).ToList().ConvertAll(c => (iResult)c);
                        order = Ordering.Descending;
                    }
                }
                lastPropertyQueried = Property;
            }
            else
            {
                throw new NotImplementedException("You can't sort an empty list");
            }
        }
        public new void SortByProperty(string Property)
        {
            sortByPropertyValue(Property);
        }
        public new GroupRepresentationTFIDFReport Search(string SearchTerm)
        {

            GroupRepresentationTFIDFReport Output = new GroupRepresentationTFIDFReport(search(SearchTerm).ConvertAll(c => (GroupRepresentationTFIDFResult)c), flipOrdering(order), lastPropertyQueried);
            if (Output.QRList.Count > 0)
            {
                Output.sortByPropertyValue(lastPropertyQueried);
            }
            return Output;
        }
        public GroupRepresentationTFIDFReport(List<GroupRepresentationTFIDFResult> Inputs, Ordering Order, string LastProperty = "Name"):base(Inputs.ConvertAll(c=>(GroupRepresentationResult)c), Order)
        {

        }
    }
}
