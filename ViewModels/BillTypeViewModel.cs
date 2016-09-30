using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
    public class BillTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
        public string ExpenseOfBudgetUser { get; set; }
        public string RevenueCode { get; set; }
        public string PurposeOfPayment { get; set; }
        public string SuspenseAccount { get; set; }
    }
}
