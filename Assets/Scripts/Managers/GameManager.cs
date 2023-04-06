using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

public class GameManager : MonoBehaviour
{

    public bool go;
    public bool finished;
    private bool active;
    public GameObject maze;
    public GameObject pause;
    public GameObject complete;
    private DepthOfField dof;
    public VolumeProfile vol;
    public TextMeshProUGUI levelCount;
    public BallMove ball;
    public Timer timer;

    void Awake()
    {
        levelCount.text = "Level: " + LevelController.currentlevel.ToString();
        pause.SetActive(false);
        complete.SetActive(false);
        vol.TryGet<DepthOfField>(out dof);
        dof.mode.overrideState = false;
        var prefab = Instantiate(maze, new Vector3(0, 0, 0), Quaternion.identity);
        prefab.GetComponent<MazeGen>().scale = LevelController.mazeScale;
        prefab.GetComponent<MazeGen>().size = LevelController.mazeSize;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            active = !pause.activeSelf;
            pause.SetActive(active);
            Time.timeScale = Convert.ToInt32(!active);
            dof.mode.overrideState = active;
        }

        if (finished)
        {
            go = false;
            complete.SetActive(!active);
            dof.mode.overrideState = true;
            ball.active = false;
		}
    }

	public void LoadMenu()
	{
		UpdateTime();
		UpdateStats();
		Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
	}

    public void LoadNext()
    {
		UpdateTime();
		UpdateStats();
		Time.timeScale = 1;
        LevelGen(LevelController.currentlevel);
        SceneManager.LoadScene("Main");
    }

    public void QuitApp()
    {
        Debug.Log("Quit");
        UpdateTime();
        UpdateStats();
		SceneManager.LoadScene("Saving");
    }

    private void LevelGen(int level)
    {
        LevelController.currentlevel = level;
        LevelController.mazeScale = 10f;
        LevelController.mazeSize = 4 + level;
    }

    private void UpdateStats()
    {
		if (finished && LevelController.currentlevel == LevelController.nextlevel - 1)
		{
            LevelController.currentlevel = LevelController.nextlevel;
            LevelController.nextlevel++;
		}
	}

    private void UpdateTime()
    {
        if (finished && CheckTime())
        {
            LevelController.timeList[LevelController.currentlevel - 1] = timer.timerText;
        }
    }

    private bool CheckTime()
    {
        string oldS = LevelController.timeList[LevelController.currentlevel - 1];
        int oldI = int.Parse(oldS.Substring(0, 2)) + int.Parse(oldS.Substring(3, 2));
        string newS = timer.timerText;
		int newI = int.Parse(newS.Substring(0, 2)) + int.Parse(newS.Substring(3, 2));
        if (newI < oldI)
        {
            return true;
        }
		return false;
    }
}
