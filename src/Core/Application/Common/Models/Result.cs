namespace WoundCareApi.Core.Application.Common.Models;

public class Result<T>
{
    public bool Succeeded { get; private set; }
    public string Error { get; private set; }
    public T Data { get; private set; }

    private Result(bool succeeded, string error, T data)
    {
        Succeeded = succeeded;
        Error = error;
        Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, string.Empty, data);
    }

    public static Result<T> Failure(string error)
    {
        return new Result<T>(false, error, default);
    }
}
