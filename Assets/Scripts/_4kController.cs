using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using Cinemachine;

public class _4kController : MonoBehaviour
{
	public GameObject fork;
	bool canmove = false;
	public GameObject startbot;
	public GameObject VCAM;
	public GameObject particle;
	float xPosition;
	Vector3 Position;
	public Texture2D mouseOverCursor;
	public CursorMode cursorMode = CursorMode.Auto;
	Vector2 cursorHotspot;
	public GameObject Lift;
	public GameObject LiftSprite;
    // Start is called before the first frame update
    void Start()
    {
		LiftSprite.GetComponent<Animator>().SetFloat("Height", (Lift.transform.position.y + 3.3f)/-4.36f);
		xPosition = fork.transform.position.x;
		cursorHotspot = new Vector2 (mouseOverCursor.width / 2, mouseOverCursor.height / 2);
		Cursor.SetCursor(null, cursorHotspot, cursorMode);
    }

    // Update is called once per frame
    void Update()
    {
        if (startbot.GetComponent<ChangeRobot>().currbot.name != "Forklift")
		{
			canmove = false;
			particle.SetActive(false);
			fork.GetComponent<Animator>().SetBool("IsActive", false);
		}
		if (canmove)
		{
			Position = new Vector3(xPosition, fork.transform.position.y, 0);
			fork.transform.position = Position;
			if (Input.GetKey(KeyCode.D) && xPosition < 89.2)
			{
				xPosition = xPosition + 2.0f * Time.deltaTime;
				Lift.transform.position = new Vector2(Lift.transform.position.x + 2.0f * Time.deltaTime, Lift.transform.position.y);
				fork.GetComponent<Animator>().SetBool("IsWalking", true);
			}
			if (Input.GetKey(KeyCode.A) && xPosition > 74.25)
			{
				xPosition = xPosition - 2.0f * Time.deltaTime;
				Lift.transform.position = new Vector2(Lift.transform.position.x - 2.0f * Time.deltaTime, Lift.transform.position.y);
				fork.GetComponent<Animator>().SetBool("IsWalking", true);
			}
			if (Input.GetKey(KeyCode.Space) && Lift.transform.position.y < -3.3f)
			{
				Lift.transform.position = new Vector2(Lift.transform.position.x, Lift.transform.position.y + 0.1f);
				LiftSprite.GetComponent<Animator>().SetFloat("Height", (Lift.transform.position.y + 3.3f)/-4.36f);
			}
			if (Input.GetKey(KeyCode.LeftControl) && Lift.transform.position.y > -7.66f)
			{
				Lift.transform.position = new Vector2(Lift.transform.position.x, Lift.transform.position.y - 0.1f);
				LiftSprite.GetComponent<Animator>().SetFloat("Height", (Lift.transform.position.y + 3.3f)/-4.36f);
			}
			else if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
			{
				fork.GetComponent<Animator>().SetBool("IsWalking", false);
			}
		}
    }
	void OnMouseDown()
	{
		startbot.GetComponent<ChangeRobot>().currbot.GetComponent<PlatformerCharacter2D>().enabled = false;
		startbot.GetComponent<ChangeRobot>().currbot.GetComponent<Platformer2DUserControl>().enabled = false;
		canmove = true;
		particle.SetActive(true);
		VCAM.GetComponent<CinemachineVirtualCamera>().Follow = fork.transform;
		startbot.GetComponent<ChangeRobot>().currbot = fork;
		fork.GetComponent<Animator>().SetBool("IsActive", true);
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
