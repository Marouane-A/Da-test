using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace MVC
{
    public class LdapAuthentication
    {
            private String _path;
            private String _filterAttribute;
            private Label _errLabel;

        public object another { get; private set; }

        public LdapAuthentication(String path, Label errLabel)
            {
                _path = path;
                _errLabel = errLabel;
            }

            public bool IsAuthenticated(String domain, String username, String pwd)
            {
                String domainAndUsername = domain + @"\" + username;
                DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);

                try
                {//Bind to the native AdsObject to force authentication.
                    Object obj = entry.NativeObject;

                    DirectorySearcher search = new DirectorySearcher(entry);

                    search.Filter = "(SAMAccountName=" + username + ")";
                    search.PropertiesToLoad.Add("cn");
                    

                SearchResult result = search.FindOne();

                if (null == result)
                    {
                        return false;
                    }

                    //Update the new path to the user in the directory.
                    _filterAttribute = (String)result.Properties["cn"][0];
                

                _errLabel.Text = _errLabel.Text + " |  path : "+ _path;

            }
            catch (Exception ex)
                {
                    throw new Exception("Error authenticating user. " + ex.Message);
                }

                return true;
            }

            public String GetGroups(String domain, String username, String pwd)
            {
                String domainAndUsername = domain + @"\" + username;
                DirectoryEntry entry = new DirectoryEntry(_path, domainAndUsername, pwd);
                DirectorySearcher search = new DirectorySearcher(entry);

                search.Filter = "(cn=" + _filterAttribute + ")";
                search.PropertiesToLoad.Add("memberOf");
                StringBuilder groupNames = new StringBuilder();


            try
            {
                    SearchResult result = search.FindOne();
                    int propertyCount = result.Properties["memberOf"].Count;
                    //_errLabel.Text = _errLabel.Text + "Count : " + propertyCount ;
                    String dn;
                    int equalsIndex, commaIndex;

                    for (int propertyCounter = 0; propertyCounter < propertyCount; propertyCounter++)
                    {
                        dn = (String)result.Properties["memberOf"][propertyCounter];

                        equalsIndex = dn.IndexOf("=", 1);
                        commaIndex = dn.IndexOf(",", 1);
                        if (-1 == equalsIndex)
                        {
                            return null;
                        }

                        groupNames.Append(dn.Substring((equalsIndex + 1), (commaIndex - equalsIndex) - 1));
                        groupNames.Append("|");

                    }
                //errorLabel.Text = errorLabel.Text + groupNames + "  -|-  ";
                }
                catch (Exception ex)
                {
                    throw new Exception("Error obtaining group names. " + ex.Message);
                }
                return groupNames.ToString();
            }
        }
}