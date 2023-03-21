using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using System.Text;

public class Login : MonoBehaviour
{
	private SHA256 md5;
	private byte[] vs;

	[SerializeField] private GameObject popUp;

	void Awake()
	{
		popUp.SetActive(false);
		md5 = SHA256CryptoServiceProvider.Create();
		TryLogin();
	}

	public void TryLogin()
	{
		vs = Encoding.ASCII.GetBytes(SystemInfo.deviceUniqueIdentifier);
		md5.ComputeHash(vs);

		LoginWithCustomIDRequest request = new LoginWithCustomIDRequest
		{
			CustomId = ByteArrayToString(md5.Hash),
			CreateAccount = true
		};

		PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
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
		popUp.SetActive(true);
	}

	private void OnLoginSuccess(LoginResult obj)
	{
		SceneManager.LoadScene("MainMenu");
	}

}