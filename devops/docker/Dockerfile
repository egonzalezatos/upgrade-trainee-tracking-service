FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
EXPOSE 80
WORKDIR /app
COPY ./release .

#Config ENVS
#DATABASE
ENV DB_SERVER=mongo
ENV DB_USERNAME=admin
ENV DB_PASSWORD=admin 
ENV DB_PORT=27017
ENV DB_DATABASE=course-tracking-db

#REDIS
ENV REDIS_HOST=localhost
ENV REDIS_PORT=6379
ENV REDIS_PASS=Admin123

#GRPC
ENV GRPC_PMS_HOST=profile-manage-ip
ENV GRPC_PMS_PORT=666
ENV GRPC_CS_HOST=course-ip
ENV GRPC_CS_PORT=667

ENTRYPOINT ["dotnet", "Upgrade.TraineeTracking.dll"]