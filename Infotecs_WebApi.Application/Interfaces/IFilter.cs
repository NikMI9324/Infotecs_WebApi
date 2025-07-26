using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infotecs_WebApi.Application.Interfaces
{
    internal interface IFilter<T>
    {
        IQueryable<T> Apply(IQueryable<T> query);
    }
}
