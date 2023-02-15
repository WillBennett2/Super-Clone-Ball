using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [HideInInspector]public static Rigidbody m_RB;

    [Header("Movement Info")]
    public float m_MoveSpeed = 1000f;
    public float m_MoveForceZ;
    public float m_MoveForceX;
    public float m_MoveForce = 5f;
    public static bool m_CanMove = true;
    float verticleTilt;
    float horizontalTilt;
    public Transform m_CameraTransfrom;

    // Start is called before the first frame update
    void Start()
    {
        m_RB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        verticleTilt = Input.GetAxis("Vertical"); // left joystick
        horizontalTilt = Input.GetAxis("Horizontal");
    }
    private void FixedUpdate()
    {
        Move(verticleTilt, horizontalTilt);
    }
    public void Move(float verticalTilt, float horizontalTilt)
    {
        if (verticalTilt <= -0.5f)
        {
            verticalTilt = -0.5f;
        }
        Vector3 m_MoveForceZ = (horizontalTilt * m_MoveSpeed * m_CameraTransfrom.right) * Time.deltaTime;
        Vector3 m_MoveForceX = (verticalTilt * m_MoveSpeed * m_CameraTransfrom.forward) * Time.deltaTime;

        m_RB.AddForce(m_MoveForceZ, ForceMode.Acceleration);
        m_RB.AddForce(m_MoveForceX, ForceMode.Acceleration);

        //float m_MoveForceZ = (horizontalTilt * m_MoveSpeed) * Time.deltaTime;
        //float m_MoveForceX = (verticalTilt * m_MoveSpeed) * Time.deltaTime;
        //m_RB.AddRelativeForce(Vector3.right * m_MoveForceZ, ForceMode.Acceleration);
        //m_RB.AddRelativeForce(Vector3.forward * m_MoveForceX, ForceMode.Acceleration);

    }
}

