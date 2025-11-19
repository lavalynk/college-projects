package RentCabin;

import java.lang.reflect.Constructor;
import java.lang.reflect.Field;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.lang.reflect.Parameter;

/**
 * RentCabinReflection.java
 *
 * Demonstrates Java reflection to the RentCabin class
 * by following the homework steps exactly:
 *   1) Absolute name
 *   2) Simple name
 *   3) Package name
 *   4) All constructors
 *   5) Constructor with int param
 *   6) Initialize object with 2001 sq ft
 *   7) Declared methods (exclude inherited)
 *   8) Get computeRentalCost(int)
 *   9) Invoke computeRentalCost(2)
 *   10) List parameters
 *   11) Get return type
 *   12) List public fields
 *   13) Return a private field
 *   14) Set private field accessible
 *   15) Print private field value
 * 
 * 
 * @author Keith Brock
 * @version 1.0
 */

public class RentCabinReflection {
    /**
     * Main method to demonstrate reflection on RentCabin.
     * @param args not used
     */
    public static void main(String[] args) {
        try {
            // Use the fully qualified class name: "RentCabin.RentCabin"
            Class<?> cabinClass = Class.forName("RentCabin.RentCabin");

            // 1) Absolute name of the class
            System.out.println("1) Absolute class name: " + cabinClass.getName());

            // 2) Simple name of the class (without package info)
            System.out.println("2) Simple class name: " + cabinClass.getSimpleName());

            // 3) Package name of the class
            Package pkg = cabinClass.getPackage();
            System.out.println("3) Package name: " + (pkg != null ? pkg.getName() : "Default Package"));

            // 4) Get all the constructors
            Constructor<?>[] constructors = cabinClass.getDeclaredConstructors();
            System.out.println("4) All Constructors:\n");
            for (Constructor<?> constructor : constructors) {
                System.out.println("   " + constructor);
            }
            System.out.println();

            // 5) Get a constructor with a specific argument (int)
            Constructor<?> intConstructor = cabinClass.getDeclaredConstructor(int.class);
            System.out.println("5) Constructor with int parameter: " + intConstructor + "\n");

            // 6) Initialize an object of RentCabin with 2001 sq ft (large cabin)
            Object rentCabinInstance = intConstructor.newInstance(2001);
            System.out.println("6) Created RentCabin instance with 2001 square feet.\n");

            // 7) Get all the declared methods (excluding inherited)
            Method[] declaredMethods = cabinClass.getDeclaredMethods();
            System.out.println("7) Declared methods:\n");
            for (Method method : declaredMethods) {
                System.out.println("   " + method.getName());
            }
            System.out.println();

            // 8) Get the specific method: computeRentalCost(int)
            Method computeRentalCostMethod = cabinClass.getDeclaredMethod("computeRentalCost", int.class);
            System.out.println("8) Method computeRentalCost(int): " + computeRentalCostMethod + "\n");

            // 9) Call computeRentalCost method with 2 days
            Object rentalCost = computeRentalCostMethod.invoke(rentCabinInstance, 2);
            System.out.println("9) Rental cost for 2 days: $" + rentalCost + "\n");

            // 10) List all the parameters of computeRentalCost
            System.out.println("10) Parameters of computeRentalCost:\n");
            Parameter[] parameters = computeRentalCostMethod.getParameters();
            for (Parameter parameter : parameters) {
                System.out.println("    " + parameter.getName() + " : " + parameter.getType().getName());
            }
            System.out.println();

            // 11) Get the return type of computeRentalCost
            System.out.println("11) Return type of computeRentalCost: " 
                               + computeRentalCostMethod.getReturnType().getName() + "\n");

            // 12) List all public member fields of RentCabin
            Field[] publicFields = cabinClass.getFields();
            System.out.println("12) Public fields of RentCabin:\n");
            if (publicFields.length == 0) {
                System.out.println("    No public fields found.");
            } else {
                for (Field field : publicFields) {
                    System.out.println("    " + field.getName());
                }
            }
            System.out.println();

            // 13) Access a private field (here, "intSquareFeet")
            Field squareFeetField = cabinClass.getDeclaredField("intSquareFeet");
            System.out.println("13) Private field found: " + squareFeetField.getName() + "\n");

            // 14) Set the private field accessible
            squareFeetField.setAccessible(true);
            System.out.println("14) Set private field to accessible.\n");

            // 15) Print the field value of intSquareFeet from the rentCabinInstance
            Object squareFeetValue = squareFeetField.get(rentCabinInstance);
            System.out.println("15) Private field 'intSquareFeet' value: " + squareFeetValue);

        } catch (ClassNotFoundException e) {
            System.err.println("RentCabin class not found: " + e.getMessage());
        } catch (NoSuchMethodException e) {
            System.err.println("Specified method not found: " + e.getMessage());
        } catch (NoSuchFieldException e) {
            System.err.println("Specified field not found: " + e.getMessage());
        } catch (InstantiationException e) {
            System.err.println("Error instantiating RentCabin: " + e.getMessage());
        } catch (IllegalAccessException e) {
            System.err.println("Illegal access: " + e.getMessage());
        } catch (InvocationTargetException e) {
            System.err.println("Invocation target exception: " + e.getMessage());
        } catch (IllegalArgumentException e) {
            System.err.println("Illegal argument provided: " + e.getMessage());
        }
    }
}
