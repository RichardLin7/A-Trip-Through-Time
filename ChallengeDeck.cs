using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChallengeDeck : MonoBehaviour
{
    public struct Challenge
    {
        //creates scenario struct
        public string scenario;
        //public string proceed;

        //creates result list for each event
        public string[] result;
    }

    //Holds a list of events
    static public List<Challenge> ChallengeList = new List<Challenge>();

    //holds current story decision
    static public Challenge currChallenge = new Challenge();

    //10 elements int Challenge List
    static public Challenge challenge_0;
    static public Challenge challenge_1;
    static public Challenge challenge_2;
    static public Challenge challenge_3;
    static public Challenge challenge_4;
    static public Challenge challenge_5;
    static public Challenge challenge_6;
    static public Challenge challenge_7;
    static public Challenge challenge_8;
    static public Challenge challenge_9;

    //Holds the UI elements
    static public GameObject currScenario_go;
    static public Text currScenario_txt;
    //static public GameObject currProceed_go;
    //static public Text currProceed_txt;
    static public GameObject currResult_go;
    static public Text currResult_txt;

    //GameObject proceed_go;
    //static public Button proceed_btn;

    //Switch to control if proceed buton is on
    //static public bool is_proceed_on = false;

    //Used to track index through ChallengeList
    static public int currChallenge_num = 0;

    // Start is called before the first frame update
    void Start()
    {
        //Load the challenges
        LoadChallenges();

        //Captures all GUI Game Objects for acces to text of each challenge
        currScenario_go = GameObject.Find("Scenario_txt");
        currScenario_txt = currScenario_go.GetComponent<Text>();
        //currProceed_go = GameObject.Find("Proceed_txt");
        //currProceed_txt = currProceed_go.GetComponent<Text>();
        currResult_go = GameObject.Find("Result_txt");
        currResult_txt = currResult_go.GetComponent<Text>();

        //Captures all GUI game object for access to buttons
        //proceed_go = GameObject.Find("Proceed_btn");
        //proceed_btn = proceed_go.GetComponent<Button>();

        //Loads GUI Text object with start content
        currScenario_txt.text = "Welcome to A Trip Through Time! With a broken time machine in hand, " +
            "you have 3 rolls to make it back to the museum before the portal closes!";

        currResult_txt.text = "Please choose whether you are going to be a human or dog. (if unselected, it will be dog.)";
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    /*Method: PostNewChallenge()
     * Parameters: None
     * Returns: void
     * Class Scope Effects: currChallenge_num, currScenario_txt.text
     * 
     * Called Methods: none
     * 
     * Description Post new challenge to game GUI. Iterates through ChallengeList with
     * max as many challenges written
     * 
     * Version/Date: <Ver#1/ 04-24>
     */
    static public void Replay()
    {
        currChallenge_num = 0;

        currScenario_txt.text = "Welcome to A Trip Through Time!";

        currResult_txt.text = " With a broken time machine in hand, " +
            "you have 3 rolls to make it back to the museum before the portal closes!";
    }

    /*Method: PostNewChallenge()
     * Parameters: None
     * Returns: void
     * Class Scope Effects: currChallenge_num; currScenario_txt.text
     * 
     * Called Methods: none
     * 
     * Description: Post new events to game GUI. Iterates through CallengeList with
     * max as many challenges written.
     * 
     * Verison/Date: <Ver#2 / 04-20>
     */
    static public void PostNewChallenge()
    {
        print("currChallenge_num: " + currChallenge_num);

        //add condition of state. in_progress so that on replay, the start text is not erased
        //by posting a new challenge.
        if (Game.current_state == Game.State.in_progress)
        {
            currChallenge = ChallengeList[currChallenge_num];

            currScenario_txt.text = currChallenge.scenario;

            //declares index
            int index;
            //initiliazes index and figures out what event is played.
            if (Game.current_character == Game.Character.human)
                index = 1;
            else if (Game.current_character == Game.Character.dog)
                index = 0;
            else
                index = 0;

            currResult_txt.text = currChallenge.result[index];

            //Move up 1 for human
            if (Game.current_character == Game.Character.human)
                switch (currChallenge_num)
                {
                    case (1): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    case (4): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    case (6): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    case (8): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    default: ;
                        break;
                }
            //Move up 1 for dog
            if (Game.current_character == Game.Character.dog)
                switch (currChallenge_num)
                {
                    case (0): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    case (2): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    case (3): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    case (5): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    case (7): Path.TeleportToNextWayPoint(Avatar.avatar);
                        break;
                    default:;
                        break;
                }
            
            Game.CheckGameState();

            if (currChallenge_num < 4) // change this upper bound as Challenge list grows.
            {
                currChallenge_num++;
            }
            else
                currChallenge_num = 0;
        }

    }

    /*Method: LoadChallenges()
    * Parameters: None
    * Returns: void
    * Class Scope Effects: Initiantes challenge_0 to 9 structs,
    * ChallengeList
    * 
    * Called Methods: none
    * 
    * Description: Post new challenge to game GUI
    * 
    * Version/Date : <Ver #2 / 04-25>
    */
    static void LoadChallenges()
    {
        //Challenge 0- does not run cause its at starting waypoint.
        challenge_0.result = new string[2];
        challenge_0.scenario = "You found yourself in front of a very hungry Velociraptor...";

        challenge_0.result[0] = "You barked at it! You scare it away!";
        challenge_0.result[1] = "It scares you away!";
        ChallengeList.Add(challenge_0);

        //Challenge 1
        challenge_1.result = new string[2];
        challenge_1.scenario = "You arrive in front of a caveman!";

        challenge_1.result[0] = "He tried to hunt you! run away!";
        challenge_1.result[1] = "He is interested in your trinkets from the future. You were able to appease the caveman peacefully.";
        ChallengeList.Add(challenge_1);

        //Challenge 2
        challenge_2.result = new string[2];
        challenge_2.scenario = "You find yourself in early civilization, straw huts all around you with small farms...";

        challenge_2.result[0] = "The villagers takes care of you and see you as a pet.";
        challenge_2.result[1] = "The villagers does not recognize you. You were chased out of there.";
        ChallengeList.Add(challenge_2);

        //Challenge 3
        challenge_3.result = new string[2];
        challenge_3.scenario = "You find yourself in the Middle Ages. The port city you are transported to is currently getting invaded by THE MONGOLS.";

        challenge_3.result[0] = "The invaders payed no attention to you presence and you were able to escape.";
        challenge_3.result[1] = "The invaders are attacking you as well! You were barely able to escape!";
        ChallengeList.Add(challenge_3);

        //Challenge 4
        challenge_4.result = new string[2];
        challenge_4.scenario = "You find youself in Rome during its peak in history. They are currently holding a Feriae(festival)...";

        challenge_4.result[0] = "The Roman citizens suspects you of stealing food from a stall! They chase you away!";
        challenge_4.result[1] = "The Roman citizens, seeing you with your future gadgets, thinks you are a demi-god! You enjoyed the festival.";
        ChallengeList.Add(challenge_4);

        //Challenge 5
        challenge_5.result = new string[2];
        challenge_5.scenario = "You find youself in the Medieval Ages. It looks like the time machine took you inside a castle...";

        challenge_5.result[0] = "The princess of the castle saw you and treated you with care.";
        challenge_5.result[1] = "The guards saw you as a intruder! You are thrown out of the castle.";
        ChallengeList.Add(challenge_5);

        //Challenge 6
        challenge_6.result = new string[2];
        challenge_6.scenario = "You find yourself in the Classical Age. The time machine transported you in the middle of a lecture hall...";

        challenge_6.result[0] = "You are ousted from the hall immediately.";
        challenge_6.result[1] = "Your attire appeals to their curiosity. They asked you many questions and aided you.";
        ChallengeList.Add(challenge_6);

        //Challenge 7
        challenge_7.result = new string[2];
        challenge_7.scenario = "You find yourself in the Age of Exploration. You are on massive ship headed to the new world!";

        challenge_7.result[0] = "The crew and the passengers on the ship liked your fluffy, cute company and you made it to shore safely.";
        challenge_7.result[1] = "You find out that this is a pirate ship! You quickly activate you broken time machine before they threw you overboard.";
        ChallengeList.Add(challenge_7);

        //Challenge 8
        challenge_8.result = new string[2];
        challenge_8.scenario = "You find yourself in the middle of The CIVIL WAR. Great.";

        challenge_8.result[0] = "You are a dog and you do not know what to do! You run around in circles....";
        challenge_8.result[1] = "You were able to convince the men in battle that you were on their side. Nice!";
        ChallengeList.Add(challenge_8);

        //Challenge 9 - not run - endpoint tile.
        challenge_9.result = new string[2];
        challenge_9.scenario = "You arrive in the Industrial Age. You are in a city, surrounded by automobiles and factories," +
            "and a man approaches you...";

        challenge_9.result[0] = "Thinking you are a stray, he takes you to the pound.";
        challenge_9.result[1] = "He greeted you and noticed your broken time machine. He took you to a shop and got it fixed!";
        ChallengeList.Add(challenge_9);
    }
}