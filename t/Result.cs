namespace t;
public class Result<T>
{
    public bool RequestSuccess { get; }
    public T? Data { get; }
    public string Message { get; }

    private Result(bool requestSuccess, T? data, string message)
    {
        RequestSuccess = requestSuccess;
        Data = data;
        Message = message;
    }

    public static Result<T> Success(T? data, string message)
    {
        return new Result<T>(true, data, message);
    }

    public static Result<T> Failure(string message)
    {
        return new Result<T>(false, default, message);
    }

}