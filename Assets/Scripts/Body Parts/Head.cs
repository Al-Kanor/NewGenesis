using UnityEngine;
using System.Collections;

public class Head : BodyPart {
    #region Public attributes
    public MainCamera.CameraMode cameraMode;
    #endregion

    #region Public methods
    public void ApplyCameraMode() {
        Camera.main.GetComponent<MainCamera> ().Mode = cameraMode;
    }
    #endregion
}
