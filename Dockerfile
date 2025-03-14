FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY MoneyKeeper-Back.csproj .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT [ "dotnet", "MoneyKeeper-Back.dll" ]