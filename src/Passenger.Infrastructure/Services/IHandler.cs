using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Passenger.Infrastructure.Services
{
    public interface IHandler : IService
    {
        IHandlerTask Run(Func<Task> run);

        IHandlerTaskRunner Validate(Func<Task> validate);

        Task ExecuteAllAsync();
    }
}
