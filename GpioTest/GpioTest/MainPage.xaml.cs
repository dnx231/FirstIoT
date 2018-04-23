using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Devices.Gpio;

// Die Elementvorlage "Leere Seite" wird unter https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x407 dokumentiert.

namespace GpioTest
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int[] _pinNumbers = new[] { 12, 16, 20, 21 };
        private GpioPin[] _pins = new GpioPin[4];
        private bool _gpioConnected = false;

        public MainPage()
        {
            this.InitializeComponent();
            _gpioConnected = InitGPIO();
        }

        private bool InitGPIO()
        {
            var gpio = GpioController.GetDefault();
            if (gpio == null)
                return false;
            for (int i = 0; i < 4; i++)
            {
                _pins[i] = gpio.OpenPin(_pinNumbers[i]);
                _pins[i].Write(GpioPinValue.Low);
                _pins[i].SetDriveMode(GpioPinDriveMode.Output);
            }
            return true;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cbCurrent = (CheckBox)sender;
            int channelNumber = Int32.Parse(cbCurrent.Tag.ToString()) - 1;
            if (_gpioConnected)
            {
                _pins[channelNumber].Write(GpioPinValue.High);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cbCurrent = (CheckBox)sender;
            int channelNumber = Int32.Parse(cbCurrent.Tag.ToString()) - 1;
            if (_gpioConnected)
            {
                _pins[channelNumber].Write(GpioPinValue.Low);
            }
        }
    }
}
