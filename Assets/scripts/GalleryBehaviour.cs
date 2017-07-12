using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryBehaviour : MonoBehaviour {

    [Range(5f, 15f)]
    public float lerpVelocity = 5f;
    [Range(0.01f, 0.1f)]
    public float lerpConstraint = 0.05f;

    private bool swipe = false, canSwitchIndex = true;
    private Vector3 initMousePosition;
    private float initFillAmount;
    private Image[] images;
    private RectTransform thisPanel;
    private int activeImageIndex;
    private KnobPanelBehaviour knobPanel;


    private void Awake() {
        thisPanel = GetComponent<RectTransform>();
        knobPanel = transform.FindChild("KnobPanel").GetComponent<KnobPanelBehaviour>();
        knobPanel.Init(transform.childCount);
        images = new Image[transform.childCount];
        activeImageIndex = transform.childCount - 1;
        int i = 0;
        foreach (Transform t in transform) {
            images[i] = t.GetComponent<Image>();
            i++;
        }
    }

    private void Update() {
        if (swipe) {
            float swipeDistance = Input.mousePosition.x - initMousePosition.x;
            swipeDistance = General.ConvertRange(-thisPanel.rect.width, thisPanel.rect.width, -1f, 1f, swipeDistance);
            swipeDistance = swipeDistance < -1 ? -1 : swipeDistance > 1 ? 1 : swipeDistance;
            if (canSwitchIndex) {
                int aux = activeImageIndex;
                activeImageIndex =
                    swipeDistance < 0 && initFillAmount == 0 ? activeImageIndex - 1
                    : swipeDistance >= 0 && initFillAmount == 1 ? activeImageIndex + 1
                    : activeImageIndex;
                activeImageIndex =
                    activeImageIndex < 0 ? 0
                    : activeImageIndex >= images.Length ? images.Length - 1
                    : activeImageIndex;

                knobPanel.SetIndex(activeImageIndex);
                if (aux != activeImageIndex) {
                    canSwitchIndex = false;
                    initFillAmount = images[activeImageIndex].fillAmount;
                }
            }
            float fillValue = initFillAmount + swipeDistance;
            fillValue = fillValue < 0 ? 0 : fillValue > 1 ? 1 : fillValue;
            images[activeImageIndex].fillAmount = fillValue;
        } else {
            float fillAmount;
            if ((fillAmount = images[activeImageIndex].fillAmount) > 0f && fillAmount < 1f) {
                images[activeImageIndex].fillAmount =
                    Mathf.Lerp(fillAmount, fillAmount < 0.5f ? 0f : 1f, lerpVelocity * Time.deltaTime);
                images[activeImageIndex].fillAmount = images[activeImageIndex].fillAmount < lerpConstraint ? 0f 
                    : images[activeImageIndex].fillAmount > 1 - lerpConstraint ? 1f 
                    : images[activeImageIndex].fillAmount;

            }
        }
    }

    public void onPointerDown() {
        Debug.Log("onPointerDown");
        initMousePosition = Input.mousePosition;
        initFillAmount = images[activeImageIndex].fillAmount;
        swipe = true;
        canSwitchIndex = true;
    }

    public void onPointerUp() {
        Debug.Log("onPointerUp");
        swipe = false;
    }
}
