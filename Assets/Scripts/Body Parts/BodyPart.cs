using UnityEngine;
using System.Collections;

public class BodyPart : MonoBehaviour {
    #region Public attributes
    
    #endregion

    #region Private attributes
    protected Character owner;
    #endregion

    #region Getters / Setters
    public Character Owner {
        get { return owner; }
        set { owner = value; }
    }
    #endregion
}
