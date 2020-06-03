using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBHV : MonoBehaviour
{
    public bool IsAIControlling;
    public GameObject Projectile;
    private GeneticAlg AIControl;
    private float rotationSpeed = 1.5f;
    [SerializeField]
    private float cannonForce=5f;
    private int currentShot = 0;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        AIControl = GetComponent<GeneticAlg>();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
       if (!IsAIControlling){ //Player Control
            ///Movement
        AlignCannon_arrow();
            
            ///Firing
            if (Input.GetButtonDown("Fire")){
                //Debug.Log("Requesting to fire...");
                Fire();
            }
       }
       else if(AIControl.individualReady && !AIControl.undergoingDraw && !ProjectBHV.projSpawned){//AI Control and new individual is ready to be analyzed (Cannon is in control)
            AlignCannon_value(AIControl.currentIndividual.angles[currentShot]);
            //Debug.Log("This individual angle is " + AIControl.currentIndividual.angles[currentShot]);
            Fire(); //Giving Control to projectile
            currentShot++;
            if (currentShot >= AIControl.shotQuantity){
                AIControl.individualReady = false;// Analysis has begun (Cannon no longer has control)
                currentShot = 0;
            }
       }
       
    }
    public void Fire(){
        if (!ProjectBHV.projSpawned){
            //Debug.Log("Firing cannon!"); 
            Vector3 projSpawnPoint =  new Vector3(this.transform.position.x, this.transform.position.y,this.transform.position.z);
            var proj = Instantiate(Projectile, projSpawnPoint, this.transform.rotation);
            proj.GetComponent<Rigidbody2D>().AddForce(transform.up*cannonForce, ForceMode2D.Impulse);
        }
        else{
            Debug.Log("There already exists one projectile");
        }
    }

    /// 
    /// Function called when the user wants to aim the cannon with the mouse
    /// 
    public void AlignCannon_mouse(Vector3 Target, Vector3 orig){
        Vector3 _direction;
        float rotAngle;

        _direction = (Target - orig).normalized;
        //Debug.Log(_direction);

        rotAngle = 90f - Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        this.transform.rotation = Quaternion.AngleAxis(-rotAngle, Vector3.forward);  
    }

    ///
    ///Function called when the user wants to aim the cannon with the arrows
    ///
    public void AlignCannon_arrow(){

        if (Input.GetButton("Horizontal-Right")){
            this.transform.Rotate(Vector3.back* rotationSpeed);  
         }
        else if (Input.GetButton("Horizontal-Left")){
            this.transform.Rotate(Vector3.forward* rotationSpeed);
        }
    }

    ///
    ///Function called when the cannon has to be aimed according to value
    ///
    public void AlignCannon_value(float angle){
        this.transform.eulerAngles =  new Vector3(0, 0, angle);
    }
}
