#!/bin/bash

# latest version major/min
latestver=1.5
function setup_dapr {
	echo "setting up dapr.."

	dapr init
	dapr_version=$(dapr --version)
	
	echo "checking dapr version $dapr_version"
}
function is_docker_running {
if ! docker info > /dev/null 2>&1; then
  echo "Dapr requires docker engine to be running"
  exit 1
fi
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
#dapr init
