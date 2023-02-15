using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSorter : MonoBehaviour
{
    [SerializeField] private int m_CurrentLevel = 0;
    [SerializeField] private int m_LastSceneIndex;
    [SerializeField] private float m_LoadDelay;
    private Coroutine m_SceneChangeCoroutine;

    private void Awake()
    {
        SceneManager.LoadScene("Level" + m_CurrentLevel.ToString(), LoadSceneMode.Additive);
        //SceneManager.LoadScene("SandBox", LoadSceneMode.Additive);
    }

    private void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            if(m_SceneChangeCoroutine == null)
            {
                m_SceneChangeCoroutine = StartCoroutine(C_SceenChange());
            }
        }

        //stoping co
        //StopCoroutine(m_SceneChangeCoroutine);
        //m_SceneChangeCoroutine = null;
    }

    private IEnumerator C_SceenChange()
    {
        yield return new WaitForSeconds(m_LoadDelay);
        IncreaseSceneNum();
    }
    private void IncreaseSceneNum()
    {
        SceneManager.UnloadSceneAsync("Level" + m_CurrentLevel.ToString());
        m_CurrentLevel++;
        m_CurrentLevel %= m_LastSceneIndex;
        SceneManager.LoadScene("Level" + m_CurrentLevel.ToString(), LoadSceneMode.Additive);
    }
}

