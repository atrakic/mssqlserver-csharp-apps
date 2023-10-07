all:
	DOCKER_BUILDKIT=1 docker-compose up --remove-orphans -d

test:
	./tests/test.sh $(db)

healthcheck:
	docker inspect $(db) --format "{{ (index (.State.Health.Log) 0).Output }}"

clean:
	docker-compose down --remove-orphans -v --rmi local
