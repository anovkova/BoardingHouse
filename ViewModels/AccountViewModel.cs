namespace ViewModels
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public RentViewModel Rent { get; set; }
        public StatusViewModel Status { get; set; }
        public string Description { get; set; }
        public string Amount { get; set; }
        public int Month { get; set; }
    }
}
