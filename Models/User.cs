using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class User : Entity
    {
        [DisplayName("UserName")]
        [Required(ErrorMessage = "FieldRequered")]
        [MinLength(5)]
        public string? Name { get; set; }
        [Required]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9]).{8,}$", ErrorMessage = "ComplexPassword")]
        public string? Password { get; set; }

        public int SomeInt { get; set; }
        public bool SomeBool { get; set; }
    }
}
