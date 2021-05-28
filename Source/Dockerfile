FROM mcr.microsoft.com/dotnet/nightly/sdk:5.0 AS build
WORKDIR /app

# copy full solution over
COPY . .

#FROM build AS test  
#LABEL test=true  
#RUN dotnet test -c Release --results-directory /testresults --logger "trx;LogFileName=test_results.trx" /app/KadOzenka.Dal.Tests/KadOzenka.Dal.UnitTests.csproj
#RUN dotnet test -c Release --results-directory /testresults --logger "trx;LogFileName=test_results2.trx" /app/KadOzenka.Web.Tests/KadOzenka.Web.UnitTests.csproj

# запуск Unit-тестов для DAL
FROM build AS unit_tests_dal
WORKDIR /app/KadOzenka.Dal.UnitTests
# можно запустить тесты во время сборки образа, но тогда будут проблемы 
# при внедрении в pipeline, т.е. не откуда будет вытащить результаты тестирования
RUN dotnet test --logger:trx
#CMD ["dotnet", "test", "--logger:trx"]

# запуск Unit-тестов для WEB
# ERROR: падают из-за ошибки при копировании KadOzenka.Web.Config
FROM unit_tests_dal AS unit_tests_web
WORKDIR /app/KadOzenka.Web.UnitTests
RUN dotnet test --logger:trx
#CMD ["dotnet", "test", "--logger:trx"]
