using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoeperStar.Models.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Firstname")]
        [Required(ErrorMessage = "Firstname is required")]
        public string FirstName { get; set; }

        [Display(Name = "Lastname")]
        [Required(ErrorMessage = "Lastname is required")]
        public string LastName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Display(Name = "Delivery address")]
        [Required(ErrorMessage = "Delivery address is required")]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number is required")]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Account Type")]
        public string Role { get; set; }
    }
}
