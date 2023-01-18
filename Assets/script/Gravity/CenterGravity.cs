using System;
using Assets.script;
using System.Collections;
using System.Collections.Generic;
using script;
using UnityEngine;

public class CenterGravity : Gravity
{
    public override void GravityApplied(GameObject obj)
    {
        if (CalculatePriority(obj))
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            Vector3 direction = obj.transform.position - transform.position;
            direction.Normalize();
            float distance = Vector3.Distance(this.transform.position, obj.transform.position);
            float force = Power * obj.GetComponent<Rigidbody2D>().mass * this.GetComponent<Rigidbody2D>().mass / Mathf.Pow(distance, 2);
            rb.velocity = Vector2.Lerp(rb.velocity, direction * force, Time.deltaTime);

            try
            {
                Attractable attractable = obj.GetComponent<Attractable>();
                attractable.Direction = direction;
            }
            catch (System.Exception)
            {
                
            }
        }
    }

    public void Start()
    {
        gameObject.tag = "Gravity";
        UpdateObjectInField();
    }
}
