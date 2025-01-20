using AM.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Interfaces
{
    public interface IPlaneService : IService<Plane>
    {
        List<Flight> FlightsOfLatestPlanes(int n);
        void DeleteAllPlanesTenYears();
        List<Passenger> PassengerAtDate(Plane plane,DateTime date);
    }
}
