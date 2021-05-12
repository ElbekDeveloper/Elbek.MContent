using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.Models
{
    public class MContentValidationResult
    {
        private int _statusCode = 400;
        public bool IsValid { get; set; }
        public int StatusCode
        {
            get => _statusCode;
            private set
            {
                if (Errors.Any() == false)
                {
                    _statusCode = 200;
                }
            }
        }
        public List<string> Errors { get; set; } = new List<string>();

        public bool UpdateIsValidFlagOnError()
        {
            if (Errors.Any() == false)
            {
                IsValid = true;
            }
            return IsValid;
        }

    }
}
