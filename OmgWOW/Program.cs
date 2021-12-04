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

            // variable to make toggle system for operating intake
            int runthat = 0;

            // variable to make toggle system for operating belt
            int runthis = 0;

            // variable to make toggle system for shooter
            int runme = 0;

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

                // when X button is pressed, move intake forward by 100% 
                if (gamepad.GetButton(1) == true && runthat == 0)
                {
                    motorI.Set(ControlMode.PercentOutput, 1);

                    runthat = 1;
                }

                // when X button is pressed again, move intake backward by 100%
                if (gamepad.GetButton(1) == true && runthat == 1)
                {
                    motorI.Set(ControlMode.PercentOutput, -1);

                    runthat = 0;
                }

                // when Y button is pressed, stop intake movement
                if (gamepad.GetButton(4) == true)
                {
                    motorI.Set(ControlMode.PercentOutput, 0);
                }

                // when B button is pressed, stop belt movement
                if (gamepad.GetButton(3) == true)
                {
                    motorB.Set(ControlMode.PercentOutput, 0);
                }

                // when A button is pressed, move belt forward by 100%
                if (gamepad.GetButton(2) == true && runthis == 0)
                {
                    motorB.Set(ControlMode.PercentOutput, 1);

                    runthis = 1;
                }

                // when A button is pressed again, move belt backward by 100%
                if (gamepad.GetButton(2) == true && runthis == 1)
                {
                    motorB.Set(ControlMode.PercentOutput, -1);

                    runthis = 0;
                }

                // when LB is pressed, move shooter motor forward by 100%
                if (gamepad.GetButton(5) == true && runme == 0)
                {
                    motorS.Set(ControlMode.PercentOutput, 1);

                    runme = 1;
                }

                // when LB is pressed again, move shooter motor backward by 100%
                if (gamepad.GetButton(5) == true && runme == 1)
                {
                    motorS.Set(ControlMode.PercentOutput, -1);

                    runme = 0;
                }

                // when RB is pressed, stop shooter motor movement
                if (gamepad.GetButton(6) == true)
                {
                    motorS.Set(ControlMode.PercentOutput, 0);
                }

                if (gamepad.GetButton(7) == true)
                {
                    long startTime = millis();

                    while (millis() - startTime < 3000)
                    {
                        // move forward for 3 seconds
                        motorL.Set(ControlMode.PercentOutput, 1);
                        motorR.Set(ControlMode.PercentOutput, -1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();

                    while (millis() - startTime < 1000)
                    {
                        // turn for 1 second
                        motorL.Set(ControlMode.PercentOutput, 1);
                        motorR.Set(ControlMode.PercentOutput, 1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();
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
        public static long millis()
        {
            return DateTime.Now.Ticks / (TimeSpan.TicksPerMillisecond);
        }
    }
}
