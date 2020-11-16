namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderQueries
    {
        Task<Report> GetReportAsync(int id);

    }
}
