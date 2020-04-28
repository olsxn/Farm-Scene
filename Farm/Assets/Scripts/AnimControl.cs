using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControl : MonoBehaviour
{

    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.Play("jumping");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.CrossFade("running", 0.5f);
        }
        if (Input.GetKeyUp(KeyCode.W)) {
            anim.CrossFade("resting", 0.2f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("resting");
        }
    }
}
