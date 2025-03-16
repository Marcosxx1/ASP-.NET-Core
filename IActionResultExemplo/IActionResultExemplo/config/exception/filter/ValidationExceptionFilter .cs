using ApiExemploCC.application.dto;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ApiExemploCC.config.exception.filter
{
    public class ValidationExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ValidationException validationException)
            {
                var validationErrors = new Dictionary<string, string>();
                foreach (var error in validationException.ValidationResult.MemberNames)
                {
                    validationErrors[error] = validationException.ValidationResult.ErrorMessage;
                }

                var validationErrorResponse = new ValidationErrorResponse(
                    "Erro de Validação",
                    "Um ou mais erros de validação ocorreram.",
                    validationErrors
                );

                context.Result = new BadRequestObjectResult(validationErrorResponse);
                context.ExceptionHandled = true;
            }
        }
    }


}
