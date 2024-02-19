# Use a imagem de SDK do ASP.NET Core como base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copie o projeto e restaurar pacotes
#COPY *.csproj ./
#RUN dotnet restore

# Copie e publique o aplicativo
COPY . ./
RUN dotnet publish -c Release -o out


# Criar a imagem de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expor a porta do aplicativo
EXPOSE 8010
EXPOSE 8020


# Definir o comando de inicialização do aplicativo
ENTRYPOINT ["dotnet", "rinha.dll"]