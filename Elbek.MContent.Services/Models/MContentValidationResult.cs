using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        ///TODO 2.1 это логика свойства IsValid.
        /// UpdateIsValidFlagOnError - удалить, логику реализовать в get свойства IsValid
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
