using MVC.ADServices;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;

namespace MVC.AdServices
{
    public class ADAuthFactory : Disposable, IADAuthFactory
    {
        private DirectoryEntry _userEntry;
        private ADAuthentification _adAth; 
        public DirectoryEntry userEntry
        {
            get
            {
               return _userEntry;
            }
        }

        public ADAuthFactory(string userName,String pwd , string path )
        {
            _adAth = new ADAuthentification(userName, pwd, path);
            _userEntry = _adAth.GetEntry();

        }
        protected override void DisposeCore()
        {
            if (_userEntry != null)
                _userEntry.Dispose();
        }
    }
}