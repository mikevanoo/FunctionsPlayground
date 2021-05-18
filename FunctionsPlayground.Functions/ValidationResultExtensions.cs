using System.Collections.Generic;
using System.Linq;
using System.Net;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace FunctionsPlayground.Functions
{
    public static class ValidationResultExtensions
    {
        public static BadRequestObjectResult ToBadRequest(this ValidationResult validationResult, string title)
        {
            var errorsByPropertyName = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .Select(g => new KeyValuePair<string, string[]>(
                    g.Key,
                    g.Select(x => x.ErrorMessage).ToArray())
                );

            var vpd = new ValidationProblemDetails
            {
                Status = (int)HttpStatusCode.BadRequest,
                Title = title,
                Detail = "One or more validation errors occurred.",
            };

            foreach (var error in errorsByPropertyName)
            {
                vpd.Errors.Add(error);
            }

            return new BadRequestObjectResult(vpd);
        }
    }
}