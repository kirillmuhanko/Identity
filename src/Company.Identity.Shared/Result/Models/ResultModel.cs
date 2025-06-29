using System.Text.Json.Serialization;

namespace Company.Identity.Shared.Result.Models;

public class ResultModel<T> where T : notnull
{
    private readonly Dictionary<string, List<string>> _errors = new();

    [JsonInclude] private int Status { get; set; }
    [JsonInclude] private string Title { get; set; } = "Success";
    [JsonInclude] private string Type { get; set; } = "about:blank";
    [JsonInclude] private string? TraceId { get; set; }

    public T Value { get; private set; } = default!;

    [JsonInclude]
    [JsonPropertyName("errors")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    private Dictionary<string, List<string>>? Errors =>
        _errors.Count > 0 ? new Dictionary<string, List<string>>(_errors) : null;

    public bool IsSuccess => Status is >= 200 and < 300;
    public bool HasErrors => _errors.Count > 0;

    public void AddError(string key, string message)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentException.ThrowIfNullOrWhiteSpace(message);

        if (!_errors.ContainsKey(key))
            _errors[key] = [];

        _errors[key].Add(message);
    }

    private void CopyErrorsFrom<TFrom>(ResultModel<TFrom> source) where TFrom : notnull
    {
        if (source.Errors is null) return;

        foreach (var (key, messages) in source.Errors)
        foreach (var msg in messages)
            AddError(key, msg);
    }

    public static ResultModel<T> Fail(
        string title = "Error",
        int status = 400,
        string type = "https://tools.ietf.org/html/rfc9110#section-15.5.1")
    {
        return new ResultModel<T>
        {
            Status = status,
            Title = title,
            Type = type
        };
    }

    public static ResultModel<T> FailFrom<TFrom>(ResultModel<TFrom> source) where TFrom : notnull
    {
        var result = Fail(source.Title, source.Status, source.Type);
        result.TraceId = source.TraceId;
        result.CopyErrorsFrom(source);
        return result;
    }

    public static ResultModel<T> FailWithError(string key, string message, int status = 400)
    {
        var result = Fail(status: status);
        result.AddError(key, message);
        return result;
    }

    public static ResultModel<T> Ok(T data)
    {
        ArgumentNullException.ThrowIfNull(data);

        return new ResultModel<T>
        {
            Status = 200,
            Value = data
        };
    }
}