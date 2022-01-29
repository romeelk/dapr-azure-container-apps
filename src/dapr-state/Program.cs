﻿using System.Diagnostics;
using System.Threading;
using System.Timers;
using daprstate.Models;

Console.WriteLine("Welcome to a simple Game simulator using Dapr state building block");

var gameController = new GameController(new Game("Doom", GameType.ShootemUp));

Console.WriteLine("Lets get going. initialising game");

var savedGameState = gameController.GetGameState();

if(savedGameState != null)
{
    Console.WriteLine($"Your last game score was {savedGameState?.GameScore}");
    Console.WriteLine($"Your last game started at {savedGameState?.StartTime.ToString()}");
    Console.WriteLine($"You last finished your game at {savedGameState?.EndTime.ToString()}");
}

var stopWatch = new Stopwatch();
stopWatch.Start();
var thread = new Thread(gameController.Start);

thread.Start();  

while (true)
{   
    if(stopWatch.Elapsed.Seconds == 50)
    {
        gameController.End();
    
        Console.WriteLine("Game simulation finished...");
        break;
    }
    Thread.Sleep(1000);
}

thread.Join();