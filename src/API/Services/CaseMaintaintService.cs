using Microsoft.EntityFrameworkCore;
using WoundCareApi.src.Infrastructure.Persistence;

namespace WoundCareApi.API.Services;

public class CaseMaintainService
{
    private readonly CRSDbContext _context;

    private readonly ILogger<CaseMaintainService> _logger;

    public CaseMaintainService(CRSDbContext context, ILogger<CaseMaintainService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task MoveCase(string fromCaseId, string toCaseId, string userId)
    {
        try
        {
            // 檢查fromCase和toCase是否存在
            var fromCase = await _context.CRS_Cases.FindAsync(Guid.Parse(fromCaseId));
            var toCase = await _context.CRS_Cases.FindAsync(Guid.Parse(toCaseId));

            if (fromCase == null || toCase == null)
            {
                throw new InvalidOperationException(
                    $"Case not found: fromCase={fromCaseId}, toCase={toCaseId}"
                );
            }

            // 檢查fromCase和toCase是否在同一個病人底下
            if (
                fromCase.LIfeTimeNumber != toCase.LIfeTimeNumber
                || fromCase.EncounterNumber != toCase.EncounterNumber
            )
            {
                throw new InvalidOperationException(
                    "Cases must belong to the same patient and encounter"
                );
            }

            // 使用交易來確保資料一致性
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 批量更新 CaseRecords
                await _context.CRS_CaseRecords
                    .Where(x => x.PtCasePuid == Guid.Parse(fromCaseId))
                    .ExecuteUpdateAsync(
                        s => s.SetProperty(b => b.PtCasePuid, Guid.Parse(toCaseId))
                    );

                // 批量更新 CareSeriesMaps
                await _context.CRS_CareSeriesMaps
                    .Where(x => x.PtCasePuid == Guid.Parse(fromCaseId))
                    .ExecuteUpdateAsync(
                        s => s.SetProperty(b => b.PtCasePuid, Guid.Parse(toCaseId))
                    );

                // 標記原始 Case 為已關閉
                _context.Update(fromCase);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Failed to move case: {ex.Message}", ex);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error moving case from {FromCaseId} to {ToCaseId}",
                fromCaseId,
                toCaseId
            );
            throw;
        }
    }

    public async Task MoveSeriesToCase(string seriesInstanceUid, string toCaseId, string userId)
    {
        try
        {
            _logger.LogInformation(
                "Starting to move series {SeriesId} to case {CaseId}",
                seriesInstanceUid,
                toCaseId
            );

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var series = await _context.CRS_CareSeriesMaps.FirstOrDefaultAsync(
                    x => x.DicomSeriesUid == seriesInstanceUid
                );
                var toCase = await _context.CRS_Cases.FindAsync(Guid.Parse(toCaseId));

                if (series == null || toCase == null)
                {
                    throw new InvalidOperationException(
                        $"Series or case not found: seriesInstanceUid={seriesInstanceUid}, toCaseId={toCaseId}"
                    );
                }

                series.PtCasePuid = Guid.Parse(toCaseId);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation(
                    "Successfully moved series {SeriesId} to case {CaseId}",
                    seriesInstanceUid,
                    toCaseId
                );
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(
                    ex,
                    "Failed to move series {SeriesId} to case {CaseId}",
                    seriesInstanceUid,
                    toCaseId
                );
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MoveSeriesToCase");
            throw;
        }
    }

    public async Task MoveRecordCase(string recordId, string toCaseId, string userId)
    {
        try
        {
            _logger.LogInformation(
                "Starting to move record {RecordId} to case {CaseId}",
                recordId,
                toCaseId
            );

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var record = await _context.CRS_CaseRecords.FindAsync(Guid.Parse(recordId));
                var toCase = await _context.CRS_Cases.FindAsync(Guid.Parse(toCaseId));

                if (record == null || toCase == null)
                {
                    throw new InvalidOperationException(
                        $"Record or case not found: recordId={recordId}, toCaseId={toCaseId}"
                    );
                }

                record.PtCasePuid = Guid.Parse(toCaseId);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                _logger.LogInformation(
                    "Successfully moved record {RecordId} to case {CaseId}",
                    recordId,
                    toCaseId
                );
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(
                    ex,
                    "Failed to move record {RecordId} to case {CaseId}",
                    recordId,
                    toCaseId
                );
                throw;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in MoveRecordCase");
            throw;
        }
    }
}
