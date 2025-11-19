package RentCabin;

/**
 * This class calculates the rental cost for a cabin based on its square footage.
 * Depending on the size, it sets the cabin type and the rate per night.
 * 
 * Updated to mimic the teacher's RentAuto example.
 * 
 * @author Keith Brock
 * @version 1.1
 */
public class RentCabin {
    // Private member variables using Hungarian notation
    private int intSquareFeet;
    private double dblRate;
    private String type;
    
    // Public field to store rental cost (like teacher's price field)
    public int price;
    
    /**
     * Determines cabin type and rate based on size.
     *
     * @param intSquareFeet the square footage of the cabin
     */
    public RentCabin(int intSquareFeet) {
        this.intSquareFeet = intSquareFeet;
        if (intSquareFeet < 1000) {
            this.type = "small";
            this.dblRate = 100.0;
        } else if (intSquareFeet <= 2000) {
            this.type = "mid-sized";
            this.dblRate = 200.0;
        } else {
            this.type = "large";
            this.dblRate = 300.0;
        }
    }
    
    /**
     * Gets the nightly rate.
     *
     * @return the rate per night
     */
    public double getRate() {
        return dblRate;
    }
    
    /**
     * Sets the nightly rate.
     *
     * @param dblRate the new rate per night
     */
    public void setRate(double dblRate) {
        this.dblRate = dblRate;
    }
    
    /**
     * Gets the cabin type.
     *
     * @return the type of the cabin
     */
    public String getType() {
        return type;
    }
    
    /**
     * Sets the cabin type.
     *
     * @param type the new cabin type
     */
    public void setType(String type) {
        this.type = type;
    }
    
    /**
     * Computes the rental cost by multiplying the nightly rate by the number of days.
     * This method sets the public field 'price' and prints the result.
     *
     * @param days the number of days the cabin is rented
     */
    public void computeRentalCost(int days) {
        price = (int)(dblRate * days);
        System.out.println("The cost of your cabin rental is " + price + " USD");
    }
}
