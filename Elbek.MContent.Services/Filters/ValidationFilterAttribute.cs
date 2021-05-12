﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Elbek.MContent.Services.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Elbek.MContent.Services.Filters
{
    public class ValidationFilterAttribute : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                    .SelectMany(v => v.Errors)
                    .Select(v => v.ErrorMessage)
                    .ToList();
                throw new ValidationException(errors);
            }
            else
                await next();
        }
    }
}
