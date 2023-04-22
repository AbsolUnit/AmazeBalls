using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;

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
    public GameObject noMoreText;
    public Button nextButt;
	public Image starS;
    public TextMeshProUGUI tGoalS;
	public Image starG;
	public TextMeshProUGUI tGoalG;
	public Sprite goldStar;
	public Sprite silverStar;
	public BallMove ball;
    public Timer timer;

    void Awake()
    {
		tGoalS.text = LevelController.silverList[LevelController.currentlevel - 1];
		tGoalG.text = LevelController.goldList[LevelController.currentlevel - 1];
		noMoreText.SetActive(false);
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
            if (LevelController.currentlevel >= 12)
            {
                noMoreText.SetActive(true);
                nextButt.interactable = false;
            }
            dof.mode.overrideState = true;
            ball.active = false;
            if (TimeVal(timer.timerText) <= TimeVal(LevelController.silverList[LevelController.currentlevel - 1]))
            {
                starS.sprite = silverStar;
<<<<<<< Updated upstream
				tGoalS.enabled = false;
			}
            else
            {
				tGoalS.enabled = true;
			}
			if (TimeVal(timer.timerText) <= TimeVal(LevelController.goldList[LevelController.currentlevel - 1]))
			{
				starG.sprite = goldStar;
                tGoalG.enabled = false;
			}
            else
            {
				tGoalG.enabled = true;
=======
				if (TimeVal(timer.timerText) <= TimeVal(LevelController.goldList[LevelController.currentlevel - 1]))
				{
					starG.sprite = goldStar;
				}
>>>>>>> Stashed changes
			}
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
        int oldI = TimeVal(oldS);
		string newS = timer.timerText;
		int newI = TimeVal(newS);
		if (newI > oldI && oldI != 0)
        {
            return false;
        }
		return true;
    }

	private int TimeVal(string t)
	{
		int tI = int.Parse(t.Substring(0, 2)) * 60 + int.Parse(t.Substring(3, 2));
		return tI;
	}
}
