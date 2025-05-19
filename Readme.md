# APITesteConhecimento

# Video esplicativo do projeto e imagens estão na pasta zipada do repositorio.

## Projeto para apresentação de conhecimento em C#, .NET Core., EntityFramework, PostgreSQL, xUnit. 

### Projeto cadastro de usuários contendo principais operações CRUD.

# Rodar projeto em ambiente on premisse local:

## Rodar Migrations

### Instalar
dotnet tool install --global dotnet-ef
### Rodar Migration inicial no projeto API
dotnet ef migrations add InitialCreate --project APITesteConhecimento --startup-project APITesteConhecimento
### Colocar migration no banco
dotnet ef database update --project APITesteConhecimento --startup-project APITesteConhecimento



## Comandos para containerização das aplicações separadas em ambiente de testes antes de fazer o Docker composse

### Criar network para os containers
docker network create testeconhecimento
### Ver a rede
docker network ls

## DB

### Baixar imagem do Postgres
docker pull postgres:latest
### sobe container do banco
docker run --name postgres_db_testeconhecimento --network testeconhecimento -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=1234 -e POSTGRES_DB=db_bteste_conhecimento -p 5432:5432 -d postgres


