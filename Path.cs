using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    //Array that hold all possible way-points
    static public GameObject[] path = new GameObject[10];

    //Enums corresponds to the 0 to 9 way-points in path[]
    static public int current_waypoint_num = 0;

    //Next way-point number to go after roll
    static public int target_waypoint_num = 0;

    //Hold next way-point object
    static public GameObject target_waypoint;

    //run at game start
    void Start()
    {
        //Loads all way-points from Unity editor into path[]
        LoadWayPoints();
    }

    //Resets Rath field variables to the game's original state
    static public void Replay()
    {
        target_waypoint_num = 0;
        GoToNextWayPoint(Avatar.avatar);
    }

    //Update is called once per frame
    void Update()
    {
        //Check if avatar is off target, and runs the method if off target
        if(!Avatar.onTarget)
        {
            Path.StopAtNextWayPoint(Avatar.avatar);
        }
    }

    /*Method: LoadWayPoints()
     * Parameters: None
     * Returns: void
     * Class Scope Effects: path[] elements are all initialized
     * Call Methods; none
     * 
     *Version/Date: <Ver#1 / 04-20>
     */
     void LoadWayPoints()
    {
        path[0] = GameObject.Find("start-point");
        path[1] = GameObject.Find("wp_1");
        path[2] = GameObject.Find("wp_2");
        path[3] = GameObject.Find("wp_3");
        path[4] = GameObject.Find("wp_4");
        path[5] = GameObject.Find("wp_5");
        path[6] = GameObject.Find("wp_6");
        path[7] = GameObject.Find("wp_7");
        path[8] = GameObject.Find("wp_8");
        path[9] = GameObject.Find("end-point");
    }

    /* Method: GoToNextWayPoint()
     * Parameters: GameObject
     * Returns: Void
     * Class Scope Effects: Avatar.is_on_target, target_waypoint_num
     * Called Methods: Game.CheckGameState();
     * 
     * Description: Start the animation of the avatar
     * 
     * Version/Date: <Ver#1 / 04-20> Modified from textbook
     */
     static public void GoToNextWayPoint(GameObject avatar)
    {
        //Count rolls
        Game.counter++;

        Avatar.onTarget = false;

        target_waypoint_num = Mathf.Min(9, target_waypoint_num + Dice.diceResult_num);

        //debugging report
        print("GoToNextWayPoint: " + target_waypoint_num);

        //calculate direction to target pickup and start moving towards it 
        target_waypoint = path[target_waypoint_num];

        //debugging report
        //print("NEXT | x: " + target_waypoint.transform.position.x + ", " + "y: " + target_waypoint.transform.position.y);

        //After checking not null, creates direction, normalizes it, 
        //then animates with AddForce & Impulse. Avatar must have RigidBody2d Component

        if (target_waypoint != null)
        {
            //Vectorizes from different positions of target_waypoint and avatar
            Vector2 direction = new Vector2(target_waypoint.transform.position.x - avatar.transform.position.x, target_waypoint.transform.position.y - avatar.transform.position.y);

            //Normalize
            direction.Normalize();

            //Animates avatar
            avatar.GetComponent<Rigidbody2D>().AddForce(direction * Avatar.ImpulseForceMagnitude, ForceMode2D.Impulse);



        }
    }

    /* Method: TeleportToNextWayPoint()
     * Parameters: GameObject
     * Returns: Void
     * Class Scope Effects: Avatar.is_on_target, target_waypoint_num
     * Called Methods: Game.CheckGameState();
     * 
     * Description: Start the animation of the avatar based on event
     * 
     * Version/Date: <Ver#1 / 05-2>
     */
    static public void TeleportToNextWayPoint(GameObject avatar)
    {

        Avatar.onTarget = false;

        target_waypoint_num = Mathf.Min(9, target_waypoint_num + 1);

        //debugging report
        print("GoToNextWayPoint: " + target_waypoint_num);

        //calculate direction to target pickup and start moving towards it 
        target_waypoint = path[target_waypoint_num];

        //debugging report
        //print("NEXT | x: " + target_waypoint.transform.position.x + ", " + "y: " + target_waypoint.transform.position.y);

        //After checking not null, creates direction, normalizes it, 
        //then animates with AddForce & Impulse. Avatar must have RigidBody2d Component

        if (target_waypoint != null)
        {
            //Vectorizes from different positions of target_waypoint and avatar
            Vector2 direction = new Vector2(target_waypoint.transform.position.x - avatar.transform.position.x, target_waypoint.transform.position.y - avatar.transform.position.y);

            //Normalize
            direction.Normalize();

            //Animates avatar
            avatar.GetComponent<Rigidbody2D>().AddForce(direction * Avatar.ImpulseForceMagnitude, ForceMode2D.Impulse);



        }
    }
    /*Method: StopAtNextWayPoint()
     *Parameters: GameObject
     *Returns: void
     *Class Scope Effects: Avatar.onTarget, current_waypoint_num 
     *Called Methods: Game.CheckGameState();
     * 
     *Description: Stops the animation of avatar
     * 
     * Version/Date: <Version#1 / 04-18>
     */
    static public void StopAtNextWayPoint(GameObject avatar)
    {
        //print("1 | stop at next way-point");
        //debugging report of positions
        //print("AVA | x: " + avatar.transform.position.x + ", " + "y: " + target_waypoint.transform.position.y);

        //print("NEXT | x: " + target_waypoint.transform.position.x + ", " + " y: " + target_waypoint.transform.position.y);

        //Check if animated avatar is within range of center point of target_waypoint
        if(Mathf.Abs(avatar.transform.position.x - target_waypoint.transform.position.x) < 10 
            && Mathf.Abs(avatar.transform.position.y - target_waypoint.transform.position.y) < 10)
        {
            //print("2 | stop at next way-point");
            //Set velocity to zero
            avatar.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            //Changes onTarget
            Avatar.onTarget = true;

            //passes target to current_waypoint
            current_waypoint_num = target_waypoint_num;

            //plays challenge that is on waypoint
            ChallengeDeck.currChallenge_num = target_waypoint_num;

            //post next event
            ChallengeDeck.PostNewChallenge();

            Dice.rollDice_btn.interactable = true;
            Dice.are_dice_on = true;

        }

        //Runs helper method
        Game.CheckGameState();
    }

    
}
