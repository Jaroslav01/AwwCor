using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.User.Queries.Dto;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Messages.Command.GreateMessage;

public class CreateMessageCommand : IRequest<Guid>
{
    public Guid ChatId { get; set; }
    public string MessageText { get; set; }
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateMessageCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var chat = _context.ChatUsers.FirstOrDefault(chatUser => chatUser.ChatId == request.ChatId && chatUser.ApplicationUserId == _currentUserService.UserIdGuid);
        if (chat == null) throw new NotImplementedException();
        
        var message = new Message()
        {
            ChatId = request.ChatId,
            MessageText = request.MessageText
        };
        
        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);
        return message.Id;
    }
}