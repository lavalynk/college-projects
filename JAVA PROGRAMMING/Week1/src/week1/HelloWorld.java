package week1;


import javax.swing.JOptionPane;

/**
* The Hello program implements an application that
* simply displays constants and variables to the standard output.
*
* @author  TLG
* @version 1.0
* @since   2018-12-17
*/
public class HelloWorld
{
	/** This is Java's main method entry point of this program.
	 *  The program uses constants and variables.
	 */
	public static void main( String astrCommandLine[] ) 	{
		
	JOptionPane.showMessageDialog(null, "Welcome");
	JOptionPane.showMessageDialog(null, "Hello", "My Message",JOptionPane.INFORMATION_MESSAGE );

	}
}