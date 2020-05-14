using System;

namespace demo.Models
{
    public class tokenParameter
    {
        public string Secret { get; set; }

        public string Issuer { get; set; }

        public int AccessExpiration { get; set; }

        public int RefreshExpiration { get; set; }
    }
}
