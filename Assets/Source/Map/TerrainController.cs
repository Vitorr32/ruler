using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerrainController : MonoBehaviour, IPointerClickHandler
{
    public delegate void OnClick(Vector3 worldPoint);
    public static event OnClick onTerrainClicked;

    private Terrain terrain = null;

    // Start is called before the first frame update
    void Start() {
        terrain = GetComponent<Terrain>();
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (eventData.button == PointerEventData.InputButton.Right) {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (terrain.GetComponent<Collider>().Raycast(ray, out hit, Mathf.Infinity)) {
                onTerrainClicked?.Invoke(hit.point);
            }
        }
    }

    public

    // Update is called once per frame
    void Update() {

    }
}
