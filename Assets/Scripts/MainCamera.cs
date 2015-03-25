using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
    #region Public attributes
    public Transform target;    // Target to follow
    public float speed = 5; // Speed at which the camera follows the target
    public float distance = 9;  // Distance from the target
    public float height = 1.4f;    // Height relative to the target
    public float freeZoneRadius = 80;    // Radius of the "free target move" zone (centered)
    #endregion

    #region Private attributes
    
    #endregion

    #region Private methods
    void FixedUpdate () {
        Vector3 dest = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z - distance);
        Camera camera = GetComponent<Camera> ();
        float targetScreenheight = camera.WorldToScreenPoint (target.transform.position).y;
        if (Mathf.Abs (targetScreenheight) > camera.pixelHeight / 2 + freeZoneRadius || Mathf.Abs (targetScreenheight) < camera.pixelHeight / 2 - freeZoneRadius) {
            dest.y = target.transform.position.y + height;
        }
        transform.position = Vector3.Lerp (transform.position, dest, speed * Time.fixedDeltaTime);
    }
    #endregion
}
