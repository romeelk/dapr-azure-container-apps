#!/bin/bash

# latest version major/min
latestver=1.5
function setup_dapr {
	echo "setting up dapr.."

	dapr init
	dapr_version=$(dapr --version)

	echo "checking dapr version $dapr_version"
}

function check_dapr_containers {
	alldaprContainers=("dapr_placement" "dapr_zipkin" "dapr_redis")

	echo "checking all dapr containers are running...."

	for container in ${alldaprContainers[@]}; do
  		echo "checking" $container
		if ! docker ps | grep $container > /dev/null 2>&1; then
			echo "❌ Dapr container $container is not running"
		else
			echo "✅ Dapr $container is running!!"
		fi
	done
}
#check if dapr is already installed
dapr_install_path="$HOME/.dapr/bin/daprd"

if [[ -f "$dapr_install_path" ]]; 
then
	echo "✅ dapr already installed"
	exit 1
else
	setup_dapr	
fi

echo "checking dapr containers are running..."
check_dapr_containers 

