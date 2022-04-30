using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Chats.Commands;

public class CreateChatCommand : IRequest<Guid>
{
    public Guid ContactId { get; set; } 
}

public class CreateChatQueryHandler : IRequestHandler<CreateChatCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public CreateChatQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<Guid> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        var anotherUser = _context.ApplicationUsers.FirstOrDefault(user => user.Id == request.ContactId);
        var currentUser = _context.ApplicationUsers.FirstOrDefault(user => user.Id == _currentUserService.UserIdGuid);
        
        if (currentUser == null || anotherUser == null) throw new NotImplementedException();
        
        var myChatUserList = _context.ChatUsers.Where(chatUser => chatUser.ApplicationUserId == currentUser.Id).ToList();
        var myChatIds = new List<Guid>();
        foreach (var myChatUser in myChatUserList)
        {
            myChatIds.Add(myChatUser.ChatId);
        }
        var chatAlreadyExists = _context.ChatUsers.FirstOrDefault(chatUser => myChatIds.Contains(chatUser.ChatId) && chatUser.ApplicationUserId == anotherUser.Id);
        if (chatAlreadyExists != null) throw new NotImplementedException($"Chat already exists, with id: {chatAlreadyExists.Id}");
        
        var chat = new Chat()
        {
            LastMessage = "Hi"
        };
        _context.Chats.Add(chat);
        //_context.SaveChangesAsync(cancellationToken);
        var currentUserChat = new ChatUser()
        {
            ChatId = chat.Id,
            ApplicationUserId = currentUser.Id,
            Name = anotherUser.UserName
        };
        _context.ChatUsers.Add(currentUserChat);
        var anotherUserChat = new ChatUser() {
            ChatId = chat.Id,
            ApplicationUserId = anotherUser.Id,
            Name = currentUser.UserName
        };
        _context.ChatUsers.Add(anotherUserChat);
        

        await _context.SaveChangesAsync(cancellationToken);
        
        return await Task.FromResult(chat.Id);
    }
}