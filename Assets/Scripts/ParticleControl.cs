using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleControl : MonoBehaviour
{
	public GameObject startbot;
	public GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        particle.transform.position = new Vector3 (startbot.GetComponent<ChangeRobot>().currbot.transform.position.x, startbot.GetComponent<ChangeRobot>().currbot.transform.position.y + 0.6f, -0.8f);
    }
}
