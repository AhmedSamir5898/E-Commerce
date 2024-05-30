using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.DTOS
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1, double.MaxValue , ErrorMessage ="Price Must be Greater than Zero !!")]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brnad { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be At Least 1 ")]
        public int Quantity { get; set; }
    }
}