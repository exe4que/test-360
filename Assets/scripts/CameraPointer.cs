using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPointer : MonoBehaviour {

    Transform target;

    RaycastHit hit;
    void Update() {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Physics.Raycast(this.transform.position, fwd, out hit);
        if (hit.collider != null && hit.transform.tag.Equals("Target")) {
            target = hit.transform;
            target.SendMessage("onHover");
        } else {
            target = null;
        }

        if (Input.GetMouseButtonDown(0)) {
            if (target != null) {
                target.SendMessage("onClick");
            }

        }
    }
}
