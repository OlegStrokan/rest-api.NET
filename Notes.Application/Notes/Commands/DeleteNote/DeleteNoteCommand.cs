using MediatR;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommand: IRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}