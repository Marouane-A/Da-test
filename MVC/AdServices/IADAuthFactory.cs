using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.AdServices
{
    public interface IADAuthFactory : IDisposable
    {
       
            // instancié l'objet Directory Entry
            DirectoryEntry userEntry { get; }
        

    }
}
