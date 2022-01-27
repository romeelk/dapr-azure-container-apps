using System.Diagnostics;
using System.Threading;
using System.Timers;
using daprstate.Models;

Console.WriteLine("Welcome to a simple Game simulator using Dapr state building block");

Console.WriteLine("Do you want to start the the game simulator");

var gameController = new GameController(new Game("Doom",GameType.ShootemUp));

Console.WriteLine("\nLets get going. initialising game");

var stopWatch = new Stopwatch();
stopWatch.Start();
var thread = new Thread(gameController.Start);

thread.Start();  

var gameCounter = 0;
while (true)
{   
    if(gameCounter == 60)
    {
        gameController.End();
    
        Console.WriteLine($"Game simulation finished...");
        break;
    }
    Thread.Sleep(1000);
    gameCounter++;
}

thread.Join();