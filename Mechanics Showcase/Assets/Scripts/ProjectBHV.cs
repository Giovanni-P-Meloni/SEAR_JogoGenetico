using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectBHV : MonoBehaviour
{

    public static bool projSpawned = false;
    private float lastPosY;
    private string lastCollision = null;

    void Awake()
    {
        ProjectBHV.projSpawned = true; //The projectile receives control    
        lastPosY = this.transform.position.y;
    }
    void Update()
    {

        if (this.gameObject.transform.position.y < -5.5f) Destroy(this.gameObject);


    }
     /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {

        if(other.gameObject.tag == "Boundary" && lastCollision == "Boundary" && lastPosY == this.transform.position.y){ 
            Debug.Log("Infinite Loop");
            Destroy(this.gameObject);
        } 
        lastCollision = other.gameObject.tag;
        lastPosY = this.transform.position.y;
    }

    /// <summary>
    /// This function is called when the MonoBehaviour will be destroyed.
    /// </summary>
    void OnDestroy()
    {
        //Debug.Log("Projectile has been destroyed, giving control...");
        ProjectBHV.projSpawned = false; //Projectile no longer in control
    }
}
