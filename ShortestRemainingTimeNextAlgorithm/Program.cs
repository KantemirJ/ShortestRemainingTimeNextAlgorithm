using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // Example processes with arrival time and burst time
        List<Process> processes = new List<Process>
        {
            new Process("P1", 0, 6),
            new Process("P2", 2, 4),
            new Process("P3", 4, 2),
            new Process("P4", 6, 8)
        };

        // Simulate the SRTN scheduling algorithm
        SRTNScheduler(processes);
    }

    static void SRTNScheduler(List<Process> processes)
    {
        int currentTime = 0;

        while (processes.Count > 0)
        {
            // Get processes that have arrived but not yet completed
            var runnableProcesses = processes.Where(p => p.ArrivalTime <= currentTime && p.RemainingBurstTime > 0);

            if (runnableProcesses.Any())
            {
                // Select the process with the shortest remaining burst time
                var shortestProcess = runnableProcesses.OrderBy(p => p.RemainingBurstTime).First();

                // Execute the process for 1 time unit
                Console.WriteLine($"Executing {shortestProcess.Name} at time {currentTime}");
                shortestProcess.RemainingBurstTime--;

                // Check if the process has completed
                if (shortestProcess.RemainingBurstTime == 0)
                {
                    Console.WriteLine($"{shortestProcess.Name} completed at time {currentTime}");
                    processes.Remove(shortestProcess);
                }
            }
            else
            {
                // If no processes are runnable, move to the next time unit
                Console.WriteLine($"No runnable processes at time {currentTime}");
            }

            currentTime++;
        }
    }
}

class Process
{
    public string Name { get; }
    public int ArrivalTime { get; }
    public int BurstTime { get; }
    public int RemainingBurstTime { get; set; }

    public Process(string name, int arrivalTime, int burstTime)
    {
        Name = name;
        ArrivalTime = arrivalTime;
        BurstTime = burstTime;
        RemainingBurstTime = burstTime;
    }
}
