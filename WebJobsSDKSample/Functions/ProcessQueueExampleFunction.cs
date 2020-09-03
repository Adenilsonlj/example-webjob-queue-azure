using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Services.ProcessQueueExample;
using System;
using System.Threading.Tasks;

namespace WebJobs
{
    public class ProcessQueueExampleFunction
    {
        public IConfiguration _configuration;
        public IProcessQueueExampleService _processQueueExampleService;
        
        public ProcessQueueExampleFunction( IProcessQueueExampleService processQueueExampleService)
        {
            _processQueueExampleService = processQueueExampleService;
        }

        public async Task ProcessQueueMessage([QueueTrigger("queue")] string message, ILogger logger, ExecutionContext executionContext)
        {
            try
            {
                logger.LogInformation(message + await _processQueueExampleService.ConsultaAsync());

            }catch(Exception ex)
            {
                logger.LogError(ex.Message);
            }
        }
    }
}