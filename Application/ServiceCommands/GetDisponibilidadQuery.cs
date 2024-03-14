using Domain;
using Infraestructure.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.ServiceCommands
{
    public class GetDisponibilidadQuery : IRequest<List<Disponibilidad>>
    {
        public string ubicacion { get; set; }
    }

    public class GetDisponibilidadQueryHandler : IRequestHandler<GetDisponibilidadQuery, List<Disponibilidad>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetDisponibilidadQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Disponibilidad>> Handle(GetDisponibilidadQuery request, CancellationToken cancellationToken)
        {
            var result = await _unitOfWork.Repository<Disponibilidad>().GetAllAsync().Where(x => x.Ubicacion.Localidad == request.ubicacion).ToListAsync(cancellationToken);
            return result;
        }
    }
}
