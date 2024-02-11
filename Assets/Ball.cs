using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Vector3 initialPos;
    public Rigidbody body;

    public bool isShooted;
    private Vector3 shootPoint;
    private Vector3 shootTarget;
    [SerializeField] AnimationCurve curve;
    private float smooth;
    private float t;

    private bool rotate;
    private Transform SPIN;
    public float rotationSpeed=2;
    private int rotateDirection=1;

    private Type lastShooter { get;  set; }
    private bool missed;
    private bool outside;

    private void OnEnable()
    {
        GameManager.ResetEvent += ResetBall;
    }

    private void OnDisable()
    {
        GameManager.ResetEvent -= ResetBall;
    }

    void Start()
    {
        initialPos = transform.position;
        SPIN = this.transform.GetChild(0);
    }

    private void Update()
    {
        if(rotate) SPIN.Rotate(Vector3.right* rotationSpeed * rotateDirection) ;
    }

    private void FixedUpdate()
    {
        if (missed) return;
        if (isShooted)
        {
            if (smooth < 1)
            {
                t += Time.deltaTime / GameManager._instance.speedDuration;
                smooth = curve.Evaluate(t);
                transform.position = Vector3.Lerp(shootPoint, shootTarget, smooth);
            }
            else if(!outside)
            {
                missed = true;
                rotate = false;
                if (lastShooter == typeof(Oppenent))
                {
                    GameManager._instance.PlayerLose_();
                }
                else
                {
                    GameManager._instance.PlayerWon_();

                }
            }
        }
    }

    public void Shoot<T>(Vector3 targetBall , Vector3 targetPlayer )
    {
        rotate = true;
        isShooted = false;
        t = 0;
        smooth = 0;
        shootPoint = transform.position;
        shootTarget = new Vector3(targetBall.x, transform.position.y, targetBall.z );
        isShooted = true;
        lastShooter = typeof(T);
        if (lastShooter == typeof(Oppenent)) {
            FindObjectOfType<PlayerController>().MoveTo(targetBall);
            this.rotateDirection = -1;
        }
        else if (lastShooter == typeof(PlayerController))
        {
            this.rotateDirection = 1;
            GameManager._instance.Passe();
            FindObjectOfType<Oppenent>().MoveTo(targetPlayer);
        }

    }

    public void StopBall()
    {
        body.isKinematic = true;
        body.velocity = Vector3.zero;
        body.isKinematic = false;
    }

    public void Stop()
    {
        rotate = false;
        isShooted = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (missed) return;
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.GetComponentInParent<Obstacles>().OnObstacleItemTouched();
            collision.transform.GetComponent<Rigidbody>().AddForce(Vector3.forward*2, ForceMode.Impulse);
            Stop();
            missed = true;
            ObstacleTouched();
        }

       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ObstacleWall"))
        {
            outside = true;
            ObstacleTouched();
        }
        if (other.gameObject.CompareTag("Coin") && lastShooter == typeof(PlayerController))
        {
            GameManager._instance.GetCoin(other.transform.position);
            FindObjectOfType<GenerateCoin>().CoibCollected();
            print("Coin");
            Destroy(other.gameObject);
        }
    }

    private void ObstacleTouched()
    {
        if (lastShooter == typeof(Oppenent))
        {
            GameManager._instance.PlayerWon_();
        }
        else
        {
            GameManager._instance.PlayerLose_();
        }
    }

    public void ResetBall()
    {
        this.transform.position = initialPos;
        isShooted = false;
        t = 0;
        smooth = 0;
        missed = false;
        outside = false;
    }

    public bool CanShoot() => (!missed && !outside);
}
