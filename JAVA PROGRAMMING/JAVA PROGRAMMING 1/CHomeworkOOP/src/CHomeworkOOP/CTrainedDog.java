package CHomeworkOOP;

public class CTrainedDog extends CDog {
    private String m_strBreed;

    // SetBreed method with boundary checking
    public void SetBreed(String strNewBreed) {
        int intStopIndex = strNewBreed.length();
        if (intStopIndex > 10) {
            intStopIndex = 10; // Clip to 10 characters
        }
        m_strBreed = strNewBreed.substring(0, intStopIndex);
    }

    // GetBreed method
    public String GetBreed() {
        return m_strBreed;
    }

    // PlayDead method with logic based on age
    public void PlayDead() {
        if (GetAge() < 5) {
            System.out.println("Bang! Oh, you got me.");
        } else {
            System.out.println("Treat first. Trick second.");
        }
    }

    // FetchDrink method for additional behavior
    public void FetchDrink() {
        System.out.println("Fetching water... and maybe some treats!");
    }

    // Initialize method for CTrainedDog
    public void Initialize(String strBreed) {
        SetType("Trained Dog");
        SetBreed(strBreed);
    }

    // Constructors
    public CTrainedDog() {
        super();
        Initialize("");
    }

    public CTrainedDog(String strName) {
        super(strName);
        Initialize("");
    }

    public CTrainedDog(String strName, int intAge) {
        super(strName, intAge);
        Initialize("");
    }

    public CTrainedDog(String strName, int intAge, float sngWeight) {
        super(strName, intAge, sngWeight);
        Initialize("");
    }

    public CTrainedDog(String strName, int intAge, float sngWeight, String strBreed) {
        super(strName, intAge, sngWeight);
        Initialize(strBreed);
    }
    
    public void FetchNewspaper() {
        if (GetAge() < 2) {
            System.out.println("I'm too young.");
        } else {
            System.out.println("Here's your paper.");
        }
    }
    // Print method to display all properties
    @Override
    public void Print() {
        System.out.println("Name: " + GetName());
        System.out.println("Age: " + GetAge());
        System.out.println("Weight: " + GetWeight());
        System.out.println("Breed: " + GetBreed());
        Bark();
        Fetch();
        PlayDead();
        FetchDrink();
        System.out.println("");
    }
}
