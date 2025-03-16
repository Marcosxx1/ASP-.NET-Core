namespace IActionResultExemplo.config.exception
{
    public class ExceptionFactory
    {
        public static EntityAlreadyExistsException EntityAlreadyExists(string identifier) {
            return new EntityAlreadyExistsException(
                $"Recurso '{identifier}' já existe.",
                $" Já existe um atendente com o e-mail: {identifier}"
                );
        }

        public static ResourceNotFoundException ResourceNotFound()
        {
            return new ResourceNotFoundException(
                "Recurso não encontrado.",
                "O recurso solicitado não foi localizado."
            );
        }
    }
}
