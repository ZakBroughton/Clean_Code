using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment_2_GUI.ApplicationServices.Implementations
{
    // Defines a service for periodically executing a specified task on a dashboard.
    // This class manages a repeating task that is executed at defined time intervals until explicitly stopped. 
    // The periodic execution is controlled via a CancellationToken for clean stoppage and error handling.
    public class DashboardPollingService
    {
        private readonly Func<Task> _updateAction;
        private readonly TimeSpan _pollingInterval;
        private CancellationTokenSource _cancellationTokenSource;

        public DashboardPollingService(Func<Task> updateAction, TimeSpan pollingInterval)
        {
            _updateAction = updateAction ?? throw new ArgumentNullException(nameof(updateAction));
            _pollingInterval = pollingInterval;
        }

        public void Start()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Task.Run(async () => await PollingLoop(_cancellationTokenSource.Token));
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async Task PollingLoop(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    await Task.Delay(_pollingInterval, cancellationToken);
                    if (!cancellationToken.IsCancellationRequested)
                    {
                        await _updateAction();
                    }
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            catch (Exception ex)
            {
             
                Console.WriteLine($"Error in polling loop: {ex.Message}");
               
            }
        }
    }
}