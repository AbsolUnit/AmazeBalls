using PlayFab;
using PlayFab.ClientModels;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
	private string eventName;
	public void RecordEvent(string id)
	{
		Debug.Log("Event Registered");

		WriteClientPlayerEventRequest request = new WriteClientPlayerEventRequest();
		request.EventName = eventName;
		request.CustomTags = new Dictionary<string, string>
		{
			{ "ID", id }
		};

		PlayFabClientAPI.WritePlayerEvent(request, OnSuccess, OnError);
	}

	private void OnSuccess(WriteEventResponse obj)
	{
		Debug.Log("Event Recorded");
	}

	private void OnError(PlayFabError obj)
	{
		Debug.Log("Event Failed");
		Debug.Log(obj.ErrorMessage);
	}

	public void SetName(string name)
	{
		eventName = name;
	}
}