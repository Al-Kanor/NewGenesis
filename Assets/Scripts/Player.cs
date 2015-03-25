using UnityEngine;
using System.Collections;

public class Player : Character {
    #region Private methods
    void FixedUpdate () {
        if (Input.GetButton ("Left")) {
            inputX = -1;
        }
        else if (Input.GetButton ("Right")) {
            inputX = 1;
        }
        else {
            inputX = 0;
        }

        Move ();
    }

    void Update () {
        if (isGrounded && Input.GetButtonDown ("Jump")) {
            speedY = 1;
        }

        if (Input.GetButtonDown ("Fire1")) {
            Shoot ();
        }
    }
    #endregion
}
