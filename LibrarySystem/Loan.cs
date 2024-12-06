using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations.Model;

namespace LibrarySystem
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public required string ISBN { get; set; } 
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
