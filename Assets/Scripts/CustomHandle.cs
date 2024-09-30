using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public enum HandleTypes
{
    Arrow, Circle, Cone, Cube, Dot, Rectangle, Sphere
}

[System.Serializable]
public class HandleParams
{
    public float size = 1.0f;
    public float offset = 0.0f;
    public HandleTypes Type = HandleTypes.Arrow;
}

public class CustomHandle : MonoBehaviour
{
    [SerializeField]
    public HandleParams handleParams;

    private Rigidbody cc_rb;
    private BaseMaple cc_Maple;

    private void Awake()
    {
        cc_rb = GetComponent<Rigidbody>();
        Debug.Assert(cc_rb != null);

        cc_Maple = GetComponent<BaseMaple>();
        Debug.Assert(cc_Maple != null);
    }

    public Vector3 getVelocityVector()
    {
        if (!cc_rb)
        {
            cc_rb = GetComponent<Rigidbody>();
            Debug.Assert(cc_rb != null);
        }
        
        return cc_rb.velocity;
    }

    public Vector3 getForceVector()
    {
        if (!cc_Maple)
        {
            cc_Maple = GetComponent<BaseMaple>();
            Debug.Assert(cc_Maple != null);
        }
        return cc_Maple.getForceVector();
    }
}