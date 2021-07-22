using AniAPI.NET.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AniAPI.NET.Filters
{
    public class UserFilter : IFilter
    {
        public string Username { get; set; }
        public string Email { get; set; }

        protected override void FillQueryParameters()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                Parameters.Add("username", Username);
            }

            if (!string.IsNullOrEmpty(Email))
            {
                Parameters.Add("email", Email);
            }
        }
    }
}
