using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryBehaviour : MonoBehaviour {

    [Range(3f,10f)]
    public float lerpVelocity = 5f;

    private bool swipe = false;
    private Vector3 initMousePosition;
    private Image[] images;
    private RectTransform thisPanel;
    private int activeImageIndex;


    private void Awake() {
        thisPanel = GetComponent<RectTransform>();
        images = new Image[transform.childCount];
        activeImageIndex = transform.childCount - 1;
        int i = 0;
        foreach (Transform t in transform) {
            images[i] = t.GetComponent<Image>();
            i++;
        }

    }

    private void Update () {
        if (swipe) {
            float swipeDistance = Input.mousePosition.x - initMousePosition.x;
            swipeDistance = General.ConvertRange(-thisPanel.rect.width, thisPanel.rect.width, -1f, 1f, swipeDistance);
            swipeDistance = swipeDistance < -1 ? -1 : swipeDistance > 1 ? 1 : swipeDistance;
            float fillValue = 1 + swipeDistance;
            fillValue = fillValue < 0 ? 0 : fillValue > 1 ? 1 : fillValue;
            images[activeImageIndex].fillAmount = fillValue;
        } else {
            if (images[activeImageIndex].fillAmount > 0f && images[activeImageIndex].fillAmount < 1f) {
                images[activeImageIndex].fillAmount = 
                    Mathf.Lerp(images[activeImageIndex].fillAmount, images[activeImageIndex].fillAmount < 0.5f ? 0f : 1f, lerpVelocity * Time.deltaTime);
            }
        }
	}

    public void onPointerDown() {
        Debug.Log("onPointerDown");
        swipe = true;
        initMousePosition = Input.mousePosition;
    }

    public void onPointerUp() {
        Debug.Log("onPointerUp");
        swipe = false;
    }
}
