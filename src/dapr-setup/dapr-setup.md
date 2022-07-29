# Setting up Dapr

To setup Dapr you need to install the Dapr CLI

In this repo I will be using a Mac to install Dapr

## Pre-requesites

As we will be using Dapr in container mode. Make sure you have installed the following:

* Docker Desktop
* A reputable IDE. I am using VSCode

## Installing the CLI

I recommend you use Brew to install Dapr

```
brew install dapr/tap/dapr-cli

```

For other installation methods goto: https://docs.dapr.io/getting-started/install-dapr-cli/

## Check dapr is installed

Once installed you should be able to see the Dapr cli:

```
host:~ rk$ Dapr

	 __                
    ____/ /___ _____  _____
   / __  / __ '/ __ \/ ___/
  / /_/ / /_/ / /_/ / /    
  \__,_/\__,_/ .___/_/     
	      /_/            
									   
===============================
Distributed Application Runtime

Usage:
  dapr [command]

Available Commands:
  build-info     Print build info of Dapr CLI and runtime
  completion     Generates shell completion scripts
  components     List all Dapr components. Supported platforms: Kubernetes
  configurations List all Dapr configurations. Supported platforms: Kubernetes
  dashboard      Start Dapr dashboard. Supported platforms: Kubernetes and self-hosted
  help           Help about any command
  init           Install Dapr on supported hosting platforms. Supported platforms: Kubernetes and self-hosted
  invoke         Invoke a method on a given Dapr application. Supported platforms: Self-hosted
  list           List all Dapr instances. Supported platforms: Kubernetes and self-hosted
  logs           Get Dapr sidecar logs for an application. Supported platforms: Kubernetes
  mtls           Check if mTLS is enabled. Supported platforms: Kubernetes
  publish        Publish a pub-sub event. Supported platforms: Self-hosted
  run            Run Dapr and (optionally) your application side by side. Supported platforms: Self-hosted
  status         Show the health status of Dapr services. Supported platforms: Kubernetes
  stop           Stop Dapr instances and their associated apps. Supported platforms: Self-hosted
  uninstall      Uninstall Dapr runtime. Supported platforms: Kubernetes and self-hosted
  upgrade        Upgrades or downgrades a Dapr control plane installation in a cluster. Supported platforms: Kubernetes

Flags:
  -h, --help          help for dapr
      --log-as-json   Log output in JSON format
  -v, --version       version for dapr

Use "dapr [command] --help" for more information about a command.
```

## Dapr initialisation

Once the dapr CLI has been installed you must make sure that the Dapr is initialized.
This is because Dapr will run its building blocks as a sidecar. If you are running this in container mode this means 
a container sidecar. 

To make everything easier for you I have a bash script which checks if dapr is initialized. if not it performs initialization
and checks the Dapr runtime containers are runnning:

```
.\dapr-init.sh
```

If docker is not running you will get output as follows:

```
host:dapr-setup rkhm$ ./dapr-init.sh 
⛔️ Dapr requires docker engine to be running
```

If that is the case start Docker desktop on your Mac

Run the script again and you should see the following output:

```
Docker desktop is running!
checking dapr initialisation...
✅ dapr already installed
checking all dapr containers are running....
checking dapr_placement
✅ Dapr dapr_placement is running!!
checking dapr_zipkin
✅ Dapr dapr_zipkin is running!!
checking dapr_redis
✅ Dapr dapr_redis is running!!

```

Further you can check via docker the above dapr containers are running the following bash:

```
 ls | grep dapr | wc -l
 3
```
This confirms three dapr containers are running.

## Dapr dashboard

Dapr comes with a local dashboard. For those familiar with Kuberenetes it is like the Kubernetes dashboard.

To run it locally:

```
dapr dashboard
```

Navigate to http://localhost:8080/overview

Spend some time navigating the pages.

That is it. Next we will look up at boostrapping a simple stateful C# program with Dapr state block.

