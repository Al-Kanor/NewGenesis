using UnityEngine;
using System.Collections;

public class Head : BodyPart {
    #region Public attributes
    public MainCamera.Mode cameraMode;
    #endregion

    #region Public methods
    public void ApplyCameraMode() {
        Camera.main.GetComponent<MainCamera> ().NewMode = cameraMode;
    }
    #endregion
}
