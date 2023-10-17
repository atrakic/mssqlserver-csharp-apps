MAKEFLAGS += --silent

DB ?= db

.PHONY: all test healthcheck clean

all: clean
	$(MAKE) app

%:
	DOCKER_BUILDKIT=1 docker-compose up --build --force-recreate --no-color --remove-orphans $@ -d
	docker-compose ps -a

test:
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

release: ## Release (eg. V=0.0.1)
	 @[ "$(V)" ] && [ ! -z "$(GITHUB_TOKEN)" ] \
		 && read -p "Press enter to confirm and push tag v$(V) to origin, <Ctrl+C> to abort ..." \
		 && git tag $(V) -m "release: $(V)" \
		 && git push origin $(V) -f \
		 && git fetch --tags --force --all -p \
		 && curl -H "Authorization: token $(GITHUB_TOKEN)" \
				-X POST	\
				-H "Accept: application/vnd.github.v3+json"	\
				https://api.github.com/repos/atrakic/$(shell basename $$PWD)/releases \
				-d "{\"tag_name\":\"$(V)\",\"generate_release_notes\":true}"

-include .env
