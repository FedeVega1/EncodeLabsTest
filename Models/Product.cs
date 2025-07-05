using System.ComponentModel.DataAnnotations;

namespace EncodeLabsTest.Models
{
    public class ProductDTO
    {
        [Required, MinLength(4)]
        public string Name { get; set; } = "";

        [Required]
        public string Description { get; set; } = "";

        [Required, Range(0.1, double.MaxValue)]
        public double Price { get; set; } = 0.0;

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; } = 0;
    }

    public class Product
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public double Price { get; set; } = 0.0;
        public int Quantity { get; set; } = 0;
    }
}
