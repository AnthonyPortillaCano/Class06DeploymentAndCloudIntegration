# CloudDemoApp - Deployment and Cloud Integration Demo

## 1. Deploying .NET Applications to Azure

### Azure Web App for Containers
- Build the Docker image:
  ```bash
  docker build -t cloud-demo-app .
  ```
- Push to Azure Container Registry (ACR) or Docker Hub.
- Configure your Azure Web App for Containers to use the image.

## 2. Implementing CI/CD Pipelines (GitHub Actions)

Create `.github/workflows/docker-azure.yml`:
```yaml
name: Build and Deploy Docker to Azure Web App for Containers

on:
  push:
    branches: [ main ]

jobs:
  build-and-deploy:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v3

      - name: Set up Docker Buildx
        uses: docker/setup-buildx-action@v3

      - name: Login to Azure Container Registry
        uses: azure/docker-login@v1
        with:
          login-server: ${{ secrets.ACR_LOGIN_SERVER }}
          username: ${{ secrets.ACR_USERNAME }}
          password: ${{ secrets.ACR_PASSWORD }}

      - name: Build and push Docker image
        run: |
          docker build -t ${{ secrets.ACR_LOGIN_SERVER }}/cloud-demo-app:latest .
          docker push ${{ secrets.ACR_LOGIN_SERVER }}/cloud-demo-app:latest

      - name: Deploy to Azure Web App for Containers
        uses: azure/webapps-deploy@v2
        with:
          app-name: ${{ secrets.AZURE_WEBAPP_NAME }}
          images: ${{ secrets.ACR_LOGIN_SERVER }}/cloud-demo-app:latest
```

## 3. Monitoring and Logging Strategies

- Integrated with **Application Insights** for distributed tracing and logging.
- Add your Application Insights connection string to `appsettings.json` or as an environment variable:
  ```json
  "ApplicationInsights": {
    "ConnectionString": "<YOUR_CONNECTION_STRING>"
  }
  ```
- In `Program.cs`:
  ```csharp
  builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["ApplicationInsights:ConnectionString"]);
  ```
- View logs and metrics in the Azure Portal under Application Insights.

---

## Summary
- Dockerized .NET app ready for Azure Web App for Containers
- CI/CD pipeline with GitHub Actions
- Cloud monitoring and logging with Application Insights 