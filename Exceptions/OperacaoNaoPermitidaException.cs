namespace LibraryApi.Exceptions;

public class OperacaoNaoPermitidaException : Exception
{
    public OperacaoNaoPermitidaException(string message) : base(message) { }
}
