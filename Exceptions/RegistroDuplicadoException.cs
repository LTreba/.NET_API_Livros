namespace LibraryApi.Exceptions;

public class RegistroDuplicadoException : Exception
{
    public RegistroDuplicadoException() : base("Registro Duplicado") { }
}
