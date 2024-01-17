-include ../../.env

all: clean
	DOCKER_BUILDKIT=1 docker-compose up --no-color --remove-orphans -d

clean:
	docker-compose down --remove-orphans -v --rmi local
