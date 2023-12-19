using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Process> processes = [];
        Console.WriteLine(
            "Menu:\n" +
            "1.Create process (name, arrival time, execution time)\r\n" +
            "2.List created processes\r\n" +
            "3.Generate and execute scheduler\r\n" +
            "4.Exit\r\n");
        Console.WriteLine("Choose from menu:");
        while (true)
        {
            string[] choise = Console.ReadLine().Split(" ");
            if (choise[0] == "4") 
            {
                break;
            }
            switch (choise[0])
            {
                case "1":
                    processes.Add(new Process(choise));
                    break;
                case "2":
                    foreach (Process p in processes)
                    {
                        Console.WriteLine("Process name:" + p.Name + " arrival time:" + p.ArrivalTime + " execution time:" + p.BurstTime);
                    }
                    break;
                case "3":
                    SRTNScheduler(processes);
                    break;
            }
        }
    }

    static void SRTNScheduler(List<Process> processes)
    {
        int currentTime = 0;

        while (processes.Count > 0)
        {
            var runnableProcesses = processes.Where(p => p.ArrivalTime <= currentTime && p.RemainingBurstTime > 0);

            if (runnableProcesses.Any())
            {
                var shortestProcess = runnableProcesses.OrderBy(p => p.RemainingBurstTime).First();

                Console.WriteLine($"Executing {shortestProcess.Name} at time {currentTime}");
                shortestProcess.RemainingBurstTime--;

                if (shortestProcess.RemainingBurstTime == 0)
                {
                    Console.WriteLine($"{shortestProcess.Name} completed at time {currentTime}");
                    processes.Remove(shortestProcess);
                }
            }
            else
            {
                Console.WriteLine($"No runnable processes at time {currentTime}");
            }

            currentTime++;
        }
    }
}

class Process(string[] process)
{
    public string Name { get; } = process[1];
    public int ArrivalTime { get; } = Int32.Parse(process[2]);
    public int BurstTime { get; } = Int32.Parse(process[3]);
    public int RemainingBurstTime { get; set; } = Int32.Parse(process[3]);
}
