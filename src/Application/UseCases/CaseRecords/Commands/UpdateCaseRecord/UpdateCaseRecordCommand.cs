using System.Text.Json;
using MediatR;
using TeraLinkaCareApi.Application.Common.Results;
using TeraLinkaCareApi.Application.DTOs;
using TeraLinkaCareApi.Core.Domain.Entities;
using TeraLinkaCareApi.Core.Domain.Interfaces;
using TeraLinkaCareApi.Infrastructure.Persistence;
using TeraLinkaCareApi.Infrastructure.Persistence.UnitOfWork.Interfaces;

namespace TeraLinkaCareApi.Application.UseCases.CaseRecords.Commands.UpdateCaseRecord;

public record UpdateCaseRecordCommand(Guid ReportId, CaseFormDataDto FormData, string UserId)
    : IRequest<Result<Unit>>;

public class UpdateCaseRecordCommandHandler : IRequestHandler<UpdateCaseRecordCommand, Result<Unit>>
{
    private readonly IRepository<PtCaseRecord, CRSDbContext> _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCaseRecordCommandHandler(
        IRepository<PtCaseRecord, CRSDbContext> repository,
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
            var updateReport = new PtCaseRecord
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
