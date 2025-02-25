//Nedarvning
using System.Security.Cryptography.X509Certificates;

public class Animal {
    public void Eat() {
        Console.WriteLine("Dyret spiser");
    }

    public virtual void MakeSound() {
        Console.WriteLine("Generic animal sound");
    }
}

public class Dog : Animal {
    public void Sit() {
        Console.WriteLine("Dyret sætter sig");
    }

    public override void MakeSound() {
        Console.WriteLine("Woof");
    }
}

public class Cat : Animal {
    public void LayDown() {
        Console.WriteLine("Dyret ligger sig");
    }

    public override void MakeSound() {
        Console.WriteLine("Meow");
    }
}


//Interface
public interface IFlyable {
    public void Fly();
}

public class Bird : IFlyable {
    public void Fly() {
        Console.WriteLine("Denne metode er implementeret fra IFlyable");
    }
}

//Polymorfi
public class AnimalSoundMaker {
    public void MakeAnimalSound(Animal animal) {
        animal.MakeSound();
    }
}

//Run
public class Run {
    public static void Main(string[] args) {
        //Polymorfi demo
        AnimalSoundMaker soundMaker = new AnimalSoundMaker();

        Animal myDog = new Dog();
        Animal myCat = new Cat();

        soundMaker.MakeAnimalSound(myDog); // Output: Woof
        soundMaker.MakeAnimalSound(myCat); // Output: Meow

        Console.WriteLine("\n\n");

        //Interface demo
        Bird bird = new Bird();
        bird.Fly();

        Console.WriteLine("\n\n");

        //Arv demo
        Animal animal = new Animal();
        Dog dog = new Dog();
        Cat cat = new Cat();

        Console.WriteLine("Animal");
        animal.Eat();
        //animal.Sit();
        //animal.LayDown();

        Console.WriteLine("Dog");
        dog.Eat();
        dog.Sit();
        //dog.LayDown();

        Console.WriteLine("Cat");
        cat.Eat();
        //cat.Sit();
        cat.LayDown();
    }
}
