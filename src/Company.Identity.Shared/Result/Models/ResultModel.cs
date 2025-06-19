namespace Company.Identity.Shared.Result.Models;

public class ResultModel<T>
{
    private ResultModel(bool success, T value, List<ErrorDetailModel> errors)
    {
        Success = success;
        Value = value;
        Errors = errors;
    }

    public bool Success { get; }

    public T Value { get; }

    public List<ErrorDetailModel> Errors { get; }

    public static ResultModel<T> Ok(T value)
    {
        var result = new ResultModel<T>(true, value, []);
        return result;
    }

    public static ResultModel<T> Fail(string code, string message)
    {
        var error = new ErrorDetailModel
        {
            Code = code,
            Message = message
        };

        var result = new ResultModel<T>(false, default!, [error]);
        return result;
    }

    public static ResultModel<T> Fail(List<ErrorDetailModel> errors)
    {
        var result = new ResultModel<T>(false, default!, errors);
        return result;
    }
}