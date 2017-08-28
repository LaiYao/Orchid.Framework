FROM microsoft/aspnetcore:1.1
ARG source
WORKDIR /app
EXPOSE 10270
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "Orchid.Cloud.Agent.dll"]