MAKEFLAGS += --silent

DB ?= db
MSSQL_SA_PASSWORD ?=

%:
	DOCKER_BUILDKIT=1 docker-compose up --no-color --remove-orphans $@ -d
	docker-compose ps -a

test:
	$(MAKE) $(DB)
	while ! \
		[[ "$$(docker inspect --format "{{json .State.Health }}" $(DB) | jq -r ".Status")" == "healthy" ]];\
		do \
		echo "waiting $(DB) ..."; \
		sleep 1; \
		done
	./tests/test.sh $(DB) $(MSSQL_SA_PASSWORD)

healthcheck:
	docker inspect $(DB) --format "{{ (index (.State.Health.Log) 0).Output }}"

clean:
	docker-compose down --remove-orphans -v --rmi local
