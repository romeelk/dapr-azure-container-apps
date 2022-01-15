#!/bin/bash
set -euo pipefail

docker container ls --filter "name=dapr"