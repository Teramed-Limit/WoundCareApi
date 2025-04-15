using MediatR;
using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Application.Common.Results;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.UseCases.CaseMaintain.Commands.MoveCase;

public record MoveCaseCommand : IRequest<Result>
{
    public string FromCaseId { get; init; }
    public string ToCaseId { get; init; }
    public string UserId { get; init; }
}

public class MoveCaseCommandHandler : IRequestHandler<MoveCaseCommand, Result>
{
    private readonly CRSDbContext _context;
    private readonly ILogger<MoveCaseCommandHandler> _logger;

    public MoveCaseCommandHandler(CRSDbContext context, ILogger<MoveCaseCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(MoveCaseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation(
                "開始移動案例: FromCaseId={FromCaseId}, ToCaseId={ToCaseId}",
                request.FromCaseId,
                request.ToCaseId
            );

            if (!Guid.TryParse(request.FromCaseId, out var fromCaseGuid))
            {
                return Result.Failure("無效的來源案例ID格式");
            }

            if (!Guid.TryParse(request.ToCaseId, out var toCaseGuid))
            {
                return Result.Failure("無效的目標案例ID格式");
            }

            var fromCase = await _context.PtCases.FindAsync(
                new object[] { fromCaseGuid },
                cancellationToken
            );
            var toCase = await _context.PtCases.FindAsync(
                new object[] { toCaseGuid },
                cancellationToken
            );

            if (fromCase == null || toCase == null)
            {
                _logger.LogWarning(
                    "找不到案例: FromCaseId={FromCaseId}, ToCaseId={ToCaseId}",
                    request.FromCaseId,
                    request.ToCaseId
                );
                return Result.Failure(
                    $"找不到案例: fromCase={request.FromCaseId}, toCase={request.ToCaseId}"
                );
            }

            if (
                fromCase.LIfeTimeNumber != toCase.LIfeTimeNumber
                || fromCase.EncounterNumber != toCase.EncounterNumber
            )
            {
                _logger.LogWarning(
                    "案例必須屬於同一個病人和就醫紀錄: FromCaseId={FromCaseId}, ToCaseId={ToCaseId}",
                    request.FromCaseId,
                    request.ToCaseId
                );
                return Result.Failure("案例必須屬於同一個病人和就醫紀錄");
            }

            using var transaction = await _context.Database.BeginTransactionAsync(
                cancellationToken
            );
            try
            {
                await _context.PtCaseRecords
                    .Where(x => x.PtCasePuid == fromCaseGuid)
                    .ExecuteUpdateAsync(
                        s => s.SetProperty(b => b.PtCasePuid, toCaseGuid),
                        cancellationToken
                    );

                await _context.DicomSeriesMaps
                    .Where(x => x.PtCasePuid == fromCaseGuid)
                    .ExecuteUpdateAsync(
                        s => s.SetProperty(b => b.PtCasePuid, toCaseGuid),
                        cancellationToken
                    );

                _context.Update(fromCase);
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "成功移動案例: FromCaseId={FromCaseId}, ToCaseId={ToCaseId}",
                    request.FromCaseId,
                    request.ToCaseId
                );

                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(
                    ex,
                    "移動案例失敗: FromCaseId={FromCaseId}, ToCaseId={ToCaseId}",
                    request.FromCaseId,
                    request.ToCaseId
                );
                return Result.Failure($"移動案例失敗: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "移動案例時發生錯誤: FromCaseId={FromCaseId}, ToCaseId={ToCaseId}",
                request.FromCaseId,
                request.ToCaseId
            );
            return Result.Failure("移動案例時發生錯誤");
        }
    }
}
