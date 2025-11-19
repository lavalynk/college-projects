package edu.cincinnatistate.pay;

/**
 * Represents total hours worked and allows adding, subtracting, and retrieving hours.
 * @author Keith Brock
 * @version 1.0
 */
public class HoursWorked {
    private int intTotalHours;

    /**
     * Initializes HoursWorked with non-negative initial hours.
     * @param hours initial hours, must be non-negative.
     */
    public HoursWorked(int intHours) {
        if (intHours < 0)
            throw new IllegalArgumentException("Initial hours cannot be negative");
        this.intTotalHours = intHours;
    }

    /**
     * Adds non-negative hours to total hours.
     * @param hours hours to add.
     */
    public void addHours(int intHours) {
        if (intHours < 0)
            throw new IllegalArgumentException("Cannot add negative hours");
        intTotalHours += intHours;
    }

    /**
     * Subtracts non-negative hours from total hours, ensuring total remains non-negative.
     * @param hours hours to subtract.
     */
    public void subtractHours(int intHours) {
        if (intHours < 0)
            throw new IllegalArgumentException("Cannot subtract negative hours");
        if (intTotalHours - intHours < 0)
            throw new IllegalArgumentException("Total hours cannot be negative");
        intTotalHours -= intHours;
    }

    /**
     * Returns the current total hours.
     * @return total hours.
     */
    public int getTotalHours() {
        return intTotalHours;
    }
}
