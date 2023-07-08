using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ColliderData
{
    public GameObject gameObject;
    private Bounds bounds;
    public Bounds Bounds
    {
        get
        {
            bounds.center = gameObject.transform.position;
            return bounds;
        }
        set
        {
            bounds = value;
        }
    }

    public ColliderData(Collider collider)
    {
        gameObject = collider.gameObject;
        bounds = collider.bounds;
    }

    public ColliderData(Collider2D collider)
    {
        gameObject = collider.gameObject;
        bounds = collider.bounds;
    }
}
