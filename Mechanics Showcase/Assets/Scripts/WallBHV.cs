using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBHV : MonoBehaviour
{
    [SerializeField]
    private int value = 5;
    [SerializeField]
    private bool isPermanent = false;
    /// <summary>
    /// Sent when an incoming collider makes contact with this object's
    /// collider (2D physics only).
    /// </summary>
    /// <param name="other">The Collision2D data associated with this collision.</param>
    void OnCollisionEnter2D(Collision2D other)
    {
//        Debug.Log(other.gameObject.name + " Collided with " + this.name);
        if (other.gameObject.tag == "Projectile" && !isPermanent){
            this.gameObject.SetActive(false);
            //Destroy(this.gameObject);
            Player.Score += value;
        }
    }
}
