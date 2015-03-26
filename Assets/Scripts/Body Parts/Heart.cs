using UnityEngine;
using System.Collections;

public class Heart : BodyPart {
    #region Public attributes
    public int life;
    #endregion

    #region Private attributes
    private int lifeMax;
    #endregion

    #region Public methods
    public void TakeDamage (int damage) {
        life = Mathf.Clamp (life - damage, 0, life);
        if (0 == life) {
            Destroy (owner.gameObject);
        }
    }
    #endregion

    #region Private methods
    void Start () {
        lifeMax = life;
    }
    #endregion
}
