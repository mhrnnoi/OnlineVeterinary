FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /application
EXPOSE 80

COPY ..\..\Content\src\*.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final-env
WORKDIR /application
COPY --from=build-env /application/out .
ENTRYPOINT [ "dotnet","OnlineVeterinary.Application.dll" ]

