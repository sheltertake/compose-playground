FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /app

COPY . .

# RUN dotnet run
# RUN /bin/bash

RUN dotnet restore 
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

EXPOSE 5002
ENTRYPOINT ["dotnet", "api2.dll"]
# RUN /bin/bash