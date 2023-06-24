using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Controllers.Services
{
    public class AuthResponse
    {
        public bool Result { get; private set; }
        public List<string> Error { get; private set; }
        public string Token { get; private set; }


        public static AuthResponse NoRole()
        {

            return new AuthResponse()
            {
                Error = new List<string>() { "this role is not available" },
                Result = false,
                Token = null
            };

        }
        public static AuthResponse Success(string token)
        {
            return new AuthResponse()
            {
                Error = null,
                Result = true,
                Token = token
            };

        }



        public static AuthResponse IncorrectPasswordOrEmail()
        {
            return new AuthResponse()
            {
                Error = new List<string> { "email or Password is incorrect" },
                Result = false,
                Token = null
            };


        }

        // public static List<string> NoUserWithThisEmail()
        // {
        //     Error = new List<string> { "No User registered With This Email " };
        //     Result = false;
        //     Token = null;
        //     return new List<string>(){Error.ToString() + Result.ToString() + Token};

        // }

        public static AuthResponse SomethingWentWrong()
        {
            return new AuthResponse()
            {
                Error = new List<string> { "something went wrong " },
                Result = false,
                Token = null
            };



        }



        public static AuthResponse InvalidInput()
        {


            return new AuthResponse()
            {
                Error = new List<string> { "entered invalid input" },
                Result = false,
                Token = null
            };

        }

        public static AuthResponse EmailAlreadyExist()
        {
            return new AuthResponse()
            {
                Error = new List<string> { "entered email is alraedy exist you cant sign up whit it" },
                Result = false,
                Token = null
            };




        }



    }
}
