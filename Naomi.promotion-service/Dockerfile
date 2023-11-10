FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://*:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Naomi.promotion-service/Naomi.promotion-service.csproj", "Naomi.promotion-service/"]
RUN dotnet restore "Naomi.promotion-service/Naomi.promotion-service.csproj"
COPY . .
WORKDIR "/src/Naomi.promotion-service"
RUN dotnet build "Naomi.promotion-service.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Naomi.promotion-service.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Naomi.promotion-service.dll"]
