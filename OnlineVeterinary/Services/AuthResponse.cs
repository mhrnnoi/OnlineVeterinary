using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineVeterinary.Controllers
{
    public class AuthResponse
    {
        public bool Result { get; set; }
        public List<string> Error { get; set; }
        public string Token { get; set; }

        public AuthResponse(ResponseEnum response)
        {
            CheckResponse(response);
        }
        public AuthResponse(ResponseEnum response, string token)
        {
            SuccessAuth(token);
        }


        private void CheckResponse(ResponseEnum response)
        {
            switch (response)
            {
                case ResponseEnum.InvalidInput:
                    InvalidInput();
                    break;
                case ResponseEnum.EmailAlreadySignedUp:
                    EmailAlreadyExist();
                    break;
                case ResponseEnum.Somethingwentwrong:
                    SomethingWentWrong();
                    break;
                case ResponseEnum.NoUserWithThisEmail:
                    NoUserWithThisEmail();
                    break;
                case ResponseEnum.IncorrectPassword:
                    IncorrectPassword();
                    break;

                default:
                    throw new InvalidOperationException("What the heck ? something wrong with register or login");
            }
        }

        private void IncorrectPassword()
        {
            Error = new List<string> { "userName or Password is incorrect" };
            Result = false;
            Token = null;
        }

        private void NoUserWithThisEmail()
        {
            Error = new List<string> { "No User registered With This Email " };
            Result = false;
            Token = null;
        }

        private void SomethingWentWrong()
        {
            Error = new List<string> { "something went wrong " };
            Result = false;
            Token = null;
        }

        private void SuccessAuth(string token)
        {
            Error = null;
            Result = true;
            Token = token;
        }

        private void InvalidInput()
        {
            Error = new List<string> { "entered invalid input" };
            Result = false;
            Token = null;


        }

        private void EmailAlreadyExist()
        {
            Error = new List<string> { "entered email is alraedy exist you cant sign up whit it" };
            Result = false;
            Token = null;


        }



    }
}
