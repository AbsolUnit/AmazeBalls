using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using System.Text;
using Newtonsoft.Json;

public class Login : MonoBehaviour
{
	private SHA256 md5;
	private byte[] vs;

	[SerializeField] private GameObject popUp;
	[SerializeField] private GameObject PFevent;

	void Awake()
	{
		popUp.SetActive(false);
		md5 = SHA256CryptoServiceProvider.Create();
		TryLogin();
	}

	public void TryLogin()
	{
		popUp.SetActive(false);
		vs = Encoding.ASCII.GetBytes(SystemInfo.deviceUniqueIdentifier);
		md5.ComputeHash(vs);

		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
		{
			CustomId = ByteArrayToString(md5.Hash),
			CreateAccount = true
		};

		PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
	}

	public void QuitApp()
	{
		Application.Quit();
		Debug.Log("Application Quit");
	}

	private string ByteArrayToString(byte[] hash)
	{
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < hash.Length; i++)
		{
			sb.Append(hash[i]);
		}
		return sb.ToString();
	}

	private void OnLoginFailure(PlayFabError obj)
	{
		Debug.Log("Connection Failed");
		Debug.Log(obj);
		popUp.SetActive(true);
		PFevent.GetComponent<Event>().SetName("LoginFail");
		PFevent.GetComponent<Event>().RecordEvent("Login");
		
	}

	private void OnLoginSuccess(LoginResult obj)
	{
		GetUserData();
	}

	private void GetUserData()
	{
		PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataSuccess, OnDataFailure);
	}

	private void OnDataSuccess(GetUserDataResult result)
	{
		if (result.Data != null && result.Data.ContainsKey("Current") && result.Data.ContainsKey("Next") && result.Data.ContainsKey("LastT") && result.Data.ContainsKey("ListT"))
		{
			SetUserData(result.Data["Current"].Value, result.Data["Next"].Value, result.Data["LastT"].Value, result.Data["ListT"].Value);
		}
		else
		{
			Debug.Log("Plafab Data Incomplete");
		}
		var epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
		double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
		Debug.Log(timestamp);
		LevelController.sessionStart = timestamp;
		SceneManager.LoadScene("MainMenu");
	}

	private void OnDataFailure(PlayFabError obj)
	{
		Debug.Log("Data Failure!");
	}

	private void SetUserData(string current, string next, string lastT, string listT)
	{
		LevelController.timeList = JsonConvert.DeserializeObject<List<string>>(listT);
		LevelController.lastTime = lastT;
		LevelController.currentlevel = int.Parse(current);
		LevelController.nextlevel = int.Parse(next);
	}

}