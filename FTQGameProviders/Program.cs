using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

#region
// Values
string tips = "Use /help command to see all commands";
string privacy_info = "During the process your pc's background works will be monitored locally, no data will be sent to any server or third party. " +
                      "If you have any concerns, please look at the source code on Traslox's github or just don't use the command.";
string start = "/start";
string startinfo = "Process started, please wait 1 minutes";
string help = "/help";
string exit = "/exit";
string exitinfo = "Exiting the application...";
string autodetect = "/autodetect";
string autodetectinfo = "Autodetect activated";
string answer = "";
int autodetectcontroler = 0;
string[] launchersToClose = { "EpicGamesLauncher", "Steam", "XboxPcApp", "XboxPcAppFT", "XboxPcTray", "XboxGameBarWidgets"};
int time3seconds = 3000;
int time60seconds = 60000;
int time150seconds = 150000;
#endregion

while (true)
{
    if (autodetectcontroler == 0)
    {
        Console.WriteLine(tips);

        answer = Console.ReadLine();
    }

    if (autodetectcontroler == 1)
    {
        answer = start;
    }

    if (answer == help)
    {
        Console.WriteLine("Available commands:");
        Console.WriteLine("/info - Purpose of the application");
        Console.WriteLine("/start - Start the process and close launchers");
        Console.WriteLine("/help - Show this help message");
        Console.WriteLine("/exit - Exit the application");
        Console.WriteLine("/autodetect - Monitoring the system to prevent apps work in background in every 3.5 minutes");
        continue;
    }

    if (answer == "/info")
    {
        Console.WriteLine("The console application mainly made for GOG Galaxy");
        continue;
    }

    if (answer == "/devmode")
    {
        Console.WriteLine("Which background service you want to look at exact name");
        string devmode_answer = Console.ReadLine();

        foreach (var process in Process.GetProcesses())
        {
            if (process.ProcessName.ToLower().Contains(devmode_answer))
            {
                Console.WriteLine(process.ProcessName);
            }
        }
    }

    if (answer == start)
    {

        Console.WriteLine(startinfo);
        Thread.Sleep(time60seconds);

        foreach (var launcher in launchersToClose)
        {
            CloseProcess(launcher);
        }

        static void CloseProcess(string processName)
        {
            bool anyProcessClosed = false;
            try
            {
                foreach (var process in Process.GetProcessesByName(processName))
                {
                    process.Kill();
                    Console.WriteLine($"{processName} forced to quit");
                    anyProcessClosed = true;
                }
                if (!anyProcessClosed)
                {
                    Console.WriteLine($"{processName} was not running, no action taken.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{processName} error occured, please contact with developer or try again: {ex.Message}");
            }
        }

    }

    if (answer == autodetect)
    {
        Console.WriteLine(privacy_info +
            "please if you agree press y, if you don't just press any key expect y button on your keyboard");
        string privacy_answer = Console.ReadLine();

        if (privacy_answer != "y")
        {
            Console.WriteLine("Autodetect cancelled.");
            continue;
        }

        if (privacy_answer == "y")
        {
            Console.WriteLine(autodetectinfo);
            autodetectcontroler = ++autodetectcontroler;
            continue;
        }    
    }

    if (answer == exit)
    {
        Console.WriteLine(exitinfo);
        Thread.Sleep(time3seconds);
        Environment.Exit(0);
    }

    if(autodetectcontroler == 1)
    {
        Console.WriteLine("monitoring of pc resuming...");
        Thread.Sleep(time150seconds);
    }
}