using Elbek.MContent.DataAccess.Data;
using Elbek.MContent.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elbek.MContent.Services.Extensions
{
public static class ResultExtensions
{
    public static MContentResult<T> ConvertFromValidationResult<T>(this MContentValidationResult validationResult)
    {
        var result = new MContentResult<T>();

        result.Errors = validationResult.Errors;
        result.StatusCode = validationResult.StatusCode;
        result.Data = default(T);
        return result;
    }
}
}
