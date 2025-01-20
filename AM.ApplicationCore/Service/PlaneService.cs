using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Service
{
    public class PlaneService : Service<Plane>, IPlaneService
    {
        public PlaneService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public void DeleteAllPlanesTenYears()
        {
            List<Plane> allplanes = GetAll().ToList();
            foreach (Plane plane in allplanes)
            {
                TimeSpan age = DateTime.Now - plane.ManufactureDate;
                if(age > TimeSpan.FromDays(365 * 10))
                {
                    Delete(plane);
                }
            }
        }
        public List<Flight> FlightsOfLatestPlanes(int n)
        {
            List<Plane> allplanes = GetAll().ToList();
            List<Flight> result = new List<Flight>();
            for(int i = 0; i < n+1; i++) 
            {
                foreach (Flight flight in allplanes[i].Flights)
                {
                    result.Add(flight);
                }               
            }
            var sortedResult = result.OrderBy(f => f.FlightDate).ToList();
            return sortedResult;
        }
        public List<Passenger> PassengerAtDate(Plane plane, DateTime date)
        {
            List<Passenger> result = new List<Passenger>();
            foreach(Flight flight in plane.Flights)
            {
                if(flight.FlightDate == date)
                {
                    foreach(Passenger p in flight.Passengers)
                    {
                        result.Add(p);
                    }
                }
            }
            return result;
        }
    }
}
