package edu.cincinnatistate.pay;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import java.util.logging.Logger;
import static org.junit.jupiter.api.Assertions.assertTrue;

/**
 * JUnit tests for HoursWorked verifying add, subtract, and error conditions.
 */
public class HoursWorkedTest {
    private HoursWorked hw;
    private static final Logger logger = Logger.getLogger(HoursWorkedTest.class.getName());

    /**
     * Initializes HoursWorked with 8 hours before each test.
     */
    @BeforeEach
    public void setUp() {
        hw = new HoursWorked(8);
        logger.info("Setup: HoursWorked initialized with " + hw.getTotalHours() + " hours.");
    }

    /**
     * Tests addition of hours: 8 + 3 = 11, then 11 + 2 = 13.
     */
    @Test
    public void testAddHours() {
        hw.addHours(3);
        assertTrue(hw.getTotalHours() == 11, "After adding 3 hours, expected 11 but got " + hw.getTotalHours());
        logger.info("testAddHours: After adding 3 hours, total is " + hw.getTotalHours());

        hw.addHours(2);
        assertTrue(hw.getTotalHours() == 13, "After adding 2 more hours, expected 13 but got " + hw.getTotalHours());
        logger.info("testAddHours: After adding 2 more hours, total is " + hw.getTotalHours());
    }

    /**
     * Tests subtraction of hours: 8 - 3 = 5, then 5 - 2 = 3.
     */
    @Test
    public void testSubtractHours() {
        hw.subtractHours(3);
        assertTrue(hw.getTotalHours() == 5, "After subtracting 3 hours, expected 5 but got " + hw.getTotalHours());
        logger.info("testSubtractHours: After subtracting 3 hours, total is " + hw.getTotalHours());

        hw.subtractHours(2);
        assertTrue(hw.getTotalHours() == 3, "After subtracting 2 more hours, expected 3 but got " + hw.getTotalHours());
        logger.info("testSubtractHours: After subtracting 2 more hours, total is " + hw.getTotalHours());
    }

    /**
     * Tests that subtracting more hours than available throws an exception.
     */
    @Test
    public void testSubtractHoursFailure() {
        try {
            hw.subtractHours(10); 
            // If we get here, no exception was thrown:
            System.out.println("No exception was thrown. This test will fail now.");
            logger.severe("No exception thrown. Failing test...");
            assertTrue(false, "Expected exception when subtracting 10 hours from 8, but none was thrown.");
        } catch (IllegalArgumentException e) {
            // If we reach here, an exception was thrown as expected:
            System.out.println("Caught IllegalArgumentException: " + e.getMessage());
            logger.info("testSubtractHoursFailure: Caught expected exception - " + e.getMessage());
            assertTrue(e.getMessage().contains("Total hours cannot be negative"),
                "Expected error message to contain 'Total hours cannot be negative', but got: " + e.getMessage());
        }
    }
}


