using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float time;
    private bool started;

    private int mins;
    private int secs;
    private int milSecs;

    private string minsText;
    private string secsText;
    private string milSecsText;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.go) started = true;
        else started = false;
        if (started) time += Time.deltaTime;

        mins = (int)Mathf.Abs(time / 60);
        secs = (int)Mathf.Abs(time - (mins * 60));
        milSecs = (int)Mathf.Abs((time - (mins * 60) - secs) * 100);

        if(mins < 10)
		{
            minsText = "0" + mins;
		}
		else
		{
            minsText = mins.ToString();
		}
        if (secs < 10)
        {
            secsText = "0" + secs;
        }
        else
        {
            secsText = secs.ToString();
        }
        if (milSecs < 10)
        {
            milSecsText = "0" + milSecs;
        }
        else
        {
            milSecsText = milSecs.ToString();
        }

        text.text = minsText + ":" + secsText; //+ ":" + milSecsText;
    }
}
