using UnityEngine;
using System.Collections;

public class ArmLeft : Arm {
    #region Public attributes
    public float recoilStrength;
    public GameObject bulletPrefab;
    #endregion

    private float lastShoot;

    #region Public methods
    #endregion

    #region Private methods
    protected override void Shoot () {

        //if (canShoot) {
        GameObject bulletObject = Instantiate (bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bulletObject.GetComponent<Bullet> ().Direction = owner.Direction;
        string layerName = "Ally" == owner.tag ? "Ally Bullet" : "Enemy Bullet";
        bulletObject.layer = LayerMask.NameToLayer (layerName);

        // Recoil
        //owner.transform.Translate (-owner.Direction * recoilStrength);

        base.Shoot ();
        //}*/

    }

    void Start () {
        shootTimer = shootDelay;
    }

    IEnumerator UpdateShoot () {
        do {
            if (Time.time - lastShoot >= shootDelay) {
                Shoot ();
                lastShoot = Time.time;
            }
            yield return new WaitForSeconds (shootDelay);
            canShoot = true;
        } while (true);
    }
    #endregion
}
