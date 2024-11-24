//Jimmy Vegas Unity Tutorial
//This script is for door animation


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenDoor : MonoBehaviour
{
    public GameObject doorPivot;
    public AudioSource doorOpen;
    public AudioSource doorClose;

    void Start()
    {
        StartCoroutine(DoorOpening());
    }

    IEnumerator DoorOpening()
    {
        yield return new WaitForSeconds(3);
        doorOpen.Play();
        doorPivot.GetComponent<Animator>().Play("WoodenDoorAnim");
        yield return new WaitForSeconds(4);
        doorClose.Play();
    }

}
