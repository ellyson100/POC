using POC.API.Model;
using System.ComponentModel.DataAnnotations;

namespace POC.API.ViewModels
{
    public class ProductViewModel 
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(1000, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 2)]
        public string Description { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Range(0, 9999999999999999.99)]
        public decimal Price { get; set; }

        public Product ToEntity()
        {
            var entity = new Product();

            entity.Id = Id;
            entity.Name = Name;
            entity.Description = Description;
            entity.Price = Price;

            return entity;
        }
    }
}
