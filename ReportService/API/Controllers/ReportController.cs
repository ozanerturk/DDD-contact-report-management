using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.AggregatesModel.ReportAggregate;
using KafkaFlow.Producers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {

        private readonly ILogger<ReportController> _logger;
        private readonly IReportRepository reportRepository;
        private readonly IProducerAccessor producerAccessor;

        // private readonly IReportRepository personRepository;

        public ReportController(ILogger<ReportController> logger, IReportRepository reportRepository, IProducerAccessor producerAccessor)
        {
            _logger = logger;
            this.reportRepository = reportRepository;
            this.producerAccessor = producerAccessor;
        }

        [Route("{identityGuid}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [HttpGet]
        public async Task<ActionResult> GenerateReport(string identityGuid)
        {

            var report = await reportRepository.FindAsync(identityGuid);
            if (report == null)
            {
                return NotFound();
            }

            if (report.Status == Domain.Status.PENDING)
            {
                return new JsonResult(new
                {
                    identitiy = report.IdentityGuid,
                    requestDate = report.RequestDate,
                    status = report.Status.ToString()
                });
            }
            else
            {
                return new JsonResult(new

                {
                    identitiy = report.IdentityGuid,
                    requestDate = report.RequestDate,
                    status = report.Status.ToString(),
                    totalPhoneNumbers = report.TotalDistinctPhoneNumbers,
                    totalPerson = report.TotalDistinctPersons

                });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GenerateReport([FromBody] GenerateReportRequest request)
        {

            var report = new Report(Guid.NewGuid().ToString(), request.location);
            report = reportRepository.Add(report);

            await reportRepository.UnitOfWork.SaveChangesAsync();
            await producerAccessor[KafkaHelper.ReportEventProducer].ProduceAsync(Guid.NewGuid().ToString(), new GenerateReportEvent()
            {
                identity = report.IdentityGuid,
                location = report.Location
            });

            return Accepted(new
            {
                reportId = report.IdentityGuid
            });
        }

    }
}
