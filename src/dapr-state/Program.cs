using System.Threading;
using daprstate.Models;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to a simple Game simulator using Dapr state building block");

Console.WriteLine("Do you want to start the the game simulator");

var gameController = new GameController(new Game("Doom",GameType.ShootemUp));

var key = Console.ReadKey();

var thread = new Thread(gameController.Start);
    
if(key.KeyChar == 'y')
{
     Console.WriteLine("\nLets get going. initialising game");

     thread.Start();
}

while (true)
{
    Console.Write("At any time stop the game by pressing enter ");
    var input = Console.ReadLine();

    gameController.End();
    
    if(!gameController.IsGameRunning)
    {
        break;
    }
}
Console.WriteLine("Game simulator game has finished...");