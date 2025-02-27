using CecoChat.Contracts.Messaging;
using CecoChat.Data;
using FluentValidation;

namespace CecoChat.Server.Messaging.Endpoints;

public interface IInputValidator
{
    AbstractValidator<SendMessageRequest> SendMessageRequest { get; }

    AbstractValidator<ReactRequest> ReactRequest { get; }

    AbstractValidator<UnReactRequest> UnReactRequest { get; }
}

public sealed class InputValidator : IInputValidator
{
    public AbstractValidator<SendMessageRequest> SendMessageRequest { get; } = new SendMessageRequestValidator();

    public AbstractValidator<ReactRequest> ReactRequest { get; } = new ReactRequestValidator();

    public AbstractValidator<UnReactRequest> UnReactRequest { get; } = new UnReactRequestValidator();
}

public sealed class SendMessageRequestValidator : AbstractValidator<SendMessageRequest>
{
    public SendMessageRequestValidator()
    {
        RuleFor(x => x.ReceiverId)
            .ValidUserId();
        RuleFor(x => x.Data)
            .ValidMessageData();
    }
}

public sealed class ReactRequestValidator : AbstractValidator<ReactRequest>
{
    public ReactRequestValidator()
    {
        RuleFor(x => x.MessageId)
            .ValidMessageId();
        RuleFor(x => x.SenderId)
            .ValidUserId();
        RuleFor(x => x.ReceiverId)
            .ValidUserId();
        RuleFor(x => x.Reaction)
            .ValidReaction();
    }
}

public sealed class UnReactRequestValidator : AbstractValidator<UnReactRequest>
{
    public UnReactRequestValidator()
    {
        RuleFor(x => x.MessageId)
            .ValidMessageId();
        RuleFor(x => x.SenderId)
            .ValidUserId();
        RuleFor(x => x.ReceiverId)
            .ValidUserId();
    }
}
