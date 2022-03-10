FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoPipelineDemo/GoPipelineDemo.csproj", "GoPipelineDemo/"]
RUN dotnet restore "GoPipelineDemo/GoPipelineDemo.csproj"
COPY . .
WORKDIR "/src/GoPipelineDemo"
RUN dotnet build "GoPipelineDemo.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GoPipelineDemo.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GoPipelineDemo.dll"]
