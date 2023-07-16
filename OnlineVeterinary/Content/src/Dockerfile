FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
WORKDIR /src


COPY ["./OnlineVeterinary.Application/OnlineVeterinary.Application.csproj", "src/OnlineVeterinary.Application/"]
COPY ["./OnlineVeterinary.Contracts/OnlineVeterinary.Contracts.csproj", "src/OnlineVeterinary.Contracts/"]
COPY ["./OnlineVeterinary.Infrastructure/OnlineVeterinary.Infrastructure.csproj", "src/OnlineVeterinary.Infrastructure/"]
COPY ["./OnlineVeterinary.Domain/OnlineVeterinary.Domain.csproj", "src/OnlineVeterinary.Domain/"]
COPY ["./OnlineVeterinary.Api/OnlineVeterinary.Api.csproj", "src/OnlineVeterinary.Api/"]

RUN dotnet restore "src/OnlineVeterinary.Api/OnlineVeterinary.Api.csproj"


COPY . ./

WORKDIR "/src/OnlineVeterinary.Api/"
RUN dotnet build -c Release -o /app/build


FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS runtime
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet","OnlineVeterinary.Api.dll" ]

