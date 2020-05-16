using System;
using Gdk;
using Gtk;
using IoTSharp.Components;
using Unosquare.RaspberryIO.Abstractions;
using Unosquare.WiringPi;

public interface IScreen
{
    void OnKeyDown(EventKey evnt);
    void OnRotatoryClicked();
}

enum WindowScreen
{
    Main,
    Menu,
}

public class RotaryEncoder : IoTComponent, IRotaryEncoder3
{
    bool clkLastState;

    readonly IGpioPin clockPin;
    readonly IGpioPin dtPin;

    public long Value { get; private set; }

    public RotaryEncoder (Connectors clockConnector, Connectors dtConnector)
    {
        this.clockPin = clockConnector.Pin();
        this.clockPin.InputPullMode = GpioPinResistorPullMode.PullUp;
        this.clockPin.PinMode = GpioPinDriveMode.Input;
        this.clockPin.RegisterInterruptCallback(EdgeDetection.FallingEdge, () => {
            Console.WriteLine("");
        });

        this.dtPin = dtConnector.Pin();
        this.dtPin.InputPullMode = GpioPinResistorPullMode.PullUp;
        this.dtPin.PinMode = GpioPinDriveMode.Input;
        dtPin.RegisterInterruptCallback(EdgeDetection.FallingEdge, () => {
            Console.WriteLine("");
        });


        Value = 0;
        clkLastState = this.clockPin.Value;
    }

    public override void OnUpdate()
    {

        var clkState = clockPin.Value;
        var dtState = dtPin.Value;
        if (clkState != clkLastState)
        {
            if (dtState != clkState)
                Value += 1;
            else
                Value -= 1;
            Console.WriteLine(Value);
        }

        clkLastState = clkState;
    }

    public override void OnDispose()
    {
        base.OnDispose();
    }
}

public class MainWindow : Gtk.Window
{
    IScreen currentWidget;
    RotaryEncoder3 rotaryEncoder3;
    SimpleButton simpleButton;

    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        rotaryEncoder3 = new RotaryEncoder3(Connectors.GPIO17, Connectors.GPIO18);
        simpleButton = new SimpleButton(Connectors.GPIO27);
       
        ChangeScreen(WindowScreen.Main);

        simpleButton.Clicked += (s, e) => Click();

        ShowAll();
    }

    int count = 0;

    void Click ()
    {
        count++;
        if (count >= 3)
        {
            count = 0;
            currentWidget.OnRotatoryClicked();
        }
    }

    protected override bool OnKeyPressEvent(EventKey evnt)
    {
        currentWidget.OnKeyDown(evnt);
        return base.OnKeyPressEvent(evnt);
    }


    protected override bool OnConfigureEvent(EventConfigure evnt)
    {
        var configure = base.OnConfigureEvent(evnt);
        //RefreshImage ();
        return configure;
    }


    void ChangeScreen (WindowScreen screen)
    {
        switch (screen)
        {
            case WindowScreen.Main:
                var mainScreen = new StatusMenuWidget();
                mainScreen.MenuScreen += (s, e) => {
                    ChangeScreen (WindowScreen.Menu);
                };
                SetScreenWidget (mainScreen);
                break;
            case WindowScreen.Menu:
                var menuScreen = new MenuWidget();
                menuScreen.ShowMainScreen += (s, e) => {
                    ChangeScreen (WindowScreen.Main);
                };
                SetScreenWidget (menuScreen);
                break;
        }
    }

    void SetScreenWidget (IScreen widget)
    {
        if (Child != null)
            Remove(Child);

        currentWidget = widget;
        Add(widget as Widget);
        ShowAll ();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Gtk.Application.Quit();
        a.RetVal = true;
    }
}
