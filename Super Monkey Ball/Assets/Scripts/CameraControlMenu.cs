using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlMenu : MonoBehaviour
{
    [Header("Scene Info")]
    public GameObject m_Player;
    private Rigidbody m_PlayerRB;
    public GameObject m_Camera;
    [Header("Tilting Info")]
    [Range(-10f, 10f)] public float m_DistanceFromPlayer;
    private float initalXRot = 20f;
    [Range(0f, 100f)] public float m_TiltSpeed = 2f;
    [Range(0f, 1f)] public float m_Sensitivity = 0.5f;
    [Range(0f, 20f)] public float m_MaxTiltAngleX;
    [Range(0f, 20f)] public float m_MaxTiltAngleZ;


    void Start()
    {
        m_PlayerRB = m_Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float verticalTilt = Input.GetAxis("Vertical"); // left joystick
        float horizontalTilt = Input.GetAxis("Horizontal");

        float verticalTiltOther = Input.GetAxis("Vertical2");// right joystick

        float limitedVerticleTilt = initalXRot - (verticalTilt * m_MaxTiltAngleX); // limits the angle the camera can travel (initialRot is for the camera to remember the 25 angle) (25 - (0.5 * 10)) = 20 moving the camera down 5
        Quaternion targetXRot = Quaternion.Euler(limitedVerticleTilt, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z); // this is for applying the float to a quaternion. (applies the camera's original z+y as they dont change)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetXRot, m_TiltSpeed * Time.deltaTime); // applies

        float limitedHorizontalTilt = horizontalTilt * m_MaxTiltAngleZ; // limits but doesnt need to return to a predetermined angle as 0 is wanted as default
        Quaternion targetZRot = Quaternion.Euler(m_Camera.transform.rotation.eulerAngles.x, m_Camera.transform.rotation.eulerAngles.y, limitedHorizontalTilt); // this is for applying the float to a quaternion. (applies the camera's original x+y as they dont change)
        m_Camera.transform.rotation = Quaternion.RotateTowards(m_Camera.transform.rotation, targetZRot, m_TiltSpeed * Time.deltaTime); // applies

        //rotating camera on y
        float verticalSpin = -verticalTiltOther * m_Sensitivity;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + verticalSpin, transform.rotation.eulerAngles.z);
        m_Player.transform.rotation = Quaternion.Euler(0, (transform.rotation.eulerAngles.y - initalXRot) + verticalSpin, transform.rotation.eulerAngles.z);


    }
    private void LateUpdate() // uses late as it always need to move the camera after the player moves
    {
        //keep camera behind ball with set distance
        transform.position = new Vector3(m_Player.transform.position.x, 3.0f, m_DistanceFromPlayer);
        //transform.position = m_Player.transform.position - (transform.forward * m_DistanceFromPlayer);
        transform.LookAt(m_Player.transform.position);
    }
}
