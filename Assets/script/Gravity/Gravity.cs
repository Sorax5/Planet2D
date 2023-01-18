using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.script
{
    public abstract class Gravity : MonoBehaviour
    {
        [SerializeField]
        private float priority = 0;
        public float Priority { get => priority; set => priority = value; }

        [SerializeField]
        private float power = 0;
        public float Power { get => power; set => power = value; }

        [SerializeField]
        private List<GameObject> objectInField = new List<GameObject>();
        public List<GameObject> ObjectInField { get => objectInField; set => objectInField = value; }

        public void Awake()
        {
            tag = "Gravity";
        }

        public abstract void GravityApplied(GameObject obj);

        public bool CalculatePriority(GameObject obj)
        {
            bool priority = true;
            float d1 = Vector2.Distance(transform.position, obj.transform.position);
            foreach (GameObject gravity in GameObject.FindGameObjectsWithTag("Gravity"))
            {
                
                if(gravity == this.gameObject)
                {
                    continue;
                }
                float d2 = Vector2.Distance(gravity.transform.position, obj.transform.position);

                // verifying wich distance is the smallest
                if (d1 > d2)
                {
                    // if the gravity is stronger than the current gravity, the priority is false
                    priority = false;
                    break;
                   /* if (gravity.GetComponent<Gravity>().Priority > Priority)
                    {
                        priority = false;
                        break;
                    }*/
                }
            }
            return priority;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            foreach (GameObject obj in ObjectInField)
            {
                GravityApplied(obj);
            }
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Attractable" && !ObjectInField.Contains(collision.gameObject))
            {
                ObjectInField.Add(collision.gameObject);
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Attractable" && ObjectInField.Contains(collision.gameObject))
            {
                ObjectInField.Remove(collision.gameObject);
            }
        }
        
        public void UpdateObjectInField()
        {
            // foreach gameobject who have tag Attractable
            Collider2D collider = GetComponent<Collider2D>();
            foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Attractable"))
            {
                if (collider.bounds.Contains(obj.transform.position))
                {
                    if (!ObjectInField.Contains(obj))
                    {
                        ObjectInField.Add(obj);
                    }
                }
                else
                {
                    if (ObjectInField.Contains(obj))
                    {
                        ObjectInField.Remove(obj);
                    }
                }
            }
           
        }
    }
}
