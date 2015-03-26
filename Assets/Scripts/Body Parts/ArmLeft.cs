using UnityEngine;
using System.Collections;

public class ArmLeft : Arm {
    #region Public attributes
    public GameObject bulletPrefab;
    #endregion

    #region Public methods
    public void Shoot () {
        if (canShoot) {
            GameObject bulletObject = Instantiate (bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bulletObject.GetComponent<Bullet> ().Direction = owner.Direction;
            string layerName = "Ally" == owner.tag ? "Ally Bullet" : "Enemy Bullet";
            bulletObject.layer = LayerMask.NameToLayer (layerName);
            base.Shoot ();
        }
    }
    #endregion

    #region Private methods
    void Start () {
        
    }

    IEnumerator UpdateShootDelay () {
        do {
            shootTimer -= Time.fixedDeltaTime;
            if (shootTimer <= 0) {
                canShoot = true;
            }
            yield return new WaitForFixedUpdate ();
        } while (!canShoot);
    }
    #endregion
}
