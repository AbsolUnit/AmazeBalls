using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelAvailability : MonoBehaviour
{
    public List<GameObject> levels;

	private void Start()
	{
		for (int i = 0; i < LevelController.nextlevel; i++)
		{
			GetChild(levels[i], "Image (" + (i + 1) + ")").SetActive(false);
			levels[i].GetComponent<Button>().interactable = true;
		}

		//for (int i = 0; i < LevelController.currentlevel - 1; i++)
		//{
		//	if (i == LevelController.currentlevel - 1)
		//	{
		//		GetChild(levels[i], "Time (" + (i + 1) + ")").GetComponent<TextMeshProUGUI>().text = LevelController.lastTime;
		//	}
		//}

		for (int j = 0; j < LevelController.currentlevel; j++){
			GetChild(levels[j], "Time (" + (j + 1) + ")").GetComponent<TextMeshProUGUI>().text = LevelController.timeList[j];
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
