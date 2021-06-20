using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Controller controller;
    public GameObject cam;
    public Vector3 camDelta;
    public Level currentLevel;
    public float speed = 250;
    public float jumpForce = 25;
    public float smoothing = 60f;

    public KeyCode move, evadeLeft, evadeRight, jump;

    private void Start()
    {
        cam.transform.position = transform.position + camDelta;
    }

    void Update()
    {
        gameObject.transform.position = controller.transform.position;
    }
}
