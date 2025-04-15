using MediatR;
using Microsoft.EntityFrameworkCore;
using TeraLinkaCareApi.Application.Common.Results;
using TeraLinkaCareApi.Infrastructure.Persistence;

namespace TeraLinkaCareApi.Application.UseCases.CaseMaintain.Commands.MoveSeriesToCase;

public record MoveSeriesToCaseCommand : IRequest<Result>
{
    public string SeriesInstanceUid { get; init; }
    public string ToCaseId { get; init; }
    public string UserId { get; init; }
}

public class MoveSeriesToCaseCommandHandler : IRequestHandler<MoveSeriesToCaseCommand, Result>
{
    private readonly CRSDbContext _context;
    private readonly ILogger<MoveSeriesToCaseCommandHandler> _logger;

    public MoveSeriesToCaseCommandHandler(
        CRSDbContext context,
        ILogger<MoveSeriesToCaseCommandHandler> logger
    )
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result> Handle(
        MoveSeriesToCaseCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _logger.LogInformation(
                "開始移動系列: SeriesId={SeriesId}, ToCaseId={ToCaseId}",
                request.SeriesInstanceUid,
                request.ToCaseId
            );

            if (!Guid.TryParse(request.ToCaseId, out var toCaseGuid))
            {
                return Result.Failure("無效的目標案例ID格式");
            }

            using var transaction = await _context.Database.BeginTransactionAsync(
                cancellationToken
            );
            try
            {
                var series = await _context.DicomSeriesMaps.FirstOrDefaultAsync(
                    x => x.DicomSeriesUid == request.SeriesInstanceUid,
                    cancellationToken
                );
                var toCase = await _context.PtCases.FindAsync(
                    new object[] { toCaseGuid },
                    cancellationToken
                );

                if (series == null || toCase == null)
                {
                    _logger.LogWarning(
                        "找不到系列或案例: SeriesId={SeriesId}, ToCaseId={ToCaseId}",
                        request.SeriesInstanceUid,
                        request.ToCaseId
                    );
                    return Result.Failure(
                        $"找不到系列或案例: seriesInstanceUid={request.SeriesInstanceUid}, toCaseId={request.ToCaseId}"
                    );
                }

                series.PtCasePuid = toCaseGuid;
                await _context.SaveChangesAsync(cancellationToken);
                await transaction.CommitAsync(cancellationToken);

                _logger.LogInformation(
                    "成功移動系列: SeriesId={SeriesId}, ToCaseId={ToCaseId}",
                    request.SeriesInstanceUid,
                    request.ToCaseId
                );

                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(cancellationToken);
                _logger.LogError(
                    ex,
                    "移動系列失敗: SeriesId={SeriesId}, ToCaseId={ToCaseId}",
                    request.SeriesInstanceUid,
                    request.ToCaseId
                );
                return Result.Failure($"移動系列失敗: {ex.Message}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "移動系列時發生錯誤: SeriesId={SeriesId}, ToCaseId={ToCaseId}",
                request.SeriesInstanceUid,
                request.ToCaseId
            );
            return Result.Failure("移動系列時發生錯誤");
        }
    }
}
