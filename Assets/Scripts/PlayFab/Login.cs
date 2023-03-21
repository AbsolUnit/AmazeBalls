using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using PlayFab;
using PlayFab.ClientModels;
using System;
using UnityEngine.SceneManagement;
using System.Text;

public class LoginAnon : MonoBehaviour
{
	//From https://docs.microsoft.com/en-us/gaming/playfab/features/authentication/login/login-basics-best-practices
	//Your game should use an anonymous login for creating a new account and linking new devices to an existing account. 
	//We recommend this because some players may abandon a game that asks for an e-mail or identifiable information.
	//However, once the anonymous login is complete, you should provide the option to add recoverable login credentials,
	//and provide some explanation regarding the benefits.
	void Awake()
	{
		SHA256 md5 = SHA256CryptoServiceProvider.Create();

		byte[] vs = Encoding.ASCII.GetBytes(SystemInfo.deviceUniqueIdentifier);
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
	}

	private void OnLoginSuccess(LoginResult obj)
	{
		SceneManager.LoadScene("MainMenu");
	}

}