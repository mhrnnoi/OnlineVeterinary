using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Controllers.Services
{
    public class AuthResponse
    {
         public static bool Result { get; set; }
        public static List<string> Error { get; set; }
        public static string Token { get; set; }

        
        public static List<string> NoRole()
        {
            Error = new List<string>() {"this role is not available"};
            Result = false;
            Token = null;
            return new List<string>(){Error.ToString() + Result.ToString() + Token};
        }
        public static List<string> Success(string token)
        {
            Error = null;
            Result = true;
            Token = token;
            return new List<string>(){Error.ToString() + Result.ToString() + Token};
        }

        

        public static List<string> IncorrectPasswordOrEmail()
        {
            Error = new List<string> { "email or Password is incorrect" };
            Result = false;
            Token = null;
            return new List<string>(){Error.ToString() + Result.ToString() + Token};

        }

        // public static List<string> NoUserWithThisEmail()
        // {
        //     Error = new List<string> { "No User registered With This Email " };
        //     Result = false;
        //     Token = null;
        //     return new List<string>(){Error.ToString() + Result.ToString() + Token};

        // }

        public static List<string> SomethingWentWrong()
        {
            Error = new List<string> { "something went wrong " };
            Result = false;
            Token = null;
            return new List<string>(){Error.ToString() + Result.ToString() + Token};

        }

        

        public static List<string> InvalidInput()
        {
            Error = new List<string> { "entered invalid input" };
            Result = false;
            Token = null;
            return new List<string>(){Error.ToString() + Result.ToString() + Token};



        }

        public static List<string> EmailAlreadyExist()
        {
            Error = new List<string> { "entered email is alraedy exist you cant sign up whit it" };
            Result = false;
            Token = null;
            return new List<string>(){Error.ToString() + Result.ToString() + Token};



        }



    }
}
