namespace Domain
{
    public class Bill
    {
        public virtual int Id { get; set; }
        public virtual BillType BillType { get; set; }
        public virtual Rent Rent { get; set; }
        public virtual Status Status { get; set; }
        public virtual string Amount { get; set; }
        public virtual int Month { get; set; }
        public virtual byte[] BillContent { get; set; }
    }
}
