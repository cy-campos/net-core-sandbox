using System;

namespace NetCoreSandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
        }
    }
    
    class Person
    {
        Actions actions;
        
        public Person() 
        {
            Console.WriteLine("A new person wakes up");
            actions = new Actions();
            actions.WorkDoneEventHandler += onWorkDone;
            
            startDay();
            
        }
        
        private void startDay() 
        {
            Console.WriteLine("Lets start our day");
            Actions.CookFoodHandler cookBreakfast = cookEggs;
            actions.CookFood(cookBreakfast);
            
            actions.DoWork();
        }
        
        private void onWorkDone(object sender, EventArgs args)
        {
            actions.GoHome();
            Actions.CookFoodHandler cookDinner = cookEggs;
            cookDinner += cookSteak;
            cookDinner += cookPotatoes;

            actions.CookFood(cookDinner);
        }
        
        public void cookEggs(string specialPrep) 
        {
            Console.WriteLine("He cooks eggs" + specialPrep);
        }
        
        public void cookSteak(string specialPrep)
        {
            Console.WriteLine("Cooks steak");
        }
        
        public void cookPotatoes(string specialPrep)
        {
            Console.WriteLine("Cooks potatoes");
        }
    }
    
    public class Actions
    {
        public Actions() { }
                
        public event EventHandler WorkDoneEventHandler;
        protected void OnWorkDone(object sender, EventArgs args)
        {
            if (WorkDoneEventHandler != null)
                WorkDoneEventHandler.Invoke(sender, args);
        }
        
        public void DoWork() 
        {
            Console.WriteLine("Doing some work...");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Done working!");

            OnWorkDone(this, EventArgs.Empty);
        }
        
        public void GoHome() 
        {
            Console.WriteLine("Going home...");
            System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Worker is home!");
        }

        public delegate void CookFoodHandler(string specialPrep);
        
        public void CookFood(CookFoodHandler cookFoodHandler) 
        {
            cookFoodHandler("..yummy..");
        }
    }
}
