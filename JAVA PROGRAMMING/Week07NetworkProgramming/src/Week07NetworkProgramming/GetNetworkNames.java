package Week07NetworkProgramming;

import java.net.*;

/**
 * Abstract: Demonstrates how to use InetAddress to retrieve network-related information, 
 * including local host details, domain name resolution, and IP address lookups.
 * @author Keith Brock
 * @version 1.0
 */
public class GetNetworkNames {

    /**
     * The main method retrieves and displays network-related information:
     * - Gets the local machine's hostname and IP address.
     * - Resolves the IP address of www.cincinnatistate.edu.
     * - Lists all IP addresses associated with www.google.com.
     * - Retrieves the IP address of another specified domain.
     * 
     * @param args Command-line arguments (not used in this program).
     */
    public static void main(String[] args) {
        try {
            // Get the local machine's name
            InetAddress localhost = InetAddress.getLocalHost();
            System.out.println("Your system is: " + localhost);

            // Get IP address of Cincinnati State
            InetAddress cState = InetAddress.getByName("www.cincinnatistate.edu");
            System.out.println("Cincinnati State's IP: " + cState.getHostAddress());

            // List all addresses of Google
            InetAddress[] google = InetAddress.getAllByName("www.google.com");
            System.out.println("Google's IP addresses:");
            for (InetAddress addr : google) {
                System.out.println(addr.getHostAddress());
            }

            // Get IP address of another favorite domain
            String favoriteDomain = "www.uo.com";
            InetAddress favorite = InetAddress.getByName(favoriteDomain);
            System.out.println(favoriteDomain + " IP Address: " + favorite.getHostAddress());

        } catch (UnknownHostException e) {
            System.out.println("Host lookup failed: " + e.getMessage());
        }
    }
}
