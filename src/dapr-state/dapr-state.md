# Getting started with state block

In this example we are going to use dotnet 6.0
to create a simple program demonstrating the Dapr state building block.

## Create the project

First we create a dotnet console project:

```
dotnet new console --name dapr-state
```

We then import the Dapr-Client package

```
dotnet add package Dapr.Client
```

## Build the sample

The sample project is a simple program that simulates a game for 
50 seconds. The purpose is to demonstrate Dapr's state building block.

To build the sample run the following command:

```
dotnet build
```

## Running via Dapr

Once you have built it you must use the dapr CLI to execute the .NET console program.

Each running dapr application is identified by an Id.

```
dapr run --app-id dapr-state dotnet run
```

Stop the program.

Run the program and you will see the program is stateful by saving the state of the 
last game.

```

== APP == Welcome to a simple Game simulator using Dapr state building block
== APP == Lets get going. initialising game
== APP == Your last game score was 733
== APP == Your last game started at 29/01/2022 00:14:03
== APP == You last finished your game at 29/01/2022 00:14:53
```

## How state is saved

Dapr decouples your program from the underlying state store. In this example
we are using the default redis store.

Dapr defines this in yaml. This can be found in the .dapr/components folder.

```
cat /Users/rkhm/.dapr/components/statestore.yaml 
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: statestore
spec:
  type: state.redis
  version: v1
  metadata:
  - name: redisHost
    value: localhost:6379
  - name: redisPassword
    value: ""
  - name: actorStateStore
    value: "true"
```
In .Net using the DaprClient you simply make a call to save state as follows:

```
daprClient.SaveStateAsync("statestore","currentGame",game.GameState).GetAwaiter().GetResult();   
```
The SaveStateAsync takes three parameters. The name of the configures state store "statestore", the
key of the state you want to persist and finally the actual state.

## Clear the state

If you want to clear the state in the Redis state side car logon to the docker container.
Run the REDIS FLUSHALL command.

```
docker exec -it dapr_redis redis-cli
127.0.0.1:6379> FLUSHALL
OK

```
