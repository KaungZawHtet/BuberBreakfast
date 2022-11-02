using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;

namespace BuberBreakfast.ServiceErrors;

public static class Errors
{
    public static class Breakfast
    {
        public static Error NotFound => Error.NotFound(
            code : "111",
            description : "Not found"
        );

        public static Error InvalidName => Error.Validation(
           code: "112",
           description: $"Valid name must be at least {Models.Breakfast.MinNameLength} and at most {Models.Breakfast.MaxNameLength} characters"
       );


        public static Error InvalidDescription => Error.Validation(
            code: "112",
            description: $"Valid description must be at least {Models.Breakfast.MinDescriptionLength} and at most {Models.Breakfast.MaxDescriptionLength} characters"
        );
    }
    
}