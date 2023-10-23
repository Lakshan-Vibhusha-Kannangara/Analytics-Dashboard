
FROM ubuntu:latest

ENV MYSQL_ROOT_PASSWORD=989o987Sun#
ENV MYSQL_DATABASE=SchoolDB


RUN apt-get update && apt-get install -y \
    mysql-server \
    curl \
    software-properties-common \
    supervisor


RUN apt-get remove -y nodejs


RUN curl -fsSL https://deb.nodesource.com/setup_16.x | bash -
RUN apt-get install -y nodejs


RUN apt-add-repository universe
RUN apt-get update
RUN apt-get install -y dotnet-sdk-6.0


RUN mkdir -p /app


RUN groupadd -r mygroup && useradd -r -g mygroup -m myuser


WORKDIR /app


RUN chown -R myuser:mygroup /app

# Set npm global configuration in the user's home directory
RUN mkdir -p /home/myuser/.npm-global
RUN chown -R myuser:mygroup /home/myuser/.npm-global
RUN npm config set prefix /home/myuser/.npm-global

# Update the PATH to include the user's npm global bin directory
ENV PATH="/home/myuser/.npm-global/bin:$PATH"

# Create a directory for the frontend
RUN mkdir -p /app/Frontend

# Set the working directory to the frontend directory
WORKDIR /app/Frontend

# Copy the entire Angular project into the image
COPY ./Frontend /app/Frontend

# Install Angular CLI locally (not globally) to avoid permission issues
RUN npm install @angular/cli

# Install frontend dependencies and build the Angular app
RUN npm install
RUN npm run build

# Switch to the API directory
WORKDIR /app/Api

# Copy the .NET Core API code into the image
COPY ./Api /app/Api

# Restore and publish the .NET Core application
RUN dotnet restore
RUN dotnet publish -c Release -o out

# Configure supervisord
COPY supervisord.conf /etc/supervisor/conf.d/supervisord.conf


EXPOSE 80
EXPOSE 4200

# Start supervisord as the entry point
CMD ["/usr/bin/supervisord"]
