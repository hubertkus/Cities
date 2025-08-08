# Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
RUN mkdir /app ; mkdir /out
WORKDIR /app
COPY . .
RUN dotnet restore /app/Cities.csproj
RUN dotnet publish /app/Cities.csproj -c Release -o /out

# Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
RUN mkdir /app
COPY --from=build /out /app
ENV ASPNETCORE_URLS=http://*:80
WORKDIR /app
CMD ./Cities
