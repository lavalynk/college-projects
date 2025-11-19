package CHomeworkOOP;

public class HWConstructors {
    public static void main(String[] args) {
        Step1ConstructorsAndOverloading();
        Step2InheritanceAndSuper();
    }

    public static void Step1ConstructorsAndOverloading() {
    	System.out.println("Step 1 - Constructors and Overloading");
    	System.out.println("------------------------------------");
    	
        CDog clsDog1 = new CDog();
        CDog clsDog2 = new CDog("Buddy");
        CDog clsDog3 = new CDog("Charlie", 3);
        CDog clsDog4 = new CDog("Max", 12, 25.0f);

        System.out.println("Dog 1:");
        clsDog1.Print();

        System.out.println("Dog 2:");
        clsDog2.Print();

        System.out.println("Dog 3:");
        clsDog3.Print();

        System.out.println("Dog 4:");
        clsDog4.Print();
    }
    
    public static void Step2InheritanceAndSuper() {
        System.out.println("Step 2 - Inheritance and super");
        System.out.println("------------------------------------");

        CTrainedDog clsSuperBuster1 = new CTrainedDog();
        CTrainedDog clsSuperBuster2 = new CTrainedDog("2SuperBuster");
        CTrainedDog clsSuperBuster3 = new CTrainedDog("3SuperBuster", 11);
        CTrainedDog clsSuperBuster4 = new CTrainedDog("4SuperBuster", 11, 40);
        CTrainedDog clsSuperBuster5 = new CTrainedDog("5SuperBuster", 11, 40, "Basset Hound");

        clsSuperBuster1.Print();
        clsSuperBuster2.Print();
        clsSuperBuster3.Print();
        clsSuperBuster4.Print();
        clsSuperBuster5.Print();
    }
}
