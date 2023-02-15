using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockBounce : MonoBehaviour
{
    private Animator m_Animator;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }


    // Update is called once per frame
    void Update()
    {
        m_Animator.SetTrigger("Bounce");
    }
}
