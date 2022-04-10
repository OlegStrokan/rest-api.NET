using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Commands.DeleteNote;

public class DeleteNoteCommandHandler: IRequestHandler<DeleteNoteCommand>
{
    private readonly INotesDbContext _dbContext;

    public DeleteNoteCommandHandler(INotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Unit тип - тип, означающий пустой обьект
    
    public async Task<Unit> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var enitity = await _dbContext.Notes.FindAsync(new object[] {request.Id}, cancellationToken);

        if (enitity == null | enitity?.UserId != request.UserId)
        {
            throw new NotFoundException(nameof(Note), request.Id);
        }

        _dbContext.Notes.Remove(enitity);

        await _dbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}