package FinalProject;

/**
 * Name: CVehicle
 * Abstract: Abstract parent class for all vehicles. Encapsulates shared attributes and methods.
 * 
 * @author Keith Brock
 * @since 12/07/2024
 */
public abstract class CVehicle {

    protected int intWheels;
    protected int intNumOfMPG;

    /**
     * Default constructor for CVehicle.
     * Initializes the shared attributes of a vehicle.
     */
    public CVehicle() {
        // Default values for vehicle attributes
        intWheels = 0;
        intNumOfMPG = 0;
    }

    /**
     * Name: getHowToDrive
     * Abstract: Abstract method to be overridden by subclasses to define how the vehicle is driven.
     * 
     * @return A string describing how to drive the vehicle.
     */
    public abstract String getHowToDrive();

    /**
     * Name: getMPG
     * Abstract: Returns the miles per gallon (MPG) for the vehicle.
     * 
     * @return The MPG of the vehicle.
     */
    public int getMPG() {
        return intNumOfMPG;
    }

    /**
     * Name: getWheels
     * Abstract: Returns the number of wheels on the vehicle.
     * 
     * @return The number of wheels.
     */
    public int getWheels() {
        return intWheels;
    }
}

