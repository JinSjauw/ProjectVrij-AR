using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurCamera : MonoBehaviour
{
    [SerializeField] GameObject blurPlane;
    public void Blur(bool state)
    {
        blurPlane.SetActive(state);
    }

}
