using UnityEngine;
using System.Collections;

public class Enemy : Character {
    #region Public attributes
    /* Tweak the value for the character doesn't fall
     * Too low : Character doesn't move
     * Too high : The character doesn't detect the end of the platform, and falls
     */
    public float checkHoleScope = 1.5f;
    public float checkWallScope = 10f;
    #endregion

    #region Public methods
    public override void Die () {
        LevelManager.Instance.EndLevel ();
        base.Die ();
    }
    #endregion

    #region Private methods
    void FixedUpdate () {
        // Hole detection
        RaycastHit hit;
        if (!Physics.Raycast (transform.position, new Vector3 (direction.x, -1, 0), out hit, checkHoleScope, groundMask)) {
            inputX *= -1;    // Input simulation
        }

        // Wall detection
        if (Physics.Raycast (transform.position, new Vector3 (direction.x, 0, 0), out hit, checkWallScope, groundMask)) {
            inputX *= -1;    // Input simulation
        }

        Move ();
    }

    void Start () {
        base.Start ();
        inputX = direction.x;    // Input simulation
    }
    #endregion
}
