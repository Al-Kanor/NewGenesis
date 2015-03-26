using UnityEngine;
using System.Collections;

public class GameManager : Singleton<GameManager> {
    #region Private methods
    void Update () {
        if (Input.GetButtonDown ("Submit")) {
            Debug.Log ("submit");
        }
        if (Input.GetButtonDown ("Cancel")) {
            Debug.Log ("cancel");
        }
    }
    #endregion
}
