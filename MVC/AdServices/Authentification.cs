using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace MVC.AdServices
{
    public class Authentification
    {
        private DirectoryEntry _entry;
        private List<string> _groups;
        private string _tenant;
        private ADAuthFactory _adF;
        private string _userName;
        private string _cn;
        

        public Authentification(string userName, string pwd, string path)
        {
            _adF = new ADAuthFactory(userName, pwd, path);
            _entry = _adF.userEntry;
            _userName = userName;
            

        }

        public bool IsAuthenticated()
        {


            try
            {//Bind to the native AdsObject to force authentication.
                Object obj = _entry.NativeObject;

                DirectorySearcher search = new DirectorySearcher(_entry);

                search.Filter = "(SAMAccountName=" + _userName + ")";
                search.PropertiesToLoad.Add("cn");


                SearchResult result = search.FindOne();

                if (null == result)
                {
                    return false;
                }

                //Update the new path to the user in the directory.
                _cn = (String)result.Properties["cn"][0];


                // _errLabel.Text = _errLabel.Text + " |  path : " + _path;

            }
            catch (Exception ex)
            {
                throw new Exception("Error authenticating user. " + ex.Message);
            }

            return true;
        }

        public Boolean isMemberOfGroup(string group)
        {
            DirectorySearcher directorySearcher = new DirectorySearcher(_entry);
            directorySearcher.Filter = "(cn=" + _userName + ")";
            directorySearcher.PropertiesToLoad.Add(DirectoryAttributes.MemberOf);

            SearchResult results = directorySearcher.FindOne();

            if (results != null)
            {

                var memberOf = results.Properties[DirectoryAttributes.MemberOf]; // Force evaluation now so you can have a poke about.

                return memberOf[0].ToString().Contains("CN=" + group);
            }



            return false;
        }
        public string GetOrganisationUnit()
        {
            //DirectorySearcher directorySearcher = new DirectorySearcher(_entry);
            //directorySearcher.Filter = "(cn=" + _userName + ")";
            //directorySearcher.PropertiesToLoad.Add(DirectoryAttributes.CommonName);
            //var x = "null";
            //SearchResult results = directorySearcher.FindOne();
            //if (results != null)
            //{

            //    var aa = results.Properties[DirectoryAttributes.DistinguishedName].ToString(); // Force evaluation now so you can have a poke about.
            //   // x = aa;
            //    string[] one = aa.Split(',');

            //    foreach (var it in one)
            //    {
            //        if (it.Contains("OU="))
            //        {
            //            x = it.Replace("OU=", "");

            //        }
            //    }


            //}
            //return x;
            String domainAndUsername = "DEVSTORM" + @"\" + _userName;
            PrincipalContext domainContext = new PrincipalContext(ContextType.Domain, "192.168.126.189:389", "dc=devstorm,dc=tn", domainAndUsername, "KingHolding2007.");

            UserPrincipal user = UserPrincipal.FindByIdentity(domainContext, _userName);

            /* Retreive the container
             */
            DirectoryEntry deUser = user.GetUnderlyingObject() as DirectoryEntry;
            DirectoryEntry deUserContainer = deUser.Parent;
            string aa = deUserContainer.Properties["distinguishedName"].Value.ToString();
            string[] one = aa.Split(',');
            foreach (var it in one)
            {
                if (it.Contains("OU"))
                {
                    return it.Replace("OU=", " ");
                }
            }
                return aa;


        }
        public List<string> GetGroups()
        {
            _groups = new List<string>();
            DirectorySearcher directorySearcher = new DirectorySearcher(_entry);
            directorySearcher.Filter = "(SAMAccountName=" + _userName + ")";
            directorySearcher.PropertiesToLoad.Add(DirectoryAttributes.MemberOf);

            SearchResult results = directorySearcher.FindOne();
            
            if (results != null)
            {
                var memberOf = results.Properties[DirectoryAttributes.MemberOf]; // Force evaluation now so you can have a poke about.

                string[] groups = memberOf[0].ToString().Split(',');
                foreach (var item in groups)
                {
                    if (item.Contains("CN=")) {
                        _groups.Add(item.Replace("CN=", ""));   
                    }       
                }
            }
            return _groups;
        }
    }
}