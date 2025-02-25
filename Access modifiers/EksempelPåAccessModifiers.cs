public class EksempelPåAccessModifiers {
    public int PublicField; // Tilgængelig fra alle steder
    private int PrivateField; // Kun tilgængelig inden for denne klasse
    protected int ProtectedField; // Tilgængelig inden for denne klasse og afledte klasser (Nedarvede klasser)
    internal int InternalField; // Tilgængelig inden for samme assembly (samme program)
    protected internal int ProtectedInternalField; // Tilgængelig inden for samme assembly og afledte klasser (samme program og nedarvede klasser)

    public EksempelPåAccessModifiers() {
        PublicField = 1;
        PrivateField = 2;
        ProtectedField = 3;
        InternalField = 4;
        ProtectedInternalField = 5;
    }
}

public class DerivedExample : EksempelPåAccessModifiers {
    public void DerivedMethod() {
        PublicField = 10; // Tilladt
        // PrivateField = 20; // Ikke tilladt, da det er privat i baseklassen
        ProtectedField = 30; // Tilladt, da det er beskyttet i baseklassen
        InternalField = 40; // Tilladt, da det er internt og inden for samme assembly
        ProtectedInternalField = 50; // Tilladt, da det er beskyttet internt
    }
}
