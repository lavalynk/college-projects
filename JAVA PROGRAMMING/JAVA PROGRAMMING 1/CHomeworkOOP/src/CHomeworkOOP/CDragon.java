package CHomeworkOOP;

public class CDragon extends CAnimal {
    private int m_intHeadCount = 1; // Default head count to 1

    /**
     * Method: SetHeadCount
     * Sets the head count of the dragon, with a minimum of 1 head.
     */
    public void SetHeadCount(int headCount) {
        if (headCount < 1) {
            m_intHeadCount = 1; 
        } else {
            m_intHeadCount = headCount;
        }
    }

    /**
     * Method: GetHeadCount
     * @return m_intHeadCount
     */
    public int GetHeadCount() {
        return m_intHeadCount;
    }

    /**
     * Method: BreatheFire
     * Outputs fire-breathing action based on the number of dragon heads.
     */
    public void BreatheFire() {
        if (GetHeadCount() == 1) {
            System.out.println("The dragon breathes fire.");
        } else {
            System.out.println("The dragon breathes from each of its " + GetHeadCount() + " heads:");
            for (int i = 0; i < GetHeadCount(); i++) {
                System.out.println("*** Breathe Fire ***");
            }
        }
    }

    /**
     * Method: MakeNoise
     * Outputs the noise made by a dragon.
     */
    @Override
    public void MakeNoise() {
        System.out.println("Rawr!");
    }
}
