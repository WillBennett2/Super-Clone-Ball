using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LateCameraController : MonoBehaviour
{
    public GameObject m_Player;

    void Update()
    {
        transform.LookAt(m_Player.transform.position); // doesnt need to be a lateUpdate as its not waiting for player input
    }

}
