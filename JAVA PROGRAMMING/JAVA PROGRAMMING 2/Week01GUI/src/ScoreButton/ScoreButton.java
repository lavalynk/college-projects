package ScoreButton;

import javax.swing.*;
import java.awt.Color;
import java.awt.event.ActionListener;
import java.awt.event.ActionEvent;

/**
* A simple GUI application that keeps track of team scores based on button clicks.
* It provides buttons to increment, decrease, and reset the scores for two teams: Red and Blue.
* 
* This application uses the Swing library for the GUI and implements ActionListener
* to handle button click events.
* 
* @author Keith Brock
* Class: Java Programming 2
* Abstract: Create an application that will track the score for the Red Team and Blue Team.
*/
public class ScoreButton implements  ActionListener
{
		
/**
 * Default constructor for the ScoreButton class.
 * Initializes the class without any parameters.
 */
public ScoreButton() {
    // Default constructor
}

int intRedScoreAmount = 0;
int intBlueScoreAmount = 0;

// Define the JLabels
JLabel redLabel, blueLabel, redScore, blueScore;
// Define the JButtons
JButton redButton, blueButton, decreaseRedButton, decreaseBlueButton, resetButton;
// Define the JPanels
JPanel titlePanel, scorePanel, buttonPanel;

	/**
	 * Creates and configures the content pane for the application.
	 * @return A JPanel containing the application's GUI components.
	 * @throws NullPointerException if a required component is not initialized.
	 */
	public JPanel createContentPane () throws NullPointerException {
	
	    // We create a bottom JPanel to place everything on.
	    JPanel ScoreBoard = new JPanel();
	    ScoreBoard.setLayout(null);
	
	    // Creation of a Panel to contain the title labels
	    titlePanel = new JPanel();
	    titlePanel.setLayout(null);
	    titlePanel.setLocation(10, 0);
	    titlePanel.setSize(250, 30);
	    ScoreBoard.add(titlePanel);
	
	    // Create the red Team label
	    redLabel = new JLabel("Red Team");
	    redLabel.setLocation(0, 0);
	    redLabel.setSize(120, 30);
	    redLabel.setHorizontalAlignment(0);
	    redLabel.setForeground(Color.red);
	    titlePanel.add(redLabel);
	
	    // Create the blue Team label
	    blueLabel = new JLabel("Blue Team");
	    blueLabel.setLocation(130, 0);
	    blueLabel.setSize(120, 30);
	    blueLabel.setHorizontalAlignment(0);
	    blueLabel.setForeground(Color.blue);
	    titlePanel.add(blueLabel);
	
	    // Creation of a Panel to contain the score labels.
	    scorePanel = new JPanel();
	    scorePanel.setLayout(null);
	    scorePanel.setLocation(10, 40);
	    scorePanel.setSize(260, 30);
	    ScoreBoard.add(scorePanel);
	
	    // Creation of the label to hold the redScore
	    redScore = new JLabel(""+intRedScoreAmount);
	    redScore.setLocation(0, 0);
	    redScore.setSize(120, 30);
	    redScore.setHorizontalAlignment(0);
	    scorePanel.add(redScore);
	    
	    // Creation of the label to hold the blueScore
	    blueScore = new JLabel(""+intBlueScoreAmount);
	    blueScore.setLocation(130, 0);
	    blueScore.setSize(120, 30);
	    blueScore.setHorizontalAlignment(0);
	    scorePanel.add(blueScore);
	
	    // Creation of a Panel to contain all the JButtons.
	    buttonPanel = new JPanel();
	    buttonPanel.setLayout(null);
	    buttonPanel.setLocation(10, 80);
	    buttonPanel.setSize(280, 240);
	    ScoreBoard.add(buttonPanel);
	
	    // Create the button for the Red Score click
	    redButton = new JButton("Red Score!");
	    redButton.setLocation(0, 0);
	    redButton.setSize(120, 30);
	    redButton.addActionListener(this);
	    buttonPanel.add(redButton);
	
	    // Create the button for the Red Decrease click    
	    decreaseRedButton = new JButton("-1 for Red");
	    decreaseRedButton.setLocation(0, 35);
	    decreaseRedButton.setSize(120, 30);
	    decreaseRedButton.addActionListener(this);
	    buttonPanel.add(decreaseRedButton);    
	
	    // Create the button for the blue Score click
	    blueButton = new JButton("Blue Score!");
	    blueButton.setLocation(130, 0);
	    blueButton.setSize(120, 30);
	    blueButton.addActionListener(this);
	    buttonPanel.add(blueButton);
	
	    // Create the button for the Blue Decrease click    
	    decreaseBlueButton = new JButton("-1 for Blue");
	    decreaseBlueButton.setLocation(130, 35);
	    decreaseBlueButton.setSize(120, 30);
	    decreaseBlueButton.addActionListener(this);
	    buttonPanel.add(decreaseBlueButton);
	    
	    // Create the button for resetting the scores
	    resetButton = new JButton("Reset Score");
	    resetButton.setLocation(0, 70);
	    resetButton.setSize(250, 30);
	    resetButton.addActionListener(this);
	    buttonPanel.add(resetButton);
	
	    ScoreBoard.setOpaque(true);
	    return ScoreBoard;
	}
	
	
	
