using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {

        private readonly ILogger<ReportController> _logger;
        // private readonly IReportRepository personRepository;

        public ReportController(ILogger<ReportController> logger)
        {
            _logger = logger;
        }
    }

}
