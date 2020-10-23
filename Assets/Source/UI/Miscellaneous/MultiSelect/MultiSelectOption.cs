using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiSelectOption : MonoBehaviour
{
    public int value;
    public string label;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void setOption(int value, string label) {
        this.value = value;
        this.label = label;

        this.gameObject.SetActive(true);
    }

    public void OnDisable() {
        this.value = -1;
        this.label = null;
    }
}
