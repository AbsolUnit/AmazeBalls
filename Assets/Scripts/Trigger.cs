using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
	public GameManager gm;

	private void Awake()
	{
		gm = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (gameObject.CompareTag("Start"))
			{
				gm.go = true;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			if (gameObject.CompareTag("End"))
			{
				gm.go = false;
				gm.finished = true;
			}
		}
	}
}
