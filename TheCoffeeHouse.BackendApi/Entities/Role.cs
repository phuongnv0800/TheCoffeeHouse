using Microsoft.AspNetCore.Identity;

namespace TheCoffeeHouse.BackendApi.Entities
{
    public class Role
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
