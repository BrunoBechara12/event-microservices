namespace t;

public class Result<T>
{
    public bool RequestSuccess { get; }
    public T? Value { get; }
    public string Message { get; }

    private Result(bool requestSuccess, T? value, string message)
    {
        RequestSuccess = requestSuccess;
        Value = value;
        Message = message;
    }

    public static Result<T> Success(T? value, string message)
    {
        return new Result<T>(true, value, message);
    }

    public static Result<T> Failure(string message)
    {
        return new Result<T>(false, default, message);
    }

}