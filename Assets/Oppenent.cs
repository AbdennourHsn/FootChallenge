using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum Foot
{
    right,
    left
}
public class Oppenent : MonoBehaviour
{
    private AnimationManager animManager;
    public Ball ball;
    private Vector3 initialPos;
    public OppenentFoot leftFoot;
    public OppenentFoot rightFoot;
    public Transform obstacle;
    private Foot footShoot = Foot.left;
    public PlayerController player;
    private bool shooted;


    private void OnEnable()
    {
        GameManager.PlayerWonEvent += Lose;
        GameManager.PlayerLoseEvent += Win;
        GameManager.ResetEvent += ResetOppenent;
    }

    private void OnDisable()
    {
        GameManager.PlayerWonEvent -= Lose;
        GameManager.PlayerLoseEvent -= Win;
        GameManager.ResetEvent -= ResetOppenent;
    }

    void Start()
    {
        initialPos = transform.position;
        animManager = this.GetComponent<AnimationManager>();
    }

    public void Move(Vector3 target , float force)
    {
        MoveTo(target);
    }

    public void MoveTo(Vector3 pos, float time = 3f)
    {
        CancelMoving();
        shooted = false;
        if (transform.position.x < pos.x)
        {
            animManager.MoveRight();
            footShoot = Foot.right;
        }
        else
        {
            animManager.MoveLeft();
            footShoot = Foot.left;
        }
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

    private void setFoot(Foot foot)
    {
        bool right = foot == Foot.right;
        rightFoot.gameObject.SetActive(!right);
        leftFoot.gameObject.SetActive(right);
    }

    public void ShootBall(float force=10)
    {
        //print("Oppenet shoot");
        shooted = true;
        setFoot(footShoot);
        if (footShoot == Foot.left)
        {
            animManager.PasseLeft();
        }
        else
            animManager.PasseRight();
    }
    private void Update()
    {
        transform.LookAt(new Vector3(obstacle.position.x, transform.position.y, obstacle.position.z));

        Ray ray = new Ray(transform.position, ball.transform.position - transform.position);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 2))
        {
            if (hit.collider.CompareTag("Ball"))
            {
                if (shooted) return;
                if(ball.CanShoot()) ShootBall();
            }
        }
    }

    private void Win()
    {
        animManager.Victory();
        rightFoot.gameObject.SetActive(false);
        leftFoot.gameObject.SetActive(false);
    }

    private void Lose()
    {
        animManager.Lose();
        rightFoot.gameObject.SetActive(false);
        leftFoot.gameObject.SetActive(false);
    }


    public void ResetOppenent()
    {
        leftFoot.gameObject.SetActive(true);
        transform.position = initialPos;
        this.animManager.Idle();
    }
}
