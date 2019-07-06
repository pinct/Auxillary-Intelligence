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
	public GameObject particles;
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
		else
		{
			StartCoroutine(Startup());
		}
    }

    // Update is called once per frame
    void Update()
    {
        if (currbot != robot)
		{
			robot.GetComponent<PlatformerCharacter2D>().enabled = false;
			robot.GetComponent<Platformer2DUserControl>().enabled = false;
			robot.GetComponent<Animator>().SetBool("IsActive", false);
			robot.GetComponent<Animator>().SetBool("Ground", true);
			particles.SetActive(false);
		}
		currbot = startbot.GetComponent<ChangeRobot>().currbot;
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
		robot.GetComponent<Animator>().SetBool("IsActive", true);
		VCAM.GetComponent<CinemachineVirtualCamera>().Follow = robot.transform;
		startbot.GetComponent<ChangeRobot>().currbot = robot;
		currbot = robot;
		particles.SetActive(true);
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
	IEnumerator Startup()
	{
		robot.GetComponent<Animator>().SetBool("Ground", true);
		robot.GetComponent<Animator>().SetBool("IsActive", true);
		robot.GetComponent<PlatformerCharacter2D>().enabled = false;
		robot.GetComponent<Platformer2DUserControl>().enabled = false;
		particles.SetActive(false);
		yield return new WaitForSeconds(3.1f);
		particles.SetActive(true);
		robot.GetComponent<PlatformerCharacter2D>().enabled = true;
		robot.GetComponent<Platformer2DUserControl>().enabled = true;
	}
}
