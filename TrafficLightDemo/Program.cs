using System;

namespace TrafficLightDemo
{
    class Program
    {
        // Class: TrafficLightController that requires a 1 second event called Tick
        // Summary: Class contains an event Tick, delegate TickHandler, Subscribe method
        // and Start method. The event has no parameters and nor return type.
        public class TrafficLightController
        {
            // define the event Tick. It is of type TickHandler
            public event TickHandler Tick;
            // the delegate signature is now declared.  It is called TickHandler.  It has no 
            // parameters and it returns nothing
            public delegate void TickHandler();
            // Subscribe receives an event handler method and adds it to the event invocation 
            // list Tick
            // The event handler method must be of type TickHandler
            public void Subscribe(TickHandler T)
            {
                // Add the event handler to the event invocation list
                Tick += T;
            }
            // Start will sleep for 1 second and then fire the event Tick until someone hits 
            // a key on the keyboard
            public void Start()
            {
                // while there is no key hit
                while (Console.KeyAvailable == false)
                {
                    // sleep for 1 second
                    System.Threading.Thread.Sleep(1000);
                    // if there is at least one element in the event invocation list, fire 
                    // the event
                    if (Tick != null) Tick();
                }
                return;
            }
        }

        // Class: Traffic Light 
        // Summary: Class contains 3 backing field string and integer variables. A 
        // constructor retrieve name and time and set the data of the parameter. A 
        // subscribe method to the TrafficLightController, and click method to initiate 
        // the time and traffic light color change.
        public class TrafficLight
        {
            // Declare variables string TrafficLight Color and integer counter and start 
            // time.        
            public string TrafficLightColor;
            private int counter;
            int StartSecond;

            // A constructor hold 2 parameters that retrieve and save traffic light color,
            // and start time
            public TrafficLight(string name, int startSecond)
            {
                TrafficLightColor = name;
                StartSecond = startSecond;
                return;
            }

            // A method that subscribe the TrafficLightController class and call the click
            // method
            public void ConnectToController(TrafficLightController m)
            {
                m.Subscribe(Click);
            }

            // A method to call back each second in the ConnectToController. It prints the 
            // message of the traffic light time count and traffic light color change. 
            private void Click()
            {
                // initialize the offset = start time
                int offset = StartSecond;
                // initialize the module of 16 seconds
                int mod = counter % 16;
                // set module = module + offset time.
                mod += offset;
                // Case shows 0, 16, 8, 14 seconds and print the message of the count seconds
                // & traffic light color.
                switch (mod)
                {
                    // At 0 seconds
                    case 0:
                    // Traffic light time count 16 seconds and display the current traffic
                    // light color
                    case 16:
                        Console.WriteLine($"At second {counter} {TrafficLightColor} is red");
                        break;
                    // Traffic light time count 8 seconds and display the current traffic
                    // light color
                    case 8:
                        Console.WriteLine($"At second {counter} {TrafficLightColor} is " +
                            $"green");
                        break;
                    // Traffic light time count 14 seconds and display the current traffic
                    // light color
                    case 14:
                        Console.WriteLine($"At second {counter} {TrafficLightColor} is " +
                            $"yellow");
                        break;
                }
                counter++;
            }
        }
        static void Main(string[] args)
        {
            // make an instance of the traffic light controller
            TrafficLightController TLC = new TrafficLightController();
            // offset within a 16 second cycle
            // Create traffic light instances for North/South and East/West
            TrafficLight NS = new TrafficLight("Bangerter Highway North and South Bound", 0);
            TrafficLight EW = new TrafficLight("6200 South East & West Bound", 8);
            // Connect each traffic light instance to the controller
            NS.ConnectToController(TLC);
            EW.ConnectToController(TLC);
            // Once they are connected, start the controller
            TLC.Start();
        }
    }
}
