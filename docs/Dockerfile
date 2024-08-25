# ---- Doxygen Build Stage ----
FROM ubuntu:latest AS doxygen-build

WORKDIR /docs

RUN apt-get update && apt-get install -y doxygen sed

COPY docs/doxygen/Doxyfile /docs/
COPY docs/doxygen/mainpage.md /docs/
COPY docs/doxygen/doxygen-awesome-css/ /docs/doxygen-awesome-css/
COPY UnityProject/Assets/Scripts/ /docs/code/

RUN sed -i 's|../../UnityProject/Assets/Scripts|/docs/code|' Doxyfile
RUN doxygen Doxyfile

# ---- Nginx Stage ----
FROM nginx

# Copy the Doxygen documentation
COPY --from=doxygen-build /docs/html /usr/share/nginx/html/docs/dev

# Add custom Nginx configuration file
COPY /docs/nginx.conf /etc/nginx/

# Expose port 80
EXPOSE 80