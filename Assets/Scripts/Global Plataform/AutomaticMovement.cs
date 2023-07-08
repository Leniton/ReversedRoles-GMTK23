using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomaticMovement : MonoBehaviour
{
    [SerializeField] Plataform_Script plataform;
    [SerializeField] bool moving = false;
    [SerializeField] float maxEdgeDistance = 1;
    ColliderData plataformData;

    void Awake()
    {
        if (plataform) plataform.GetComponent<Plataform_Script>();
        plataform.input.x = moving ? 1 : 0;

        plataform.physicsHandler.CollisionEnter += CheckPlataformBounds;
        plataform.physicsHandler.CollisionExit += RemovePlataformData;
    }

    private void OnDestroy()
    {
        plataform.physicsHandler.CollisionEnter -= CheckPlataformBounds;
        plataform.physicsHandler.CollisionExit -= RemovePlataformData;
    }

    void Update()
    {
        if(moving && plataformData.gameObject)
        {
            if (plataform.input.x == 0) plataform.input.x = 1;
            float distanceFromCenter = Vector3.Distance(transform.position, plataformData.Bounds.center);
            float distanceFromEdge = plataformData.Bounds.extents.x - distanceFromCenter;
            if (distanceFromEdge < maxEdgeDistance)
            {
                plataform.input.x = -Mathf.Sign(transform.position.x - plataformData.Bounds.center.x);
            }
        }
        else
        {
            plataform.input.x = 0;
        }
    }

    void CheckPlataformBounds(CollisionData data)
    {
        if (data.collider.gameObject.CompareTag("Chão"))
        {
            plataformData = data.collider;
        }
    }

    void RemovePlataformData(CollisionData data)
    {
        if (data.collider.gameObject.CompareTag("Chão"))
        {
            plataformData = default;
        }
    }
}
