using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Group17_ProjectAssignment
{
    public class DBString
    {
        public string ConString ()
        {
            string ConString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ComputerShop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

            return ConString;
        }
    }
}
