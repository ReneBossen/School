public delegate void Warning(int temperature);
internal class KørPowerPlant {
    static void Main(string[] args) {
        PowerPlant plant = new PowerPlant();
        plant.SetWarning(warningToConsole);

        for (int i = 0; i < 10; i++) {
            plant.HeatUp();
        }

        Console.ReadLine();
    }
    private static void warningToConsole(int t) {
        Console.WriteLine("Advarsel, temperaturen er " + t);
    }
}
public class PowerPlant {
    private Warning _Warning;
    Random random = new Random();

    public void addWarning(Warning wa) {
        _Warning += wa;
    }
    public void SetWarning(Warning wa) {
        _Warning = wa;
    }
    public void HeatUp() {

        int t = random.Next(100);
        if (t > 50) {
            _Warning.Invoke(t);
        }
    }
}