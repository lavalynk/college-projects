package Week13XMLLogger;

import java.io.IOException;
import java.util.logging.*;

/**
 * Week13XMLLogger demonstrates how to use Java's built-in logging system
 * to log messages of various severity levels (SEVERE, WARNING, INFO) to an XML file.
 * It generates multiple exception scenarios and logs them to mylog.xml.
 * 
 * Author: Keith Brock
 * Version: 1.0
 */
public class Week13XMLLogger {
	/**
	 * Default constructor for Week13XMLLogger.
	 * Required for JavaDoc compliance.
	 */
	public Week13XMLLogger() {
	    // No initialization needed
	}
	
    private static final Logger logger = Logger.getLogger(Week13XMLLogger.class.getName());

    /**
     * The main method to run the logger demonstration.
     * It configures the logger, triggers various exception cases,
     * and logs the messages in both console and mylog.xml.
     *
     * @param args command-line arguments (not used)
     */
    public static void main(String[] args) {
        configureLogger();

        logger.info("Starting exception simulation...");
        System.out.println("Starting exception simulation...");

        // Simulate 4 different exceptions for logging demonstration
        simulateArithmeticException();
        simulateNullPointerException();
        simulateArrayIndexException();
        simulateNumberFormatException();

        logger.info("Finished logging all exceptions.");
        System.out.println("Finished logging all exceptions.");
    }

    /**
     * Configures the logger to use a FileHandler with XML formatting.
     */
    private static void configureLogger() {
        try {
            LogManager.getLogManager().reset();
            FileHandler fileHandler = new FileHandler("mylog.xml");
            fileHandler.setFormatter(new XMLFormatter());
            logger.addHandler(fileHandler);
            logger.setLevel(Level.ALL);
        } catch (IOException exIO) {
            System.err.println("Logger configuration failed: " + exIO.getMessage());
        }
    }

    /**
     * Simulates a division by zero to trigger ArithmeticException.
     */
    private static void simulateArithmeticException() {
        try {
            // This triggers ArithmeticException without assigning to a variable
            System.out.println(42 / 0);
        } catch (ArithmeticException exArithmetic) {
            String strMessage = "SEVERE: Division by zero error - " + exArithmetic.toString();
            logger.severe(strMessage);
            System.out.println(strMessage);
        }
    }


    /**
     * Simulates a null pointer access to trigger NullPointerException.
     */
    private static void simulateNullPointerException() {
        try {
            // Directly trigger the NullPointerException
            System.out.println(((String) null).length());
        } catch (NullPointerException exNull) {
            String strMessage = "WARNING: Attempted to access a null object - " + exNull.toString();
            logger.warning(strMessage);
            System.out.println(strMessage);
        }
    }


    /**
     * Simulates accessing an out-of-bounds index in an array.
     */
    private static void simulateArrayIndexException() {
        try {
            int[] intArray = new int[3];
            System.out.println(intArray[10]); // Triggers ArrayIndexOutOfBoundsException
        } catch (ArrayIndexOutOfBoundsException exBounds) {
            String strMessage = "WARNING: Array index out of range - " + exBounds.toString();
            logger.warning(strMessage);
            System.out.println(strMessage);
        }
    }


    /**
     * Simulates improper string-to-number conversion.
     */
    private static void simulateNumberFormatException() {
        try {
            Integer.parseInt("abc123"); // Triggers NumberFormatException
        } catch (NumberFormatException exFormat) {
            String strMessage = "INFO: Number format issue occurred - " + exFormat.toString();
            logger.info(strMessage);
            System.out.println(strMessage);
        }
    }

}
