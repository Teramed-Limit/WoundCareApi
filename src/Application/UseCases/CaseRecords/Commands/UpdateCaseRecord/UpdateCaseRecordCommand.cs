using System.Text.Json;
using MediatR;
using WoundCareApi.Application.Common.Results;
using WoundCareApi.Application.DTOs;
using WoundCareApi.Core.Domain.Entities;
using WoundCareApi.Core.Domain.Interfaces;
using WoundCareApi.Infrastructure.Persistence;
using WoundCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace WoundCareApi.Application.UseCases.CaseRecords.Commands.UpdateCaseRecord;

public record UpdateCaseRecordCommand(Guid ReportId, CaseFormDataDto FormData, string UserId)
    : IRequest<Result<Unit>>;

public class UpdateCaseRecordCommandHandler : IRequestHandler<UpdateCaseRecordCommand, Result<Unit>>
{
    private readonly IRepository<CRS_CaseRecord, CRSDbContext> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCaseRecordCommandHandler(
        IRepository<CRS_CaseRecord, CRSDbContext> repository,
        IUnitOfWork unitOfWork
    )
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Unit>> Handle(
        UpdateCaseRecordCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var updateReport = new CRS_CaseRecord
            {
                Puid = request.ReportId,
                FormData = JsonSerializer.Serialize(request.FormData.FormData),
                FormDefinePuid = request.FormData.FormDefinePuid,
                CareProviderId = request.UserId
            };

            await _repository.UpdatePartialAsync(
                updateReport,
                "Puid",
                "FormData",
                "FormDefinePuid",
                "CareProviderId"
            );
            await _unitOfWork.SaveAsync();

            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            return Result<Unit>.Failure(ex.Message);
        }
    }
}
