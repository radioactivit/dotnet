using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Supermarket.API.Resources
{
    public class AccessTokenResource
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expiration { get; set; }
    }
}
