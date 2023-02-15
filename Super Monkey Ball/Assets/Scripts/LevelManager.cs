using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int m_CurrentLevel;
    public int m_OldCurrentLevel;
    // Start is called before the first frame update
    void Start()
    {
        m_OldCurrentLevel = m_CurrentLevel = PlayerPrefs.GetInt("Level");
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CurrentLevel > m_OldCurrentLevel)
        {
            PlayerPrefs.SetInt("Level",m_CurrentLevel);
            m_OldCurrentLevel = m_CurrentLevel;
        }
    }

    public void IncreaseLevel()
    {
        m_CurrentLevel++;
        Debug.Log(m_CurrentLevel);
    }
}
