using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Resources
{
    public class UserResource
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
