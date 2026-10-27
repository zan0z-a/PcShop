using System.ComponentModel.DataAnnotations;

namespace PcShop.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите название")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Введите описание")]
        public string Description { get; set; } = "";

        [Range(1, 10000000, ErrorMessage = "Неверная цена")]
        public int Price { get; set; }

        [Required(ErrorMessage = "Введите ссылку на картинку")]
        public string ImageUrl { get; set; } = "";

        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}