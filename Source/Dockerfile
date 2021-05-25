FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0 AS build
WORKDIR /app
#COPY *.sln .
#COPY src/Example.Service/*.csproj ./src/Example.Service/
#COPY test/Example.Service.UnitTest/*.csproj ./test/Example.Service.UnitTest/
#COPY test/Example.Service.ComponentTest/*.csproj ./test/Example.Service.ComponentTest/
#RUN dotnet restore
# copy full solution over
COPY . .

#RUN dotnet build
#FROM build AS testrunner
#WORKDIR /app/test/Example.Service.UnitTest
#CMD ["dotnet", "test", "--logger:trx"]

# run the unit tests
#FROM build AS test
#WORKDIR /app/test/Example.Service.UnitTest
#RUN dotnet test --logger:trx

#FROM build AS test  
#LABEL test=true  
#RUN dotnet test -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /app/KadOzenka.Dal.Tests/KadOzenka.Dal.Tests.csproj
#RUN dotnet test -c Release --results-directory /testresults --logger "trx;LogFileName=test_results2.trx" /app/KadOzenka.Web.Tests/KadOzenka.Web.Tests.csproj

# запуск Unit-тестов для DAL
FROM build AS unitTestsFromDal
WORKDIR /app/KadOzenka.Dal.Tests
# можно запустить тесты во время сборки образа, но тогда буднт проблемы 
# при внедрении в pipeline
#RUN dotnet test --logger:trx
CMD ["dotnet", "test", "--logger:trx"]

# запуск Unit-тестов для WEB
#FROM build AS unitTestsFromWeb
#WORKDIR /app/KadOzenka.Web.Tests
# можно запустить тесты во время сборки образа, но тогда буднт проблемы 
# при внедрении в pipeline
#RUN dotnet test --logger:trx
#CMD ["dotnet", "test", "--logger:trx"]

# publish the API
#FROM build AS publish
#WORKDIR /app/src/Example.Service
#RUN dotnet publish -c Release -o out

# run the api
#FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0 AS runtime
#WORKDIR /app
#COPY --from=publish /app/src/Example.Service/out ./
#EXPOSE 80
#ENTRYPOINT ["dotnet", "Example.Service.dll"]