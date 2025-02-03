using System.Diagnostics;
using System.Runtime.InteropServices;
using FluxTune;
class Program
{
    static void Main(string[] args)
    {
        MainCore botCore = new MainCore();
        botCore.StartBot();
        Console.WriteLine("Bot is running...");
        Console.ReadKey();
    }
}
