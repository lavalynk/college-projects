package CHomeworkOOP;

public class CAnimal {
    private String m_strName;
    private String m_strType;

    /**
     * Method: SetName
     * Sets the name of the animal with a maximum length of 50 characters.
     */
    public void SetName(String name) {
        if (name.length() > 50) {
            m_strName = name.substring(0, 50);
        } else {
            m_strName = name;
        }
    }

    /**
     * Method: GetName
     * @return m_strName
     */
    public String GetName() {
        return m_strName;
    }

    /**
     * Method: SetType
     * Sets the type of the animal with a maximum length of 50 characters.
     */
    public void SetType(String type) {
        if (type.length() > 50) {
            m_strType = type.substring(0, 50);
        } else {
            m_strType = type;
        }
    }

    /**
     * Method: GetType
     * @return m_strType
     */
    public String GetType() {
        return m_strType;
    }

    /**
     * Method: MakeNoise
     * Outputs a default noise for the animal.
     */
    public void MakeNoise() {
        System.out.println("Undefined");
    }
}
