using System;
using Microsoft.AspNetCore.Identity;

namespace Flower.Core.Entities
{
	public class AppUser:IdentityUser
	{
        public string FullName { get; set; }
    }
}

