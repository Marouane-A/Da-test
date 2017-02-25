using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Web;

namespace MVC.AdServices
{
    public class Authentification
    {
        private DirectoryEntry _entry;
        private ICollection<string> _groups;
        private string _tenent;
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
            DirectorySearcher directorySearcher = new DirectorySearcher(_entry);
            directorySearcher.Filter = "(cn=" + _userName + ")";
            directorySearcher.PropertiesToLoad.Add(DirectoryAttributes.DistinguishedName);
            var x = "";
            SearchResult results = directorySearcher.FindOne();
            if (results != null)
            {
                
                var aa = results.Properties[DirectoryAttributes.DistinguishedName].ToString(); // Force evaluation now so you can have a poke about.
                x = aa;
                //string[] one = aa.Split(',');

                //foreach (var it in one)
                //{
                //    if (it.Contains("OU"))
                //    {
                //       x= it.Replace("OU=", "");
                       
                //    }
                //}


            }
            return x;
            }
            //public ICollection<string> GetGroups()
            //{

            //    DirectorySearcher directorySearcher = new DirectorySearcher(_entry);
            //    directorySearcher.Filter = "(cn=" + _userName + ")";
            //    directorySearcher.PropertiesToLoad.Add(DirectoryAttributes.MemberOf);

            //    SearchResult results = directorySearcher.FindOne();

            //    if (results != null)
            //    {

            //        var memberOf = results.Properties[DirectoryAttributes.MemberOf]; // Force evaluation now so you can have a poke about.

            //        string[] groups = memberOf[0].ToString().Split(',');
            //        foreach (var item in groups)
            //        {
            //            string[] one = item.Split(',');
            //            foreach (var it in one)
            //            {
            //                if (it.Contains("CN="))
            //                    _groups.Add(it.Replace("CN=", ""));
            //            }
            //        }


            //    }
            //    return _groups;
            //}
        }
}