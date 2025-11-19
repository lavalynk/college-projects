package week1;

/**
 * The MyFirstPgm program implements an application that
 * simply displays constants and variables to the standard output.
 * @author Tomie Gartland
 */
public class MyFirstPgm {

	/**
	 * Main method
	 * @param args
	 */
	public static void main(String[] args) {
		// constants/variables
		final int intCONSTANT_EXAMPLE = 1;
		int intHours = 40;
		
		// Display the output
		System.out.println("Java Programming is Fun!");
		//System.out.println( 40 / 0 );
		System.out.println("Constant: " + intCONSTANT_EXAMPLE + "\n");
		System.out.println("Variable: " + intHours);
	}

}