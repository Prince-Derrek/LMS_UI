using System.ComponentModel.DataAnnotations;

namespace LMS_UI.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Username is required during Login!")]
        [MinLength(4, ErrorMessage = "Username is atleast 4 characters long!")]
        [MaxLength(15, ErrorMessage = "Username has a maximum of 15 characters!")]

        public string userName
        { get; set; }

        [Required(ErrorMessage = "Password is required!")]
        [MinLength(8, ErrorMessage = "Password must be atleast 8 characters long!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*])[A-Za-z\d!@#$%^&*]{8,}$",
           ErrorMessage = "Password must be atleast 8 characters and include one uppercase letter, one lowercase letter, one number and one special character ie: (!@#$%^&*).No Spaces should be included!!")]
        public string userPassword
        { get; set; }
    }
}