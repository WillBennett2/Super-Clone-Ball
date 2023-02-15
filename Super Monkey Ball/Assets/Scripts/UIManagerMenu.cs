using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManagerMenu : MonoBehaviour
{
    [HideInInspector] public static UIManager instance;

    [Header("UI Text")]
    public Text Title;

    [Header("Info Screen")]
    private bool m_OnInfo= false;
    public GameObject m_InfoScreen;
    public Button m_Back;

    [Header("Pause Screen")]
    public GameObject m_PauseScreen;
    public Button m_Resume;
    public AudioSource m_Select;
    public Button m_ExitButton;


    [Header("Info for Text")]
    public bool m_Paused = false;

    [Header("Player Object")]
    public GameObject m_Player;
    public bool m_OnMenu;


    [Header("Info for Times")]
    public bool m_InLevelSelect = false;
    public GameObject m_TimeSB;
    private string m_BestTimeSB;
    public GameObject m_TimeL1;
    private string m_BestTime01;
    public GameObject m_TimeL2;
    private string m_BestTime02;
    public GameObject m_TimeL3;
    private string m_BestTime03;

    private void Start()
    {
        m_BestTimeSB = PlayerPrefs.GetFloat("Time0").ToString();
        m_TimeSB.GetComponent<TMPro.TextMeshPro>().text = m_BestTimeSB;

        m_BestTime01 = PlayerPrefs.GetFloat("Time1").ToString();
        m_TimeL1.GetComponent<TMPro.TextMeshPro>().text = m_BestTime01;

        m_BestTime02 = PlayerPrefs.GetFloat("Time2").ToString();
        m_TimeL2.GetComponent<TMPro.TextMeshPro>().text = m_BestTime02;

        m_BestTime03 = PlayerPrefs.GetFloat("Time3").ToString();
        m_TimeL3.GetComponent<TMPro.TextMeshPro>().text = m_BestTime03;

    }

    // Update is called once per frame
    void Update()
    { 
        if (!m_Paused)
        {
            if (Input.GetButtonDown("Pause"))
            {
                Pause();
                CloseInfo();
                
            }
        }
        else
        {
            if (Input.GetButtonDown("Resume"))
            {
                //Resume();
            }

        }

        if (m_Player.transform.position.y <= -5f)
        {
            //m_FalloutText.gameObject.SetActive(true);
            StartCoroutine(GameManager.Sleep());
            if (GameManager.m_Reset)
                GameManager.ResetLevel();
        }
    }
    public void Pause()
    {

        m_Paused = true;
        m_Select.Play();
        //m_PauseScreen.SetActive(true);
        Time.timeScale = 0.0f;
        if (m_OnMenu)
            Time.timeScale = 1.0f;
    }
    public void Resume()
    {
        m_Paused = false;
        m_Select.Play();
        //m_PauseScreen.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void OpenInfo()
    {
        m_OnInfo = true;
        m_InfoScreen.SetActive(true);
    }
    public void CloseInfo()
    {
        m_OnInfo = false;
        m_InfoScreen.SetActive(false);
        //GameManager.ResetLevel();
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
