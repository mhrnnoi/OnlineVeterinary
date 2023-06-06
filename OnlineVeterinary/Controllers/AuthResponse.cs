using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Controllers
{
    public class AuthResponse
    {
        public bool Result { get; set; }
        public List<string>  Error { get; set; }
        public string  Token { get; set; }
    }
}