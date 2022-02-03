using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dapr.Client;
using System.Threading;
using orderproducer;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Welcome to Dapr order simulator");

string PUBSUB_NAME = "order_pub_sub";
string TOPIC_NAME = "orders";
var orderStatuses = new List<string>{"Pending","Processing","Complete"};
var orderStatusGen = new Random(orderStatuses.Count);
var orderIdGen  = new Random();
while (true)
{
    System.Threading.Thread.Sleep(1000);
     var orderStatus = orderStatuses[orderStatusGen.Next(0,orderStatuses.Count)];
    int orderId = orderIdGen.Next(1, 1000);
    CancellationTokenSource source = new CancellationTokenSource();
    CancellationToken cancellationToken = source.Token;
    using var client = new DaprClientBuilder().Build();
    var order = new Order{Id=orderId, OrderStatus = orderStatus, OrderTime=DateTime.UtcNow};
    //Using Dapr SDK to publish a topic
    await client.PublishEventAsync(PUBSUB_NAME, TOPIC_NAME, order, cancellationToken);
    Console.WriteLine($"Order:{order.Id} being processed");
}