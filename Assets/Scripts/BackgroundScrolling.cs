using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour {

    public float backgroundSize;

    private Transform cameraTransform;
    private Transform[] layers;
    int leftIndex = 0;
    int rightIndex = 2;

    private void Start()
    {
        // 0 - left 1 - middle - 2 - right
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for (int i = 0 ; i < transform.childCount; i++){
            layers[i] = transform.GetChild(i);
        }
       
    }
    // Update is called once per frame
    void Update () {
        //Debug.Log("camera: "+cameraTransform.position.x);
        //Debug.Log("rightIndex: " + layers[rightIndex].position.x);
        Debug.Log("left:" + leftIndex);
        Debug.Log("right:" + rightIndex);

        if (cameraTransform.position.x > layers[rightIndex].position.x)
        {
            ScrollRight();
        }

        if (cameraTransform.position.x < layers[leftIndex].position.x)
        {
            ScrollLeft();
        }
    }

    private void ScrollRight()
    {
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if(leftIndex > 2)
        {
            leftIndex = 0;
        }
        
    
    }

    private void ScrollLeft()
    {
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0)
        {
            rightIndex = 2;
        }
    }
}
