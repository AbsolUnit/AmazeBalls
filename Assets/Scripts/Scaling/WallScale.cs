using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScale : MonoBehaviour
{

    public GameObject maze;
    public int[] pos;
    private float scale;
    private float size;
    private float posOffset;
    private bool scaling;
    private bool temp = true;
    private Vector3 playerPos;
    private float x;
    private float z;
    private RigidbodyConstraints constraints;
    private Vector3 posGoal = new Vector3();

    void Update()
    {
        scale = maze.GetComponent<MazeGen>().scale;
        size = maze.GetComponent<MazeGen>().size;
        scaling = maze.GetComponent<MazeGen>().scaling;
        posOffset = ((scale * size) / 2) - scale / 2;
        if (gameObject.tag == "hWall")
		{
            transform.localScale = new Vector3(scale * 1.25f, scale / 2, scale / 4);
            transform.position = new Vector3((pos[0] * scale) - posOffset, 0.5f, (pos[1] * scale - scale / 2) - posOffset);
        }
        else if (gameObject.tag == "vWall")
		{
            transform.localScale = new Vector3(scale * 1.25f, scale / 2, scale / 4);
            transform.position = new Vector3((pos[0] * scale - scale / 2) - posOffset, 0.5f, (pos[1] * scale) - posOffset);
        }
        else if (gameObject.tag == "Floor")
		{
            transform.localScale = new Vector3(size * scale, 1, size * scale);
        }
        else if (gameObject.tag == "Start")
		{
            transform.localScale = new Vector3(scale - (scale / 4) - 0.2f, 0.1f, scale - (scale / 4) - 0.2f);
            transform.position = new Vector3(-posOffset, -0.4f, -posOffset);
        }
        else if (gameObject.tag == "End")
		{
            transform.localScale = new Vector3(scale - (scale / 4) - 0.2f, 0.1f, scale - (scale / 4) - 0.2f);
            transform.position = new Vector3(posOffset, -0.4f, posOffset);
        }
        else if (gameObject.tag == "Player")
		{
            if (temp && scaling)
			{
                playerPos = transform.position;
                x = playerPos.x / scale;
                z = playerPos.z / scale;
                constraints = gameObject.GetComponent<Rigidbody>().constraints;
                transform.position = new Vector3(playerPos.x, 1, playerPos.z);
                gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                temp = false;
			}
            if (scaling && !temp)
			{
                posGoal = new Vector3(x * scale, 1, z * scale);
                transform.position = Vector3.Slerp(transform.position, posGoal, 0.1f);
			}
            if (!scaling)
			{
                gameObject.GetComponent<Rigidbody>().constraints = constraints;
                temp = true;
			}
		}
        else if (gameObject.tag == "ScaleCollect")
		{
            if (temp && scaling)
            {
                playerPos = transform.position;
                x = playerPos.x / scale;
                z = playerPos.z / scale;
                transform.position = new Vector3(playerPos.x, 1, playerPos.z);
                temp = false;
            }
            if (scaling && !temp)
            {
                transform.position = new Vector3(x * scale, 1, z * scale);
            }
            if (!scaling)
            {
                temp = true;
            }
        }
        
    }
}
