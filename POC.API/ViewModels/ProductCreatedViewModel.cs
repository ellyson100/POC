using POC.API.Model;
using System.ComponentModel.DataAnnotations;

namespace POC.API.ViewModels
{
    public class ProductCreatedViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }

        public ProductCreatedViewModel()
        {
            
        }

        public ProductCreatedViewModel(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
        }

        public Product ToEntity()
        {
            var entity = new Product();

            entity.Name = Name; 
            entity.Description = Description;
            entity.Price = Price;

            return entity;
        }
    }
}
