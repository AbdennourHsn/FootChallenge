using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private AnimationManager animManager;
    public PlayerFoot leftFoot;
    public PlayerFoot rightFoot;
    private Vector3 initialPos;
    private Foot footShoot = Foot.left;
    private float smouth=0;
    public Transform obstacle;

    private void OnEnable()
    {
        InputManager.SwipeDone +=ShootBall;
        GameManager.PlayerWonEvent += Win;
        GameManager.PlayerLoseEvent += Lose;
        GameManager.ResetEvent += ResetPlayer;
    }

    private void OnDisable()
    {
        InputManager.SwipeDone -=ShootBall;
        GameManager.PlayerWonEvent -= Win;
        GameManager.PlayerLoseEvent -= Lose;
        GameManager.ResetEvent -= ResetPlayer;
    }

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        animManager = this.GetComponent<AnimationManager>();
    }

    public void MoveTo(Vector3 pos , float time=3f)
    {
        CancelMoving();
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

    private IEnumerator MoveLerping(Vector3 pos, float time )
    {
        float smooth = 0f;
        Vector3 initialPosition = transform.position;
        Vector3 targetPosition = new Vector3(pos.x, transform.position.y, transform.position.z);
        while (smooth < 1f)
        {
            smooth += Time.deltaTime*time;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, smooth);
            yield return null;
        }
        animManager.Idle();
    }

    private void CancelMoving()
    {
        StopAllCoroutines();
        smouth = 0;
    }


    public void ShootBall(Vector3 targetball, Vector3 targetPlayer , float force)
    {
        if (footShoot == Foot.left)
        {
            animManager.PasseLeft();
            leftFoot.SetParameters(targetball, targetPlayer, force);

        }
        else
        {
            animManager.PasseRight();
            rightFoot.SetParameters(targetball, targetPlayer, force);
        }
    }

    private void Update()
    {
        transform.LookAt(new Vector3(obstacle.position.x, transform.position.y, obstacle.position.z));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReloadCurrentScene();
        }
    }

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void Win()
    {
        leftFoot.gameObject.SetActive(false);
        rightFoot.gameObject.SetActive(false);
        animManager.Victory();
    }

    private void Lose()
    {
        leftFoot.gameObject.SetActive(false);
        rightFoot.gameObject.SetActive(false);
        animManager.Lose();
    }

    public void ActivateShootLeft() => this.leftFoot.gameObject.SetActive(true);

    public void DisactivateShootLeft() => this.leftFoot.gameObject.SetActive(false);


    public void ActivateShootRight() => this.rightFoot.gameObject.SetActive(true);

    public void DisactivateShootRight() => this.rightFoot.gameObject.SetActive(false);

    public void ResetPlayer()
    {
        transform.position = this.initialPos;
        this.animManager.Idle();
    }
}
