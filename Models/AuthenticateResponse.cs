using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Models
{
    public class AuthenticateResponse
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string mail { get; set; }
        public string token { get; set; }


        public AuthenticateResponse(User.User user, string _token)
        {
            id = user.id;
            name = user.name;
            surname = user.surname;
            mail = user.mail;
            token = _token;
        }
    }
}
