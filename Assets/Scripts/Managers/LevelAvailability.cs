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
			
			if (TimeVal(LevelController.timeList[i - 1]) > 0)
			{
				GetChild(GetChild(levels[i - 1], "StarContainer (" + i + ")"), "StarB").GetComponent<Image>().sprite = bronzeStar;
				if (CompTime(LevelController.timeList[i - 1], LevelController.silverList[i - 1]))
				{
					GetChild(GetChild(levels[i - 1], "StarContainer (" + i + ")"), "StarS").GetComponent<Image>().sprite = silverStar;
					if (CompTime(LevelController.timeList[i - 1], LevelController.goldList[i - 1]))
					{
						GetChild(GetChild(levels[i - 1], "StarContainer (" + i + ")"), "StarG").GetComponent<Image>().sprite = goldStar;
					}
				}
			}

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

	private bool CompTime(string a, string b)
	{
		int aI = TimeVal(a);
		int bI = TimeVal(b);
		if (aI <= bI)
		{
			return true;
		}
		return false;
	}

	private int TimeVal(string t)
	{
		int tI = int.Parse(t.Substring(0, 2)) * 60 + int.Parse(t.Substring(3, 2));
		return tI;
	}
}
