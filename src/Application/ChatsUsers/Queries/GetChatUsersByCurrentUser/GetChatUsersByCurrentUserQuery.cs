using CleanArchitecture.Application.Chats.Queries.GetChatByCurrentUser;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.ChatsUsers.Queries.GetChatUsersByCurrentUser;

public class GetChatUsersByCurrentUserQuery : IRequest<List<ChatUser>>
{
    
}

public class GetChatUsersByCurrentUserQueryHandler : IRequestHandler<GetChatUsersByCurrentUserQuery, List<ChatUser>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly ISender _mediator;

    public GetChatUsersByCurrentUserQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, ISender mediator)
    {
        _context = context;
        _currentUserService = currentUserService;
        _mediator = mediator;
    }

    public async Task<List<ChatUser>> Handle(GetChatUsersByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var chats = await _mediator.Send(new GetChatByCurrentUserQuery());
        
        var chatsId = new List<Guid>();
        foreach (var chat in chats)
        {
           chatsId.Add(chat.Id); 
        }

        var chatUsers = _context.ChatUsers.Where(chatUser =>
            chatsId.Contains(chatUser.ChatId) && chatUser.ApplicationUserId == _currentUserService.UserIdGuid).ToList();
        return chatUsers;
    }
}
