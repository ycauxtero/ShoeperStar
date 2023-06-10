using Microsoft.AspNetCore.Identity;

namespace ShoeperStar.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DeliveryAddress { get; set; }
    }
}
