using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using Cinemachine;

public class PlatformControl : MonoBehaviour
{
	public GameObject platform;
	bool canmove = false;
	public bool intrigger = false;
	public GameObject on;
	public GameObject off;
	public GameObject LastBot;
	public GameObject startbot;
	public GameObject VCAM;
	public GameObject trans;
	public GameObject particle;
	float xPosition;
	Vector3 Position;
	public Texture2D mouseOverCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	Vector2 cursorHotspot;
    // Start is called before the first frame update
    void Start()
    {
        on.GetComponent<SpriteRenderer>().enabled = false;
		xPosition = platform.transform.position.x;
		cursorHotspot = new Vector2 (mouseOverCursor.width / 2, mouseOverCursor.height / 2);
		Cursor.SetCursor(null, cursorHotspot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (startbot.GetComponent<ChangeRobot>().currbot.name != "Platform")
		{
			off.GetComponent<SpriteRenderer>().enabled = true;
			on.GetComponent<SpriteRenderer>().enabled = false;
			canmove = false;
			particle.SetActive(false);
		}
		if (canmove && intrigger)
		{
			LastBot.transform.position = trans.transform.position;
		}
		if (canmove)
		{
			Position = new Vector3(xPosition, platform.transform.position.y, 0);
			platform.transform.position = Position;
			if (Input.GetKey(KeyCode.D) && xPosition < 42.0f)
			{
				xPosition = xPosition + 2.0f * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A) && xPosition > 34.0f)
			{
				xPosition = xPosition - 2.0f * Time.deltaTime;
			}
		}
    }
	void OnMouseDown()
	{
		startbot.GetComponent<ChangeRobot>().currbot.GetComponent<PlatformerCharacter2D>().enabled = false;
		startbot.GetComponent<ChangeRobot>().currbot.GetComponent<Platformer2DUserControl>().enabled = false;
		canmove = true;
		particle.SetActive(true);
		LastBot = startbot.GetComponent<ChangeRobot>().currbot;
		VCAM.GetComponent<CinemachineVirtualCamera>().Follow = platform.transform;
		on.GetComponent<SpriteRenderer>().enabled = true;
		startbot.GetComponent<ChangeRobot>().currbot = platform;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		intrigger = true;
	}
	void OnTriggerExit2D(Collider2D other)
	{
		intrigger = false;
	}
	void OnMouseEnter()
	{
		if (!canmove)
		{
			Cursor.SetCursor(mouseOverCursor, cursorHotspot, cursorMode);
		}
	}
	void OnMouseExit()
	{
		Cursor.SetCursor(null, cursorHotspot, cursorMode);
	}
}