	/**
	 * Handles button click events for incrementing, decrementing, and resetting scores. 
	 * @param e The ActionEvent triggered by a button click.
	 */
	@Override	
	public void actionPerformed(ActionEvent e) {
		
		//if the red button is clicked, add 1 to the red score
	    if(e.getSource() == redButton)
	    {
	        intRedScoreAmount = intRedScoreAmount + 1;
	        redScore.setText(""+intRedScoreAmount);
	    }
	    //if the blue button is pushed, add 1 to the blue score
	    else if(e.getSource() == blueButton)
	    {
	        intBlueScoreAmount = intBlueScoreAmount + 1;
	        blueScore.setText(""+intBlueScoreAmount);
	    }
	    else if(e.getSource() == decreaseRedButton)
	    {
	        if (intRedScoreAmount > 0) {
	            intRedScoreAmount = intRedScoreAmount - 1;
	            redScore.setText("" + intRedScoreAmount);
	        }
	    }
	    else if(e.getSource() == decreaseBlueButton)
	    {
	        if (intBlueScoreAmount > 0) {
	            intBlueScoreAmount = intBlueScoreAmount - 1;
	            blueScore.setText("" + intBlueScoreAmount);
	        }
	    }    
	    //if the reset is clicked, reset the red and blue scores
	    else if(e.getSource() == resetButton)
	    {
	        intRedScoreAmount = 0;
	        intBlueScoreAmount = 0;
	        redScore.setText(""+intRedScoreAmount);
	        blueScore.setText(""+intBlueScoreAmount);
	    }
	}
	
	
	
	/**
	 * Creates and shows the GUI for the application.
	 */
	private static void createAndShowGUI() {
	    JFrame.setDefaultLookAndFeelDecorated(true);
	    JFrame frame = new JFrame(" Play with JButton Scores! ");
	
	    ScoreButton demo = new ScoreButton();
	    frame.setContentPane(demo.createContentPane());
	
	    frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	
	    frame.setSize(280, 240);
	    //make the frame visible
	    frame.setVisible(true);  
	}
	
	
	
	/**
	 * Main method to start the application. Schedules the GUI creation on the
	 * event-dispatching thread.
	 * 
	 * @param args (not used).
	 */
	public static void main(String[] args) {
	    //Schedule a job for the event-dispatching thread:
	    //creating and showing this application's GUI.
	    SwingUtilities.invokeLater(new Runnable() {
	        public void run() {
	            createAndShowGUI();
	        }
	    });
	}	
}


