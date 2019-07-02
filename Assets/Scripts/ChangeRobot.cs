using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using Cinemachine;

public class ChangeRobot : MonoBehaviour
{
	public GameObject robot;
	public GameObject currbot;
	public GameObject startbot;
	public GameObject VCAM;
	public Texture2D mouseOverCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	Vector2 cursorHotspot;
    //Start is called before the first frame update
    void Start()
    {
		cursorHotspot = new Vector2 (mouseOverCursor.width / 2, mouseOverCursor.height / 2);
		Cursor.SetCursor(null, cursorHotspot, cursorMode);
        if (robot.name != "StartingCharacter")
		{
			robot.GetComponent<PlatformerCharacter2D>().enabled = false;
			robot.GetComponent<Platformer2DUserControl>().enabled = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (currbot != robot)
		{
			robot.GetComponent<PlatformerCharacter2D>().enabled = false;
			robot.GetComponent<Platformer2DUserControl>().enabled = false;
		}
    }
	void OnMouseDown()
	{
		if (currbot.name != "Platform")
		{
			currbot.GetComponent<PlatformerCharacter2D>().enabled = false;
			currbot.GetComponent<Platformer2DUserControl>().enabled = false;
		}
		robot.GetComponent<PlatformerCharacter2D>().enabled = true;
		robot.GetComponent<Platformer2DUserControl>().enabled = true;
		VCAM.GetComponent<CinemachineVirtualCamera>().Follow = robot.transform;
		startbot.GetComponent<ChangeRobot>().currbot = robot;
		currbot = robot;
	}
	void OnMouseEnter()
	{
		if (robot != startbot.GetComponent<ChangeRobot>().currbot)
		{
			Cursor.SetCursor(mouseOverCursor, cursorHotspot, cursorMode);
		}
	}
	void OnMouseExit()
	{
		Cursor.SetCursor(null, cursorHotspot, cursorMode);
	}
}
