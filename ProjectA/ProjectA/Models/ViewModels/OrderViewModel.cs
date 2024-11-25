namespace ProjectA.Models.ViewModels
{
    public class OrderViewModel
    {
         public string Ward { get; set; } 
        public string District { get; set; } 
        public string Province { get; set; } 
        public string Street { get; set; } 

        public string OrderNotes { get; set; }

        public string PaymentMethod { get; set; }
    }
}
