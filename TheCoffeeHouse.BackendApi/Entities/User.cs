using Microsoft.AspNetCore.Identity;
using TheCoffeeHouse.Utilities.Enum;

namespace TheCoffeeHouse.BackendApi.Entities
{
    public class User
    {
        public string ID { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string Code { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Gender Gender { get; set; } = Gender.Male;

        public string Address { get; set; }

        public string Avatar { get; set; }

        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
