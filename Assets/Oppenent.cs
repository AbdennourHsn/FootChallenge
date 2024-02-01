using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oppenent : MonoBehaviour
{
    private AnimationManager animManager;
    public Ball ball;
    public LeftFoot leftFoot;
    public PlayerController player;
    private bool shooted;
    void Start()
    {
        animManager = this.GetComponent<AnimationManager>();
    }

    public void MoveTo(Vector3 pos, float time = 3f)
    {
        CancelMoving();
        shooted = false;
        if (transform.position.x < pos.x)
        {
            animManager.MoveRight();
        }
        else animManager.MoveLeft();
        StartCoroutine(MoveLerping(pos, time));
    }

    private IEnumerator MoveLerping(Vector3 pos, float time)
    {
        float smooth = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = new Vector3(pos.x, transform.position.y, transform.position.z);

        while (smooth < 1f)
        {
            smooth += Time.deltaTime * time;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, smooth);
            yield return null;
        }
        animManager.Idle();
    }

    private void CancelMoving()
    {
        StopAllCoroutines();
    }

    public void ShootBall( float force=10)
    {
        shooted = true;
        animManager.PasseLeft();
    }
    private void Update()
    {
        Ray ray = new Ray(transform.position, ball.transform.position - transform.position);

        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 4))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                if (shooted) return;
                ShootBall();
            }
        }
    }
}
