using Hangfire;

namespace Application.Jobs
{
    public class RecurrentJobsExecutor
    {
        private readonly RecurrentJobConfiguration _config;

        public RecurrentJobsExecutor(RecurrentJobConfiguration config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        private string GenerateCronExpression(int minutes)
        {
            var timespan = TimeSpan.FromMinutes(minutes);

            List<string> cronArgs = new List<string>();
            cronArgs.Add(timespan.Minutes > 0 ? $"0/{timespan.Minutes}" : "*");
            cronArgs.Add(timespan.Hours > 0 ? $"0/{timespan.Hours}" : "*");
            cronArgs.Add("* * *");

            var cronExpr = string.Join(" ", cronArgs);
            return cronExpr;
        }

        public void ConfigureTasks()
        {
            var taskName = "TaskName";
            if (_config._frecuenciaTarea > 0)
            {
                RecurringJob.AddOrUpdate(taskName, () => Task(), GenerateCronExpression(_config._frecuenciaTarea));
            }
            else
            {
                RecurringJob.RemoveIfExists(taskName);
            }



        }

        public void Task()
        {
            Console.WriteLine("Tarea programada");
        }


    }
}
