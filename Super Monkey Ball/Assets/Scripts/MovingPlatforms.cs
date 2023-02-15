using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [Header("Info")]
    public bool m_CanSpin = false;
    public bool m_CanMove = false;
    public Vector3 m_SpinSpeed = new Vector3(0f, 0f, 0f);
    public float m_MoveSpeed = 0f;
    public Vector3 m_StartPos;
    public Vector3 m_EndPos;
    public bool m_AtEndPoint = false;


    void Start()
    {
        m_StartPos = transform.position;
    }
    private void Awake()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(m_CanSpin)
        {
            transform.Rotate(m_SpinSpeed.x*Time.deltaTime, m_SpinSpeed.y * Time.deltaTime, m_SpinSpeed.z * Time.deltaTime, Space.Self);
            //transform.rotation = Quaternion.Euler(transform.rotation.x + m_SpinSpeed, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        }

        if(m_CanMove)
        {
            if(transform.position != m_EndPos && m_AtEndPoint == false)
            {

                transform.position = Vector3.MoveTowards(transform.position,m_EndPos,m_MoveSpeed * Time.deltaTime);
            }
            else
            {
                m_AtEndPoint = true;
            }

            if(transform.position != m_StartPos && m_AtEndPoint == true)
            {
                transform.position = Vector3.MoveTowards(transform.position, m_StartPos, m_MoveSpeed*Time.deltaTime);
            }
            else
            {
                m_AtEndPoint = false;
            }
        }
    }
}
