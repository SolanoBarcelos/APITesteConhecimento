# APITesteConhecimento

# Video esplicativo do projeto e imagens est�o na pasta zipada do repositorio.

## Projeto para apresenta��o de conhecimento em C#, .NET Core., EntityFramework, PostgreSQL, xUnit. 

### Projeto cadastro de usu�rios contendo principais opera��es CRUD.

# Rodar projeto em ambiente on premisse local:

## Rodar Migrations

### Instalar
dotnet tool install --global dotnet-ef
### Rodar Migration inicial no projeto API
dotnet ef migrations add InitialCreate --project APITesteConhecimento --startup-project APITesteConhecimento
### Colocar migration no banco
dotnet ef database update --project APITesteConhecimento --startup-project APITesteConhecimento



## Comandos para containeriza��o das aplica��es separadas em ambiente de testes antes de fazer o Docker composse

### Criar network para os containers
docker network create testeconhecimento
### Ver a rede
docker network ls

## DB

### Baixar imagem do Postgres
docker pull postgres:latest
### sobe container do banco
docker run --name postgres_db_testeconhecimento --network testeconhecimento -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=1234 -e POSTGRES_DB=db_bteste_conhecimento -p 5432:5432 -d postgres


