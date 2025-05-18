using Microsoft.AspNetCore.Authorization;
using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Controllers
{
    [Authorize]
    public class ReseniaController : ApiController
    {
        private readonly IReseniaService _reseniaService;
        public ReseniaController(IReseniaService reseniaService)
        {
            _reseniaService = reseniaService;
        }
    }
}
