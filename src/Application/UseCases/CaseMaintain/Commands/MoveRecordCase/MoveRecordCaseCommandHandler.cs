using MediatR;
using WoundCareApi.Application.Common.Results;
using WoundCareApi.Infrastructure.Persistence;

namespace WoundCareApi.Application.UseCases.CaseMaintain.Commands.MoveRecordCase;

public record MoveRecordCaseCommand : IRequest<Result>
{
    public string RecordId { get; init; }
    public string ToCaseId { get; init; }
    public string UserId { get; init; }
}

public class MoveRecordCaseCommandHandler : IRequestHandler<MoveRecordCaseCommand, Result>
{
    private readonly CRSDbContext _context;
    private readonly ILogger<MoveRecordCaseCommandHandler> _logger;

    public MoveRecordCaseCommandHandler(
        CRSDbContext context,
        ILogger<MoveRecordCaseCommandHandler> logger
    )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(
        MoveRecordCaseCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _logger.LogInformation(
                "開始移動記錄: RecordId={RecordId}, ToCaseId={ToCaseId}",
                request.RecordId,
                request.ToCaseId
            );

            if (!Guid.TryParse(request.RecordId, out var recordGuid))
            {
                return Result.Failure("無效的記錄ID格式");
            }

            if (!Guid.TryParse(request.ToCaseId, out var toCaseGuid))
            {
                return Result.Failure("無效的目標案例ID格式");
            }

            using var transaction = await _context.Database.BeginTransactionAsync(
                cancellationToken
            );
            try
            {
                var record = await _context.CRS_CaseRecords.FindAsync(
                    new object[] { recordGuid },
                    cancellationToken
                );
                var toCase = await _context.CRS_Cases.FindAsync(
                    new object[] { toCaseGuid },
                    cancellationToken
                );

                if (record == null || toCase == null)
                {
                    _logger.LogWarning(
                        "找不到記錄或案例: RecordId={RecordId}, ToCaseId={ToCaseId}",
                        request.RecordId,
                        request.ToCaseId
                    );
                    return Result.Failure(
                        $"找不到記錄或案例: recordId={request.RecordId}, toCaseId={request.ToCaseId}"
                    );
                }

                record.PtCasePuid = toCaseGuid;
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "成功移動記錄: RecordId={RecordId}, ToCaseId={ToCaseId}",
                    request.RecordId,
                    request.ToCaseId
                );

                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(
                    ex,
                    "移動記錄失敗: RecordId={RecordId}, ToCaseId={ToCaseId}",
                    request.RecordId,
                    request.ToCaseId
                );
                return Result.Failure($"移動記錄失敗: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "移動記錄時發生錯誤: RecordId={RecordId}, ToCaseId={ToCaseId}",
                request.RecordId,
                request.ToCaseId
            );
            return Result.Failure("移動記錄時發生錯誤");
        }
    }
}
