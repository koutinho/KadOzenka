Удаление незадействованных томов 
	docker volume rm $(docker volume ls -q)

Остановка всех томов 
	docker-compose down --volumes

Удаление всех остановленных контейнеров
    docker rmi -f $(docker images -qa)

docker-compose -f docker-compose.local.yml up -d
docker-compose -f docker-compose.debug.yml up -d
docker-compose -f docker-compose.qa.yml up -d
docker-compose -f docker-compose.demo.yml up -d
docker-compose -f docker-compose.uat.yml up -d

cd dbmigrations

docker-compose -f docker-compose.local.yml up -d
docker-compose -f docker-compose.local.yml down

removecontainers() {
    docker stop $(docker ps -aq)
    docker rm $(docker ps -aq)
}

armageddon() {
    removecontainers
    docker network prune -f
    docker rmi -f $(docker images --filter dangling=true -qa)
    docker volume rm $(docker volume ls --filter dangling=true -q)
    docker rmi -f $(docker images -qa)
}