using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string name ,float trasactionSpeed=0.1f)
    {
        this.animator.Play(name, 0, trasactionSpeed);
    }

    public void MoveLeft()
    {
        PlayAnimation("Move Left");
    }

    public void MoveRight()
    {
        PlayAnimation("Move Right");
    }

    public void PasseLeft()
    {
        PlayAnimation("Soccer Pass Left Leg");
    }

    public void PasseRight()
    {
        PlayAnimation("Soccer Pass Right Leg");
    }

    public void Idle()
    {
        PlayAnimation("Idle");
    }

    public void Victory()
    {
        PlayAnimation("Victory");
    }

    public void Lose()
    {
        PlayAnimation("Lose");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PasseLeft();
        }
    }
}
