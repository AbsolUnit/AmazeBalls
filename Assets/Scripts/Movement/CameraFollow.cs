using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject ball;
    public float moveSpeed = 0.5f;
    private Vector3 posGoal;

    // Start is called before the first frame update
    void Update()
    {
        posGoal = new Vector3(ball.transform.position.x, ball.transform.position.y + 20, ball.transform.position.z);
        transform.position = Vector3.Slerp(transform.position, posGoal, moveSpeed);
        //transform.rotation = Quaternion.Euler(45, 0, 0);
    }
}
