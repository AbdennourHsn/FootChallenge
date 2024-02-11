using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftFoot : MonoBehaviour
{
    public Vector3 targetBall;
    public Vector3 targetPlayer;
    public float force;

    public void SetParameters(Vector3 target , Vector3 targetPlayer , float force)
    {
        this.targetBall = target;
        this.targetPlayer = targetPlayer;
        this.force = force;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.GetComponent<Ball>().Shoot<PlayerController>(this.targetBall , this.targetPlayer);
        }
    }
}
