namespace Shared;

public record Error(string Code, string Message)
{
    public static readonly Error None = new(string.Empty, string.Empty);

    public static Error InvalidData(string description) => new(INVALID_DATA, description);
    public static Error NotFound(string description) => new(NOT_FOUND, description);
    public static Error Conflict(string description) => new(CONFLICT, description);


    public const string INVALID_DATA = "Invalid data";
    public const string NOT_FOUND = "Not found";
    public const string CONFLICT = "Conflict";
}