#!/bin/bash
set -euo pipefail

# latest version major/min
latestver=1.5
function setup_dapr {
	echo "setting up dapr.."

	dapr init
	dapr_version=$(dapr --version)

	echo "checking dapr version $dapr_version"
}
<<<<<<< HEAD:src/dapr-setup/dapr-init.sh
function check_docker_running {
	if ! docker info > /dev/null 2>&1; then
  		echo "⛔️ Dapr requires docker engine to be running"
  		exit 1
	else
		echo "Docker desktop is running!"
	fi
=======

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
>>>>>>> c730e86e6bf39f3f2407101ece4ee826ce8cd12d:dapr-init.sh
}
#check if dapr is already installed
dapr_install_path="$HOME/.dapr/bin/daprd"

<<<<<<< HEAD:src/dapr-setup/dapr-init.sh
check_docker_running

echo "checking dapr initialisation..."

## bad just simulating a check...
sleep 1

=======
>>>>>>> c730e86e6bf39f3f2407101ece4ee826ce8cd12d:dapr-init.sh
if [[ -f "$dapr_install_path" ]]; 
then
	echo "✅ dapr already installed"
	exit 1
else
	setup_dapr	
fi
<<<<<<< HEAD:src/dapr-setup/dapr-init.sh
=======

echo "checking dapr containers are running..."
check_dapr_containers 

>>>>>>> c730e86e6bf39f3f2407101ece4ee826ce8cd12d:dapr-init.sh
