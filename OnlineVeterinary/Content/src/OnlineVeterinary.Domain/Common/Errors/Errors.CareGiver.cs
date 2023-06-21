using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace OnlineVeterinary.Domain.Common.Errors
{
    public static class Errors
    {
        public static class CareGiver
        {
            public static Error Iwant => Error.Failure(code : "CareGiver.invalid", description : "NotFi bad requested" );
        }
    }
}