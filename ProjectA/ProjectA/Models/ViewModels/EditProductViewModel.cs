namespace ProjectA.Models.ViewModels
{
    public class EditProductViewModel
    {   
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public int InStock { get; set; }

        public decimal Price { get; set; }

        public string Unit { get; set; }

        public IFormFile Image { get; set; }


    }
}
