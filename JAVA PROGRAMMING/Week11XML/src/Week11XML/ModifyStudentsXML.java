package Week11XML;

/**
 * ModifyStudentsXML.java
 * Reads students.xml, modifies student data, and saves to students_updated.xml.
 * Modifications:
 * - Uppercases all student names.
 * - Adds a new <gpa> element with a default value of 4.0.
 * 
 * Author: Keith Brock
 * Version: 1.0
 */

import org.w3c.dom.*;
import javax.xml.parsers.*;
import javax.xml.transform.*;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import java.io.File;
/**
 * A utility class that reads an XML file named "students.xml",
 * uppercases all student names, and changes their majors to "Computer Programming".
 * The final output is written to "students_updated.xml".
 *
 * @author  Keith Brock
 * @version 1.1
 */
public class ModifyStudentsXML {
    /**
     * The main entry point of the application.
     */
    public static void main(String[] args) {
        try {
            File inputFile = new File("students.xml");
            DocumentBuilderFactory dbFactory = DocumentBuilderFactory.newInstance();
            DocumentBuilder dBuilder = dbFactory.newDocumentBuilder();
            Document doc = dBuilder.parse(inputFile);

            doc.getDocumentElement().normalize();

            NodeList students = doc.getElementsByTagName("Student");

            for (int i = 0; i < students.getLength(); i++) {
                Node studentNode = students.item(i);

                if (studentNode.getNodeType() == Node.ELEMENT_NODE) {
                    Element studentElement = (Element) studentNode;

                    // Modify <name> to uppercase
                    Element nameElement = (Element) studentElement.getElementsByTagName("name").item(0);
                    String upperName = nameElement.getTextContent().toUpperCase();
                    nameElement.setTextContent(upperName);

                    // Add new <gpa> element with value "4.0"
                    Element majorElement = (Element) studentElement.getElementsByTagName("major").item(0);
                    // Only modify if <major> exists
                    if (majorElement != null) {
                        majorElement.setTextContent("Computer Programming");
                    }
                }
            }

            // Write the updated document to students_updated.xml
            TransformerFactory transformerFactory = TransformerFactory.newInstance();
            Transformer transformer = transformerFactory.newTransformer();
            transformer.setOutputProperty(OutputKeys.INDENT, "yes");

            DOMSource source = new DOMSource(doc);
            StreamResult result = new StreamResult(new File("students_updated.xml"));

            transformer.transform(source, result);

            System.out.println("students_updated.xml created successfully with modifications.");

        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
        }
    }
}
