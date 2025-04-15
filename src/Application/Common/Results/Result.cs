namespace TeraLinkaCareApi.Application.Common.Results;

public class Result
{
    public bool Succeeded { get; protected set; }
    public string Error { get; protected set; }

    protected Result(bool succeeded, string error)
    {
        Succeeded = succeeded;
        Error = error;
    }

    public static Result Success()
    {
        return new Result(true, string.Empty);
    }

    public static Result Failure(string error)
    {
        return new Result(false, error);
    }
}

public class Result<T> : Result
{
    public T Data { get; private set; }

    protected Result(bool succeeded, string error, T data)
        : base(succeeded, error)
    {
        Data = data;
    }

    public static Result<T> Success(T data)
    {
        return new Result<T>(true, string.Empty, data);
    }

    public new static Result<T> Failure(string error)
    {
        return new Result<T>(false, error, default);
    }
}
