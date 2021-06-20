using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public Player player;
    public GroundedCheck check;
    Rigidbody physics;

    float speed;
    float jumpForce;

    float mov = 0;
    float evd = 0;
    bool jmp = false;

    int currentSmoothing = -1;

    bool updateRotation = false;

    Quaternion newRotation = Quaternion.identity;

    private void Start()
    {
        physics = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        speed = player.speed;
        jumpForce = player.jumpForce;

        mov = 0;
        evd = 0;
        jmp = false;

        if (Input.GetKey(player.move))
        {
            mov = 1;
        }

        if (Input.GetKey(player.evadeRight))
        {
            evd = 1;
        }
        else if (Input.GetKey(player.evadeLeft))
        {
            evd = -1;
        }

        if (Input.GetKey(player.jump) && check.isGrounded)
        {
            jmp = true;
        }

        move(mov, evd, jmp);
        updateRot();

    }

    void move(float move, float evade, bool jump)
    {
        Vector3 mov = Vector3.zero;

        if (move > 0)
        {
            mov += player.transform.forward * speed * Time.fixedDeltaTime;
        }

        if (evade > 0)
        {
            mov += player.transform.right * speed * Time.fixedDeltaTime;
        }

        if (evade < 0)
        {
            mov += - player.transform.right * speed * Time.fixedDeltaTime;
        }

        physics.AddForce(mov);

        if (jump)
        {
            physics.AddForce(player.transform.up * jumpForce, ForceMode.Impulse);
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            updateRotation = true;
            newRotation = collision.collider.transform.rotation;
        }
    }

    void updateRot()
    {
        if (updateRotation)
        {
            float sm = player.smoothing;

            Quaternion s = new Quaternion(
                (newRotation.x - player.transform.rotation.x)/sm ,
                (newRotation.y - player.transform.rotation.y)/sm, 
                (newRotation.z - player.transform.rotation.z)/sm, 
                (newRotation.w - player.transform.rotation.w)/sm
                );

            player.transform.rotation = new Quaternion(
                    player.transform.rotation.x + s.x,
                    player.transform.rotation.y + s.y, 
                    player.transform.rotation.z + s.z, 
                    player.transform.rotation.w + s.w);

            currentSmoothing ++;
            
            updateRotation = currentSmoothing != sm;
    
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Fall"))
        {
            physics.velocity = Vector3.zero;
            physics.freezeRotation = true;
            transform.position = player.currentLevel.currentCheckPoint.transform.position;
            physics.freezeRotation = false;
        }

        if (other.CompareTag("CheckPoint"))
        {
            player.currentLevel.currentCheckPoint = other.GetComponent<CheckPoint>();
        }
    }
}