using UnityEngine;
using System.Collections;

public class Player : Character {
    #region Private methods
    void FixedUpdate () {
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

        if (Input.GetButtonDown ("Fire Left")) {
            ShootLeft ();
        }

        if (Input.GetButtonDown ("Fire Right")) {
            ShootRight ();
        }
    }
    #endregion
}
