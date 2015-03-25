using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    #region Public attributes
    public float speed = 10;
    public int damage = 1;
    #endregion

    #region Private attributes
    private Vector3 direction;
    #endregion

    #region Getters / Setters
    public Vector3 Direction {
        get { return direction; }
        set { direction = value; }
    }
    #endregion

    #region Private methods
    void FixedUpdate () {
        transform.Translate (direction * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter (Collision collision) {
        Character character = collision.gameObject.GetComponent<Character> ();
        if (null != character) {
            character.TakeDamage (damage);
        }
        Destroy (gameObject);
    }
    #endregion
}
