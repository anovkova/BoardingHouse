namespace ViewModels
{
    public class BillViewModel
    {
        public int Id { get; set; }
        public BillTypeViewModel BillType { get; set; }
        public RentViewModel Rent { get; set; }
        public StatusViewModel Status { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public int Month { get; set; }
    }
}
