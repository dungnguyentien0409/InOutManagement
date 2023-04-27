#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["InOutHistoryApi/InOutHistoryApi.csproj", "InOutHistoryApi/"]
COPY ["Common/Common.csproj", "Common/"]
COPY ["DataAccessEF/DataAccessEF.csproj", "DataAccessEF/"]
COPY ["Domain/Domain.csproj", "Domain/"]
RUN dotnet restore "InOutHistoryApi/InOutHistoryApi.csproj"
COPY . .
WORKDIR "/src/InOutHistoryApi"
RUN dotnet build "InOutHistoryApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InOutHistoryApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InOutHistoryApi.dll"]
