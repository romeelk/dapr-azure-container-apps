using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Dapr.Client;
using System.Threading;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

string PUBSUB_NAME = "order_pub_sub";
string TOPIC_NAME = "orders";
while (true)
{
    System.Threading.Thread.Sleep(1000);
    Random random = new Random();
    int orderId = random.Next(1, 1000);
    CancellationTokenSource source = new CancellationTokenSource();
    CancellationToken cancellationToken = source.Token;
    using var client = new DaprClientBuilder().Build();
    //Using Dapr SDK to publish a topic
    await client.PublishEventAsync(PUBSUB_NAME, TOPIC_NAME, orderId, cancellationToken);
    Console.WriteLine("Published data: " + orderId);
}