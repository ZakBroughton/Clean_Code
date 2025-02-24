using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.ui_command
{

    // This class implements the Null Object design pattern
    public class NullCommand : UI_Command
    {

        public NullCommand()
        {
        }

        public void Execute()
        {
        }
        public async Task ExecuteAsync()
        {
            await Task.CompletedTask; // No asynchronous operation, just returning a completed task
        }
    }
}
