using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryBehaviour : MonoBehaviour {

    [Range(5f, 15f)]
    public float lerpVelocity = 10f;
    [Range(0.01f, 0.1f)]
    public float lerpConstraint = 0.07f;
    [Range(0.1f, 0.9f)]
    public float minimumSwipe = 0.2f;

    private bool swipe = false, canSwitchIndex = true;
    private Vector3 initMousePosition;
    private float initFillAmount, swipeDistance;
    private Image[] images;
    private RectTransform mainPanelRecttransform;
    private Transform mainPanel;
    private int activeImageIndex;
    private KnobPanelBehaviour knobPanel;


    private void Awake() {
        mainPanel = transform.FindChild("MainPanel");
        mainPanelRecttransform = mainPanel.GetComponent<RectTransform>();
        knobPanel = transform.FindChild("KnobPanel").GetComponent<KnobPanelBehaviour>();
        knobPanel.Init(mainPanel.childCount + 1);
        images = new Image[mainPanel.childCount];
        activeImageIndex = mainPanel.childCount - 1;
        int i = 0;

        foreach (Transform t in mainPanel) {
            images[i] = t.GetComponent<Image>();
            i++;
        }
    }

    private void Start() {
        UpdateKnobPanel();
    }

    private void Update() {
        if (swipe) {
            swipeDistance = Input.mousePosition.x - initMousePosition.x;
            swipeDistance = General.ConvertRange(-mainPanelRecttransform.rect.width, mainPanelRecttransform.rect.width, -1f, 1f, swipeDistance);
            swipeDistance = swipeDistance < -1 ? -1 : swipeDistance > 1 ? 1 : swipeDistance;
            if (canSwitchIndex) {
                if (swipeDistance > 0 && initFillAmount == 1) {
                    activeImageIndex = activeImageIndex < images.Length - 1 ? activeImageIndex + 1 : activeImageIndex;
                    initFillAmount = 0;
                    canSwitchIndex = false;
                }
            }
            float fillValue = initFillAmount + swipeDistance;
            fillValue = fillValue < 0 ? 0 : fillValue > 1 ? 1 : fillValue;
            if (knobPanel.GetIndex() == 0 && swipeDistance > 0) {
                fillValue = 1;
            }
            //Debug.Log("activeImageIndex = " + activeImageIndex + " - fillValue (" + fillValue + ") = initFillAmount (" + initFillAmount + ") + swipeDistance (" + swipeDistance + ")");
            images[activeImageIndex].fillAmount = fillValue;
        } else {
            float fillAmount;
            if ((fillAmount = images[activeImageIndex].fillAmount) > 0f && fillAmount < 1f) {
                float target = swipeDistance > minimumSwipe || (swipeDistance > -minimumSwipe && swipeDistance <= 0f) ? 1f : 0f;

                images[activeImageIndex].fillAmount = Mathf.Lerp(fillAmount, target, lerpVelocity * Time.deltaTime);
                if (images[activeImageIndex].fillAmount < lerpConstraint) {
                    images[activeImageIndex].fillAmount = 0f;
                    UpdateKnobPanel();
                }
                if (images[activeImageIndex].fillAmount > 1 - lerpConstraint) {
                    images[activeImageIndex].fillAmount = 1f;
                    UpdateKnobPanel();
                }
            }
        }
    }

    private void UpdateKnobPanel() {
        for (int i = images.Length - 1; i >= 0; i--) {
            if (images[i].fillAmount == 1) {
                knobPanel.SetIndex(images.Length - (i + 1));
                activeImageIndex = i;
                return;
            }
        }
        knobPanel.SetIndex(images.Length);
    }

    public void onPointerDown() {
        //Debug.Log("onPointerDown");
        initMousePosition = Input.mousePosition;
        initFillAmount = images[activeImageIndex].fillAmount;
        Debug.Log("initFillAmount = " + initFillAmount);
        swipe = true;
        canSwitchIndex = true;
    }

    public void onPointerUp() {
        //Debug.Log("onPointerUp");
        swipe = false;
    }
}
