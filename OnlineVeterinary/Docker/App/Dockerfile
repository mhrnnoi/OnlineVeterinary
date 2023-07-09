FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build-env
EXPOSE 80
WORKDIR /app
COPY ..\..\Content\ OnlineVeterinary.sln ./
RUN dotnet restore
COPY ..\..\Content\src\*.csproj ./
RUN dotnet restore

COPY ..\..\Content\src\* ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine AS final-env
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet","OnlineVeterinary.Api.dll" ]

