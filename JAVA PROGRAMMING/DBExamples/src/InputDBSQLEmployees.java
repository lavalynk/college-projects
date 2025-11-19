//import for sql Connection, DriverManager, ResultSet, Statement
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.Statement;
import com.microsoft.sqlserver.jdbc.*;	// For connecting to SQL Server

/**
* Abstract: 
* @author TLG
* @since  11/12/2019
* @version 1.0
*/
public class InputDBSQLEmployees
{
		//define the Connection
		private static Connection m_conAdministrator;
		//define the table, primary key, and column
		/**
		 * main method: intro to database processing
		 * This will connect to the db and display records on the console
		 */
		public static void main(String[] args) {
	 
	        try {
	        	
	    			// Can we connect to the database?
	    			if ( OpenDatabaseConnectionSQLServer( ) == true )
	    			{	
						// Yes, load the teams list
	    				LoadListFromDatabase( "TEmployees", "intEmployeeID" , "strFirstName" );
	    			}
	    			else
	    			{
	    				// No, warn the user ...
	    				System.out.println("Error loading the table");
	    			}
	    			
	    		System.out.println("Process Complete");
	        }
	            catch 	(Exception e) {
	            	System.out.println("An I/O error occurred: " + e.getMessage());
	        	}
		}
		

		/** 
		 * 	method name: This will load the list from the table.	
		 */
		public static boolean LoadListFromDatabase( String strTable, String strPrimaryKeyColumn, String strNameColumn ) {
			
			//set flag to false
			boolean blnResult = false;
			
			try
			{
				String strSelect = "";
				Statement sqlCommand = null;
				ResultSet rstTSource = null;
				int intID = 0;
				String strName = "";
			
				// Build the SQL string
				strSelect = "SELECT " + strPrimaryKeyColumn + ", " + strNameColumn
							+ " FROM " + strTable
							+ " ORDER BY " + strNameColumn; 
						
				// Retrieve the all the records	
				sqlCommand = m_conAdministrator.createStatement( );
				rstTSource = sqlCommand.executeQuery( strSelect );
				// Loop through all the records
				while( rstTSource.next( ) == true )
				{
					// Get ID and Name from current row
					intID = rstTSource.getInt( 1 );
					strName = rstTSource.getString( 2 );
					// Print the list
					System.out.println("Table is: " + strTable + " Primary key: " + intID + " strName: " + strName);
				}
				// Clean up
				rstTSource.close( );
				sqlCommand.close( );
				// Success
				blnResult = true;
			}
			catch 	(Exception e) {
				System.out.println( "Error loading table" );
				System.out.println( "Error is " + e );
			}
			
			return blnResult;
			}

		/** 
		 * method name: OpenDatabaseConnectionMSAccess
		 * The opens the database connection	
		 * This requires the following drivers: Use UCanAccess, an open-source JDBC driver.
		 * Include the following jar files in your code:
		 *		ucanaccess-2.0.7.jar
		 *		jackcess-2.0.4.jar
		 *		commons-lang-2.6.jar
		 *		commons-logging-1.1.3.jar
		 *		hsqldb.jar
		 *	To include those files select "Project / Properties / Java Build Path"
		 *	from the menu.  Click on the "Libraries" tab.  Click "Add External JARs".
		 *	Browse to the above jar files, which should be in a directory in your
		 * 	project (e.g. JDBC-to-MSAccess).  Select all five files and click "Open".  Click "OK".
		 *	
		 * Be sure to add the drivers to your program by selecting Project >> Properties >> Java Build Path
		 */
		public static boolean OpenDatabaseConnectionMSAccess( )
		{
			boolean blnResult = false;
			
			try {
				String strConnectionString = "";
				
				// Server name/port, IP address/port or path for file based DB like MS Access
				// System.getProperty( "user.dir" ) => Current working directory from where
				// application was started
				strConnectionString = "jdbc:ucanaccess://" + System.getProperty( "user.dir" )
									+ "\\Database\\dbHCM.accdb";
				// Open a connection to the database
				m_conAdministrator = DriverManager.getConnection( strConnectionString );
				// Success
				blnResult = true;
			}
			catch 	(Exception e) {
				System.out.println( "Try again - error in OpenDB ");
				System.out.println( "Error is " + e );
			}
			return blnResult;
		}
		/**
		 * OpenDatabaseConnectionSQLServer - get SQL db connection
		 * @return blnResult
		 */
		public static boolean OpenDatabaseConnectionSQLServer( )
		{
			boolean blnResult = false;
			
			try
			{
				SQLServerDataSource sdsTeamsAndPlayers = new SQLServerDataSource( );
				//tg-comment out --sdsTeamsAndPlayers.setServerName( "localhost" ); // localhost or IP or server name
				sdsTeamsAndPlayers.setServerName( "localhost\\SQLExpress" ); // SQL Express version
				sdsTeamsAndPlayers.setPortNumber( 1433 );
				sdsTeamsAndPlayers.setDatabaseName( "dbHCM" );
				
				// Login Type:
				
					// Windows Integrated
					//tg-comment out --sdsTeamsAndPlayers.setIntegratedSecurity( true );
					
					// OR
					
					// SQL Server
				     sdsTeamsAndPlayers.setUser( "sa" );
					 sdsTeamsAndPlayers.setPassword( "" );	// Empty string "" for blank password
				
				// Open a connection to the database
				m_conAdministrator = sdsTeamsAndPlayers.getConnection(  );
				
				// Success
				blnResult = true;
			}
			catch( Exception excError )
			{
				// Display Error Message
				System.out.println( "Cannot connect - error: " + excError );

				// Warn about SQL Server JDBC Drivers
				System.out.println( "Make sure download MS SQL Server JDBC Drivers");
			}
			
			return blnResult;
		}
		
		
		/**
		* Name: CloseDatabaseConnection
		* Abstract: Close the connection to the database
		*/ 
		public static boolean CloseDatabaseConnection( )
		{
			boolean blnResult = false;
			
			try
			{
				// Is there a connection object?
				if( m_conAdministrator != null )
				{
					// Yes, close the connection if not closed already
					if( m_conAdministrator.isClosed( ) == false ) 
					{
						m_conAdministrator.close( );
						
						// Prevent JVM from crashing
						m_conAdministrator = null;
					}
				}
				// Success
				blnResult = true;
			}
			catch( Exception excError )
			{
				// Display Error Message
				System.out.println( excError );
			}
			
			return blnResult;
		}

}
