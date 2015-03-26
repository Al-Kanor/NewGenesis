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
    public virtual void Shoot () {
        canShoot = false;
        shootTimer = shootDelay;
        StartCoroutine ("UpdateShootDelay");
    }
    #endregion
}
