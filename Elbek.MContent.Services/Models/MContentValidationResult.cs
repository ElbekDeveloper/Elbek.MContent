using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.Models
{
    public class MContentValidationResult
    {
        ///TODO 2.2 хардкодить 400 не нужно, что если у тебя будет другой невалидный код, например 404
        /// поле _statusCode здесь тоже не нужно
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
