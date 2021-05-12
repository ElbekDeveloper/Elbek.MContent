using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Elbek.MContent.Services.Exceptions;
using Elbek.MContent.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Elbek.MContent.Services.Filters
{
    public class ExceptionFilterAttribute: Attribute, IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context.Exception != null)
            {
                switch (context.Exception)
                {
                    case InvalidOperationException error:
                        context.Result = new BadRequestObjectResult(new MContentResult<object>()
                        {
                            Errors = new List<string>()
                            {
                                error.Message
                            }
                        });
                        break;
                    case ArgumentException error:
                        context.Result = new BadRequestObjectResult(new MContentResult<object>()
                        {
                            Errors = new List<string>()
                            {
                                error.Message
                            }
                        });
                        break;
                    case ValidationException error:
                        context.Result = new BadRequestObjectResult(new MContentResult<object>()
                        {
                            Errors = error.ValidationErrors,
                            StatusCode = error.StatusCode
                        });
                        break;
                    case VersionNotFoundException error:
                        context.Result = new BadRequestObjectResult(new MContentResult<object>()
                        {
                            Errors = new List<string>()
                            {
                                error.Message
                            },
                            StatusCode = 404
                        });
                        break;
                    default:
                        context.Result = new ObjectResult(new MContentResult<object>()
                        {
                            Errors = new List<string>()
                            {
                                context.Exception.Message
                            }
                        }) {StatusCode = 500};
                        break;
                }
            }

            return Task.FromResult<Object>(null);
        }
    }
}
