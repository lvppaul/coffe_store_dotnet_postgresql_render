using Microsoft.AspNetCore.Mvc.Filters;
using PRN232.Lab1.CoffeeStore.Services.Exceptions;

namespace PRN232.Lab1.CoffeeStore.APIS.Filters
{
    public class ValidationFilter : IActionFilter

    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                    .Where(ms => ms.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                throw new ValidationException(errors);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
