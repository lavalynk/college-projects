package FinalProject;

/**
 * Name: Motorbike
 * Abstract: Subclass of Vehicle. Overrides methods to define motorbike-specific attributes and behaviors.
 * 
 * @author Keith Brock
 * @since 12/07/2024
 */
public class CMotorbike extends CVehicle {
    /**
     * Default constructor for Motorbike. Initializes wheels and MPG.
     */
    public CMotorbike() {
        intWheels = 2;
        intNumOfMPG = 50;
    }

    @Override
    public String getHowToDrive() {
        return "Use handlebars.";
    }
}

