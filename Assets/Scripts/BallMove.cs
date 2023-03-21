using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour
{
    private Rigidbody rb;
    public bool active;
	[Range(1f,10f)]    
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        active = true;
        speed = 8f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (active)
		{
            rb.AddForce(Vector3.right * GetH());
            rb.AddForce(Vector3.forward * GetV());
        }
    }

    private float GetH()
	{
        float v = 0f;

        if (Input.GetAxis("Horizontal") > 0.2f)
        {
            v = speed;
        }
        else if (Input.GetAxis("Horizontal") < -0.2f)
        {
            v = -speed;
        }

        return v;
	}

    private float GetV()
    {
        float v = 0f;

        if (Input.GetAxis("Vertical") > 0.2f)
        {
            v = speed;
        }
        else if (Input.GetAxis("Vertical") < -0.2f)
        {
            v = -speed;
        }

        return v;
    }
}
