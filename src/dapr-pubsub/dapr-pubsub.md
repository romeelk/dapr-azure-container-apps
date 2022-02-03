# Dapr pub/sub

The following demo demonstrates how to use Dapr pub/sub building block.
The pub/sub building block provides a convenient programming abstraction
for developers building loosely coupled microservices/distributed applications.

There are many pub/sub message oriented middleware systems to use. Each of those
will have there own implementation details. Dapr allows developers to avoid the glue
of plumbing these infrastructure concerns by delegating the messaging to Dapr.
Dapr will then essentially route a message to a supported message middleware provider.

## Architecture

![dapr pub sub architecture](https://docs.dapr.io/images/pubsub-overview-components.png)

From the above architecture diagram the key components include:

* Your app
* Dapr pub/sub side car
* A pub/sub component (Redis, GCP pub/sub, Azure service bus)
* A pub/sub yaml component file


## Pub/sub component

When you first initialise your Dapr environment, Dapr will create a default components folder in your
home profile path:

```
/Users/<userprofile>/.dapr/components
```

A default pub/sub component yaml is created. This uses the default redis side car:

```
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: pubsub
spec:
  type: pubsub.redis
  version: v1
  metadata:
  - name: redisHost
    value: localhost:6379
  - name: redisPassword
    value: ""
```

If you want to customise this file then you need to amend it.

## Creating a custom pub/sub component topic

Any pub/sub middleware needs a configuration so dapr knows it is the middleware to use.

By default dapr creates a pubsub.yaml file using redis:

```
apiVersion: dapr.io/v1alpha1
kind: Component
metadata:
  name: order_pub_sub
spec:
  type: pubsub.redis
  version: v1
  metadata:
  - name: redisHost
    value: localhost:6379
  - name: redisPassword
    value: ""
```

## Creating your subscriptions

As we want to decouple the components we next need to create the subscriptions yaml in the same
components folder:

```
apiVersion: dapr.io/v1alpha1
kind: Subscription
metadata:
  name: order_pub_sub
spec:
  topic: orders
  route: /checkout
  pubsubname: order_pub_sub
scopes:
- orderprocessing
- checkout

```

Let's dissect the above configuration:

* kind property. This declares that this is a subscription
* name. This metadata describes which Dapr pub/sub component a topic is going to be for
* the topic field. The name of the pub/sub topic for consumers to subscribe to
* scopes. The name of the applications (dapr appid) listening for events from this topic

## Making applications subscribe to a topic

Remember once you dapr components have been configured you are ready to subscribe 
to the pub/sub topic. Dapr will take care of the pub/sub plumbing for you via
its sidecar. The developer does not need to download the paticular pub/sub package
dependancy to communicate with.


Start the susbcribing app. In this case:
```
dapr run --app-id checkout --components-path ../components/ -p 50000 -d ../components/ -- dotnet run --urls http://*:50000

```

## Publishing items to a topic

Once you have started the app that is listening to a subscribed pub/sub topic start running the 
application that will publish events:

```
dapr run --app-id orderprocessing --components-path ../components/ --app-port 6001 --dapr-http-port 3601 --dapr-grpc-port 60001 --app-ssl dotnet run
```