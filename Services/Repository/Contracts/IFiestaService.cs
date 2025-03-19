using ErrorOr;
using RaveAppAPI.Services.Models;
using RaveAppAPI.Services.RequestModel.Fiesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaveAppAPI.Services.Repository.Contracts
{
    public interface IFiestaService
    {
        ErrorOr<Created> CreateFiesta(Fiesta fiesta);
        ErrorOr<Deleted> DeleteFiesta(string id);
        ErrorOr<List<Fiesta>> GetFiestas(GetFiestaRequest request);
        ErrorOr<Updated> UpdateFiesta(Fiesta fiesta);
    }
}
