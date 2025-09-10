using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace AzureFunctionSample.Shared;

internal static class SampleErrorResult
{
    internal static ObjectResult GetError(string message)
    {
        return new ObjectResult($"Internal Server Error: {message}")
        {
            StatusCode = (int)HttpStatusCode.InternalServerError,
        };
    }
}