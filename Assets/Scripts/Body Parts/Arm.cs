using UnityEngine;
using System.Collections;

public class Arm : BodyPart {
    #region Public attributes
    public float shootDelay;    // Delay between two shoots
    #endregion

    #region Private attributes
    protected bool canShoot = true;
    protected float shootTimer;
    #endregion

    #region Public methods
    protected virtual void Shoot () {/*
        canShoot = false;
        shootTimer = shootDelay;
        StartCoroutine ("UpdateShootDelay");*/
    }
    /*
    IEnumerator UpdateShootDelay () {
        do {
            shootTimer -= Time.fixedDeltaTime;
            if (shootTimer <= 0) {
                canShoot = true;
            }
            yield return new WaitForFixedUpdate ();
        } while (!canShoot);
    }*/
    #endregion
}
