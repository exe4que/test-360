using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour {

    public Gradient colorGradient;
    [Range(3f,10f)]
    public float lerpVelocity = 5f;
    private float value = 0f;
    MeshRenderer renderer;
    GameObject text;

    private void Awake() {
        renderer = GetComponent<MeshRenderer>();
        text = transform.GetChild(0).gameObject;
        text.SetActive(false);
    }


    private void Update() {
        renderer.material.color = colorGradient.Evaluate(value);
        value = Mathf.Lerp(value, 0f, lerpVelocity * Time.deltaTime);
    }

    public void onHover() {
        value = 1f;
    }

    public void onClick() {
        text.SetActive(!text.activeSelf);
    }
}
