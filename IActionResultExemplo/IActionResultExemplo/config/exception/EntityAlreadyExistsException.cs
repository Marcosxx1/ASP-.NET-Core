namespace IActionResultExemplo.config.exception
{
    public class EntityAlreadyExistsException : Exception
    {
        public string Title { get; }
        public string Detail { get; }

        public EntityAlreadyExistsException(string title, string detail)
            : base($"{title}: {detail}")
        {
            Title = title;
            Detail = detail;
        }

        public EntityAlreadyExistsException(string title, string detail, Exception innerException)
            : base($"{title}: {detail}", innerException)
        {
            Title = title;
            Detail = detail;
        }
    }
}