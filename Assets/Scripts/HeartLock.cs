using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLock : MonoBehaviour, Interactable
{
    [SerializeField] GameObject door;

    public void Interact()
    {
        door.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.GetComponent<HeartLock>().enabled = false;
        //this.gameObject.SetActive(false);
    }
}
