namespace backend.Auth;

public class EmailAlreadyExistsException : Exception
{
    private const string ExceptionMessage = "Email already in use.";

    public EmailAlreadyExistsException() : base(ExceptionMessage) {}
}

public class IncorrectLoginDetailsException : Exception
{
    private const string ExceptionMessage = "Invalid login credentials.";

    public IncorrectLoginDetailsException() : base(ExceptionMessage) {}
}

public class InactiveStatusException : Exception
{
    private const string ExceptionMessage = "Inactive employees cannot log in.";

    public InactiveStatusException() : base(ExceptionMessage) {}
}
