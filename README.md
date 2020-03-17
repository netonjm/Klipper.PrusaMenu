# Klipper ❤️ Prusa Menu

This repo contains some examples of configuration files to use with Klipper firmware


- **printer.cfg :** Configuration example file for a Creality CR10sPro

- **printer_macros.cfg :** Extends with new macros missing GCodes in Klipper

- **printer_menu.cfg :** Adds new menu entry points, similar in structure to those of Prusa for Klipper firmware (It is still under development). 


![prusa menu](https://github.com/netonjm/Klipper-CR10sPro/raw/master/images/screen-prusa.png)


# Getting started

Use this configuarion files it's extremely easy, simply clone this repo in your raspberry:

    git clone https://github.com/netonjm/Klipper.PrusaMenu
    
    cd Klipper.PrusaMenu
    
    make update

This will copy **printer_macros.cfg** and **printer_menu.cfg** into your home folder

Next step is open your **printer.cfg** and add this lines:

    [include printer_macros.cfg]
    [include printer_menu.cfg]

Last step is in `[display]` section override the menu with:

`menu_root: __main`

Restart your klipper service and enjoy!

# How to update

Simply go to your prusa menu folder

    cd ~/Klipper.PrusaMenu

and run:

    make update
    
Restart your klipper service and done!

