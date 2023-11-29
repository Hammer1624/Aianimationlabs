using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("Blend", 0.0f);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            animator.SetFloat("Blend", 0.0f);
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetFloat("Blend", 1f);
        }
    }
}
