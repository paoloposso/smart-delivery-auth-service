FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

EXPOSE 80

COPY . ./
RUN dotnet restore "./SmartDelivery.Auth.Api/SmartDelivery.Auth.Api.csproj"
RUN dotnet publish "SmartDelivery.Auth.Api/SmartDelivery.Auth.Api.csproj" -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SmartDelivery.Auth.Api.dll"]