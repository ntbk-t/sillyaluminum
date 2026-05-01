CWD=$(shell pwd)
PROJECT_PATH=$(CWD)/sillyaluminum

VINTAGE_STORY_ARGS=\
	--openWorld "modding test world"\
	--tracelog\
	--addModPath $(PROJECT_PATH)/bin/Release/Mods\
	--addOrigin $(PROJECT_PATH)/assets

build:
	dotnet run --project ./CakeBuild/CakeBuild.csproj

test: build
	$(VINTAGE_STORY)/Vintagestory $(VINTAGE_STORY_ARGS)