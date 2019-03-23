using System;
using System.Diagnostics;

//https://stackoverflow.com/questions/36431220/getting-a-list-of-dlls-currently-loaded-in-a-process-c-sharp
namespace ListDLLsNet
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("./ListDLLsNet <pid/all>");
                return 1;
            }

            int pid;
            if (int.TryParse(args[0], out pid))
            {
                // List Modules for PID
                try
                {
                    Process proc = Process.GetProcessById(pid);
                    Console.WriteLine("Process: {0} ID: {1}", proc.ProcessName, proc.Id);
                    try
                    {
                        foreach (ProcessModule module in proc.Modules)
                        {
                            Console.WriteLine(string.Format("\t{0}", module.FileName));
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\tAccess Denied");
                    }
                }
                catch
                {
                    Console.WriteLine("\tError: Check PID Value!");
                    return 1;
                }
                return 0;
            }

            if (string.Compare(args[0], "all") == 0)
            {
                // List Modules for All processes
                Process[] processlist = Process.GetProcesses();
                foreach (Process proc in processlist)
                {
                    Console.WriteLine("Process: {0} ID: {1}", proc.ProcessName, proc.Id);
                    try
                    {
                        foreach (ProcessModule module in proc.Modules)
                        {
                            Console.WriteLine(string.Format("\t{0}", module.FileName));
                        }
                    }
                    catch
                    {
                        Console.WriteLine("\tAccess Denied");
                    }
                }
                return 0;
            }
            Console.WriteLine("./ListDLLsNet <pid/all>");
            return 1;
        }
    }
}
