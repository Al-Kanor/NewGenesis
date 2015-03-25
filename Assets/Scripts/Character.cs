using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    #region Public attributes
    public int life = 1;    // Life (heart)
    public float speedX = 10;   // Move speed (legs)
    public float jumpHeight = 24;   // Jump height (back)
    public float accelerationY = 0.07f; // Jump acceleration (back)
    public float shootDelay = 0.5f;    // Delay between two shoots (arms)
    public bool initialLeft = true;
    public LayerMask groundMask;
    public GameObject bulletPrefab;
    public Material material;
    #endregion

    #region Private attributes
    protected int lifeMax;
    protected float inputX;
    protected bool isGrounded;
    protected float speedY = 0;
    protected Vector3 direction;
    protected bool canShoot = true;
    protected float shootTimer;
    protected Material currentMaterial;
    #endregion

    #region Public methods
    public void TakeDamage (int damage) {
        life = Mathf.Clamp (life - damage, 0, life);
        if (0 == life) {
            Destroy (gameObject);
        }
        material.color = Color.Lerp (Color.black, Color.white, (float)life / (float)lifeMax);
    }
    #endregion

    #region Private methods
    protected void Move () {
        // Changement of direction
        if (direction.x * inputX < 0) {
            direction *= -1;
            transform.GetChild (0).transform.rotation = Quaternion.Euler (new Vector3 (0, -transform.GetChild (0).transform.rotation.eulerAngles.y, 0));
        }

        // Movement
        GetComponent<Rigidbody> ().velocity = Vector3.zero;
        Vector3 move = Vector3.right * inputX;
        move *= speedX;
        
        RaycastHit hit;
        if (speedY <= 0 && Physics.Raycast (transform.position, -transform.up, out hit, 1, groundMask)) {
            isGrounded = true;
            speedY = 0;
        }
        else {
            isGrounded = false;
            speedY = Mathf.Clamp (speedY - accelerationY, -1, 1);
        }
        move += transform.up * jumpHeight * speedY;
        transform.Translate (move * Time.fixedDeltaTime);

        // Animation
        if (0 == inputX && 0 == speedY) {
            transform.GetChild (0).GetComponent<Animation> ().Play ("Idle");
        }
        else {
            transform.GetChild (0).GetComponent<Animation> ().Play ("Walk");
        }
    }

    protected void Shoot () {
        if (canShoot) {
            GameObject bulletObject = Instantiate (bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bulletObject.GetComponent<Bullet> ().Direction = direction;
            string layerName = "Ally" == tag ? "Ally Bullet" : "Enemy Bullet";
            bulletObject.layer = LayerMask.NameToLayer (layerName);
            canShoot = false;
            shootTimer = shootDelay;
            StartCoroutine ("UpdateShootDelay");
        }
    }

    protected virtual void Start () {
        direction = initialLeft ? Vector3.left : Vector3.right;
        lifeMax = life;
        material.color = Color.white;
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
