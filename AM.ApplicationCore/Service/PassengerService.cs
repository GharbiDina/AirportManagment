using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using AM.ApplicationCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Service
{
    internal class PassengerService : Service<Passenger>, IPassengerService
    {
        public PassengerService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
