using Dapper;
using Microsoft.Data.SqlClient;
using ToolBoxAPI.Models;

namespace ToolBoxAPI.Repositories
{
    public class ShortnertLinkRepository
    {
        public Link GetLinkByHash(SqlConnection connection, string hash)
        {
            var link = connection.QueryFirstOrDefault<Link>($"SELECT * FROM ShortnerLink WHERE Hash = '{hash}'");

            if (link == null)
            {
                throw new Exception("Can't find the destination for this hash code");
            }

            return link;
        }

        public Link GetLinkByDestination(SqlConnection connection, string destination)
        {
            var link = connection.QueryFirstOrDefault<Link>($"SELECT * FROM ShortnerLink WHERE Destination = '{destination}'");

            if (link == null)
            {
                throw new Exception("Can't find the destination for this hash code");
            }

            return link;
        }

        public int InsertLink(SqlConnection connection, Link link)
        {
            int newID = connection.QuerySingle<int>($"INSERT INTO ShortnerLink OUTPUT inserted.ID VALUES ('{link.Destination}', '{link.Hash}')");

            if(newID == 0)
            {
                throw new Exception("The received id is invalid");
            }

            return newID;
        }
    }
}
