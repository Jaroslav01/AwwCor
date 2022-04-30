using CleanArchitecture.Application.Common.Interfaces;
using MediatR;
using CleanArchitecture.Domain.Entities;
namespace CleanArchitecture.Application.Chats.Queries.GetChatByCurrentUser;

public class GetChatByCurrentUserQuery: IRequest<List<Chat>>
{
    
}

public class GetChatByCurrentUserQueryHandler : IRequestHandler<GetChatByCurrentUserQuery, List<Chat>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetChatByCurrentUserQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public Task<List<Chat>> Handle(GetChatByCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var chatUserList = _context.ChatUsers.Where(x => x.ApplicationUserId == _currentUserService.UserIdGuid)
            .ToList();
        var chatIds = new List<Guid>();
        foreach (var chatUser in chatUserList)
        {
            chatIds.Add(chatUser.ChatId);
        }
        var chats = _context.Chats.Where(chat => chatIds.Contains(chat.Id) ).ToList();
        if (chats.Count < 1)
        {
            throw new NotImplementedException();
        }

        return Task.FromResult(chats);
    }
}