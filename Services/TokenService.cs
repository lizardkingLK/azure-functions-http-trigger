using System.Collections.Concurrent;
using AzureFunctionSample.Shared;
using Microsoft.Extensions.Logging;

namespace AzureFunctionSample.Services;

public class TokenService(ILogger<TokenService> logger)
{
    private readonly ILogger<TokenService> _logger = logger;

    private readonly string alphabet = string.Join(null, Enumerable
    .Range(0, 26)
    .Select(item => (char)('a' + item)));

    private const int Size = 10;

    private const int TokenSize = 256;

    private readonly ConcurrentBag<TokenState> _tokens = [];

    private int cycle = 0;

    public (TokenState?, int) GetToken()
    {
        _logger.LogInformation(nameof(GetToken));

        if (_tokens.IsEmpty)
        {
            return (null, cycle);
        }

        TokenState? token = null;
        // object lockObject = new();
        // lock (lockObject)
        // {
            token = _tokens.ElementAt(Random.Shared.Next(Size));
        // }

        return (token, cycle);
    }

    public void SetTokens()
    {
        _logger.LogInformation(nameof(SetTokens));

        for (int i = 0; i < Size; i++)
        {
            _tokens.Add(CreateAccessToken());
        }

        cycle++;
    }

    private TokenState CreateAccessToken()
    => new(string.Join(null, Enumerable
        .Range(0, TokenSize)
        .Select(item => alphabet[Random.Shared.Next(Size)])), DateTime.Now.AddHours(1));
}