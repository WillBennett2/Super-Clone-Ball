using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    [HideInInspector]public static UIManager instance;
    [Header ("UI Text")]
    public Text m_ScoreText;
    public Text m_SpeedText;
    public Text m_TimerSecondsText;
    public Text m_TimerMilliSecondsText;
    public Text m_FalloutText;
    public Text m_TimeoutText;

    [Header ("Pause Screen")]
    public GameObject m_PauseScreen;
    public Button m_Resume;
    public AudioSource m_Select;
    public Button m_ExitButton;

    [Header ("Info for Text")]
    public int m_Score = 0;
    public int m_MaxCollectables = 0;
    public float  m_Timer = 60f;
    public float m_Seconds;
    public int m_MilliSeconds;
    public bool m_Paused = false;
    public Gateways m_GatewayScript;
    //private bool m_Reset = false;

    [Header ("Player Object")]
    public GameObject m_Player;
    private Rigidbody m_PlayerRB;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        m_PlayerRB = m_Player.GetComponent<Rigidbody>();
        m_SpeedText.text = m_PlayerRB.velocity.magnitude.ToString();
        m_MaxCollectables = GameObject.FindGameObjectsWithTag("Collectable").Length;
        m_ScoreText.text = m_Score.ToString() + " / " + m_MaxCollectables.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Timer <= 0)
        {
            m_TimeoutText.gameObject.SetActive(true);
            StartCoroutine(GameManager.Sleep());
            StopCoroutine(GameManager.Sleep());
            if (GameManager.m_Reset)
                GameManager.ResetLevel();
        }
        else if(!m_GatewayScript.m_LevelComplete)
        {
            m_Timer -= (1 * Time.deltaTime);
        }
        
        

        if (m_Player.transform.position.y <= -5f)
        {
            m_FalloutText.gameObject.SetActive(true);
            StartCoroutine(GameManager.Sleep());
            StopCoroutine(GameManager.Sleep());
            if (GameManager.m_Reset)
                GameManager.ResetLevel();
        }

        if (!m_Paused)
        {
            if (Input.GetButtonDown("Pause"))
            {
                Pause();
            }
        }
        else
        {
            if (Input.GetButtonDown("Resume"))
            {
                Resume();
            }

        }
        m_MilliSeconds = (int)(m_Timer * 100);
        m_Seconds = (int)m_Timer;
        Math.DivRem(m_MilliSeconds,100 ,out m_MilliSeconds);
        //Debug.Log(m_MilliSeconds);
        m_TimerSecondsText.text = ((Mathf.Round(m_Seconds * 100)) / 100).ToString();
        m_TimerMilliSecondsText.text = ((Mathf.Round(m_MilliSeconds * 100)) / 100).ToString();
        m_SpeedText.text = ((float)Mathf.Round(m_PlayerRB.velocity.magnitude)).ToString() + " m/s";
    }
    public void IncreaseScore()
    {
        m_Score += 1;
        m_ScoreText.text = m_Score.ToString() + " / " + m_MaxCollectables.ToString();
    }
    public void Pause()
    {
        m_Paused = true;
        m_Select.Play();
        m_PauseScreen.SetActive(true);
        Time.timeScale =0.0f;
        
    }
    public void Resume()
    {
        m_Paused = false;
        m_Select.Play();
        m_PauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void LoadMenu()
    {
        m_Select.Play();
        SceneManager.LoadScene("Menu");
    }
    public void CLoseGame()
    {
        Application.Quit();
    }
}
