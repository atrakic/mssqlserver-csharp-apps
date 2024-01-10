MAKEFLAGS += --silent

##.DEFAULT_GOAL := help

.PHONY: all apply bundle last undo list clean help

all:  ## Run all commands and demonstrate migration flow
	$(eval count :=$(shell dotnet ef migrations list  --no-color --no-build --json | jq 'length'))
	if [[ $(count) -eq 0 ]];then\
		dotnet ef migrations add InitialCreate; \
	else \
		$(eval count :=$(shell echo $$(($(count)+1)))) \
		dotnet ef migrations add Demo_$(count); \
	fi
	dotnet run
	git add ./Migrations
	git commit -m "chore(migrations): applied $(lastMigration)"

last: ## Get the last migration
	dotnet ef migrations script --idempotent

undo: ## Undo the last migration
	dotnet ef migrations remove --force

list: ## List all migrations
	dotnet ef migrations list  --no-color --no-build

#bundle:
#	dotnet ef migrations bundle --self-contained --verbose --force

clean: ## Remove all migrations
	dotnet ef database update 0
	dotnet ef migrations remove -f
	rm -rf ${here}/Migrations/*.cs

help:
	cat $(MAKEFILE_LIST) | grep -e "^[a-zA-Z_\-]*: *.*## *" | awk 'BEGIN {FS = ":.*?## "}; {printf "\033[36m%-30s\033[0m %s\n", $$1, $$2}'

here=$(shell pwd -P)
lastMigration := $(shell dotnet ef migrations list | tail -n 1)"

-include include.mk
