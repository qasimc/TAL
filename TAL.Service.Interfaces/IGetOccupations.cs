using System;
using System.Collections.Generic;
using System.Text;
using TAL.Domain.Models;

namespace TAL.Service.Interfaces
{
    public interface IGetOccupations
    {
        ResultValue<List<Occupation>> GetAllOccupations();
    }
}
