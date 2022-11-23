using System.ComponentModel.DataAnnotations;
namespace Model.Models
{
    public class LoginModel
    {
        [Display(Name ="User name")]
        [Required]
        [RegularExpression("[A-Za-z0-9]{2,25}$", ErrorMessage = "Enter valid name contain 2-25 letters only only")]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
