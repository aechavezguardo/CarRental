using Application.ServiceCommands;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadController : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Disponibilidad>>> Get([FromQuery] GetDisponibilidadQuery command)
        {
            return await Mediator.Send(command);
        }
    }
}