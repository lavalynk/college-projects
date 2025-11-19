package TestExceptions;
public class MultiThreadEx extends Thread{   

  public void run() {   

   System.out.println("The thread is in running state.");     }    

   public static void main(String args[]) {        

    MultiThreadEx obj=new MultiThreadEx();         

    obj.start();   

  } 

 }