using Assets.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySquare : MonoBehaviour
{

    private Vector3 upVector;

    private Vector3 downVector;

    private Vector3 rightVector;

    private Vector3 leftVector;

    public float power = 5f;
    public int priority = 0;
    public List<GameObject> toAttrac = new List<GameObject>();
    
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;

    public float Power { get => power; set => power = value; }
    
    public List<GameObject> GetAttractableObject()
    {
        List<GameObject> attractableObjects = new List<GameObject>();

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Attractable"))
        {
            // if obj is in the square

            if (obj.transform.position.x > transform.position.x - transform.localScale.x / 2 &&
                obj.transform.position.x < transform.position.x + transform.localScale.x / 2 &&
                obj.transform.position.y > transform.position.y - transform.localScale.y / 2 &&
                obj.transform.position.y < transform.position.y + transform.localScale.y / 2)
            {
                attractableObjects.Add(obj);
            }
        }

        return attractableObjects;
    }

    public void GravityApplied(GameObject obj)
    {
        if (Priority(obj))
        {

            Vector3 direction = Vector3.zero;

            if (up)
            {
                direction += upVector;
            }
            if (down)
            {
                direction += downVector;
            }
            if (left)
            {
                direction += leftVector;
            }
            if (right)
            {
                direction += rightVector;
            }

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
        foreach (GameObject gravity in GameObject.FindGameObjectsWithTag("Gravity"))
        {
            Gravity gravity1 = gravity.GetComponent<Gravity>();
        }
        return priority;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Gravity";
        
        upVector = new Vector3(Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad), Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad), 0);
        downVector = -upVector;
        leftVector = new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 0);
        rightVector = -leftVector;
        
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
}
