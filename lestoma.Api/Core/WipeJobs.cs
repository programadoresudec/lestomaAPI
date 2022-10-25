using Hangfire;
using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using System;
using System.Collections.Generic;

namespace lestoma.Api.Core
{
    public class HangFireRemove
    {
        public void WipeJobs()
        {
            var _logger = new LoggerManager();
            try
            {
                _logger.LogInformation("Clear Pending Hangfire Jobs start");
                //On new app startup remove any previous recurring jobs since they will be setup again
                using (IStorageConnection hfCon = JobStorage.Current.GetConnection())
                {
                    List<RecurringJobDto> recurringJobs = StorageConnectionExtensions.GetRecurringJobs(hfCon);
                    {
                        foreach (RecurringJobDto job in recurringJobs)
                        {
                            RecurringJob.RemoveIfExists(job.Id);
                        }
                    }
                }
                //Remove any other enqueued jobs
                var monitor = JobStorage.Current.GetMonitoringApi();
                var toDelete = new List<string>();
                foreach (QueueWithTopEnqueuedJobsDto queue in monitor.Queues())
                {
                    for (var i = 0; i < Math.Ceiling(queue.Length / 1000d); i++)
                    {
                        monitor.EnqueuedJobs(queue.Name, 1000 * i, 1000)
                            .ForEach(x => toDelete.Add(x.Key));
                        monitor.ScheduledJobs(1000 * i, 1000).ForEach(x => toDelete.Add(x.Key));
                    }
                }
                foreach (var job in toDelete)
                {
                    BackgroundJob.Delete(job);
                }
                _logger.LogInformation("Clear Pending Hangfire Jobs end");
            }
            catch (Exception e)
            {
                _logger.LogError(string.Format("Removal of Previous Sessions Jobs failed, Error {0}", e.Message), e);
            }
        }
    }
}
