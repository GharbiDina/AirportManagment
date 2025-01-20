using AM.ApplicationCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Interfaces
{
    public  interface IFlightService : IService<Flight>
    {
        public IEnumerable<DateTime> GetFlightdates(string destination);
        public void ShowFlightDetails(Plane plane);
        public int ProgrammedFlightNumber(DateTime startDate);
        public double DurationAverage(string destination);
        public IEnumerable<Flight> OrderedDurationFlights();
        public IEnumerable<Traveller> SeniorTravellers(Flight flight);
        public IEnumerable< IGrouping<string, Flight>> DestinationGroupedFlights();
        public void DestinationGroupedF();
        void GetFlights(string filterType, string filterValue);
        //
        public bool IsPlacesDisponible(Flight flight, int n);
        public List<Staff> staffList(int flightId);
        public void getPassengersWithdate(DateTime dateStart, DateTime dateEnd);
        public IEnumerable<Flight> SortFlights();
    }
}
