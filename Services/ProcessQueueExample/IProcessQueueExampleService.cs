using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProcessQueueExample
{
    public interface IProcessQueueExampleService
    {
        Task<string> ConsultaAsync();
    }
}
