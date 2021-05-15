using System.Collections.Generic;
using System.Linq;

namespace Elbek.MContent.Services.Models
{
    public class MContentValidationResult
    {
        private int _statusCode;
        public bool IsValid
        {
            get 
            {
                if (Errors.Any() == false)
                {
                    IsValid = true;
                }
                return IsValid;
            }
            set { IsValid = value; }
        }
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

    }
}
