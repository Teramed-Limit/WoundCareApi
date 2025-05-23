using MediatR;
using TeraLinkaCareApi.Application.Common.Results;
using TeraLinkaCareApi.Application.Enums;
using TeraLinkaCareApi.Common.Extensions;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.UseCases.Cases.Commands.SetCaseStatus;

public record SetCaseStatusCommand(Guid CaseId, bool IsClosed, string UserId) : IRequest<Result<bool>>;

public class SetCaseStatusCommandHandler : IRequestHandler<SetCaseStatusCommand, Result<bool>>
{
    private readonly CRSDbContext _context;

    public SetCaseStatusCommandHandler(CRSDbContext context)
    {
        _context = context;
    }

    public async Task<Result<bool>> Handle(
        SetCaseStatusCommand request,
        CancellationToken cancellationToken
    )
    {
        var case_ = await _context.PtCases.FindAsync(request.CaseId);

        if (case_ == null)
        {
            return Result<bool>.Failure($"找不到案例 ID: {request.CaseId}");
        }

        if (request.IsClosed)
        {
            case_.CaseCloseTime = DateTime.UtcNow;
            case_.CaseCloseCareProviderName = request.UserId;
            case_.CaseCloseStatus = CaseStatus.Closed.GetDescription();
            case_.ModifiedDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            case_.ModifiedUser = request.UserId;
        }
        else
        {
            case_.CaseCloseTime = null;
            case_.CaseCloseCareProviderName = request.UserId;
            case_.CaseCloseStatus = CaseStatus.InProgress.GetDescription();
            case_.ModifiedDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            case_.ModifiedUser = request.UserId;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);
    }
}