FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["KafkaDemo.WebApi/KafkaDemo.WebApi.csproj", "KafkaDemo.WebApi/"]
RUN dotnet restore "KafkaDemo.WebApi/KafkaDemo.WebApi.csproj"
COPY . .
WORKDIR "KafkaDemo.WebApi"
RUN dotnet build "KafkaDemo.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "KafkaDemo.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KafkaDemo.WebApi.dll"]
