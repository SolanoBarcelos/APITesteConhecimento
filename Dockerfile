# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia arquivos da solução e csproj dos projetos
COPY *.sln .
COPY APITesteConhecimento/*.csproj ./APITesteConhecimento/
COPY CoreContato/*.csproj ./CoreContato/


RUN dotnet restore


COPY . .


RUN dotnet publish APITesteConhecimento/APITesteConhecimento.csproj -c Release -o /app/publish


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app


COPY --from=build /app/publish .


EXPOSE 7070

# Ponto de entrada
ENTRYPOINT ["dotnet", "APITesteConhecimento.dll"]
