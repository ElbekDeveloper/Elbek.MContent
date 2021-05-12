using System;
using System.Collections.Generic;

namespace Elbek.MContent.Services.Exceptions
{
    public class ValidationException : Exception
    {
        public int StatusCode;
        public List<String> ValidationErrors { get; set; }

        public ValidationException(List<String> validationErrors, int statusCode = 400)
        {
            ValidationErrors = validationErrors;
            StatusCode = statusCode;
        }

    }
}
