namespace Domain
{
    public class BillType
    {
        public BillType()
        {
        }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string AccountNumber { get; set; }
        public virtual string Bank { get; set; }
        public virtual string ExpenseOfBudgetUser { get; set; }
        public virtual string RevenueCode { get; set; }
        public virtual string PurposeOfPayment { get; set; }
        public virtual string SuspenseAccount { get; set; }
    }
}
