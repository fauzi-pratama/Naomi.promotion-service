﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Naomi.promotion_service.Models.Response;

namespace Naomi.promotion_service.Configurations
{
    public class ValidateModelResponse : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();

                ServiceResponse<string> responseObj = new()
                {
                    Data = null,
                    Message = string.Join(",", errors),
                    Success = false,
                    Pages = 1,
                    TotalPages = 1
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
