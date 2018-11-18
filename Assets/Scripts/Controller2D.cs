using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Controller2D : MonoBehaviour {

    BoxCollider2D collider;
    RayCastOrigins rayCastOrigins;
    public CollisionsInfo collisions;
    public LayerMask collisionMask;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        UpdateRayCastOrigins();
        //Debug.DrawRay(rayCastOrigins.bottomLeft, Vector3.down, Color.red);
       // Debug.DrawRay(rayCastOrigins.bottomRight, Vector3.down, Color.red);
    }

    public void Move(Vector2 velocity)
    {
        collisions.Reset();

        //Debug.Log("Velocity: " + velocity.y);

        if(velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
            //Debug.Log("Still changing velocity to 0 ?!?! velocity : "+ velocity.y);

        }

        if(velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }

        //Debug.Log(velocity.y);

        transform.Translate(velocity);
    }

    void VerticalCollisions(ref Vector2 velocity)
    {
        float rayLength = Mathf.Abs(velocity.y);
        float directionY = Mathf.Sign(velocity.y);

        Debug.DrawRay(rayCastOrigins.bottomLeft, Vector3.down, Color.red);
        Debug.DrawRay(rayCastOrigins.bottomRight, Vector3.down, Color.red);

        RaycastHit2D hitRight = Physics2D.Raycast(rayCastOrigins.bottomRight, Vector2.down, rayLength , collisionMask);
        RaycastHit2D hitLeft = Physics2D.Raycast(rayCastOrigins.bottomLeft, Vector3.down, rayLength, collisionMask);

        if ((hitRight || hitLeft) && directionY == -1) //(hitLeft || hitRight)
        {
            
            //rayLength -= 0.1f;
           // if(rayLength < 0.2)
            //{
                velocity.y = 0;
                rayLength = hitRight.distance;

                collisions.above = true;
                Debug.DrawRay(hitRight.point, Vector3.down, Color.yellow);
                Debug.DrawRay(hitLeft.point, Vector3.down, Color.yellow);
            //}
            //Debug.Log("Left: " + hitLeft.distance);
            //Debug.Log("Right: "+hitRight.distance);
        }
    }

    void HorizontalCollisions(ref Vector2 velocity)
    {
        float rayLength = Mathf.Abs(velocity.x);
        float directionX = Mathf.Sign(velocity.x);

        Debug.DrawRay(rayCastOrigins.bottomLeft, Vector3.left, Color.red);
        Debug.DrawRay(rayCastOrigins.bottomRight, Vector3.right, Color.red);
        Debug.DrawRay(rayCastOrigins.topLeft, Vector3.left, Color.red);
        Debug.DrawRay(rayCastOrigins.topRight, Vector3.right, Color.red);

       
        RaycastHit2D hitBottomHitbox = (directionX == -1) ? Physics2D.Raycast(rayCastOrigins.bottomLeft, Vector3.left, rayLength, collisionMask) : Physics2D.Raycast(rayCastOrigins.bottomRight, Vector3.right, rayLength, collisionMask);
        RaycastHit2D hitTopHitbox = (directionX == -1) ? Physics2D.Raycast(rayCastOrigins.topLeft, Vector3.left, rayLength, collisionMask) : Physics2D.Raycast(rayCastOrigins.topRight, Vector3.right, rayLength, collisionMask);


        if ((hitBottomHitbox || hitTopHitbox) && directionX == -1)
        {
            collisions.left = true;
                velocity.x = 0;

            //Debug.Log(directionX);
            Debug.DrawRay(hitBottomHitbox.point, Vector3.left * 2, Color.yellow);
            Debug.DrawRay(hitTopHitbox.point, Vector3.left * 2, Color.yellow);
        }

        if ((hitBottomHitbox || hitTopHitbox) && directionX == 1)
        {
            collisions.right = true;
            velocity.x = 0;

            //Debug.Log(directionX);
            Debug.DrawRay(hitBottomHitbox.point, Vector3.right * 2, Color.yellow);
            Debug.DrawRay(hitTopHitbox.point, Vector3.right * 2, Color.yellow);
        }
    }

    void UpdateRayCastOrigins()
    {
        Bounds bounds = collider.bounds;

        rayCastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        rayCastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        rayCastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        rayCastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    struct RayCastOrigins
    {
        public Vector3 topLeft, topRight;
        public Vector3 bottomLeft, bottomRight;
    }

    public struct CollisionsInfo
    {
        public bool below, above;
        public bool left, right;

        public void Reset()
        {
            below = above = false;
            left = right = false;
        }
    }
}
