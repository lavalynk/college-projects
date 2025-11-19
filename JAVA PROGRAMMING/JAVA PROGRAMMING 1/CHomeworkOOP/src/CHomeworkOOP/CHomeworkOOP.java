package CHomeworkOOP;
/**
* Name: CHomeworkOOP
* Abstract: Working on OOP
* @author Keith Brock
*/
public class CHomeworkOOP {
    public static void main(String[] args) {
        ShowPolymorphism();
    }
    
    public static void ShowPolymorphism() {
        CAnimal[] acsZoo = new CAnimal[7];

        // Populate zoo with various animal instances
        CDog clsBuster = new CDog();
        clsBuster.SetName("Buster");
        clsBuster.SetType("Dog");
        clsBuster.SetAge(11);
        clsBuster.SetWeight(40);

        CCat clsSunny = new CCat();
        clsSunny.SetName("Sunny");
        clsSunny.SetType("Cat");

        CDuck clsDaffy = new CDuck();
        clsDaffy.SetName("Daffy");
        clsDaffy.SetType("Duck");

        CCow clsBessie = new CCow();
        clsBessie.SetName("Bessie");
        clsBessie.SetType("Cow");
        clsBessie.SetColor("Brown");

        CDragon clsSmaug = new CDragon();
        clsSmaug.SetName("Smaug");
        clsSmaug.SetType("Dragon");
        clsSmaug.SetHeadCount(3);

        CTrainedDog clsFifi = new CTrainedDog();
        clsFifi.SetName("Fifi");
        clsFifi.SetType("Trained Dog");
        clsFifi.SetAge(2);
        clsFifi.SetWeight(10);
        clsFifi.SetBreed("Poodle");

        // Assign animals to the array
        acsZoo[0] = clsBuster;
        acsZoo[1] = clsSunny;
        acsZoo[2] = clsDaffy;
        acsZoo[4] = clsBessie;
        acsZoo[5] = clsSmaug;
        acsZoo[6] = clsFifi;

        // Loop through the array and invoke methods based on the animal type
        for (int i = 0; i < acsZoo.length; i++) {
            if (acsZoo[i] != null) {
                System.out.println("Animal in cage #" + (i + 1) + ":");
                System.out.println("Name: " + acsZoo[i].GetName());
                System.out.println("Type: " + acsZoo[i].GetType());
                acsZoo[i].MakeNoise();

                String strAnimalType = acsZoo[i].GetType();

                if (strAnimalType.equals("Dog")) {
                    ((CDog) acsZoo[i]).Fetch();
                } else if (strAnimalType.equals("Trained Dog")) {
                    CTrainedDog trainedDog = (CTrainedDog) acsZoo[i];
                    trainedDog.PlayDead();
                    trainedDog.FetchNewspaper();
                    trainedDog.Print();
                } else if (strAnimalType.equals("Cow")) {
                    CCow cow = (CCow) acsZoo[i];
                    cow.Graze();
                    System.out.println("Color is " + cow.GetColor());
                } else if (strAnimalType.equals("Dragon")) {
                    CDragon dragon = (CDragon) acsZoo[i];
                    dragon.BreatheFire();
                }

                System.out.println();
            }
        }
    }
}
   