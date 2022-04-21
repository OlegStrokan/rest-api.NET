FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["mvc.NET/mvc.NET.csproj", "mvc.NET/"]
RUN dotnet restore "mvc.NET.csproj"
COPY . .
RUN dotnet publish "mvc.NET.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dontet", "mvc.NET.dll"]
