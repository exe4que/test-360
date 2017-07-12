using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnobPanelBehaviour : MonoBehaviour {

    public GameObject knobPrefab;
    public Color highlightColor = Color.cyan;

    private Image[] knobs;

    public void Init(int _knobQty) {
        knobs = new Image[_knobQty];
        for (int i = 0; i < _knobQty; i++) {
            GameObject aux = Instantiate(knobPrefab);
            aux.transform.SetParent(transform);
            knobs[i] = aux.GetComponent<Image>();
        }
    }

    public void SetIndex(int _index) {
        for (int i = 0; i < knobs.Length; i++) {
            Color c = _index == i ? highlightColor : Color.white;
            knobs[i].color = c;
        }
    }
}
