using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInstructions : MonoBehaviour
{
    public Queue<string> instruction = new Queue<string>();
    public Queue<string> history = new Queue<string>();

    public bool beginTutorial;
    public bool beginHistory;

    AIInstructions()
    {
        instruction.Enqueue("Hello Monsieur. It is good to be functioning again...");
        instruction.Enqueue("... in the LAB.OS10 system I see that the front door of your lab has been locked because the boxkey is not connected to the floor switch...");
        instruction.Enqueue("...and the safe bridge to the other side is down because its switch is off, as well.");
        instruction.Enqueue("Another thing, the __SPHERE__'s radio frequency is showing she is within your range. However, she seens to be still OFF.");
        instruction.Enqueue("<< _to_player_: you can SWITCH BETWEEN CAMERAS to explore the room using the keyboard keys [1] and [2] >>");
        instruction.Enqueue("<< _to_player_: to INTERACT with switches you must use the keyboard key [E] >>");

        history.Enqueue("My system shows that we are at the year 287 D.M., Which means we were switched off for about 13 years.");
        history.Enqueue("I do not have any data entry withing those years gap. It is very strange. My last data is when you finished the __SPHERE__ project and had her working as you desired.");
        history.Enqueue("Another stranger thing is that my sensor can only detect a weak readio frequency from a nearby land station... and nothing else.");
    }

    private void Awake()
    {
        beginTutorial = false;
        beginHistory = false;
    }

    private void Update()
    {
        if (beginTutorial && instruction.Count > 0)
        {
            FindObjectOfType<AIUI>().ShowText(instruction);
        }
        else if (beginHistory && history.Count > 0)
        {
            FindObjectOfType<AIUI>().ShowText(history);
        }
    }
}
