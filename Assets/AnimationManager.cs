using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    public void PlayAnimation(string name ,float trasactionSpeed=0.1f)
    {
        this.animator.Play(name, 0, trasactionSpeed);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            PlayAnimation("Soccer Pass Right Leg");
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            PlayAnimation("Soccer Pass Left Leg");
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayAnimation("Victory");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            PlayAnimation("Defeat");
        }
    }
}
