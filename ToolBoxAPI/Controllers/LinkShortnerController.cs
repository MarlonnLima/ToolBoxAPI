using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ToolBoxAPI.Models;
using ToolBoxAPI.Services;

namespace ToolBoxAPI.Controllers
{
    [ApiController]
    public class LinkShortnerController : ControllerBase
    {
        private IConfiguration _configuration;
        private string connectionString;
        private ShortnerLinkService _shortnerLinkService = new ShortnerLinkService();

        public LinkShortnerController(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("SqlServer") ?? "";
        }

        [HttpGet]
        [Route("/{hash}")]
        public ActionResult<ApiResponse<Link>> RedirectToDestinationByHash(string hash)
        {
            try
            {
                // Validations
                if (hash.Count() != 8)
                {
                    throw new ArgumentNullException("Invalid Argument");
                }

                // Search on db
                var link = _shortnerLinkService.GetLinkByHash(connectionString, hash);
                // Redirect to destination
                if(link is not null) 
                    return Redirect(link.Destination);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occured on api");
            }
            return BadRequest("We can't localize your link, try again later.");
        }

        [HttpPost]
        [Route("short")]
        public ActionResult<ApiResponse<Link>> ShortLink(Link link)
        {
            var response = new ApiResponse<Link>();
            try
            {
                // Validations
                if (link == null)
                {
                    throw new ArgumentNullException("link can't be a null argument");
                }
                if (string.IsNullOrEmpty(link.Destination))
                {
                    throw new ArgumentException("Destination can't be null or empty");
                }
                // Generate Hash
                link.GenerateHash();

                // Save on db
                response = _shortnerLinkService.SaveLink(connectionString, link);
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Error.Message = ex.Message;
                response.Error.HasOcurred = true;
                return BadRequest("An error occured on api");
            }
        }
    }
}
