/**
 * Abstract: Demonstrates handling various types of multithreading.
 * @author Keith Brock
 * @version 1.0
 */

/**
 * Demonstrates bank account transactions using multithreading.
 */
public class ProcessBankAccount implements Runnable {
    private static Account objAccount = new Account();
    private static boolean boolStartBalancePrinted = false;

    public static void main(String[] args) {
        ProcessBankAccount objUser1 = new ProcessBankAccount();
        ProcessBankAccount objUser2 = new ProcessBankAccount();

        Thread objThread1 = new Thread(objUser1);
        Thread objThread2 = new Thread(objUser2);

        objThread1.setName("Keith");
        objThread2.setName("Bob");

        objThread1.start();
        objThread2.start();
    }

    /**
     * Runs the ATM transaction simulation.
     */
    @Override
    public void run() {
        synchronized (objAccount) {
            if (!boolStartBalancePrinted) {
                System.out.println("Starting balance: " + objAccount.getBalance());
                boolStartBalancePrinted = true;
            }
        }

        for (int intIndex = 0; intIndex < 5; intIndex++) {
            makeWithdrawal(100);
        }

        makeDeposit(200);
    }

    /**
     * Attempts to withdraw money from the account.
     * @param intAmount the amount to withdraw
     */
    private synchronized void makeWithdrawal(int intAmount) {
        if (objAccount.withdraw(intAmount)) {
            System.out.println(Thread.currentThread().getName() + " successfully withdrew $" + intAmount);
            System.out.println("Remaining balance: " + objAccount.getBalance());
        } else {
            System.out.println("Not enough in account for " + Thread.currentThread().getName() + " to withdraw, account balance is " + objAccount.getBalance());
        }
    }

    /**
     * Deposits money into the account.
     * @param intAmount the amount to deposit
     */
    private synchronized void makeDeposit(int intAmount) {
        System.out.println(Thread.currentThread().getName() + " is going to add $" + intAmount);
        objAccount.deposit(intAmount);
        System.out.println(Thread.currentThread().getName() + " completes the deposit. New balance: " + objAccount.getBalance());
    }
}
