FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src

COPY *.sln ./
COPY ["WebApiTeste/WebApiTeste.csproj", "WebApiTeste/"]
COPY ["WebApiTeste.Application/WebApiTeste.Application.csproj", "WebApiTeste.Application/"]
COPY ["WebApiTeste.Domain/WebApiTeste.Domain.csproj", "WebApiTeste.Domain/"]
COPY ["WebApiTeste.Infrastructure/WebApiTeste.Infrastructure.csproj", "WebApiTeste.Infrastructure/"]

RUN dotnet restore
COPY . .

WORKDIR "/src/WebApiTeste"
#RUN dotnet build "WebApiTeste.csproj" -c Release -o /app/build
RUN dotnet build -c Release -o /app/build

FROM build AS publish
#RUN dotnet publish "WebApiTeste.csproj" -c Release -o /app/publish
RUN dotnet publish -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENV DATABASE_SERVER web-api-teste-db
ENV DATABASE_NAME testedb
ENV DATABASE_LOGIN sa
ENV DATABASE_LOGIN sa
ENV DATABASE_PASSWORD Mudar@123

ENTRYPOINT ["dotnet", "WebApiTeste.dll"]