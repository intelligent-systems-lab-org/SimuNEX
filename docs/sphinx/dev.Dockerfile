# Use an official Python runtime as a parent image
FROM python:3.9

# Set the working directory in the container to /docs
WORKDIR /docs

# Copy the requirements.txt file into the container at /docs
COPY docs/sphinx/requirements.txt /docs/

# Copy repo files to the root of the container's filesystem
COPY package.json README.md /

# Install any needed packages specified in requirements.txt
RUN pip install --no-cache-dir -r requirements.txt

# Install sphinx-autobuild for development purposes
RUN pip install sphinx-autobuild

# Make port 8000 available outside this container
EXPOSE 8000

# Define environment variable
ENV RUNNING_IN_DOCKER true

# Use sphinx-autobuild to watch for changes and rebuild the documentation automatically
CMD ["sphinx-autobuild", "/docs/source", "/docs/build/html", "--host", "0.0.0.0", "--port", "8000", "--watch", "/docs"]