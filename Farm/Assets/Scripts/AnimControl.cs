using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{

    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            anim.Play("jumping");
        }
        if (Input.GetKeyDown("w"))
        {
            anim.Play("running");
        }
        if (Input.GetKeyDown("e"))
        {
            anim.Play("resting");
        }
    }
}
