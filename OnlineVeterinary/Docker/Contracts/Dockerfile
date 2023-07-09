FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /contracts
EXPOSE 80

COPY *.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final-env
WORKDIR /contracts
COPY --from=build-env /contracts/out .
ENTRYPOINT [ "dotnet","OnlineVeterinary.Contracts.dll" ]

