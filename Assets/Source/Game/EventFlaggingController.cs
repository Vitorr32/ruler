using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFlaggingController : MonoBehaviour
{
    public static EventFlaggingController instance;
    // Start is called before the first frame update
    void Start() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
