using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    //hold the game object of dice
    GameObject diceResult_go;
    //holds Roll Dice Button GO
    GameObject rollDice_go;

    //Display what is rolled
    static Text diceResult_txt;
    //Will hold button component
    static public Button rollDice_btn; 

    //Holds result of RollDice()
    static public int diceResult_num = 0;

    static public bool are_dice_on = true;

    // Start is called before the first frame update
    void Start()
    {
        //initializes game object Dice Result
        diceResult_go = GameObject.Find("Dice_Result_txt");
        //initialize dice roll button
        rollDice_go = GameObject.Find("Roll_Dice_btn");

        //Initializes text object
        diceResult_txt = diceResult_go.GetComponent<Text>();
        //Initializes text object
        rollDice_btn = rollDice_go.GetComponent<Button>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*Method: Replay()
     * Parameters: None
     * Returns: void
     * Class Scope Effects: diceResult_num, diceResult_txt.text, are_dice_on
     * 
     * Called Methods: None
     * 
     * Description: Resets Dice field variavle to the start state
     * 
     * Verison/Date: <Ver#1 / 04-24>
     */
     static public void Replay()
    {
        diceResult_num = 0;
        diceResult_txt.text = "Dice Roll: ?";
        are_dice_on = true;
    }

    /*
     * Method:RollDice()
     * Parameters: None
     * Returns: int
     * Class Scope Effects: diceResult_num, diceResult_txt.text
     * Path.GoToNextWayPoint()
     * 
     * Called Methods: GetRandom()
     * 
     * Description: Controls dice roll process
     * 
     * Version/Date: <Ver#1 / 04-20>
     */
     public void RollDice()
    {
        if(Game.current_state != Game.State.over && are_dice_on)
        {

            are_dice_on = false;
            rollDice_btn.interactable = false;

            //Clear Deck fields
            ChallengeDeck.currScenario_txt.text = "";
            ChallengeDeck.currResult_txt.text = "";

            //passes result of GetRandom()
            diceResult_num = GetRandom();
            //diceResult_num = 1;

            //converts number to string and then puts into text in UI
            diceResult_txt.text = "Dice Roll: " + diceResult_num.ToString();

            //Sends avatar to next waypoint
            Path.GoToNextWayPoint(Avatar.avatar);
        }
        
    }
    /* Method: GetRandom()
     * Parameters: None
     * Returns: int
     * Class Scope Effects: None
     * Called Methods: Random.Range()
     * 
     * Description: returns a random number in range
     * 
     * Versio/Date: <Ver#1 / 04-20>
     */
    int GetRandom()
    {
        //RH is EXCLUSIVE
        return Random.Range(1,7);
    }
}
