package Week11XML;

/**
 * CreateStudentsXML.java
 * Generates an XML file with 5 student records.
 * Author: Keith Brock
 * Version: 1.0
 */

import org.w3c.dom.*;
import javax.xml.parsers.*;
import javax.xml.transform.*;
import javax.xml.transform.dom.DOMSource;
import javax.xml.transform.stream.StreamResult;
import java.io.File;

public class CreateStudentsXML {
    public static void main(String[] args) {
        try {
            DocumentBuilderFactory factory = DocumentBuilderFactory.newInstance();
            DocumentBuilder builder = factory.newDocumentBuilder();
            Document doc = builder.newDocument();

            Element root = doc.createElement("Students");
            doc.appendChild(root);

            root.appendChild(createStudent(doc, "S001", "Alice", "21", "Computer Science", "Female"));
            root.appendChild(createStudent(doc, "S002", "Bob", "22", "Engineering", "Male"));
            root.appendChild(createStudent(doc, "S003", "Carol", "20", "Biology", "Female"));
            root.appendChild(createStudent(doc, "S004", "David", "23", "Business", "Male"));
            root.appendChild(createStudent(doc, "S005", "Eve", "21", "Math", "Female"));

            TransformerFactory transformerFactory = TransformerFactory.newInstance();
            Transformer transformer = transformerFactory.newTransformer();
            transformer.setOutputProperty(OutputKeys.INDENT, "yes");

            DOMSource source = new DOMSource(doc);
            StreamResult result = new StreamResult(new File("students.xml"));

            transformer.transform(source, result);

            System.out.println("students.xml created successfully.");

        } catch (Exception e) {
            System.out.println("Error: " + e.getMessage());
        }
    }

    /**
     * Creates a student element with ID, name, age, major, and gender.
     */
    private static Node createStudent(Document doc, String id, String name, String age, String major, String gender) {
        Element student = doc.createElement("Student");
        student.setAttribute("id", id);

        student.appendChild(createElement(doc, "name", name));
        student.appendChild(createElement(doc, "age", age));
        student.appendChild(createElement(doc, "major", major));
        student.appendChild(createElement(doc, "gender", gender));

        return student;
    }

    private static Node createElement(Document doc, String tag, String value) {
        Element elem = doc.createElement(tag);
        elem.appendChild(doc.createTextNode(value));
        return elem;
    }
}

