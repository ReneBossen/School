//SE DELEGATES --> POWERPLANT FOR BEDRE EKSEMPEL!

public class Subject {
    public delegate void Notify(); // Delegate type
    public event Notify OnNotify; // Event using the delegate

    public void DoSomething() {
        // Her skal der være logik
        OnNotify?.Invoke(); // Notify observers
    }
}

public class Observer {
    public void React() {
        Console.WriteLine("Observer notified.");
    }
}

public class Run {
    public static void Main(string[] args) {
        Subject subject = new Subject();
        Observer observer = new Observer();

        subject.OnNotify += observer.React; // Subscribe
        subject.DoSomething(); // Output: Observer notified.
    }
}
