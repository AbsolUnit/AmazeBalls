using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using Newtonsoft.Json;

public class SaveData : MonoBehaviour
{
    public Event playFabEvent;

    // Start is called before the first frame update
    void Start()
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Current", LevelController.currentlevel.ToString()},
                {"Next", LevelController.nextlevel.ToString()},
                {"LastT", LevelController.lastTime},
                {"ListT", JsonConvert.SerializeObject(LevelController.timeList)}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnSuccess, OnFailure);
    }

    void OnSuccess(UpdateUserDataResult obj)
	{
        Debug.Log("Successfully Saved!");
		var epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
		double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
        playFabEvent.SetName("SessionLen");
        playFabEvent.RecordEvent((timestamp - LevelController.sessionStart).ToString());
		playFabEvent.SetName("QuitStage");
		playFabEvent.RecordEvent((LevelController.currentlevel).ToString());
		Application.Quit();
	}

    void OnFailure(PlayFabError obj)
	{
        Debug.Log("Saving Failed!");
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
