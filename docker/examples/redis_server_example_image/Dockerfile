# Use an existing docker image as a base
FROM alpine

# Download and install dependencies
RUN apk add --update redis

# Tell the image what to do when the container is started
CMD ["redis-server"]