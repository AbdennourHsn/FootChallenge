using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class PlayerController : MonoBehaviour
{
    private AnimationManager animManager;
    public LeftFoot leftFoot;

    private float smouth=0;
    // Start is called before the first frame update
    void Start()
    {
        animManager = this.GetComponent<AnimationManager>();
    }

    public void MoveTo(Vector3 pos , float time=3f)
    {
        CancelMoving();
        if (transform.position.x < pos.x)
        {
            animManager.MoveRight();
        }
        else animManager.MoveLeft();
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

    public void ShootBall(Vector3 directiom , float force)
    {
        leftFoot.SetParameters(directiom, force);
        animManager.PasseLeft();
    }
}
