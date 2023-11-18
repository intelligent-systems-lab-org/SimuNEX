FROM ubuntu:latest AS doxygen-build

# Set the working directory to /docs
WORKDIR /docs

# Install Doxygen and tools for modifying files (e.g., sed)
RUN apt-get update && apt-get install -y doxygen sed

# Copy the necessary doxygen files
COPY docs/doxygen/Doxyfile /docs/
COPY UnityProject/Assets/Scripts/ /docs/code/

# Modify the Doxyfile to set the correct INPUT path
RUN sed -i 's|INPUT                  = ../../UnityProject/Assets/Scripts|INPUT = /docs/code|' Doxyfile

# Command to run Doxygen
RUN doxygen Doxyfile

# Host on port 80
FROM nginx

COPY --from=doxygen-build /docs/html /usr/share/nginx/html