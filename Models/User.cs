using System.ComponentModel;

namespace Models
{
    public class User : Entity
    {
        [DisplayName("UserName")]
        public string? Name { get; set; }
        public string? Password { get; set; }
    }
}
