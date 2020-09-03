using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProcessQueueExample
{
    public class ProcessQueueExampleService : IProcessQueueExampleService
    {
        public IProcessQueueExampleRepository _processQueueExampleRepository;

        public ProcessQueueExampleService(IProcessQueueExampleRepository processQueueExampleRepository)
        {
            _processQueueExampleRepository = processQueueExampleRepository;
        }

        public async Task<string> ConsultaAsync()
        {
            return await this._processQueueExampleRepository.ConsultaAsync();
        }
    }
}
