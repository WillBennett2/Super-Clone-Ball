using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("Scene Info")]
    public GameObject m_Player;
    private Rigidbody m_PlayerRB;
    public PlayerMovement m_PlayerMovementScript;
    public GameObject m_Camera;
    [Header("Tilting Info")]
    public float m_VerticleSpin;
    [Range(0f, 10f)] public float m_DistanceFromPlayer;
    private float initalXRot = 20f;
    [Range(0f, 100f)] public float m_TiltSpeed = 2f;
    [Range(0f, 1f)] public float m_Sensitivity = 0.5f;
    [Range(0f, 20f)] public float m_MaxTiltAngleX;
    [Range(0f, 20f)] public float m_MaxTiltAngleZ;

    float verticleTilt;
    float horizontalTilt;
    float verticalTiltOther;

    private Vector3 m_FloorNormal;


    void Start()
    {
        m_PlayerRB = m_Player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        verticleTilt = Input.GetAxis("Vertical"); // left joystick
        horizontalTilt = Input.GetAxis("Horizontal");

        verticalTiltOther = Input.GetAxis("Vertical2");// right joystick

        // https://answers.unity.com/questions/358376/whats-a-good-way-to-find-the-ground-normal.html
        RaycastHit hit;
        if (Physics.Raycast(m_Player.transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            m_FloorNormal = hit.normal;
        }
        //used to get angle between world up and floor up
        float angleToTilt = Vector3.SignedAngle(Vector3.up, m_FloorNormal, transform.right);

        float limitedVerticleTilt = initalXRot - (verticleTilt * m_MaxTiltAngleX); // limits the angle the camera can travel (initialRot is for the camera to remember the 25 angle) (25 - (0.5 * 10)) = 20 moving the camera down 5
        Quaternion targetXRot = Quaternion.Euler(limitedVerticleTilt+ angleToTilt, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        // this is for applying the float to a quaternion. (applies the camera's original z+y as they dont change)
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetXRot, m_TiltSpeed * Time.deltaTime); // applies

        float limitedHorizontalTilt = horizontalTilt * m_MaxTiltAngleZ; // limits but doesnt need to return to a predetermined angle as 0 is wanted as default
        Quaternion targetZRot = Quaternion.Euler(m_Camera.transform.rotation.eulerAngles.x, m_Camera.transform.rotation.eulerAngles.y, limitedHorizontalTilt); // this is for applying the float to a quaternion. (applies the camera's original x+y as they dont change)
        m_Camera.transform.rotation = Quaternion.RotateTowards(m_Camera.transform.rotation, targetZRot, m_TiltSpeed * Time.deltaTime); // applies

        float verticalSpin = -verticalTiltOther * m_Sensitivity;

        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + verticalSpin, transform.rotation.eulerAngles.z);
        //m_Player.transform.rotation = Quaternion.Euler(m_Player.transform.rotation.x, (transform.rotation.eulerAngles.y - initalXRot) + verticalSpin, m_Player.transform.rotation.z);

    }

    private void LateUpdate() // uses late as it always need to move the camera after the player moves
    {
        //keep camera behind ball with set distance
        transform.position = m_Player.transform.position - (transform.forward * m_DistanceFromPlayer);
        transform.LookAt(m_Player.transform.position);
    }
}
