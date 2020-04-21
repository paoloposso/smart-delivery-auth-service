FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app


COPY . ./
RUN dotnet restore "./SmartDelivery.Auth.Api/SmartDelivery.Auth.Api.csproj"
RUN dotnet publish "SmartDelivery.Auth.Api/SmartDelivery.Auth.Api.csproj" -c Release -o out

EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "SmartDelivery.Auth.Api.dll"]