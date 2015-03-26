using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {
    #region Public attributes
    public GameObject armLeftPrefab;
    public GameObject armRightPrefab;
    public GameObject backPrefab;
    public GameObject headPrefab;
    public GameObject heartPrefab;
    public GameObject legLeftPrefab;
    public GameObject legRightPrefab;
    public GameObject torsoPrefab;
    public GameObject model;
    public bool initialLeft = true;
    public LayerMask groundMask;
    #endregion

    #region Private attributes
    protected ArmLeft armLeft;
    protected ArmRight armRight;
    protected Back back;
    protected Head head;
    protected Heart heart;
    protected LegLeft legLeft;
    protected LegRight legRight;
    protected Torso torso;
    protected float inputX;
    protected bool isGrounded;
    protected float speedY = 0;
    protected Vector3 direction;
    #endregion

    #region Getters / Setters
    public Vector3 Direction {
        get { return direction; }
    }
    #endregion

    #region Public methods
    public void TakeDamage (int damage) {
        heart.TakeDamage (damage);
    }
    #endregion

    #region Private methods
    protected void Move () {
        // Changement of direction
        if (direction.x * inputX < 0) {
            direction *= -1;
            model.transform.rotation = Quaternion.Euler (new Vector3 (0, -model.transform.rotation.eulerAngles.y, 0));
        }

        // Movement
        GetComponent<Rigidbody> ().velocity = Vector3.zero;
        Vector3 move = Vector3.right * inputX;
        move *= legLeft.speedX;
        
        RaycastHit hit;
        if (speedY <= 0 && Physics.Raycast (transform.position, -transform.up, out hit, 1, groundMask)) {
            isGrounded = true;
            speedY = 0;
        }
        else {
            isGrounded = false;
            if (Physics.Raycast (transform.position, transform.up, out hit, 1, groundMask)) {
                // Platform over the character => Doesn't stay in the air
                speedY = 0;
            }
            speedY = Mathf.Clamp (speedY - back.jumpAcceleration, -1, 1);
        }
        move += transform.up * back.jumpHeight * speedY;
        transform.Translate (move * Time.fixedDeltaTime);

        // Animation
        if (0 == inputX && 0 == speedY) {
            model.GetComponent<Animation> ().Play ("Idle");
        }
        else {
            model.GetComponent<Animation> ().Play ("Walk");
        }
    }

    protected void ShootLeft () {
        armLeft.Shoot ();
    }

    protected void ShootRight () {
        armRight.Shoot ();
    }

    protected virtual void Start () {
        direction = initialLeft ? Vector3.left : Vector3.right;

        // Body parts
        // Left arm
        GameObject armLeftObject = Instantiate (armLeftPrefab) as GameObject;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag ("Left Arm")) {
            if (o.layer == gameObject.layer) {
                armLeftObject.transform.parent = o.transform;
                break;
            }
        }
        armLeftObject.transform.localPosition = new Vector3 (0.047f, -0.002f, -0.008f);
        armLeftObject.transform.localRotation = Quaternion.Euler (77.69998f, 241.4f, 329.04f);
        armLeftObject.transform.localScale = new Vector3 (0.4467399f, 0.4403573f, 0.5655955f);
        armLeft = armLeftObject.GetComponent<ArmLeft> ();
        armLeft.Owner = this;

        // Right arm
        GameObject armRightObject = Instantiate (armRightPrefab) as GameObject;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag ("Right Arm")) {
            if (o.layer == gameObject.layer) {
                armRightObject.transform.parent = o.transform;
                break;
            }
        }
        armRightObject.transform.localPosition = new Vector3 (0.047f, -0.002f, -0.008f);
        armRightObject.transform.localRotation = Quaternion.Euler (77.69998f, 241.4f, 329.04f);
        armRightObject.transform.localScale = new Vector3 (0.4467399f, 0.4403573f, 0.5655955f);
        armRight = armRightObject.GetComponent<ArmRight> ();
        armRight.Owner = this;

        // Back
        GameObject backObject = Instantiate (backPrefab) as GameObject;
        //backObject.transform.parent = transform;
        back = backObject.GetComponent<Back> ();
        back.Owner = this;

        // Head
        GameObject headObject = Instantiate (headPrefab) as GameObject;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag ("Head")) {
            if (o.layer == gameObject.layer) {
                headObject.transform.parent = o.transform;
                break;
            }
        }
        headObject.transform.localPosition = new Vector3 (-0.16f, 0.14f, -0.01f);
        headObject.transform.localRotation = Quaternion.Euler (0, -1.525879e-05f, 88.15112f);
        headObject.transform.localScale = new Vector3 (0.56925f, 0.3410917f, 0.530365f);
        head = headObject.GetComponent<Head> ();
        head.Owner = this;

        // Heart
        GameObject heartObject = Instantiate (heartPrefab) as GameObject;
        //backObject.transform.parent = transform;
        heart = heartObject.GetComponent<Heart> ();
        heart.Owner = this;

        // Left leg
        GameObject legLeftObject = Instantiate (legLeftPrefab) as GameObject;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag ("Left Leg")) {
            if (o.layer == gameObject.layer) {
                legLeftObject.transform.parent = o.transform;
                break;
            }
        }
        legLeftObject.transform.localPosition = new Vector3 (0.2500004f, 0.03998247f, 0.09005298f);
        legLeftObject.transform.localRotation = Quaternion.Euler (39.34236f, 352.7302f, 94.35577f);
        legLeftObject.transform.localScale = new Vector3 (0.4467359f, 0.4403596f, 0.5656021f);
        legLeft = legLeftObject.GetComponent<LegLeft> ();
        legLeft.Owner = this;

        // Right leg
        GameObject legRightObject = Instantiate (legRightPrefab) as GameObject;
        foreach (GameObject o in GameObject.FindGameObjectsWithTag ("Right Leg")) {
            if (o.layer == gameObject.layer) {
                legRightObject.transform.parent = o.transform;
                break;
            }
        }
        legRightObject.transform.localPosition = new Vector3 (0.2500004f, 0.03998247f, 0.09005298f);
        legRightObject.transform.localRotation = Quaternion.Euler (39.34236f, 352.7302f, 94.35577f);
        legRightObject.transform.localScale = new Vector3 (0.4467359f, 0.4403596f, 0.5656021f);
        legRight = legRightObject.GetComponent<LegRight> ();
        legRight.Owner = this;

        // Torso
        GameObject torsoObject = Instantiate (torsoPrefab) as GameObject;
        //torsoObject.transform.parent = transform;
        torso = torsoObject.GetComponent<Torso> ();
        torso.Owner = this;
    }
    #endregion
}
