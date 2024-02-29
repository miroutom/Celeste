using UnityEngine;
using System.Diagnostics;

public class DebugMode : MonoBehaviour
{
    static bool debugMode = false;
    private GameObject player;
    private Player playerComponent;
    static Stopwatch timer = new Stopwatch();
    private Room room;


    private int deathCount = 0;
    private bool deathHandled = false;
    private int minutes = -1;
    private bool minuteLeft = true;

    private const string DeathCountKey = "DeathCount";
    private const string Minutes = "Minutes";

    void Start()
    {
        timer.Start();
        player = GameObject.Find("Player");
        playerComponent = player.GetComponent<Player>();
        deathCount = PlayerPrefs.GetInt(DeathCountKey, 0);
        minutes = PlayerPrefs.GetInt(Minutes, 0);
      
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            ClickListener();
        }

        if (playerComponent.state == Player.State.death && !deathHandled)
        {
            deathCount++;
            deathHandled = true;

            PlayerPrefs.SetInt(DeathCountKey, deathCount);
            PlayerPrefs.Save();
        }
        else if (playerComponent.state != Player.State.death)
        {
            deathHandled = false;
        }


        if (timer.Elapsed.Seconds > 0 && timer.Elapsed.Seconds <= 59)
        {
            minuteLeft = false;
            
        } else if (timer.Elapsed.Seconds == 0 && !minuteLeft)
        {
            minuteLeft = true;
            minutes++;
            PlayerPrefs.SetInt(Minutes, minutes);
            PlayerPrefs.Save();
        }
    }

    void ClickListener()
    {
        debugMode = !debugMode;
        if (debugMode)
        {
            UnityEngine.Debug.Log("Debug mode activated!");
        }
        else
        {
            UnityEngine.Debug.Log("Debug mode deactivated!");
        }
    }

    void OnGUI()
    {
        if (debugMode)
        {
            DebugModeActivated();
        }

    }

    void DebugModeActivated()
    {
        PrintOnScreen(0, 0, "Debug information");
        PrintOnScreen(0, 10, "Player Position: " +
            player.transform.position.ToString());
        PrintOnScreen(0, 20, "Player State: " +
            playerComponent.state.ToString());
        PrintOnScreen(0, 30, "Deaths: " + deathCount.ToString());
        PrintOnScreen(0, 40, "Timer: " + timer.Elapsed.Seconds.ToString());
        PrintOnScreen(0, 50, "Minutes spent: " + minutes.ToString());
        PrintOnScreen(0, 60, "Current room: " + room.ToString());
    }

    void PrintOnScreen(int x, int y, string str)
    {
        GUI.Label(new Rect(x, y, Screen.width, Screen.height), str);
    }
}
