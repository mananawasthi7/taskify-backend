# Deploying TaskifyApi to Render

## Prerequisites
- A GitHub account with your code pushed to a repository
- A Render account (free tier available)

## Step-by-Step Deployment Guide

### 1. Prepare Your Repository
Make sure your code is pushed to a GitHub repository with the following files:
- `dockerfile` (optimized for production)
- `render.yaml` (deployment configuration)
- `.dockerignore` (build optimization)
- All your .NET source files

### 2. Deploy to Render

#### Option A: Using render.yaml (Recommended)
1. Go to [Render Dashboard](https://dashboard.render.com/)
2. Click "New +" and select "Blueprint"
3. Connect your GitHub repository
4. Render will automatically detect the `render.yaml` file
5. Click "Apply" to deploy

#### Option B: Manual Deployment
1. Go to [Render Dashboard](https://dashboard.render.com/)
2. Click "New +" and select "Web Service"
3. Connect your GitHub repository
4. Configure the service:
   - **Name**: `taskify-api`
   - **Environment**: `Docker`
   - **Region**: Choose closest to your users
   - **Branch**: `main` (or your default branch)
   - **Dockerfile Path**: `./dockerfile`
   - **Health Check Path**: `/health`
5. Click "Create Web Service"

### 3. Environment Variables
The following environment variables are automatically set:
- `ASPNETCORE_ENVIRONMENT=Production`
- `ASPNETCORE_URLS=http://+:5000`
- `PORT` (automatically set by Render)

### 4. Verify Deployment
1. Wait for the build to complete (usually 2-5 minutes)
2. Test your API endpoints:
   - Health check: `https://your-app-name.onrender.com/health`
   - Test endpoint: `https://your-app-name.onrender.com/api/todoitems/test`
   - Swagger UI: `https://your-app-name.onrender.com/swagger` (only in development)

### 5. Update Frontend Configuration
Update your Vue.js frontend to use the new API URL:
```javascript
// Replace localhost:5000 with your Render URL
const API_BASE_URL = 'https://your-app-name.onrender.com/api';
```

## Important Notes

### Database
- SQLite database is created automatically on first run
- Data persists between deployments
- For production, consider using a managed database service

### CORS
- CORS is configured to allow your Netlify frontend
- Update the CORS policy in `Program.cs` if you change your frontend URL

### Monitoring
- Use the `/health` endpoint for monitoring
- Render provides built-in logs and metrics
- Set up alerts for downtime

### Scaling
- Free tier has limitations (sleeps after inactivity)
- Upgrade to paid plan for 24/7 availability
- Consider using Render's managed database for better performance

## Troubleshooting

### Common Issues
1. **Build fails**: Check Dockerfile syntax and .dockerignore
2. **Health check fails**: Verify `/health` endpoint is accessible
3. **Database issues**: Check migration logs in Render dashboard
4. **CORS errors**: Verify frontend URL is in CORS policy

### Logs
- View logs in Render dashboard under your service
- Check for migration errors or startup issues
- Monitor health check responses

## Security Considerations
- Database runs as non-root user
- HTTPS is automatically enabled
- Environment variables are encrypted
- Regular security updates from Microsoft

## Cost Optimization
- Free tier: $0/month (with limitations)
- Paid tier: $7/month for 24/7 availability
- Database: Free with SQLite, or $7/month for managed PostgreSQL 