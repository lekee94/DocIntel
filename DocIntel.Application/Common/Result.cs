namespace DocIntel.Application.Common;

public class Result<T>
{
    public T? Value { get; }
    public Error Error { get; }
    public bool IsSuccess => Error == Error.None;
    public bool IsFailure => !IsSuccess;

    protected Result(T? value, Error error)
    {
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(value, Error.None);
    public static Result<T> Failure(Error error) => new(default, error);
}

public record Error(string Code, string Message)
{
    //TODO: define errors here
    public static readonly Error None = new(string.Empty, string.Empty);
    public static Error NotFound(string message) => new("NotFound", message);
    public static Error Validation(string message) => new("Validation", message);
    public static Error Conflict(string message) => new("Conflict", message);
}

public class Result
{
    public Error Error { get; }
    public bool IsSuccess => Error == Error.None;
    public bool IsFailure => !IsSuccess;

    protected Result(Error error) => Error = error;

    public static Result Success() => new(Error.None);
    public static Result Failure(Error error) => new(error);
}