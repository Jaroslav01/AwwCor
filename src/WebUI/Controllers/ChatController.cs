using CleanArchitecture.Application.Chats.Commands;
using CleanArchitecture.Application.Chats.Queries.GetChatByCurrentUser;
using CleanArchitecture.Application.Chats.Queries.GetMessagesByCurrentUser;
using CleanArchitecture.Application.ChatsUsers.Queries.GetChatUsersByCurrentUser;
using CleanArchitecture.Application.Common.Security;
using CleanArchitecture.Application.Messages.Command.GreateMessage;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.WebUI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ChatController : Controller
{
    private ISender _mediator = null!;

    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    [HttpPost("CreateChat")]
    public async Task<Guid> CreateChat(CreateChatCommand command)
    {
        return await Mediator.Send(command); 
    }
    
    [HttpGet("GetChatByCurrentUser")]
    public async Task<List<Chat>> GetChatByCurrentUser()
    {
        return await Mediator.Send(new GetChatByCurrentUserQuery());
    }
    
    [HttpGet("GetChatUsersByCurrentUser")]
    public async Task<List<ChatUser>> GetChatUsersByCurrentUser()
    {
        return await Mediator.Send(new GetChatUsersByCurrentUserQuery());
    }
    
    [HttpPost("CreateMessage")]
    public async Task<Guid> GetChatUsersByCurrentUser(CreateMessageCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [HttpPost("GetMessagesByChatId")]
    public async Task<List<Message>> GetMessagesByChatId(GetMessagesByChatIdQuery query)
    {
        return await Mediator.Send(query);
    }
}