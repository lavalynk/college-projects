package Week07NetworkProgramming;

import java.io.*;  
import java.net.*;  

/**
 * Abstract: Implements a client that connects to a server via a socket on port 5555.
 * The client sends a predefined message to the server and then closes the connection.
 * @author Keith Brock
 * @version 1.0
 */
public class MyClient {  

    /**
     * The main method establishes a connection to the server on localhost at port 5555, 
     * sends a text message, and then closes the connection.
     * 
     * @param args Command-line arguments (not used in this program).
     */
    public static void main(String[] args) {  
        try {    
            // Connect to server on port 5555
            Socket s = new Socket("localhost", 5555);
            System.out.println("Connected to server...");

            // Send message
            DataOutputStream dout = new DataOutputStream(s.getOutputStream());
            dout.writeUTF("Hello from MyClient!");  
            dout.flush();
            dout.close();
            s.close();

        } catch (IOException e) {
            System.out.println("Client error: " + e.getMessage());
        }  
    }  
}  
