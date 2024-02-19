using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppenentFoot : MonoBehaviour
{
    private Vector3 playerInitialPos;
    private float rangeOfTarget = 0.4f;
    [SerializeField]
    private Transform obstacleTarget;
    private void Start()
    {
        playerInitialPos = FindObjectOfType<PlayerController>().transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Vector3 target = new Vector3(playerInitialPos.x +
                Random.Range(-rangeOfTarget, rangeOfTarget), 0, playerInitialPos.z-0.5f);
            if(GameManager._instance.oppenentLevel==OppenentLevel.medium)
                other.GetComponent<Ball>().Shoot<Oppenent>(target , target);
            else
                other.GetComponent<Ball>().Shoot<Oppenent>(ExpertShoot(), ExpertShoot());
        }
    }

    private Vector3 ExpertShoot()
    {
        Vector3 obstacle = obstacleTarget.position;
        Vector3 direction= InputManager.GetDirection(transform.position, obstacle + Vector3.right*Random.Range(-0.3f, 0.3f));
        return InputManager.GetExtendedEndPos(transform.position, playerInitialPos.z, direction);
    }
}
