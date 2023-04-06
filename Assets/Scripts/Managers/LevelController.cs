using PlayFab.ClientModels;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	public static int currentlevel = 1;
	public static float mazeScale = 10f;
	public static int mazeSize = 5;
	public static int nextlevel = 2;
	public static string lastTime = "00:00";
	public static List<string> timeList = new List<string>()
	{
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00"
	};
	public static List<string> goldList = new List<string>()
	{
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00"
	};
	public static List<string> silverList = new List<string>()
	{
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00"
	};
	public static List<string> bronzeList = new List<string>()
	{
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00",
		"00:00"
	};
}
