using System.Xml.Linq;
using Azure;
using Dapper;
using Microsoft.Data.SqlClient;
using ToolBoxAPI.Models;
using ToolBoxAPI.Repositories;

namespace ToolBoxAPI.Services
{
    public class ShortnerLinkService
    {
        private readonly ShortnertLinkRepository _shortnertLinkRepository = new();
        public Link GetLinkByHash(string connectionString, string hash)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    return _shortnertLinkRepository.GetLinkByHash(connection, hash);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }

            }
        }

        public ApiResponse<Link> SaveLink(string connectionString, Link link)
        {
            ApiResponse<Link> response = new ApiResponse<Link>();
            using (var connection = new SqlConnection(connectionString))
            {
                var alreadyHasLink = _shortnertLinkRepository.GetLinkByDestination(connection, link.Destination);

                if (alreadyHasLink != null)
                {
                    response.Data = alreadyHasLink;
                    return response;
                }
                else
                {
                    int newID = _shortnertLinkRepository.InsertLink(connection, link);
                    link.Id = newID;
                }
            }

            response.Data = link;
            return response;
        }
    }
}
