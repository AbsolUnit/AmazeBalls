using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelAvailability : MonoBehaviour
{
    public List<GameObject> levels;
	public Sprite goldStar;
	public Sprite silverStar;
	public Sprite bronzeStar;

	private void Start()
	{
		for (int i = 1; i < LevelController.nextlevel; i++)
		{
			GetChild(levels[i - 1], "Image (" + i + ")").SetActive(false);
			GetChild(levels[i - 1], "Time (" + i + ")").GetComponent<TextMeshProUGUI>().text = LevelController.timeList[i - 1];
			GetChild(GetChild(levels[i - 1], "StarContainer"), "StarG (" + i + ")").GetComponent<Image>().sprite = goldStar;
			levels[i - 1].GetComponent<Button>().interactable = true;
		}
		
	}

	private GameObject GetChild(GameObject parent,string name)
	{
		foreach (Transform child in parent.transform)
		{
			if (child.name == name)
			{
				return child.gameObject;
			}
		}
		return null;
	}
}
