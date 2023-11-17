# Use an official Python runtime as a parent image
FROM python:3.9

# Set the working directory in the container to /docs
WORKDIR /docs

# Copy the requirements.txt file into the container at /docs
COPY docs/requirements.txt /docs/

# Copy repo files to the root of the container's filesystem
COPY package.json README.md /

# Install any needed packages specified in requirements.txt
RUN pip install --no-cache-dir -r requirements.txt

# Make port 8000 available outside this container
EXPOSE 8000

# Define environment variable
ENV NAME Documentation
ENV RUNNING_IN_DOCKER true

# Run make html when the container launches
CMD ["make", "html"]
