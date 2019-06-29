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
    //Start is called before the first frame update
    void Start()
    {
        if (robot.name != "StartingCharacter")
		{
			robot.GetComponent<PlatformerCharacter2D>().enabled = false;
			robot.GetComponent<Platformer2DUserControl>().enabled = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnMouseDown()
	{
		currbot.GetComponent<PlatformerCharacter2D>().enabled = false;
		currbot.GetComponent<Platformer2DUserControl>().enabled = false;
		robot.GetComponent<PlatformerCharacter2D>().enabled = true;
		robot.GetComponent<Platformer2DUserControl>().enabled = true;
		VCAM.GetComponent<CinemachineVirtualCamera>().Follow = robot.transform;
		startbot.GetComponent<ChangeRobot>().currbot = robot;
		
	}
}
