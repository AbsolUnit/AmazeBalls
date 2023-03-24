using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCollect : MonoBehaviour
{
	public MazeGen maze;
	public Timer timer;
	public ScaleCoolDown coolDown;
	public GameManager gm;
	private float scaleGoal;
	private bool collided;

	private void Start()
	{
		scaleGoal = 4f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			collided = true;
			maze.scaling = true;
			gm.go = false;
			gameObject.GetComponent<MeshRenderer>().enabled = false;
			gameObject.GetComponent<Collider>().enabled = false;
		}
	}

	private void Update()
	{
		if (collided)
		{
			maze.scale = Mathf.Lerp(maze.scale, scaleGoal, 0.015f);
			if (Mathf.Abs(maze.scale - scaleGoal) - 0.1 <= 0 && maze.scaling)
			{
				maze.scale = scaleGoal;
				maze.scaling = false;
				gm.go = true;
				if (!coolDown.started)
				{
					coolDown.StartCoolDown(timer.time);
				}
				coolDown.AddTime(10f);
				Destroy(gameObject);
			}
		}
	}
}
