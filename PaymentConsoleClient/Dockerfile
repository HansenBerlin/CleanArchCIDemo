FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["GoPipelineDemo/CalculatorConsoleClient.csproj", "CalculatorConsoleClient/"]
RUN dotnet restore "GoPipelineDemo/CalculatorConsoleClient.csproj"
COPY . .
WORKDIR "/src/CalculatorConsoleClient"
RUN dotnet build "CalculatorConsoleClient.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculatorConsoleClient.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculatorConsoleClient.dll"]
