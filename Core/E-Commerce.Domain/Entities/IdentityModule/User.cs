using Microsoft.AspNetCore.Identity;
using System.Net;

namespace E_Commerce.Domain.Entities.IdentityModule
{
    public class User : IdentityUser
    {
        public string DisplayName { get; set; } = string.Empty;
        public Address Address { get; set; }
    }
}
