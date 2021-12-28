using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraLook : MonoBehaviour
{
    public float lookSpeed;

    private CinemachineFreeLook cinemachine;
    private Player playerInput;

    private void Awake()
    {
        playerInput = new Player();
        cinemachine = GetComponent <CinemachineFreeLook>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lookSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 delta = playerInput.PlayerMain.Look.ReadValue<Vector2>();
        cinemachine.m_XAxis.Value += delta.x * 200 * lookSpeed * Time.deltaTime;
        cinemachine.m_YAxis.Value += delta.y * lookSpeed * Time.deltaTime;
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}
