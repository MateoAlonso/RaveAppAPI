using RaveAppAPI.Services.Repository.Contracts;

namespace RaveAppAPI.Controllers
{
    public class FiestaController : ApiController
    {
        private readonly IFiestaService _fiestaService;

        public FiestaController(IFiestaService fiestaService)
        {
            _fiestaService = fiestaService;
        }

    }
}
