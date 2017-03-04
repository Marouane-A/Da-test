using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace MVC.AdServices
{
    public class ADContext
    {
        private static PrincipalContext _context;
        private ADContext() {}
        public static PrincipalContext Context
        {
            get
            {
                if (_context == null) { 
                    PrincipalContext insPrincipalContext = new PrincipalContext(ContextType.Domain, "windowsserverad.devstorm.tn",
                                       "DC=devstorm,DC=tn:389","Devstorm/2016");
                    _context = insPrincipalContext ;
                }
                return _context;
            }
        }
    }
}