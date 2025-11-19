package FinalProject;

/**
 * Name: Trailer
 * Abstract: Subclass of Vehicle. Overrides methods to define trailer-specific attributes and behaviors.
 * 
 * @author Keith Brock
 * @since 12/07/2024
 */
public class CTrailer extends CVehicle {
    /**
     * Default constructor for Trailer. Initializes wheels and sets MPG to 0.
     */
    public CTrailer() {
        intWheels = 4;
        intNumOfMPG = 0; // No fuel on trailers.
    }

    @Override
    public String getHowToDrive() {
        return "Use another vehicle to pull.";
    }

    @Override
    public int getMPG() {
        return 0; // Override to explicitly return 0 for trailers.
    }
}

