using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Chats.Queries.GetMessagesByCurrentUser;

public class GetMessagesByChatIdQuery : IRequest<List<Message>>
{
    public Guid ChatId { get; set; }
}

public class GetMessagesByChatIdQueryHandler : IRequestHandler<GetMessagesByChatIdQuery, List<Message>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetMessagesByChatIdQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public Task<List<Message>> Handle(GetMessagesByChatIdQuery request, CancellationToken cancellationToken)
    {
        //var user = _context.ApplicationUsers.FirstOrDefault(user => user.Id == _currentUserService.UserIdGuid);
        //if (user == null) throw new NotImplementedException($"User by id: {_currentUserService.UserId}, not found.");

        var chat = _context.ChatUsers.FirstOrDefault(chatUser =>
            chatUser.ApplicationUserId == _currentUserService.UserIdGuid && chatUser.ChatId == request.ChatId);
        if (chat == null) throw new NotImplementedException($"Chat by id: {request.ChatId}, not found.");

        var messages = _context.Messages.Where(message => message.ChatId == chat.Id).ToList();
        return Task.FromResult<List<Message>>(messages);
    }
}