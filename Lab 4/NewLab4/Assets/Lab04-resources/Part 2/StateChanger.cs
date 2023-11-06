using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChanger : MonoBehaviour
{

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator.SetFloat("Blend", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
