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
            complete.SetActive(!active);
            dof.mode.overrideState = true;
            ball.active = false;
        }
    }

	public void LoadMenu()
	{
        if (finished)
		{
            LevelController.currentlevel = LevelController.nextlevel;
            LevelController.nextlevel++;
		}
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
	}

    public void LoadNext()
    {
        LevelController.currentlevel = LevelController.nextlevel;
        LevelController.nextlevel++;
        Time.timeScale = 1;
        LevelGen(LevelController.nextlevel);
        SceneManager.LoadScene("Main");
    }

    public void QuitApp()
	{
        Application.Quit();
        Debug.Log("Quit");
    }

    private void LevelGen(int level)
    {
        LevelController.currentlevel = level;
        LevelController.mazeScale = 4f;
        LevelController.mazeSize = 4 + level;
    }
}
