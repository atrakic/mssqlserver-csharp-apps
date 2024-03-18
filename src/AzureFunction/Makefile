all: migrations
	func start

migrations:
	dotnet ef migrations add initial
	dotnet ef database update

