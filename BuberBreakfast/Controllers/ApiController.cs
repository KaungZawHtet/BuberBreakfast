using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BuberBreakfast.Controllers
{
    [ApiController]
    [Route("[controller]")] // can write "breakfasts" too. The same with controller name.
    public class ApiController : ControllerBase
    {

        protected IActionResult Problem (List<Error> errors)
        {

            if(errors.All( e=> e.Type == ErrorType.Validation))
            {
                var modelStateDictionary = new ModelStateDictionary();
                
                foreach (var err in errors)
                {
                    modelStateDictionary.AddModelError(err.Code, err.Description);

                }

                return ValidationProblem(modelStateDictionary);
            }

            if (errors.Any(e => e.Type == ErrorType.Unexpected))
            {
                return Problem();
            }




            var firstError = errors[0];

            var statusCode = firstError.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

            return Problem(
                statusCode : statusCode,
                title : firstError.Description
            );

        }
        
    }
}