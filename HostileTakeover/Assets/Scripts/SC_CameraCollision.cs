using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_CameraCollision : MonoBehaviour
{
    public Transform referenceTransform;
    public float collisionOffset = 0.2f; //To prevent Camera from clipping through Objects

    Vector3 defaultPos;
    Vector3 directionNormalized;
    Transform parentTransform;
    float defaultDistance;

    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.localPosition;
        directionNormalized = defaultPos.normalized;
        parentTransform = transform.parent;
        defaultDistance = Vector3.Distance(defaultPos, Vector3.zero);
    }

    // FixedUpdate for physics calculations
    void FixedUpdate()
    {
        if(parentTransform != null)
        {
            Vector3 currentPos = defaultPos;
            RaycastHit hit;
            Vector3 dirTmp = parentTransform.TransformPoint(defaultPos) - referenceTransform.position;
            if (Physics.SphereCast(referenceTransform.position, collisionOffset, dirTmp, out hit, defaultDistance))
            {
                currentPos = (directionNormalized * (hit.distance - collisionOffset));
            }

            transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * 15f);
        }
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = Mathf.Clamp(transform.position.y, 0.5f, 400);
        transform.position = pos;
    }

    public Quaternion GetTilt()
    {
        return this.transform.rotation;
    }
}
