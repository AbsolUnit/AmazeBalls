using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

	public void LoadScene(int level)
	{
		if(level == 0)
		{
			LevelGen(LevelController.currentlevel);
			SceneManager.LoadScene("Main");
		}
		else if (level == -1)
		{
			SceneManager.LoadScene("Levels");
		}
		else if (level == -2)
		{
			SceneManager.LoadScene("MainMenu");
		}
		else if (level == -3)
		{
			Debug.Log("Application Quit");
			SceneManager.LoadScene("Saving");
		}
		else
		{
			LevelGen(level);
			SceneManager.LoadScene("Main");
		}
	}

	private void LevelGen(int level)
	{
		LevelController.currentlevel = level;
		LevelController.mazeScale = 10f;
		LevelController.mazeSize = 4 + level;
	}
}
