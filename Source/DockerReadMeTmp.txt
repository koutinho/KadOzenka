# Временный файл. Будет переписан на docker-compose.

# создаем image, где -t - имя образа, 
# "." - путь к Dockerfile, --target - запуск только этапа тестирования (все, что после - запускаться не будет)
docker build -t miomo_test . --target unitTestsFromDal
docker build -t miomo_test . --target unitTestsFromWeb
docker build -t miomo_test . --target test


# запускаем процесс в контейнере на базе образа
docker run --name miomo_test miomo_test