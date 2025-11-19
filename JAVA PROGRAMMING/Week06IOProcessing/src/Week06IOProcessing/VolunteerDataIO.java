package Week06IOProcessing;

import java.io.*;

/**
 * Manages volunteer records by creating, writing, and retrieving data from Volunteers.txt in a structured format.
 * 
 * @author Keith Brock
 * @version 1.0
 */

/**
 * Handles file operations for Volunteers.txt, including creation,
 * writing, and reading of volunteer records in a formatted manner.
 */
public class VolunteerDataIO {
    /**
     * File object representing the Volunteers.txt file.
     */	
    public static final File fileVolunteers = new File("Volunteers.txt");

    /**
     * The main method executes the program by creating the volunteer file,
     * writing volunteer records, and displaying the stored data.
     * 
     * @param args Command-line arguments (not used in this program).
     */
    public static void main(String[] args) {
        createFile();
        writeVolunteers();
        printData();
    }

    /**
     * Makes sure Volunteers.txt exists and shows a message about it.
     */
    public static void createFile() {
        try {
            if (fileVolunteers.createNewFile()) {
                System.out.println("Volunteers.txt has been created successfully.");
            } else {
                System.out.println("Volunteers.txt already exists.");
            }
        } catch (IOException ioException) {
            System.out.println("Error initializing stream.");
            ioException.printStackTrace();
        }
    }

    /**
     * Writes a list of volunteers to Volunteers.txt so they can be used later.
     */
    public static void writeVolunteers() {
        Volunteer[] arrVolunteers = {
            new Volunteer("Jack", "Black", "Cincinnati", "OH", "Flying Pig"),
            new Volunteer("Jason", "Gather", "Newport", "KY", "Jazz Festival"),
            new Volunteer("Christy", "Jackson", "Hamilton", "OH", "5K"),
            new Volunteer("Keith", "Brock", "Cleves", "OH", "10K")
        };

        try (ObjectOutputStream objOutStream = new ObjectOutputStream(new FileOutputStream(fileVolunteers))) {
            for (Volunteer objVolunteer : arrVolunteers) {
                objOutStream.writeObject(objVolunteer);
            }
            System.out.println("Volunteer records successfully written to Volunteers.txt.\n");
        } catch (FileNotFoundException fileNotFoundException) {
            System.out.println("File Volunteers.txt not found. Contact the support desk and reference this error in class VolunteerDataIO.");
        } catch (IOException ioException) {
            System.out.println("Error initializing stream.");
            ioException.printStackTrace();
        }
    }

    /**
     * Reads and prints the volunteer records from Volunteers.txt in a formatted table.
     */
    public static void printData() {
        System.out.printf("%-20s %-20s %-20s %-20s %-20s%n", "First Name", "Last Name", "City", "State", "Event");
        System.out.println("-------------------------------------------------------------------------------------------------------------");

        try (ObjectInputStream objInStream = new ObjectInputStream(new FileInputStream(fileVolunteers))) {
            while (true) {
                try {
                    Volunteer objVolunteer = (Volunteer) objInStream.readObject();
                    System.out.printf("%-20s %-20s %-20s %-20s %-20s%n", objVolunteer.getFirstName(), objVolunteer.getLastName(), objVolunteer.getCity(), objVolunteer.getState(), objVolunteer.getEvent());
                } catch (EOFException eofException) {
                    break;
                }
            }
        } catch (FileNotFoundException fileNotFoundException) {
            System.out.println("File Volunteers.txt not found. Contact the support desk and reference this error in class VolunteerDataIO.");
        } catch (IOException | ClassNotFoundException exception) {
            System.out.println("Error reading from file.");
            exception.printStackTrace();
        }
    }

    static class Volunteer implements Serializable {
        public static final long serialVersionUID = 1L;
        public String strFirstName, strLastName, strCity, strState, strEvent;

        /**
         * Constructor for the Volunteer class, initializes volunteer details.
         * @param strFirstName The first name of the volunteer.
         * @param strLastName The last name of the volunteer.
         * @param strCity The city where the volunteer is from.
         * @param strState The state where the volunteer is from.
         * @param strEvent The event the volunteer is participating in.
         */
        public Volunteer(String strFirstName, String strLastName, String strCity, String strState, String strEvent) {
            this.strFirstName = strFirstName;
            this.strLastName = strLastName;
            this.strCity = strCity;
            this.strState = strState;
            this.strEvent = strEvent;
        }

        /**
         * Retrieves the first name of the volunteer.
         * @return First name as a string.
         */
        public String getFirstName() {
            return strFirstName;
        }
        /**
         * Retrieves the last name of the volunteer.
         * @return Last name as a string.
         */
        public String getLastName() {
            return strLastName;
        }
        /**
         * Retrieves the city of the volunteer.
         * @return City as a string.
         */
        public String getCity() {
            return strCity;
        }
        /**
         * Retrieves the state of the volunteer.
         * @return State as a string.
         */
        public String getState() {
            return strState;
        }
        /**
         * Retrieves the event associated with the volunteer.
         * @return Event name as a string.
         */
        public String getEvent() {
            return strEvent;
        }
    }
}
