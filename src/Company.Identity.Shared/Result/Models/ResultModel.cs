using System.Text.Json.Serialization;

namespace Company.Identity.Shared.Result.Models;

public class ResultModel<T>
{
    [JsonIgnore] private Dictionary<string, List<string>>? _errors;

    public int Status { get; set; }

    public string Title { get; set; } = "Success";

    public string Type { get; set; } = "about:blank";

    public string? TraceId { get; set; }

    public T? Value { get; set; }

    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IReadOnlyDictionary<string, IReadOnlyList<string>>? Errors =>
        _errors?.ToDictionary(kvp => kvp.Key, kvp => (IReadOnlyList<string>)kvp.Value);

    public bool IsSuccess => Status is >= 200 and < 300;

    public void AddError(string key, string message)
    {
        _errors ??= new Dictionary<string, List<string>>();

        if (!_errors.ContainsKey(key))
            _errors[key] = [];

        _errors[key].Add(message);
    }

    public static ResultModel<T> Fail(
        string title = "Error",
        int status = 400,
        string type = "https://tools.ietf.org/html/rfc9110#section-15.5.1")
    {
        return new ResultModel<T> { Status = status, Title = title, Type = type };
    }

    public static ResultModel<T> FailFrom<TFrom>(ResultModel<TFrom> source)
    {
        var result = Fail(source.Title, source.Status, source.Type);
        result.TraceId = source.TraceId;

        if (source.Errors is not null)
            foreach (var (key, messages) in source.Errors)
            foreach (var msg in messages)
                result.AddError(key, msg);

        return result;
    }

    public static ResultModel<T> Ok(T data)
    {
        return new ResultModel<T> { Status = 200, Value = data };
    }
}