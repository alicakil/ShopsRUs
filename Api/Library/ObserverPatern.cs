namespace Api.Library
{

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Notify();
    }

    public interface IObserver
    {
        void Update(ISubject subject);
    }


    class WheatherStation : ISubject
    {
        private List<IObserver> _observers;
        public float temparature
        {
            get { return temparature; }
            set { temparature = value; Notify(); } 
        }   

        public WheatherStation()
        {
            _observers = new List<IObserver>();
        }

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }
         
        public void Notify()
        {
            _observers.ForEach(o => o.Update(this)); 
        }
    }

    public class Observer : IObserver
    {
        public Observer(string ObserverName)
        {
            _ObserverName = ObserverName;
        }
        private string _ObserverName { get; set; }

        public void Update(ISubject subject)
        {
            if (subject is WheatherStation ws)
            {                
                Console.WriteLine("Observer:" + _ObserverName + "says, temp:" + ws.temparature);
            }
        }
    }


    class MainCode
    {
        void execution()
        {
            WheatherStation ws = new WheatherStation();
            Observer observer1 = new Observer("Whether Agency-1");
            Observer observer2 = new Observer("Whether Agency-2");

            ws.Attach(observer1);
            ws.Attach(observer2);

            ws.temparature = 20.2f;
            ws.temparature = 24.1f;
        }
    }
}
