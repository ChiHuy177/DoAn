namespace ProjectA.Models
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public decimal Total
        {
            get { return Price * Quantity; }
        }

        public CartItemModel() { }

        public CartItemModel(ProductModel product, int quantity) {
            Id = product.Id;
            Name = product.Name;
            Price = product.Price;
            Quantity = quantity;
            Image = product.Image;
        }
    }
}
