FROM mcr.microsoft.com/dotnet/sdk:10.0-preview AS build
WORKDIR /src

COPY ["PharmaCore.API/PharmaCore.API.csproj", "PharmaCore.API/"]
COPY ["PharmaCore.Domain/PharmaCore.Domain.csproj", "PharmaCore.Domain/"]
COPY ["PharmaCore.Application/PharmaCore.Application.csproj", "PharmaCore.Application/"]
COPY ["PharmaCore.Infrastructure/PharmaCore.Infrastructure.csproj", "PharmaCore.Infrastructure/"]

RUN dotnet restore "PharmaCore.API/PharmaCore.API.csproj"

COPY . .
WORKDIR "/src/PharmaCore.API"
RUN dotnet publish "PharmaCore.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0-preview AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 5117
ENV ASPNETCORE_URLS=http://+:5117
ENTRYPOINT ["dotnet", "PharmaCore.API.dll"]
