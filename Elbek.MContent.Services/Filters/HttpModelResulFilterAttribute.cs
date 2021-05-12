using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elbek.MContent.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Elbek.MContent.Services.Filters
{
    public class HttpModelResultFilterAttribute: Attribute, IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context,
            ResultExecutionDelegate next)
        {
            var result = context.Result as ObjectResult;
            if(result !=  null)
                result.Value = new MContentResult<object>()
                {
                    Data = result.Value, 
                    Errors = new List<string>(), 
                    StatusCode = (int)result.StatusCode
                };
            await next();
        }
    }
}
