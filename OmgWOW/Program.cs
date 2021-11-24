using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using System;
using System.Threading;

using CTRE.Phoenix;
using CTRE.Phoenix.Controller;
using CTRE.Phoenix.MotorControl;
using CTRE.Phoenix.MotorControl.CAN;

namespace OmgWOW
{
    public class Program
    {
        public static void Main()
        {
            // initialize gamepad
            GameController gamepad = new GameController(UsbHostDevice.GetInstance(0));

            // initialize left talon srx
            TalonSRX motorL = new TalonSRX(0);
            motorL.ConfigFactoryDefault();

            // initialize right talon srx
            TalonSRX motorR = new TalonSRX(1);
            motorR.ConfigFactoryDefault();

            // initialize intake talon srx
            TalonSRX motorI = new TalonSRX(2);
            motorI.ConfigFactoryDefault();

            // initialize shooter talon srx
            TalonSRX motorS = new TalonSRX(3);
            motorS.ConfigFactoryDefault();

            // initialize belt talon srx
            TalonSRX motorB = new TalonSRX(4);
            motorB.ConfigFactoryDefault();

            /* simple counter to print and watch using the debugger */
            int counter = 0;

            /* loop forever */
            while (true)
            {
                /* print the axis values */
                Debug.Print("axis left (up-down): " + gamepad.GetAxis(1) * -1);
                Debug.Print("axis right (up-down): " + gamepad.GetAxis(5) * -1);

                /* pass axis values to talons */
                motorL.Set(ControlMode.PercentOutput, gamepad.GetAxis(1) * -1);
                motorR.Set(ControlMode.PercentOutput, gamepad.GetAxis(5));

                /* controlling the motor for intake */

                // when X button is pressed, move intake forward by 50% 
                if (gamepad.GetButton(1) == true)
                {
                    motorI.Set(ControlMode.PercentOutput, 50);
                }

                // when Y button is pressed, move intake backward by 25%
                if (gamepad.GetButton(4) == true)
                {
                    motorI.Set(ControlMode.PercentOutput, -25);
                }

                // when B button is pressed, stop intake movement
                if (gamepad.GetButton(3) == true)
                {
                    motorI.Set(ControlMode.PercentOutput, 0);
                }

                /* when A button is pressed, move belt for shooter and shooter forward by 100%, 
                   wait 2.5 seconds, then stop both belt for shooter and shooter */
                if (gamepad.GetButton(2) == true)
                {
                    motorB.Set(ControlMode.PercentOutput, 100);
                    motorS.Set(ControlMode.PercentOutput, 100);

                    System.Threading.Thread.Sleep(2500);

                    motorB.Set(ControlMode.PercentOutput, 0);
                    motorS.Set(ControlMode.PercentOutput, 0);
                }

                /* allow motor control */
                CTRE.Phoenix.Watchdog.Feed();

                // add to counter value
                ++counter;

                // print counter value
                Debug.Print("Counter: " + counter);

                /* wait a bit */
                System.Threading.Thread.Sleep(100);
            }
        }
    }
}