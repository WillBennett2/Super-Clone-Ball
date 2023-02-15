using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gateways : MonoBehaviour
{
    //public GameObject m_PlayerObject;
    //public Vector3 m_StartingPos;
    [Header ("Menu Info")]
    public bool m_Locked;
    public Transform m_Lock;
    public bool m_OnMenu;
    public bool m_IsOptions = false;
    public UIManagerMenu MenuUIScript;
    public bool m_IsPlay = false;
    public bool m_IsExit = false;
    public AudioSource m_Select;

    [Header("Level Info Info")]
    public bool m_InLevel;
    public bool m_LevelComplete;
    public float m_MoveUpSpeed = 1f;
    public GameManager m_GameManagerScript;
    public string m_NextSceneName;
    public int m_LevelNum;
    public float m_timer = 3f;
    public UIManager m_UIScript;
    public float m_FinalTime;


    void Start()
    {
        //transform.position = m_StartingPos;
        m_LevelComplete = false;
        m_Lock = gameObject.transform.Find("Lock");
        if(PlayerPrefs.GetInt("Level")>=m_LevelNum)
        {
            m_Locked = false;
        }
    }


    // Update is called once per frame
    void Update()
    {
        m_Lock.gameObject.SetActive(m_Locked);
        if (m_LevelComplete)
        {
           
            PlayerMovement.m_RB.AddForce(Vector3.up * (m_MoveUpSpeed * Time.deltaTime), ForceMode.Force);
            //GameManager.Sleep();
            m_timer -= Time.deltaTime;
            if (m_timer <= 0)
            {
                if (!m_OnMenu)
                {
                    // check if time is better than previous time.
                    m_FinalTime =(m_UIScript.m_Seconds + (float)m_UIScript.m_MilliSeconds/100);
                    if (m_FinalTime > PlayerPrefs.GetFloat("Time"+m_LevelNum))
                    {
                        PlayerPrefs.SetFloat("Time" + m_LevelNum, m_FinalTime);
                    }
                    m_GameManagerScript.IncreaseLevel();
                }
                else if(m_IsExit)
                {
                    Application.Quit();
                }
                m_GameManagerScript.LoadNextLevel(m_NextSceneName);
            }
        }


  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_OnMenu)
        {
            if (m_IsOptions)
            {
                MenuUIScript.OpenInfo();
                m_Select.Play();
            }
            else
            {
                m_LevelComplete = true;
            }

        }
        else
        {
            if (other.CompareTag("Player"))
            {
                //PlayerMovement.m_CanMove = false;
                m_LevelComplete = true;

            }
        }
    }
}
