using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
	public Image HealthBar;
	public GameObject Robot;
	public GameObject StartBot;
	public Image On;
	public Image mask;
    float originalSize;
	public int maxHealth = 5;
	public int currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        originalSize = mask.rectTransform.rect.width;
		currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
		if (StartBot.GetComponent<ChangeRobot>().currbot != Robot)
		{
			On.GetComponent<Image>().enabled = false;
		}
		else
		{
			On.GetComponent<Image>().enabled = true;
		}
        Vector2 HealthPos = Camera.main.WorldToScreenPoint(new Vector2 (Robot.transform.position.x, Robot.transform.position.y + 0.8f));
		HealthBar.transform.position = HealthPos;
		
    }
	public void SetValue(float value)
    {				      
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
	public void ChangeHealth(int amount)
	{
		currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
		mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * (currentHealth / (float)maxHealth));
	}
}
