using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 initialPos;
    public Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            this.transform.position = initialPos;
        }
    }

    public void StopBall()
    {
        print("touched");
        body.isKinematic = true;
        body.velocity = Vector3.zero;
        body.isKinematic = false;

    }
}
