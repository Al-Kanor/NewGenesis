using UnityEngine;
using System.Collections;

public class Player : Character {
    #region Private methods
    void FixedUpdate () {
        // Move
        float h = Input.GetAxis ("Horizontal");

        if (h < 0) {
            inputX = -1;
        }
        else if (h > 0) {
            inputX = 1;
        }
        else {
            inputX = 0;
        }

        Move ();
    }

    void Start () {
        base.Start ();
        head.ApplyCameraMode ();
    }

    void Update () {
        if (isGrounded && Input.GetButtonDown ("Jump")) {
            speedY = 1;
        }

        // Shoot
        if (Input.GetButtonDown ("Fire Left")) {
            ShootLeft ();
        }
        else if (Input.GetButtonUp ("Fire Left")) {
            StopShootLeft ();
        }

        if (Input.GetButtonDown ("Fire Right")) {
            ShootRight ();
        }
    }
    #endregion
}
