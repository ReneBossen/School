public class BaseClass {
    public void BaseMethod() {
    }
}

public class DerivedClass : BaseClass {
    public void DerivedMethod() {
    }
}

public class Program {
    public static void Main() {
        DerivedClass derived = new DerivedClass();
        derived.BaseMethod();    // Tilladt, da DerivedClass arver fra BaseClass
        derived.DerivedMethod(); // Tilladt
    }
}
