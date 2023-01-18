using Assets.script;
using System.Collections;
using System.Collections.Generic;
using script;
using UnityEngine;

public class DirectionalGravity : Gravity
{
    [SerializeField]
    private bool up;
    public bool Up { get => up; set => up = value; }

    [SerializeField]
    private bool down;
    public bool Down { get => down; set => down = value; }

    [SerializeField]
    private bool left;
    public bool Left { get => left; set => left = value; }

    [SerializeField]
    private bool right;
    public bool Right { get => right; set => right = value; }

    private Vector3 upVector;
    private Vector3 downVector;
    private Vector3 rightVector;
    private Vector3 leftVector;


    public void Start()
    {
        upVector = new Vector3(Mathf.Cos((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad), Mathf.Sin((transform.rotation.eulerAngles.z + 90) * Mathf.Deg2Rad), 0);
        downVector = -upVector;
        leftVector = new Vector3(Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), Mathf.Sin(transform.rotation.eulerAngles.z * Mathf.Deg2Rad), 0);
        rightVector = -leftVector;
        UpdateObjectInField();
    }

    public override void GravityApplied(GameObject obj)
    {
        if (CalculatePriority(obj))
        {
            Vector3 direction = Vector3.zero;
            if (up)
            {
                direction -= upVector;
            }
            if (down)
            {
                direction -= downVector;
            }
            if (left)
            {
                direction -= leftVector;
            }
            if (right)
            {
                direction -= rightVector;
            }

            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            direction.Normalize();
            float distance = Vector3.Distance(obj.transform.position, this.transform.position);
            float force = Power * obj.GetComponent<Rigidbody2D>().mass * this.GetComponent<Rigidbody2D>().mass / Mathf.Pow(distance, 2);
            rb.velocity = Vector2.Lerp(rb.velocity, direction * force, Time.deltaTime);
            try
            {
                Attractable attractable = obj.GetComponent<Attractable>();
                attractable.Direction = direction;
            }
            catch
            {
            }
        }
    }
}
