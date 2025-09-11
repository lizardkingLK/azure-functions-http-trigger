namespace AzureFunctionSample.Shared;

public record TokenState(string AccessToken, DateTime ExpireDate);