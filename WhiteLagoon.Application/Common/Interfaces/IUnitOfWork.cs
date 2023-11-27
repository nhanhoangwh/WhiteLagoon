using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteLagoon.Application.Common.Interfaces
{
    //Creating an abstract wrapper interface
    public interface IUnitOfWork
    {
        IVillaRepository Villa { get; }
        IVillaNumberRepository VillaNumber { get; }
        IBookingRepository Booking { get; }
        IApplicationUserRepository User { get; }
        IAmenityRepository Amenity { get; }
        void Save();
    }
}
