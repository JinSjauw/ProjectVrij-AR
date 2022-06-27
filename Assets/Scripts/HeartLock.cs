using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLock : MonoBehaviour, Interactable
{
    [SerializeField] GameObject door;

    private bool isInteractable;

    private void Start() {
        isInteractable = true;
    }

    public bool isActive()
    {
        return isInteractable;
    }

    public void Interact()
    {
        door.SetActive(false);
        this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        this.gameObject.GetComponent<HeartLock>().enabled = false;
        isInteractable = false;
    }
}
