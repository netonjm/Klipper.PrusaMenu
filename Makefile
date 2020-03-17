#General vars
ARGS?=/restore /p:Configuration=Release

all:

install:
	echo "Updating current config files..."
	cp ./printer_macros.cfg ~/
	cp ./printer_menu.cfg ~/
	echo "done."

.PHONY: all install
