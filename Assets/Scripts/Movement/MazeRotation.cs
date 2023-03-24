using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeRotation : MonoBehaviour
{
    public float turnSpeed;
    private Quaternion rotaionGoal;
    public GameObject maze;
    private Vector3 middle;
    public float clamp = 45;


    // Start is called before the first frame update
    void Start()
    {
        float size = maze.GetComponent<MazeGen>().scale * maze.GetComponent<MazeGen>().size;
        float offset = (size / 2) - 2;
        middle = new Vector3(offset, 0, offset);
        rotaionGoal = Quaternion.Euler(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0.2f)
		{
            rotaionGoal *= Quaternion.Euler(0, 0, -1);
		}
        else if (Input.GetAxis("Horizontal") < -0.2f)
        {
            rotaionGoal *= Quaternion.Euler(0, 0, 1);
        }

        if (Input.GetAxis("Vertical") > 0.2f)
        {
            rotaionGoal *= Quaternion.Euler(1, 0, 0);
        }
        else if (Input.GetAxis("Vertical") < -0.2f)
        {
            rotaionGoal *= Quaternion.Euler(-1, 0, 0);
        }

        Vector3 temp = rotaionGoal.eulerAngles;
        if (temp.z > clamp && temp.z < 180)
		{
            temp.z = Mathf.Clamp(temp.z, 0, clamp);
        }else if (temp.z > 180 && temp.z < 360 - clamp)
		{
            temp.z = Mathf.Clamp(temp.z, 360 - clamp, 360);
        }

        if (temp.x > clamp && temp.x < 180)
        {
            temp.x = Mathf.Clamp(temp.x, 0, clamp);
        }
        else if (temp.x > 180 && temp.x < 360 - clamp)
        {
            temp.x = Mathf.Clamp(temp.x, 360 - clamp, 360);
        }
        temp.y = 0;
        rotaionGoal = Quaternion.Euler(temp);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotaionGoal, turnSpeed);
    }
}
