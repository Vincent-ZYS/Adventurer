using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour {

    public Camera miniMapCamera;
    public void OnZoomInClick()
    {
        if (miniMapCamera.orthographicSize >= 10)
        {
            miniMapCamera.orthographicSize--;
        }
    }
    public void OnZoomOutClick()
    {
        if (miniMapCamera.orthographicSize <= 25)
        {
            miniMapCamera.orthographicSize++;
        }
    }
}
