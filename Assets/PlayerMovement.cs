using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    //public AudioController audio;

    public float speed = 6f;
    public float gravity = -9.8f;
    public float jumpForce = 1f;
    float fallVelocity;
    public KeyCode jump;

    public bool isOnSlope = false;
    private Vector3 hitNormal;
    public float slideVelocity;
    public float slopeForceDown;

    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        SetGravity();

        PlayerSkills();

        if (direction.magnitude >= 0.1f)
        {
            
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            if(controller.isGrounded){
                //gameObject.GetComponent<Animator>().SetBool("walking", true);
                //audio.AudioSteps();
            }
        }else{
            //gameObject.GetComponent<Animator>().SetBool("walking", false);
        }
    }

    void SetGravity(){
    

        if (controller.isGrounded)
        {
            //gameObject.GetComponent<Animator>().SetBool("jumping", false);
            /*fallVelocity = gravity * Time.deltaTime;
            Vector3 gravityVector = new Vector3(0, fallVelocity, 0);
            controller.Move(gravityVector * Time.deltaTime);*/
        }else{
            fallVelocity -= gravity * Time.deltaTime;
            Vector3 gravityVector = new Vector3(0, fallVelocity, 0);
            controller.Move(gravityVector * Time.deltaTime);
        }

    }

    void PlayerSkills(){


        Debug.Log(controller.isGrounded);
        if (controller.isGrounded && Input.GetKeyDown(jump))
        {
            //gameObject.GetComponent<Animator>().SetBool("jumping", true);
            fallVelocity = jumpForce;
            Vector3 gravityVector = new Vector3(0, fallVelocity, 0);
            controller.Move(gravityVector * Time.deltaTime);
        }
    }
}
