/**
 * Abstract: Demonstrates handling various types of multithreading
 * @author Keith Brock
 * @version 1.0
 */

/**
 * Represents a bank account shared by multiple users.
 */
public class Account {
    private int intBalance = 500;

    /**
     * Gets the current balance.
     * @return the current balance
     */
    public int getBalance() {
        return intBalance;
    }

    /**
     * Withdraws an amount from the account if sufficient balance is available.
     * @param intAmount the amount to withdraw
     * @return true if withdrawal is successful, false otherwise
     */
    public synchronized boolean withdraw(int intAmount) {
        if (intBalance >= intAmount) {
            try {
                Thread.sleep(100); // Simulate ATM processing time
            } catch (InterruptedException e) {
                System.out.println("Transaction interrupted");
            }
            intBalance -= intAmount;
            return true;
        }
        return false;
    }

    /**
     * Deposits an amount into the account.
     * @param intAmount the amount to deposit
     */
    public void deposit(int intAmount) {
        try {
            Thread.sleep(100); // Simulate ATM processing time
        } catch (InterruptedException e) {
            System.out.println("Transaction interrupted");
        }
        intBalance += intAmount;
    }
}
