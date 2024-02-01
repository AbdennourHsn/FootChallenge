using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppenentFoot : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            PlayerController player = FindObjectOfType<PlayerController>();
            other.GetComponent<Ball>().StopBall();
            other.GetComponent<Rigidbody>().AddForce(-(player.transform.position-transform.position).normalized * 17, ForceMode.Impulse);
            //player.MoveTo(other.transform.position);
        }
    }
}
