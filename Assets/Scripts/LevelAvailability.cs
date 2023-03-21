using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelAvailability : MonoBehaviour
{
    public List<GameObject> levels;

	private void Start()
	{
		for (int i = 0; i < LevelController.nextlevel; i++)
		{
			levels[i].SetActive(false);
			levels[i].GetComponentInParent<Button>().interactable = true;
		}
	}
}
