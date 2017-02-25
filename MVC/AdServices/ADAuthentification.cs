using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace MVC.AdServices
{
    public class ADAuthentification
    {
        private string  _domain="DEVSTORM";
        private string _userName;
        private string _pwd;
        private string _path;

        public ADAuthentification(string userName, string pwd, string path)
        {
            _userName = userName;
            _pwd = pwd;
            _path = path;
        }
        public DirectoryEntry GetEntry()
        {
            String domainAndUsername = _domain + @"\" + _userName;
            DirectoryEntry de = new DirectoryEntry(_path, domainAndUsername, _pwd);
                return de;
        }

       

       
        
     
    }
}