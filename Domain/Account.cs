namespace Domain
{
    public class Account
    {
        public virtual int Id { get; set; }
        public virtual AccountType AccountType { get; set; }
        public virtual Rent Rent { get; set; }
        public virtual Status Status { get; set; }
        public virtual string Description { get; set; }
        public virtual string Amount { get; set; }
        public virtual int Month { get; set; }
    }
}
