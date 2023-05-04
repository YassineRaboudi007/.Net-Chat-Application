namespace ChatApplication.Application.Messages.Commands.CreateMessage
{
    public record CreateMessageCommandDTO(Guid userId,Guid roomId,string message);
}
