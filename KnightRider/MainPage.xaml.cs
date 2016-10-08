using Windows.System;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media;
using System;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace KnightRider
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private int[] pins = { 5, 6, 13, 19, 26, 12 };
        private const int MAXLEDS = 6;
        private int pinptr;

        private bool goingup;



        private GpioPinValue pinValue;
        private DispatcherTimer timer;


        private GpioPin pin1, pin2, pin3, pin4, pin5, pin6;


        public MainPage()
        {
            InitializeComponent();
            pinptr = 0;
            goingup = true;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += Timer_Tick;
            InitGPIO();

            timer.Start();

        }

        private void InitGPIO()
        {

            var gpio = GpioController.GetDefault();

            pinValue = GpioPinValue.High;

            pin1 = gpio.OpenPin(pins[0]);
            pin1.SetDriveMode(GpioPinDriveMode.Output);

            pin2 = gpio.OpenPin(pins[1]);
            pin2.SetDriveMode(GpioPinDriveMode.Output);

            pin3 = gpio.OpenPin(pins[2]);
            pin3.SetDriveMode(GpioPinDriveMode.Output);

            pin4 = gpio.OpenPin(pins[3]);
            pin4.SetDriveMode(GpioPinDriveMode.Output);

            pin5 = gpio.OpenPin(pins[4]);
            pin5.SetDriveMode(GpioPinDriveMode.Output);

            pin6 = gpio.OpenPin(pins[5]);
            pin6.SetDriveMode(GpioPinDriveMode.Output);
        }



        private void Timer_Tick(object sender, object e)
        {

            pin1.Write(GpioPinValue.High);
            pin2.Write(GpioPinValue.High);
            pin3.Write(GpioPinValue.High);
            pin4.Write(GpioPinValue.High);
            pin5.Write(GpioPinValue.High);
            pin6.Write(GpioPinValue.High);

            switch (pinptr)
            {
                case 0:
                    pin1.Write(GpioPinValue.Low);
                    break;
                case 1:
                    pin2.Write(GpioPinValue.Low);
                    break;
                case 2:
                    pin3.Write(GpioPinValue.Low);
                    break;
                case 3:
                    pin4.Write(GpioPinValue.Low);
                    break;
                case 4:
                    pin5.Write(GpioPinValue.Low);
                    break;
                case 5:
                    pin6.Write(GpioPinValue.Low);
                    break;
                default:
                    break;
            }

            //increase pin to light

            if (goingup)
            {
                if (pinptr < (MAXLEDS - 1))
                {
                    pinptr++;
                }
                else
                {
                    //at top now go down
                    goingup = false;
                    pinptr--;
                }
            }
            else
            {
                if (pinptr > 0)
                {
                    pinptr--;
                }
                else
                {
                    //at bottom go back up
                    goingup = true;
                    pinptr++;
                }

            }
        }
    }
    
}
