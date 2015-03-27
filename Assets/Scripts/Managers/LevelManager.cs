using UnityEngine;
using System.Collections;

public class LevelManager : Singleton<LevelManager> {
    private GameObject doorObject;

    public void EndLevel () {
        Destroy (doorObject);
    }

    void Start () {
        doorObject = GameObject.Find ("Door");
    }
}
