using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header ("Cameras")]
    public Camera m_MainCamera;
    public Camera m_FinCamera;

    [Header("World Info")]
    public Vector3 m_StartPos;
    public Gateways m_GatewayScript;


    [Header("PlayerInfo")]
    public GameObject m_Player;
    public int m_CurrentLevel;
    public int m_OldCurrentLevel;
    public bool m_ResetLevelCount = false;

    [HideInInspector]public static bool m_Reset = false;
    public static bool m_LoopLevel = false;

    // Start is called before the first frame update
    void Start()
    {
        m_CurrentLevel = PlayerPrefs.GetInt("Level");
        m_OldCurrentLevel = m_CurrentLevel;

        m_MainCamera.enabled = true;
        m_FinCamera.enabled = false;
        m_Player.transform.position = new Vector3(m_Player.transform.position.x, m_Player.transform.position.y + 10f, m_Player.transform.position.z) ;
    }

    void Update()
    {
        if(m_FinCamera == null)
        {
            
        }
        if (m_ResetLevelCount)
        {
            PlayerPrefs.SetInt("Level", 0);
        }
        SwitchCamera();
    }
    private void SwitchCamera()
    {
        if (m_GatewayScript.m_LevelComplete == true)
        {
            m_MainCamera.enabled = false;
            m_FinCamera.enabled = true;
        }
        else
        {
            m_MainCamera.enabled = true;
            m_FinCamera.enabled = false;
        }
            
    }

    public void LoadNextLevel(string NextScene)
    {
        SceneManager.LoadScene(NextScene);
    }
    public static void ResetLevel()
    {
        //SceneManager.sceneLoaded
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        m_Reset = false;

    }
    public static IEnumerator Sleep()
    {
        yield return new WaitForSeconds(1f);
        m_Reset = true;
    }
    public void IncreaseLevel()
    {

        m_CurrentLevel = m_GatewayScript.m_LevelNum+1;
        Debug.Log(m_CurrentLevel);
        PlayerPrefs.SetInt("Level", m_CurrentLevel);

    }
}
