package CHomeworkOOP;

public class CDog extends CAnimal {
    private float m_sngWeight;
    private int m_intAge;

    public CDog()
    {
    	Initialize ("",0,0);
    }
    
    public CDog( String strName)
    {
    	Initialize(strName, 0, 0 );
    }
    
    public CDog ( String strName, int intAge)
    {
    	Initialize(strName, intAge, 0);
    }
    
    public CDog (String strName, int intAge, float sngWeight)
    {
    	Initialize(strName, intAge, sngWeight);
    }
    public void Initialize(String strName, int intAge, float sngWeight)
    {
    	SetType("Dog");
    	SetName(strName);
    	SetAge(intAge);
    	SetWeight(sngWeight);    	
    }
    
	/**
	 * Method: SetWeight
	 */    
    public void SetWeight(float sngNewWeight) {
        if (sngNewWeight < 0) {
            sngNewWeight = 0.0f;
        }
        m_sngWeight = sngNewWeight;
    }

    
    
	/**
	 * Method: GetWeight
	 * @return m_sngWeight
	 */
    public float GetWeight() {
        return m_sngWeight;
    }

    
    
	/**
	 * Method: SetAge
	 */
    public void SetAge(int intNewAge) {
        if (intNewAge < 0) {
            intNewAge = 0;
        }
        m_intAge = intNewAge;
    }

    
	/**
	 * Method: GetAge
	 */	   
    public int GetAge() {
        return m_intAge;
    }

    
    
	/**
	 * Method: MakeNoise
	 */	   
    @Override    
    public void MakeNoise() {
        Bark();
    }
    
  
    
	/**
	 * Method: Bark
	 */
    public void Bark() {
        if (GetWeight() < 15) {
            System.out.println("Yip, yip, yip");
        } else {
            System.out.println("Woof, woof");
        }
    }

    
    
	/**
	 * Method: Fetch
	 */
    public void Fetch() {
        if (GetAge() > 10) {
            System.out.println("Woah, woah, woah.\nHow about you fetch the stick this time.\nAnd some bacon.\n");
        } else {
            System.out.println("Fetching the tasty stick");
        }
    }
    
    public void Print()
    {
    	System.out.println("Name: " + GetName());
    	System.out.println("Age: " + GetAge());
    	System.out.println("Weight: " + GetWeight());
    	
    	Bark();
    	Fetch();
    	
    	System.out.println("");
    	System.out.println("");    	
    	}
    }
   

