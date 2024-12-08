using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace sqlLab
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public UserContext() : base("name=UserDatabaseConnectionString")
        { 

        }
    }
}

