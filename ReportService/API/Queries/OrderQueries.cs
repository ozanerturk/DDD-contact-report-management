// namespace Microsoft.eShopOnContainers.Services.Ordering.API.Application.Queries
// {
//     using Dapper;
//     using System.Data.SqlClient;
//     using System.Threading.Tasks;
//     using System;
//     using System.Collections.Generic;

//     public class OrderQueries
//         : IOrderQueries
//     {
//         private string _connectionString = string.Empty;

//         public OrderQueries(string constr)
//         {
//             _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
//         }


//         public async Task<Report> GetReportAsync(int id)
//         {
//             using (var connection = new SqlConnection(_connectionString))
//             {
//                 connection.Open();

//                 var result = await connection.QueryAsync<dynamic>(
//                    @"select o.[Id] as ordernumber,o.OrderDate as date, o.Description as description,
//                         o.Address_City as city, o.Address_Country as country, o.Address_State as state, o.Address_Street as street, o.Address_ZipCode as zipcode,
//                         os.Name as status, 
//                         oi.ProductName as productname, oi.Units as units, oi.UnitPrice as unitprice, oi.PictureUrl as pictureurl
//                         FROM ordering.Orders o
//                         LEFT JOIN ordering.Orderitems oi ON o.Id = oi.orderid 
//                         LEFT JOIN ordering.orderstatus os on o.OrderStatusId = os.Id
//                         WHERE o.Id=@id"
//                         , new { id }
//                     );

//                 if (result.AsList().Count == 0)
//                     throw new KeyNotFoundException();

//                 return MapOrderItems(result);
//             }
//         }

//     }
// }
