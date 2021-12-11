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

            // initialize left shooter talon srx
            TalonSRX motorSL = new TalonSRX(3);
            motorSL.ConfigFactoryDefault();
            
            // initialize right shooter talon srx
            TalonSRX motorSR = new TalonSRX(4);
            motorSR.ConfigFactoryDefault();

            // initialize belt talon srx
            TalonSRX motorB = new TalonSRX(5);
            motorB.ConfigFactoryDefault();

            /* simple counter to print and watch using the debugger */
            int counter = 0;

            // variable to make toggle system for operating intake
            int runin = 0;
            
            // variable for shooter toggle system
            int runshoot = 0;
            
            // variable for belt toggle system
            int runbelt = 0;

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

                // when A button is pressed move intake forward by 100% 
                if (gamepad.GetButton(2) == true && runin == 0)
                {
                    motorI.Set(ControlMode.PercentOutput, 1);

                    runin = 1;
                }

                // when A button is pressed again stop intake movement
                if (gamepad.GetButton(2) == true && runin == 1)
                {
                    motorI.Set(ControlMode.PercentOutput, 0);

                    runin = 0;
                }

                /* controlling belt motor */
                
                // when RB button is pressed once move belt motor forward by 100%
                if (gamepad.GetButton(6) == true && runbelt == 0)
                {
                    motorB.Set(ControlMode.PercentOutput, 1);
                    
                    runbelt = 1;
                }

                // when RB button is pressed again stop belt movement
                if (gamepad.GetButton(6) == true && runbelt == 1)
                {
                    motorB.Set(ControlMode.PercentOutput, 0);
                    
                    runbelt = 0;
                }

                /* controlling shooter motors */
                
                // when LB is pressed once move shooter motors forward by 100%
                if (gamepad.GetButton(5) == true && runshoot == 0)
                {
                    motorSL.Set(ControlMode.PercentOutput, 1);
                    motorSR.Set(ControlMode.PercentOutput, -1);
                    
                    runshoot = 1;
                }
                
                // when LB is pressed again stop shooter motors
                if (gamepad.GetButton(5) == true && runshoot == 1)
                {
                    motorSL.Set(ControlMode.PercentOutput, 0);
                    motorSR.Set(ControlMode.PercentOutput, 0);
                    
                    runshoot = 0;
                }
                
                /* auton */

                // auton starts when Y is pressed
                if (gamepad.GetButton(4) == true) {
                    long startTime = millis();

                    while (millis() - startTime < 4000)
                    {
                        // move forward for 4 seconds
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

                    while(millis() - startTime < 3000)
                    {
                        // forward for 3 seconds
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

                    while (millis() - startTime < 3000)
                    {
                        // forward for 3 seconds
                        motorL.Set(ControlMode.PercentOutput, 1);
                        motorR.Set(ControlMode.PercentOutput, -1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();

                    while (millis() - startTime < 2000)
                    {
                        // turn for 2 seconds
                        motorL.Set(ControlMode.PercentOutput, -1);
                        motorR.Set(ControlMode.PercentOutput, -1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();

                    while (millis() - startTime < 2000)
                    {
                        // forward for 2 seconds
                        motorL.Set(ControlMode.PercentOutput, 1);
                        motorR.Set(ControlMode.PercentOutput, -1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();

                    while (millis() - startTime < 4000)
                    {
                        // forward for 4 seconds
                        motorL.Set(ControlMode.PercentOutput, 1);
                        motorR.Set(ControlMode.PercentOutput, -1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();

                    while (millis() - startTime < 3000)
                    {
                        // forward for 3 seconds
                        motorL.Set(ControlMode.PercentOutput, 1);
                        motorR.Set(ControlMode.PercentOutput, -1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();

                    while (millis() - startTime < 2000)
                    {
                        // turn for 2 seconds
                        motorL.Set(ControlMode.PercentOutput, -1);
                        motorR.Set(ControlMode.PercentOutput, -1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();

                    while (millis() - startTime < 3000)
                    {
                        // forward for 3 seconds
                        motorL.Set(ControlMode.PercentOutput, 1);
                        motorR.Set(ControlMode.PercentOutput, -1);
                    }

                    motorL.Set(ControlMode.PercentOutput, 0);
                    motorR.Set(ControlMode.PercentOutput, 0);

                    startTime = millis();

                    while (millis() - startTime < 2000)
                    {
                        // turn for 2 seconds
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
