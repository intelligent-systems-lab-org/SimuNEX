#!/bin/bash

# Check for jq and install if not present
if ! command -v jq &> /dev/null; then
    echo "jq could not be found, attempting to install..."
    sudo apt-get update && sudo apt-get install -y jq
fi

# Check for --host or -h argument
HOST_FLAG=0
for arg in "$@"; do
    if [ "$arg" == "--host" ] || [ "$arg" == "-h" ]; then
        HOST_FLAG=1
    fi
done

# Paths to package.json, conf.py, and Doxyfile (adjust these paths as needed)
PACKAGE_JSON_PATH="./package.json"
CONF_PY_PATH="docs/sphinx/source/conf.py"
DOXYFILE_PATH="docs/doxygen/Doxyfile"

# Extract version from package.json
FULL_VERSION=$(jq -r '.version' "$PACKAGE_JSON_PATH")

# Check if version was found
if [ -z "$FULL_VERSION" ]; then
    echo "Error: Version not found in package.json"
    exit 1
fi

# Extract major and minor version (assumes format major.minor.patch)
VERSION="${FULL_VERSION%.*}"

# Update 'release' in conf.py
sed -i "s/^release = .*/release = '$FULL_VERSION'/" "$CONF_PY_PATH"

# Update 'version' in conf.py
sed -i "s/^version = .*/version = '$VERSION'/" "$CONF_PY_PATH"

# Update 'PROJECT_NUMBER' in Doxyfile
sed -i "s/^PROJECT_NUMBER\s*=.*/PROJECT_NUMBER         = $FULL_VERSION/" "$DOXYFILE_PATH"

echo "Updated Sphinx conf.py and Doxyfile with full version $FULL_VERSION and version $VERSION"

# Conditionally build and run the Docker container if --host or -h argument is provided
if [ $HOST_FLAG -eq 1 ]; then

    # Building the Docker image
    echo "Building the Docker image for SimuNEX Web..."
    docker build -t simunex-web:latest -f docs/Dockerfile . -q

    # Check if Docker build was successful
    if [ $? -ne 0 ]; then
        echo "Error: Docker build failed"
        exit 1
    fi

    echo "Docker image built successfully."

    echo "Running simunex-web Docker container..."
    docker run --rm -it -p 80:80/tcp simunex-web:latest
else
    echo "Not running Docker container as no --host or -h argument was provided."
fi