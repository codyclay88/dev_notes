# this is an example showing how to extend/change an existing official image from Docker Hub

FROM nginx:latest

WORKDIR /usr/share/nginx/html
# changes the working directory to root of nginx webhost
# this is preferred to using `RUN cd /some/path`

COPY index.html index.html
# COPY the index.html file from my current local directory into the current container path. 

# I dont need to specify any EXPOSE or CMD because they are specified in the FROM
