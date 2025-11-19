package Week10Test2;

import java.io.*;

/**
 * CreateNotes - A program that creates, writes to, and displays a text file.
 * 
 * Author: Keith Brock
 * Version: 1.0
 */
public class CreateNotes {

    /**
     * Main: Entry point of the program.
     * 
     * @param args Command-line arguments (not used).
     */
    public static void main(String[] args) {
        File dir = getDirectoryFromUser();
        if (dir == null) return;

        String strFileName = getFileNameFromUser();
        File file = createFile(dir, strFileName);
        if (file == null) return;

        writeToFile(file);
        readFromFile(file);
    }

    /**
     * Prompts the user to enter a directory name and creates it if it doesn't exist.
     * 
     * @return A File object representing the directory, or null if creation fails.
     */
    private static File getDirectoryFromUser() {
        System.out.print("Enter a directory name: ");
        String strDirectoryName = ReadStringFromUser();
        File dir = new File(strDirectoryName);

        if (!dir.exists()) {
            if (dir.mkdirs()) {
                System.out.println("Directory created: " + dir.getAbsolutePath());
            } else {
                System.out.println("Failed to create directory.");
                return null;
            }
        }

        return dir;
    }

    /**
     * Prompts the user for a valid file name ending in .txt.
     * 
     * @return The valid file name entered by the user.
     */
    private static String getFileNameFromUser() {
        String strFileName;
        while (true) {
            System.out.print("Enter a file name with .txt extension: ");
            strFileName = ReadStringFromUser();
            if (strFileName.endsWith(".txt")) return strFileName;
            System.out.println("File name must end with .txt");
        }
    }

    /**
     * Creates a file in the specified directory.
     * 
     * @param dir The directory in which to create the file.
     * @param strFileName The name of the file to create.
     * @return A File object representing the file, or null if creation fails.
     */
    private static File createFile(File dir, String strFileName) {
        File file = new File(dir, strFileName);
        if (file.exists()) {
            System.out.println("File already exists.");
        } else {
            try {
                if (file.createNewFile()) {
                    System.out.println("File created: " + file.getAbsolutePath());
                }
            } catch (IOException e) {
                System.out.println("Error creating file: " + e.getMessage());
                return null;
            }
        }
        return file;
    }

    /**
     * Prompts the user to enter multiple lines of text to write to the file.
     * Ends when the user enters an empty line.
     * 
     * @param file The file to which the user input is written.
     */
    private static void writeToFile(File file) {
        try (FileWriter writer = new FileWriter(file, true)) {
            System.out.println("Type text to add to the file. Press Enter twice to finish.");
            while (true) {
                System.out.print("> ");
                String strLine = ReadStringFromUser();
                if (strLine.isEmpty()) break;
                writer.write(strLine + System.lineSeparator());
            }
        } catch (IOException e) {
            System.out.println("Error writing to file: " + e.getMessage());
        }
    }

    /**
     * Reads and displays the contents of the specified file.
     * 
     * @param file The file to read and display.
     */
    private static void readFromFile(File file) {
        System.out.println("\nContents of the file:");
        try (BufferedReader reader = new BufferedReader(new FileReader(file))) {
            String strFileLine;
            while ((strFileLine = reader.readLine()) != null) {
                System.out.println(strFileLine);
            }
        } catch (IOException e) {
            System.out.println("Error reading file: " + e.getMessage());
        }
    }

    /**
     * Reads a line of text from the user using BufferedReader.
     * 
     * @return The string entered by the user.
     */
    public static String ReadStringFromUser() {
        String strBuffer = "";
        try {
            BufferedReader burInput = new BufferedReader(new InputStreamReader(System.in));
            strBuffer = burInput.readLine();
        } catch (Exception excError) {
            System.out.println(excError.toString());
        }
        return strBuffer;
    }
}
