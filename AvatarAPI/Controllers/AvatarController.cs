using AvatarAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AvatarAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvatarController : Controller
    {
        IAvatarService _avatarService;
        public AvatarController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        [HttpGet]
        public IActionResult Get(string userIdentifier)
        {
            var url = _avatarService.GetAvatarUrl(userIdentifier);

            return Ok(url);
        }
    }
}
