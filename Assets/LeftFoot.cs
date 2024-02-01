using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFoot : MonoBehaviour
{
    public Vector3 shootDirection;
    public float force;

    public void SetParameters(Vector3 direction , float force)
    {
        shootDirection = direction;
        this.force = force;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {

            other.GetComponent<Rigidbody>().AddForce(shootDirection * 15 , ForceMode.Impulse);
        }
    }
}
