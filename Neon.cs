using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Neon : MonoBehaviour
{
    public float radius = 0.3f; 
    public int segments = 24;   
    LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = segments + 1; 
        line.useWorldSpace = false;      
        CreatePoints();
    }

    void CreatePoints()
    {
        float angle = 0f;
        for (int i = 0; i < (segments + 1); i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;
            line.SetPosition(i, new Vector3(x, y, 0f));
            angle += (360f / segments);
        }
    }
}