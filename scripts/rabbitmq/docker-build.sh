 #!/bin/bash

echo "Building RabbitMq masterkanban/rabbitmq-global-error-handler image"
docker build --tag=masterkanban/rabbitmq-global-error-handler:v0.1 .