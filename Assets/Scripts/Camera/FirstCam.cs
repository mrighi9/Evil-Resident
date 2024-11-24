using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstCam : MonoBehaviour{

    public GameObject cameraOn;
    public GameObject cameraOff;
    
    public GameObject cameraOff2;
    public GameObject cameraOff3;
    public bool camOn = false;
    public int cameraNumber; 

    void Start()
    {
        cameraNumber = 1;
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"){
            cameraOn.SetActive(true);
            cameraOff.SetActive(false);
            cameraOff2.SetActive(false);
            cameraOff3.SetActive(false);
        }
    }
}
