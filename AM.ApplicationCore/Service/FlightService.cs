using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Service
{
    public class FlightService : Service<Flight>,IFlightService
    {
        public List<Flight> Flights = new List<Flight>();

        public FlightService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        #region olderFunctions
        public void DestinationGroupedF()
        {
            var req = Flights.GroupBy(f => f.Destination);
            foreach (var item in req)
            {
                Console.WriteLine("destination: " + item.Key);
                foreach (var a in item)
                { Console.WriteLine("Décollage : " + a.FlightDate); }
            }

        }

        public IEnumerable<IGrouping<string, Flight>> DestinationGroupedFlights()
        {

            //var req = from f in Flights
            //          group f by f.Destination;
            var req = Flights.GroupBy(f => f.Destination);
            foreach (var item in req)
            {
                Console.WriteLine("destination: " + item.Key);
                foreach (var a in item)
                { Console.WriteLine("Décollage : " + a.FlightDate); }
            }


            return req;
        }

        public double DurationAverage(string destination)
        {//LINQ
           //var query = from flight in Flights
           //          where flight.Destination == destination
           //          select flight.EstimatedDuration;
           // return req.Average();
           //lambda
           return Flights.Where(a=> a.Destination == destination)
                .Select(f=>f.EstimatedDuration).Average();
        }

        public IEnumerable<DateTime> GetFlightdates(string destination)
        { 
           // List<DateTime> dates = new List<DateTime>();
            // Boucle For
            //for (int i = 0;i< Flights.Count();i++)
            //{ if (Flights[i].Destination == destination)
            //       dates.Add (Flights[i].FlightDate);

            //}
            // Foreach(var item in collection)
            //foreach( Flight f in Flights)
            //    if (f.Destination == destination)
            //        dates.Add(f.FlightDate);
            //return dates;
            //LinQ 

            // var query= from item in collection
            //where 
            //select
            // return query

            //var dates= from f in Flights
            //           where f.Destination == destination
            //           select f.FlightDate;
            //return dates;
            //Lambda 

          return Flights
                .Where(f => f.Destination == destination).Select(A=> A .FlightDate);


        }

        public void GetFlights(string filterType, string filterValue)
        {
            switch (filterType)
            {
                case "Destination":
                    foreach (Flight f in Flights)
                    {
                        if (f.Destination.Equals(filterValue))
                            Console.WriteLine(f);
                    }
                    break;
                case "Departure":
                    foreach (Flight f in Flights)
                    {
                        if (f.Departure.Equals(filterValue))
                            Console.WriteLine(f);
                    }
                    break;
                case "EstimatedDuration":
                    foreach (Flight f in Flights)
                    {
                        if (f.EstimatedDuration.Equals(int.Parse(filterValue)))
                            Console.WriteLine(f);
                    }
                    break;
                case "FlightDate":
                    foreach (Flight f in Flights)
                    {
                        if (f.FlightDate.Equals(DateTime.Parse(filterValue)))
                            Console.WriteLine(f);
                    }
                    break;
            }
        }

        public IEnumerable<Flight> OrderedDurationFlights()
        {
            //var query = from f in Flights
            //            orderby f.EstimatedDuration descending
            //            select f; 
            //return query;

            return Flights.OrderByDescending(f => f.EstimatedDuration);
        }

        public int ProgrammedFlightNumber(DateTime startDate)
        {
            //var req = from f in Flights
            //          where  startDate .CompareTo(f.FlightDate)>0 && (startDate - f.FlightDate).TotalDays > 7
            //          select f;
            //return req.Count();
            return Flights.Count(f => startDate.CompareTo(f.FlightDate) > 0 && (startDate - f.FlightDate).TotalDays > 7);
        }

        public IEnumerable<Traveller> SeniorTravellers(Flight flight)
        {
            //var query = from f in  flight.Passengers.OfType<Traveller>()
            //            orderby f.BirthDate descending
            //            select f;
            //return query.Take(3);
       return flight.Passengers.OfType<Traveller>().OrderByDescending(i => i.BirthDate).Take(3);


        }

        public void ShowFlightDetails(Plane plane)
        {
            //var query = from f in Flights
            //            where f.Plane == plane
            //            select new { f.Destination, f.FlightDate };

           var reslt=Flights.Where(f=> f.Plane == plane).Select(f => new { f.Destination, f.FlightDate }) ;

            foreach (var item in reslt)
            {
                Console.WriteLine("Destination :" + item.Destination + " Date: " + item.FlightDate);
            }
        }
        #endregion
        public bool IsPlacesDisponible(Flight flight, int n)
        {
            if (flight.Plane.Capacity != null)
            {
                int cap = flight.Plane.Capacity.GetValueOrDefault();
                int emptySeats = cap - flight.Passengers.Count;

                return emptySeats >= n;
            }                
            return false;
        }
        public List<Staff> staffList(int flightId)
        {
            Flight flight = GetById(flightId);
            List<Staff> staffs = new List<Staff>();
            foreach (Passenger passenger in flight.Passengers)
            {
                if(passenger is Staff staff)
                {
                    staffs.Add(staff);
                }
            }
            return staffs;
        }
        public void getPassengersWithdate(DateTime dateStart, DateTime dateEnd)
        {
            var group = GetMany(f => f.FlightDate >= dateStart && f.FlightDate <= dateEnd).GroupBy(f => f.FlightDate);
            foreach (var item in group)
            {
                int passengerCount = item.SelectMany(f => f.Passengers).Count();
                Console.WriteLine("Date: "+ item.Key.ToString() +" -- number of Passengers: " + passengerCount);
            }
        }
        public IEnumerable<Flight> SortFlights()
        {
            return GetAll().OrderByDescending(f => f.FlightDate);
        }
    }

}
