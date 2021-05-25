FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0 AS build
WORKDIR /app

# copy full solution over
COPY . .

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
