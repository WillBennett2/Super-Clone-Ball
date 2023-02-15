using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectables : MonoBehaviour
{
    [SerializeField] [Range(0f,100f)] private float m_SpinSpeed = 20f;
    //public GameObject m_UIGameObject;
    //private UIManager m_UIScript;
    // Start is called before the first frame update
    void Start()
    {
        //m_UIScript = m_UIGameObject.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0,0+(m_SpinSpeed*Time.deltaTime),0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Increase score
            UIManager.instance.IncreaseScore();
            gameObject.SetActive(false);
            

        }
    }
}
