using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class MainCamera : MonoBehaviour {
    #region Public enum
    public enum CameraMode {
        ACTION,
        ORTHOGRAPHIC,
        SOFT
    }
    #endregion

    #region Public attributes
    public Transform target;    // Target to follow

    // Action mode
    public float actionModeSpeed = 5; // Speed at which the camera follows the target
    public float actionModeDistance = 9;  // Distance from the target

    // Orthographic mode
    public float orthographicModeSpeed = 5; // Speed at which the camera follows the target
    public float orthographicModeSize = 9;  // Distance from the target
    public float orthographicModeHeight = 1.4f;    // Height relative to the target
    public float orthographicModeFreeZoneRadius = 80;    // Radius of the "free target move" zone (centered)

    // Soft mode
    public float softModeSpeed = 5; // Speed at which the camera follows the target
    public float softModeDistance = 9;  // Distance from the target
    public float softModeHeight = 1.4f;    // Height relative to the target
    public float softModeFreeZoneRadius = 80;    // Radius of the "free target move" zone (centered)
    #endregion

    #region Private attributes
    private CameraMode mode;
    private Camera camera;
    #endregion

    #region Getters / Setters
    public CameraMode Mode {
        get { return mode; }
        set {
            if (CameraMode.ORTHOGRAPHIC == mode && CameraMode.ORTHOGRAPHIC != value || CameraMode.ORTHOGRAPHIC != mode && CameraMode.ORTHOGRAPHIC == value) {
                // Perspective to orthographic or orthographic to perspective
                camera.orthographic = !camera.orthographic;
            }

            //GetComponent <CameraMotionBlur> ().enabled = CameraMode.ACTION == value;
            mode = value;
        }
    }
    #endregion

    #region Private methods
    void Awake () {
        camera = GetComponent<Camera> ();
        mode = CameraMode.SOFT;
    }

    void FixedUpdate () {
        Vector3 dest;

        switch (mode) {
            case CameraMode.ACTION:
                dest = new Vector3 (target.transform.position.x, target.transform.position.y, target.transform.position.z - actionModeDistance);
                transform.position = Vector3.Lerp (transform.position, dest, actionModeSpeed * Time.fixedDeltaTime);
                transform.LookAt (target);
                break;
            case CameraMode.ORTHOGRAPHIC:
                dest = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z - 1);
                float targetScreenheight = camera.WorldToScreenPoint (target.transform.position).y;
                if (Mathf.Abs (targetScreenheight) > camera.pixelHeight / 2 + orthographicModeFreeZoneRadius || Mathf.Abs (targetScreenheight) < camera.pixelHeight / 2 - orthographicModeFreeZoneRadius) {
                    dest.y = target.transform.position.y + orthographicModeHeight;
                }
                transform.position = Vector3.Lerp (transform.position, dest, orthographicModeSpeed * Time.fixedDeltaTime);
                camera.orthographicSize = orthographicModeSize;
                break;
            case CameraMode.SOFT:
                dest = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z - softModeDistance);
                targetScreenheight = camera.WorldToScreenPoint (target.transform.position).y;
                if (Mathf.Abs (targetScreenheight) > camera.pixelHeight / 2 + softModeFreeZoneRadius || Mathf.Abs (targetScreenheight) < camera.pixelHeight / 2 - softModeFreeZoneRadius) {
                    dest.y = target.transform.position.y + softModeHeight;
                }
                transform.position = Vector3.Lerp (transform.position, dest, softModeSpeed * Time.fixedDeltaTime);
                break;
        }
    }
    #endregion
}
