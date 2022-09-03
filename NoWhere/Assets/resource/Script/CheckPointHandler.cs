using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger Enter, Destroy This.");
        Destroy(this.gameObject);
    }
}
