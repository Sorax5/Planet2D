using Assets.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuterGravity : Gravity
{
    public override void GravityApplied(GameObject obj)
    {
        if (CalculatePriority(obj))
        {
            Vector3 direction = obj.transform.position - transform.position ;
            direction.Normalize();
            float distance = Vector3.Distance(obj.transform.position, this.transform.position);
            float force = Power * obj.GetComponent<Rigidbody2D>().mass * this.GetComponent<Rigidbody2D>().mass / Mathf.Pow(distance, 2);
            obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
            if (obj.name == "Player")
            {
                PlayerMovement attract = obj.GetComponent<PlayerMovement>();
                attract.Direction = direction;
            }
        }
    }

    public void Start()
    {
        gameObject.tag = "Gravity";
        UpdateObjectInField();
    }
}
