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
function check_docker_running {
	if ! docker info > /dev/null 2>&1; then
  		echo "⛔️ Dapr requires docker engine to be running"
  		exit 1
	else
		echo "Docker desktop is running!"
	fi
}
#check if dapr is already installed
dapr_install_path="$HOME/.dapr/bin/daprd"

check_docker_running

echo "checking dapr initialisation..."

## bad just simulating a check...
sleep 1

if [[ -f "$dapr_install_path" ]]; 
then
	echo "✅ dapr already installed"
	exit 1
else
	setup_dapr	
fi
