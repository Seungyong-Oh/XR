using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        anim.SetBool("openDoor", true);
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("openDoor", false);
    }
}