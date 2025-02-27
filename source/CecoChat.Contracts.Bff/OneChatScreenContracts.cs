using System.Text.Json.Serialization;
using Refit;

namespace CecoChat.Contracts.Bff;

public sealed class GetOneChatScreenRequest
{
    [JsonPropertyName("otherUserId")]
    [AliasAs("otherUserId")]
    public long OtherUserId { get; init; }

    [JsonPropertyName("messagesOlderThan")]
    [AliasAs("messagesOlderThan")]
    public DateTime MessagesOlderThan { get; init; }

    [JsonPropertyName("includeProfile")]
    [AliasAs("includeProfile")]
    public bool IncludeProfile { get; init; }
}

public sealed class GetOneChatScreenResponse
{
    [JsonPropertyName("messages")]
    [AliasAs("messages")]
    public HistoryMessage[] Messages { get; init; } = Array.Empty<HistoryMessage>();

    [JsonPropertyName("profile")]
    [AliasAs("profile")]
    public ProfilePublic? Profile { get; init; } = null!;
}
