package CHomeworkOOP;

public class CCow extends CAnimal {
    private String m_strColor = ""; // Property for color

    /**
     * Method: SetColor
     * Sets the color of the cow with a maximum length of 10 characters.
     * Defaults to "Brown" if no color is provided.
     */
    public void SetColor(String strNewColor) {
        int intStopIndex;

        if (strNewColor.equals("")) {
            strNewColor = "Brown";
        }

        intStopIndex = strNewColor.length();
        if (intStopIndex > 10) {
            intStopIndex = 10;
        }

        m_strColor = strNewColor.substring(0, intStopIndex);
    }

    /**
     * Method: GetColor
     * @return m_strColor
     */
    public String GetColor() {
        return m_strColor;
    }

    /**
     * Method: Graze
     * Outputs a message specific to the cow's grazing behavior.
     */
    public void Graze() {
        System.out.println("Mmmm, this is some tasty grass.");
    }

    /**
     * Method: MakeNoise
     * Outputs the noise made by a cow.
     */
    @Override
    public void MakeNoise() {
        System.out.println("Mooo");
    }
}
