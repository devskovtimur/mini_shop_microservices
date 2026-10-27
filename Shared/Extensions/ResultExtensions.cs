using Microsoft.AspNetCore.Http;

namespace Shared.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResponse<T>(this Result<T> result, IResult? successResult = null)
    {
        if (result.IsSuccess)
        {
            return successResult ?? Results.Ok(result.Value);
        }

        var message = result.Error!.Message;

        return result.Error!.Code switch
        {
            Error.INVALID_DATA => Results.BadRequest(message),
            Error.NOT_FOUND => Results.NotFound(message),
            Error.CONFLICT => Results.Conflict(message),

            _ => Results.InternalServerError(result.Error)
        };
    }
}