using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour {

    Controller2D controller;
    Vector2 velocity;


    float gravity = -20;
    float jumpVelocity = 12;
    float speed = 5f;

    void Start()
    {
        controller = GetComponent<Controller2D>();

    }

   void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //  Prevents stacking gravity
        if (controller.collisions.above)
        {
            velocity.y = 0;
        }
        

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = jumpVelocity;
            //Debug.Log("jump");
        }

        velocity.x = input.x * speed;

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        
    }

}
