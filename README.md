# freightSolutions

This repository contains a distributed event-driven architecture for a freight platform. The solution is divided into multiple microservices, each responsible for specific workflows and functionalities.

## Microservices

### Workflow 1
1. **Shipment Service**: Publishes shipment updates.
2. **Customs Service**: Verifies customs and publishes the clearance result.
3. **Notification Service**: Sends updates to customers about the shipment status.

### Workflow 2
1. **API Gateway & Orchestrator Service**: Starts and manages the saga.
2. **Order Service**: Creates the order and listens for compensation actions (CancelOrder).
3. **Payment Service**: Processes payments and triggers compensation in case of failure.
4. **Inventory Service**: Reserves stock and handles compensation if stock reservation fails.

## Features
- **Docker**: Each microservice has its own Dockerfile.
- **Docker Compose**: A `docker-compose.yml` file to start all microservices.
- **Kubernetes**: Configuration for auto-scaling and deployment.

## Getting Started
1. Clone the repository.
2. Build and run the microservices using Docker Compose.
3. Deploy to Kubernetes for production.

## License
This project is licensed under the terms of the LICENSE file.

