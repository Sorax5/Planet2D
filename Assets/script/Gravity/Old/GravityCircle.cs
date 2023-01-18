using Assets.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCircle : MonoBehaviour
{

    public float power = 10;
    public List<GameObject> toAttrac = new List<GameObject>();
    
    public float Power { get => power; set => power = value; }

    // Start is called before the first frame update
    void Start()
    {
        // add tag Gravity
        gameObject.tag = "Gravity";
        this.toAttrac = GetAttractableObject();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject obj in this.toAttrac)
        {
            GravityApplied(obj);
        }

        this.toAttrac = GetAttractableObject();
    }
    
    public List<GameObject> GetAttractableObject()
    {
        List<GameObject> attractableObjects = new List<GameObject>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Attractable"))
        {
            if (Vector3.Distance(obj.transform.position, this.transform.position) < this.transform.localScale.x / 2)
            {
                attractableObjects.Add(obj);
            }
        }

        return attractableObjects;
    }

    public void GravityApplied(GameObject obj)
    {
        // the further away the object is, the weaker the gravity is
        if (Priority(obj))
        {
            Vector3 direction = (this.transform.position - obj.transform.position).normalized;
            float distance = Vector3.Distance(obj.transform.position, this.transform.position);
            float force = this.power * obj.GetComponent<Rigidbody2D>().mass * this.GetComponent<Rigidbody2D>().mass / Mathf.Pow(distance, 2);
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            if (obj.name == "Player")
            {
                PlayerMovement attract = obj.GetComponent<PlayerMovement>();
                attract.Direction = direction;
            }
        }
        
    }

    public bool Priority(GameObject obj)
    {
        bool priority = true;
        float localDistance = Vector3.Distance(obj.transform.position, this.transform.position);
        foreach (GameObject gravity in GameObject.FindGameObjectsWithTag("Gravity"))
        {
            // calcul distance with obj
            float distance = Vector3.Distance(obj.transform.position, gravity.transform.position);
            if (localDistance > distance)
            {
                priority = false;
            }
        }
        return priority;
    }
}
