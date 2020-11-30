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
            string ConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Zakee\Documents\Projects\ASP.NET\Assignment\One\DatabaseCon\Data\ComputerShop.mdf;Integrated Security=True;Connect Timeout=30";

            return ConString;
        }
    }
}
