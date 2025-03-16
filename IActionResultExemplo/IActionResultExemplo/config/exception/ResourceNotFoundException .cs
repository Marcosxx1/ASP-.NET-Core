using System.Reflection;

namespace IActionResultExemplo.config.exception
{
    public class ResourceNotFoundException : Exception
    {
        public string Title {get;set;}
        public string Detail { get;set;}

        public ResourceNotFoundException(string title, string detail)
            : base($"{title}: {detail}")
        {
            Title = title; 
            Detail = detail;
        }
    }
}
