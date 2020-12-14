using System;
using System.Collections.Generic;
using System.Text;
using TAL.Domain.Models;

namespace TAL.Domain.Interfaces
{
    public interface IProvideTALData
    {
        ResultValue<Occupation> GetOccupation(string occupationName);
        ResultValue<OccupationRating> GetOccupationRating(string rating);

        ResultValue<List<Occupation>> GetAllOccupations();
    }
}
