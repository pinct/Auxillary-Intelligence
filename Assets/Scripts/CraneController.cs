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
	float xPosition;
	Vector3 Position;
	public Texture2D mouseOverCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	Vector2 cursorHotspot;
	bool toggle = false;
	public GameObject magnet;
	GameObject LastBot;
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
			crane.GetComponent<Animator>().SetBool("IsActive", false);
			if (LastBot != null)
			{
				LastBot.GetComponent<Rigidbody2D>().isKinematic = false;
			}
			toggle = false;
		}
		if (canmove)
		{
			Position = new Vector3(xPosition, crane.transform.position.y, 0);
			crane.transform.position = Position;
			if (Input.GetKey(KeyCode.D) && xPosition < 65.9)
			{
				xPosition = xPosition + 2.0f * Time.deltaTime;
				crane.GetComponent<Animator>().SetBool("IsWalkingRight", true);
				crane.GetComponent<Animator>().SetBool("IsWalking", false);
			}
			if (Input.GetKey(KeyCode.A) && xPosition > 59.2)
			{
				xPosition = xPosition - 2.0f * Time.deltaTime;
				crane.GetComponent<Animator>().SetBool("IsWalking", true);
				crane.GetComponent<Animator>().SetBool("IsWalkingRight", false);
			}
			else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				crane.GetComponent<Animator>().SetBool("IsWalking", false);
				crane.GetComponent<Animator>().SetBool("IsWalkingRight", false);
			}
			if (Input.GetButtonUp("Jump"))
			{
				if (!toggle)
				{
					crane.GetComponent<Animator>().SetBool("IsTransformed", true);
					StartCoroutine(Toggling());
				}
				else if (toggle)
				{
					crane.GetComponent<Animator>().SetBool("IsTransformed", false);
					toggle = !toggle;
				}
			}
			if (toggle)
			{
				crane.GetComponent<Animator>().SetBool("IsTransformed", true);
				LastBot.GetComponent<Rigidbody2D>().isKinematic = true;
				if (((magnet.transform.position.x - 1.0f) < LastBot.transform.position.x) && (LastBot.transform.position.x < (magnet.transform.position.x + 1.0f)))
				{
					LastBot.transform.position = Vector2.MoveTowards(LastBot.transform.position, magnet.transform.position, 0.2f);
				}
			}
			else
			{
				LastBot.GetComponent<Rigidbody2D>().isKinematic = false;
			}
		}
    }
	void OnMouseDown()
	{
		startbot.GetComponent<ChangeRobot>().currbot.GetComponent<PlatformerCharacter2D>().enabled = false;
		startbot.GetComponent<ChangeRobot>().currbot.GetComponent<Platformer2DUserControl>().enabled = false;
		canmove = true;
		LastBot = startbot.GetComponent<ChangeRobot>().currbot;
		VCAM.GetComponent<CinemachineVirtualCamera>().Follow = crane.transform;
		startbot.GetComponent<ChangeRobot>().currbot = crane;
		crane.GetComponent<Animator>().SetBool("IsActive", true);
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
	IEnumerator Toggling()
	{
		yield return new WaitForSeconds(1.3f);
		toggle = !toggle;
	}
}
