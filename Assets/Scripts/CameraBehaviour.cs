using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour {

    public Transform player;

    [SerializeField]
    private float boundX = 1f;
    [SerializeField]
    private float boundY = 1f;

	void LateUpdate () {
        Vector3 delta = Vector3.zero;

        //  Delta origins
        float dx = player.position.x - transform.position.x;

        //  X Axe
        if(dx > boundX || dx < -boundY)
        {
            if(transform.position.x < player.position.x)
            {
                //  Right
                delta.x = dx - boundX;
            }
            else
            {
                //  left
                delta.x = dx + boundX;
            }
        }

        //  Move camera
        transform.position = transform.position + delta;
	}
}
