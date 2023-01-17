using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Teleporter : MonoBehaviour
{

    [SerializeField]
    private GameObject tp1, tp2;
    public GameObject Tp1 { get => tp1; set => tp1 = value; }
    public GameObject Tp2 { get => tp2; set => tp2 = value; }

    [SerializeField]
    private float speed;
    public float Speed { get => speed; set => speed = value; }

    private Vector3 tp1Direction = new Vector3(), tp2Direction = new Vector3();

    [SerializeField]
    private LineRenderer lineRenderer;
    public LineRenderer LineRenderer { get => lineRenderer; set => lineRenderer = value; }

    [SerializeField]
    private float vertexCount = 12;
    public float VertexCount { get => vertexCount; set => vertexCount = value; }

    private float length;

    [Range(0, 360)]
    public int tp1Angle = 0;

    [Range(0, 360)]
    public int tp2Angle = 0;

    // Start is called before the first frame update
    void Start()
    {
        length = Vector3.Distance(tp1.transform.position, tp2.transform.position);
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        // set color
        lineRenderer.startColor = Color.blue;
        lineRenderer.endColor = Color.cyan;;

        TraceLine();
    }

    // Update is called once per frame
    void Update()
    {
        TraceLine();
    }

    private void TraceLine()
    {
        tp1Direction = new Vector3(Mathf.Cos(tp1Angle * Mathf.Deg2Rad), Mathf.Sin(tp1Angle * Mathf.Deg2Rad), 0).normalized;
        tp2Direction = new Vector3(Mathf.Cos(tp2Angle * Mathf.Deg2Rad), Mathf.Sin(tp2Angle * Mathf.Deg2Rad), 0).normalized;

        Vector3 inter = IntersectionPoint(tp1.transform.position, tp2.transform.position, tp1Direction, tp2Direction);

        List<Vector3> points = new List<Vector3>();

        for (float ratio = 0; ratio <= 1; ratio += 1 / vertexCount)
        {
            Vector3 tan1 = Vector3.Lerp(tp1.transform.position, inter, ratio);
            Vector3 tan2 = Vector3.Lerp(inter,tp2.transform.position , ratio);

            Vector3 bezierPoint = Vector3.Lerp(tan1,tan2, ratio);
            points.Add(bezierPoint);
        }

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());

        // Debug draw to vector
        Debug.DrawLine(tp1.transform.position, tp1.transform.position + tp1Direction, Color.red);
        Debug.DrawLine(tp2.transform.position, tp2.transform.position + tp2Direction, Color.red);

    }

    private Vector3 IntersectionPoint(Vector3 p1, Vector3 p2, Vector3 d1, Vector3 d2)
    {
        try
        {
            float a1 = d1.y / d1.x;
            float b1 = p1.y - a1 * p1.x;

            float a2 = d2.y / d2.x;
            float b2 = p2.y - a2 * p2.x;

            float x = (b2 - b1) / (a1 - a2);
            float y = a1 * x + b1;

            return new Vector3(x, y, 0);
        }
        catch
        {
            return Vector3.zero;
        }
    }

}
