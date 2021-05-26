using System;
using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;

namespace Elbek.MContent.Services.Extensions
{
    public static class ResultExtensions
    {
        public static MContentResult<T> ConvertFromValidationResult<T>(this MContentValidationResult validationResult)
        {
            var result = new MContentResult<T>
            {
                Errors = validationResult.Errors,
                StatusCode = validationResult.StatusCode,
                Data = default(T)
            };

            return result;
        }

        public static ContentType ToContentType(this string value)
        {
            if (Enum.TryParse(typeof(ContentType), value, true, out var result))
            {
                return (ContentType)result;
            }

            return default;
        }

        //public static bool TryParseWithMemberName2<TEnum>(string value, out object result)
        //{
        //    return Enum.TryParse(typeof(TEnum), value, true, out result);
        //}

        //public static TEnum? GetEnumValueOrDefault2<TEnum>(string value) where TEnum : struct
        //{
        //    Enum.TryParse(typeof(TEnum), value, true, out var result);
        //    return (TEnum?)result;
        //}
    }
}