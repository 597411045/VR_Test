using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAV_LegSlotScript : MonoBehaviour
{
    public static int LeftAttachedCount;
    public static int RightAttachedCount;
    Transform UAV_Leg;
    public bool isAttached;
    public bool isAttachAble;
    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && isAttachAble)
        {
            UAV_Leg.GetComponent<Rigidbody>().useGravity = false;
            UAV_Leg.SetParent(this.transform.parent.parent);
            UAV_Leg.localPosition = Vector3.zero;
            if (this.name.Contains("Right"))
            {
                UAV_Leg.rotation = this.transform.parent.parent.rotation;
            }
            if (this.name.Contains("Left"))
            {
                UAV_Leg.rotation = this.transform.parent.parent.rotation*Quaternion.AngleAxis(180,Vector3.up);
            }
            UAV_Leg.SetParent(null);
            FixedJoint tmp = UAV_Leg.gameObject.AddComponent<FixedJoint>();
            tmp.connectedBody = this.transform.parent.parent.GetComponent<Rigidbody>();
            this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
            isAttached = true;
            isAttachAble = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (isAttached) return;

        if (!other.gameObject.name.Contains("Leg")) return;
        UAV_Leg = other.transform;
        
        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(1, 0, 0, 0));
        isAttachAble = true;
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0, 0, 0, 0));
    }
}
