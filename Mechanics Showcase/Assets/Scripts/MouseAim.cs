using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAim : MonoBehaviour
{
    public GameObject _cannon;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _cannon = GameObject.Find("Cannon");
    }

    /// <summary>
    /// OnMouseUp is called when the user has released the mouse button.
    /// </summary>
    void OnMouseUp()
    {   
        if (!PauseMenu.GameIsPaused){
            Vector3 Target, orig;
            Target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            orig = _cannon.transform.position + Vector3.forward * (Camera.main.transform.position.z); // The multiplication is to correct the z position
        
            _cannon.GetComponent<CannonBHV>().AlignCannon_mouse(Target, orig);
        }
    }
}
