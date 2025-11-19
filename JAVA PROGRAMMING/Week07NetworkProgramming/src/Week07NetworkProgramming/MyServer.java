package Week07NetworkProgramming; 

import java.io.*;  
import java.net.*;  

/**
 * Abstract: Implements a server that listens on port 5555 for incoming client connections. 
 * Once a client connects, the server receives a message and prints it before closing the connection.
 * @author Keith Brock
 * @version 1.0
 */
public class MyServer {  

    /**
     * The main method initializes a server socket on port 5555, waits for a client connection, 
     * receives a message from the client, and displays it before closing all resources.
     * 
     * @param args Command-line arguments (not used in this program).
     */
    public static void main(String[] args){  
        try { 
            // Create a server socket bound to port 5555
            ServerSocket ss = new ServerSocket(5555);
            System.out.println("Server is running on port 5555...");

            // Listen for client connection
            Socket s = ss.accept();
            System.out.println("Client connected...");

            // Receive message from client
            DataInputStream dis = new DataInputStream(s.getInputStream());  
            String str = dis.readUTF();  
            System.out.println("Received from client: " + str);  

            // Close resources
            dis.close();
            s.close();
            ss.close();
        } catch (IOException e) {
            System.out.println("Server error: " + e.getMessage());
        }  
    }  
}  
