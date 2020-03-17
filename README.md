# Klipper + Prusa Menu

This repo contains some examples of configuration files to use with Klipper firmware

- **printer.cfg :** Configuration example file for a Creality CR10sPro

- **printer_macros.cfg :** Extends with new macros missing GCodes in Klipper

- **printer_menu.cfg :** Adds new menu entry points, similar in structure to those of Prusa for Klipper firmware (It is still under development). 


![example of a cube](https://github.com/netonjm/Klipper-CR10sPro/raw/master/images/cube.png)

![prusa menu](https://github.com/netonjm/Klipper-CR10sPro/raw/master/images/screen-prusa.png)


# Getting started

Use this configuarion files it's extremely easy, simply copy macros.cfg and menu.cfg in the same folder than **printer.cfg** and add this lines into it:

    [include printer_macros.cfg]
    [include printer_menu.cfg]

in `[display]` section add:

`menu_root: __main`


you are ready to go!
