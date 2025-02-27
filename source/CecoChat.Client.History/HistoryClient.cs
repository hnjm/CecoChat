using CecoChat.Contracts.History;
using CecoChat.Grpc;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CecoChat.Client.History;

public interface IHistoryClient : IDisposable
{
    Task<IReadOnlyCollection<HistoryMessage>> GetHistory(long userId, long otherUserId, DateTime olderThan, string accessToken, CancellationToken ct);
}

internal sealed class HistoryClient : IHistoryClient
{
    private readonly ILogger _logger;
    private readonly HistoryOptions _options;
    private readonly Contracts.History.History.HistoryClient _client;
    private readonly IClock _clock;

    public HistoryClient(
        ILogger<HistoryClient> logger,
        IOptions<HistoryOptions> options,
        Contracts.History.History.HistoryClient client,
        IClock clock)
    {
        _logger = logger;
        _options = options.Value;
        _client = client;
        _clock = clock;

        _logger.LogInformation("History address set to {Address}", _options.Address);
    }

    public void Dispose()
    {
        // nothing to dispose for now, but keep the IDisposable as part of the contract
    }

    public async Task<IReadOnlyCollection<HistoryMessage>> GetHistory(long userId, long otherUserId, DateTime olderThan, string accessToken, CancellationToken ct)
    {
        GetHistoryRequest request = new()
        {
            OtherUserId = otherUserId,
            OlderThan = olderThan.ToTimestamp()
        };

        Metadata headers = new();
        headers.AddAuthorization(accessToken);
        DateTime deadline = _clock.GetNowUtc().Add(_options.CallTimeout);
        GetHistoryResponse response = await _client.GetHistoryAsync(request, headers, deadline, ct);

        _logger.LogTrace("Received {MessageCount} messages for history between {UserId} and {OtherUserId} older than {OlderThan}", response.Messages.Count, userId, otherUserId, olderThan);
        return response.Messages;
    }
}
