# ---- Doxygen Build Stage ----
FROM ubuntu:latest AS doxygen-build

WORKDIR /docs

RUN apt-get update && apt-get install -y doxygen sed

COPY docs/doxygen/Doxyfile /docs/
COPY UnityProject/Assets/Scripts/ /docs/code/

RUN sed -i 's|INPUT                  = ../../UnityProject/Assets/Scripts|INPUT = /docs/code|' Doxyfile
RUN doxygen Doxyfile

# ---- Sphinx Build Stage ----
FROM python:3.9 AS sphinx-build

WORKDIR /docs

COPY docs/sphinx/requirements.txt /docs/
COPY docs/sphinx/source /docs/source/
COPY package.json README.md /

RUN pip install --no-cache-dir -r requirements.txt
RUN sphinx-build -M html /docs/source /docs/build

# ---- Nginx Stage ----
FROM nginx

# Copy the Doxygen documentation
COPY --from=doxygen-build /docs/html /usr/share/nginx/html/docs/api

# Copy the Sphinx documentation
COPY --from=sphinx-build /docs/build/html /usr/share/nginx/html/docs/main

# Add custom Nginx configuration file
COPY /docs/nginx.conf /etc/nginx/

# Expose port 80
EXPOSE 80