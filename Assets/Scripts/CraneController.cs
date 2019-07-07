using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using Cinemachine;

public class CraneController : MonoBehaviour
{
	public GameObject crane;
	bool canmove = false;
	public GameObject startbot;
	public GameObject VCAM;
	public GameObject particle;
	float xPosition;
	Vector3 Position;
	public Texture2D mouseOverCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	Vector2 cursorHotspot;
    // Start is called before the first frame update
    void Start()
    {
		xPosition = crane.transform.position.x;
		cursorHotspot = new Vector2 (mouseOverCursor.width / 2, mouseOverCursor.height / 2);
		Cursor.SetCursor(null, cursorHotspot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (startbot.GetComponent<ChangeRobot>().currbot.name != "Crane")
		{
			canmove = false;
			particle.SetActive(false);
		}
		if (canmove)
		{
			Position = new Vector3(xPosition, crane.transform.position.y, 0);
			crane.transform.position = Position;
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
		VCAM.GetComponent<CinemachineVirtualCamera>().Follow = crane.transform;
		startbot.GetComponent<ChangeRobot>().currbot = crane;
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		
	}
	void OnTriggerExit2D(Collider2D other)
	{
		
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
