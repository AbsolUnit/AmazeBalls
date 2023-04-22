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
		"00:13",
		"00:15",
		"00:14",
		"00:20",
		"00:25",
		"00:22",
		"00:46",
		"00:25",
		"00:48",
		"00:47",
		"00:59",
		"00:52"
	};
	public static List<string> silverList = new List<string>()
	{
		"00:20",
		"00:22",
		"00:21",
		"00:26",
		"00:30",
		"00:25",
		"00:55",
		"00:30",
		"00:55",
		"00:54",
		"01:05",
		"01:00"
	};
	public static double sessionStart;
}
