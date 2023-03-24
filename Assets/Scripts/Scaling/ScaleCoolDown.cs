using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ScaleCoolDown : MonoBehaviour
{
	private float scaleGoal;
	private float timeStamp;
	[SerializeField] private float timeGoal;
	private bool scaleUp;
	public float delta;
	public bool started;
	private GameManager gm;
	private Timer timer;
	public MazeGen maze;


	// Start is called before the first frame update
	void Start()
    {
		gm = gameObject.GetComponent<GameManager>();
		timer = gm.GetComponent<Timer>();
		scaleGoal = 10f;
	}

    // Update is called once per frame
    void Update()
    {
		if (started)
		{
			delta = timer.time - timeStamp;
			if (delta >= timeGoal)
			{
				scaleUp = true;
				maze.scaling = true;
				gm.go = false;
				started = false;
			}
		}
		else if (scaleUp)
		{
			maze.scale = Mathf.Lerp(maze.scale, scaleGoal, 0.015f);
			if (Mathf.Abs(maze.scale - scaleGoal) - 0.1 <= 0 && maze.scaling)
			{
				maze.scale = scaleGoal;
				maze.scaling = false;
				gm.go = true;
				timeGoal = 0;
				scaleUp = false;
			}
		}
	}

	public void StartCoolDown(float startTime)
	{
		timeStamp = startTime;
		started = true;
	}

	public void AddTime(float time)
	{
		timeGoal += time;
	}
}
