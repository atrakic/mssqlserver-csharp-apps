all: clean
	DOCKER_BUILDKIT=1 docker-compose up --no-color --remove-orphans -d

clean:
	docker-compose down --remove-orphans -v --rmi local

#all:
#	docker build -t aspnetapp .
#	docker run --rm -it -p 8000:8080 -e ASPNETCORE_HTTP_PORTS=8080 aspnetapp
