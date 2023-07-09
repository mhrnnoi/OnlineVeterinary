FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /api
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final-env
WORKDIR /api
COPY --from=build-env /api/out .
ENTRYPOINT [ "dotnet","OnlineVeterinary.Api.dll" ]

