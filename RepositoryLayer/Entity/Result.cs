using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entity
{
    public class Result
    {
        public bool Success { get; init; }
        public string? ErrorMessage { get; init; }

        public static Result Ok() => new Result { Success = true };

        public static Result Fail(string message) => new Result { Success = false, ErrorMessage = message };
    }

}
