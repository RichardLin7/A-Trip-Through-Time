using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Creates 3 game state values
    public enum State {start, in_progress, over};
    public enum Character {dog, human};

    //Initializing a state and character variable
    static public State current_state = State.start;
    static public Character current_character = Character.dog;

    //Set endgame counter
    static public int counter = 0;

    //Hold text game object for current state 
    GameObject currState_go;
    GameObject human_go;
    GameObject dog_go;

    //Hold text component for current state 
    static Text currState_txt;
    static public Button human_btn;
    static public Button dog_btn;

    //run at game start 
    void Start()
    {
        //Initializes for access to componet
        currState_go = GameObject.Find("Curr_State_txt");
        human_go = GameObject.Find("human_btn");
        dog_go = GameObject.Find("dog_btn");

        //Initializes using Text component
        currState_txt = currState_go.GetComponent<Text>();
        human_btn = human_go.GetComponent<Button>();
        dog_btn = dog_go.GetComponent<Button>();

        //Loads string to game interface
        currState_txt.text = current_state.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /* Method: Human()
     * Parameters: None
     * Returns: Void
     * Class Scope Effects: current_character,  ChallengeDeck.currResult_txt, dog_btn, human_btn
     * Called Methods: none
     *
     * Description: Sets player avatar to human
     * 
     * Version/Date: <Ver#1 / 04-25>
     */
    public void Human()
    {
        human_btn.interactable = false;
        dog_btn.interactable = false;
        current_character = Character.human;
        ChallengeDeck.currResult_txt.text = "You chose human! Please roll the dice!";
    }

    /* Method: Dog()
     * Parameters: None
     * Returns: Void
     * Class Scope Effects: current_character,  ChallengeDeck.currResult_txt, dog_btn, human_btn
     * Called Methods: none
     *
     * Description: Sets player avatar to dog
     * 
     * Version/Date: <Ver#1 / 04-25>
     */
    public void Dog()
    {
        human_btn.interactable = false;
        dog_btn.interactable = false;
        current_character = Character.dog;
        ChallengeDeck.currResult_txt.text = "You chose dog! Please roll the dice!";
    }


    /* Method: Replay()
     * Parameters: None
     * Returns: Void
     * Class Scope Effects: current_state, currState_txt.text
     * Called Methods: Dice.Replay(), Player.Replay(), Path.Replay(), ChallengeDeck.Replay()
     * 
     * Description: Resets Game field variables to the start state
     * 
     * Version/Date: <Ver#1 / 04-25>
     */
    public void Replay()
    {
        //Reports game state
        current_state = State.start;
        //Sends game state to game interface;
        currState_txt.text = current_state.ToString();

        //Buttons reactivated
        human_btn.interactable = true;
        dog_btn.interactable = true;

        Dice.Replay();
        Path.Replay();
        ChallengeDeck.Replay();
    }

    //Quit game on player's command
    public void Quit()
    {
        print("Thanks for playing!");

        currState_txt.text = "Thanks for playing!";

        Application.Quit();
    }


    /*  Method: CheckGameState()
     * Params: None
     * Returns: void
     * Class Scope Effects: current_state, currState_txt.text
     * Called Methods: none
     * 
     * Description: Loads a game object way-points
     * 
     * Version/Date: <Ver#1 / 04-20>
     */
    static public void CheckGameState()
    {
        //Checks waypoint position progress, currently ends at 9, restarts at 0
        if(Path.current_waypoint_num == 9)
        {
            current_state = State.over;
            ChallengeDeck.currScenario_txt.text = "You made it back before the portal closed.";
            ChallengeDeck.currResult_txt.text = "YOU WIN!"; 
        }
        else if(Path.target_waypoint_num == 0)
        {
            current_state = State.start;
        }
        else if(Game.counter == 5)
        {
            current_state = State.over;

            //stops avatar
            Avatar.onTarget = true;
            Path.current_waypoint_num = Path.target_waypoint_num;
            Avatar.avatar.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            ChallengeDeck.currScenario_txt.text = "The portal has closed.";
            ChallengeDeck.currResult_txt.text = "GAME OVER.";
        }
        else
        {
            current_state = State.in_progress;
        }

        //Sends game state to game interface
        currState_txt.text = current_state.ToString();
    }
}
