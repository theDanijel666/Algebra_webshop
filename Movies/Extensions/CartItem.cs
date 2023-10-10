using Movies.Models;

namespace Movies.Extensions
{
    public class CartItem
    {
        public Product Product { get; set; }
        public decimal Quantity { get; set; }

        public decimal getTotal()
        {
            return Product.Price * Quantity;
        }
    }
}
