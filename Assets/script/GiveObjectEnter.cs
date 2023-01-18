using Assets.script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveObjectEnter : Gravity
{
    [SerializeField]
    private Teleporter teleporter;
    public Teleporter Teleporter { get => teleporter; set => teleporter = value; }

    [SerializeField]
    private LineRenderer lineRenderer;
    public LineRenderer LineRenderer { get => lineRenderer; set => lineRenderer = value; }

    private List<int> indexs = new List<int>();

    private int index = 0;


    public void FixedUpdate()
    {
        List<GameObject> toRemove = new List<GameObject>();

        {
            foreach (GameObject obj in ObjectInField)
            {
                indexs.Add(0);
                GravityApplied(obj);
                if (indexs[ObjectInField.IndexOf(obj)] == 1)
                {
                    toRemove.Add(obj);
                }
            }
        }

        foreach (GameObject obj in toRemove)
        {
            ObjectInField.Remove(obj);
        }

    }


    public override void GravityApplied(GameObject obj)
    {
        if (index != lineRenderer.positionCount - 2)
        {
            obj.transform.position = Vector3.Lerp(lineRenderer.GetPosition(index), lineRenderer.GetPosition(index + 1), Power * Time.deltaTime);
            index++;
        }
        else
        {
            index = 1;
            ObjectInField.Remove(obj);
        }
    }

    public new void OnTriggerExit2D(Collider2D collision)
    {
    }
}
