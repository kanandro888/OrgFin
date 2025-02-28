# Etapa 1: Construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["APIExcel.csproj", "APIExcel/"]
WORKDIR "/src/APIExcel"
RUN dotnet restore

COPY . . 
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Execução
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Expor a porta correta
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "APIExcel.dll"]
