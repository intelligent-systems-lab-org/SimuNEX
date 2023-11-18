# Use an official Python runtime as a parent image
FROM python:3.9 AS sphinx-build

# Set the working directory in the container to /docs
WORKDIR /docs

# Copy the requirements.txt file into the container at /docs
COPY docs/sphinx/requirements.txt /docs/

# Copy source files
COPY docs/sphinx/source /docs/source/

# Copy repo files to the root of the container's filesystem
COPY package.json README.md /

# Install any needed packages specified in requirements.txt
RUN pip install --no-cache-dir -r requirements.txt

# Define environment variable
ENV RUNNING_IN_DOCKER true

# Build html when the container launches
RUN sphinx-build -M html /docs/source /docs/build

# Run on port 80
FROM nginx

COPY --from=sphinx-build /docs/build/html /usr/share/nginx/html