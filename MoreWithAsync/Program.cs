using System;
using System.Linq;
using System.Threading.Tasks;

namespace MoreWithAsync
{
    class Program
    {

        static TaskCompletionSource<bool> tcs;
        static Random rnd;

        static void Main(string[] args)
        {
            //Console.WriteLine(ShowTodaysInfo().Result);
            //DisplayCurrentInfo().Wait();
            Console.WriteLine($"You rolled {GetDiceRoll().Result}");          
        }

        /*static async Task Main()
        {
            tcs = new TaskCompletionSource<bool>();
            var secondHandlerFinished = tcs.Task;

            var button = new NaiveButton();
            button.Clicked += Button_Clicked_1;
            button.Clicked += Button_Clicked_2_Async;
            button.Clicked += Button_Clicked_3;

            Console.WriteLine("About to click a button...");
            button.Click();
            Console.WriteLine("Button's Click method returned.");

            await secondHandlerFinished;
        }*/

        private static async Task<string> ShowTodaysInfo()
        {
            string ret = $"Today is {DateTime.Today:D}\n" +
                         "Today's hours of leisure: " +
                         $"{await GetLeisureHours()}";
            return ret;
        }

        static async Task<int> GetLeisureHours()
        {
            // Task.FromResult is a placeholder for actual work that returns a string.  
            var today = await Task.FromResult<string>(DateTime.Now.DayOfWeek.ToString());

            // The method then can process the result in some way.  
            int leisureHours;
            if (today.First() == 'S')
                leisureHours = 16;
            else
                leisureHours = 5;

            return leisureHours;
        }

        static async Task DisplayCurrentInfo()
        {
            /*await WaitAndApologize();
            Console.WriteLine($"Today is {DateTime.Now:D}");
            Console.WriteLine($"The current time is {DateTime.Now.TimeOfDay:t}");
            Console.WriteLine("The current temperature is 76 degrees.");*/

            Task wait = WaitAndApologize();

            string output = $"Today is {DateTime.Now:D}\n" +
                            $"The current time is {DateTime.Now.TimeOfDay:t}\n" +
                            $"The current temperature is 76 degrees.\n";
            await wait;
            Console.WriteLine(output);
        }

        static async Task WaitAndApologize()
        {
            // Task.Delay is a placeholder for actual work.  
            await Task.Delay(2000);
            // Task.Delay delays the following line by two seconds.  
            Console.WriteLine("\nSorry for the delay. . . .\n");

            /*var myTask = Task.Delay(2000);
            // Task.Delay delays the following line by two seconds.  
            Console.WriteLine("\nSorry for the delay. . . .\n");
            await myTask;*/
        }


        private static void Button_Clicked_1(object sender, EventArgs e)
        {
            Console.WriteLine("   Handler 1 is starting...");
            Task.Delay(100).Wait();
            Console.WriteLine("   Handler 1 is done.");
        }

        private static async void Button_Clicked_2_Async(object sender, EventArgs e)
        {
            Console.WriteLine("   Handler 2 is starting...");
            Task.Delay(100).Wait();
            Console.WriteLine("   Handler 2 is about to go async...");
            await Task.Delay(500);
            Console.WriteLine("   Handler 2 is done.");
            tcs.SetResult(true);
        }

        private static void Button_Clicked_3(object sender, EventArgs e)
        {
            Console.WriteLine("   Handler 3 is starting...");
            Task.Delay(100).Wait();
            Console.WriteLine("   Handler 3 is done.");
        }


        private static async ValueTask<int> GetDiceRoll()
        {
            Console.WriteLine("...Shaking the dice...");
            int roll1 = await Roll();
            int roll2 = await Roll();
            return roll1 + roll2;
        }

        private static async ValueTask<int> Roll()
        {
            if (rnd == null)
                rnd = new Random();

            await Task.Delay(500);
            int diceRoll = rnd.Next(1, 7);
            return diceRoll;
        }
    }

    public class NaiveButton
    {
        public event EventHandler Clicked;

        public void Click()
        {
            Console.WriteLine("Somebody has clicked a button. Let's raise the event...");
            Clicked?.Invoke(this, EventArgs.Empty);
            Console.WriteLine("All listeners are notified.");
        }
    }
}
