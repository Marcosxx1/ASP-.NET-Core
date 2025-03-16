namespace ApiExemploCC.application.dto
{
    public class ValidationErrorResponse
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public Dictionary<string, string> Errors { get; set; }

        public ValidationErrorResponse(string title, string detail, Dictionary<string, string> errors)
        {
            Title = title;
            Detail = detail;
            Errors = errors;
        }
    }
}
