using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour2 : MonoBehaviour {

    public GameObject sphere;
    public Material[] materials;
    public Gradient colorGradient;
    [Range(3f,10f)]
    public float lerpVelocity = 5f;
    private float value = 0f;
    private MeshRenderer renderer;
    private int matIndex = 0;

    private void Awake() {
        renderer = GetComponent<MeshRenderer>();
    }


    private void Update() {
        renderer.material.color = colorGradient.Evaluate(value);
        value = Mathf.Lerp(value, 0f, lerpVelocity * Time.deltaTime);
    }

    public void onHover() {
        value = 1f;
    }

    public void onClick() {
        matIndex = matIndex < materials.Length - 1 ? matIndex + 1 : 0;
        sphere.GetComponent<MeshRenderer>().material = materials[matIndex];
    }
}
