// using Microsoft.AspNetCore.Mvc;
// using WoundCareApi.Core.Domain.CRSPatientDataDB;
// using WoundCareApi.Persistence;
// using WoundCareApi.Persistence.Repository;
// using WoundCareApi.Persistence.UnitOfWork;
//
// namespace WoundCareApi.API.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class WoundController : BaseApiController<PtWoundCare, CRSPatientDataDbContext>
// {
//     private readonly ILogger<WoundController> _logger;
//
//     public WoundController(
//         ILogger<WoundController> logger,
//         IRepository<PtWoundCare, CRSPatientDataDbContext> repository,
//         IUnitOfWork unitOfWork
//     )
//         : base(repository, unitOfWork)
//     {
//         _logger = logger;
//     }
//
//     [HttpGet("patients/{patientId}/encounters/{encounterId}/wounds")]
//     public async Task<
//         ActionResult<IDictionary<string, IEnumerable<PtWoundCare>>>
//     > GetByPatientIdAndEncounterId(string patientId, string encounterId)
//     {
//         var wounds = await _repository.GetByConditionAsync(
//             (x) => x.LifeTimeNumber == patientId && x.EncounterNumber == encounterId
//         );
//         
//         var groupedWounds = wounds
//             .GroupBy(x => x.CaseId)
//             .ToDictionary(group => group.Key ?? "undefined", group => group.AsEnumerable());
//
//         return Ok(groupedWounds);
//     }
// }
