using UnityEngine;
using System.Collections;

public class OutZone : MonoBehaviour {
    void OnTriggerEnter (Collider collider) {
        Application.LoadLevel (Application.loadedLevel);
    }
}
