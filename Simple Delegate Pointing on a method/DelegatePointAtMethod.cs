public delegate void MyDelegate(string message);

public class Example
{
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}
public class DelegatePointAtMethod
{
    private static void Main(string[] args)
    {
        Example example = new Example();
        MyDelegate del = example.ShowMessage;
        del("Hello, World!"); // Output: Hello, World!
    }
}
