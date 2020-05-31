using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject responsePrefab;
    void Start() {
        gameObject.SetActive(false);
    }

    public void ShowResponse(string response) {
        Debug.Log(response);
        gameObject.SetActive(true);

        GameObject instantiatedResponse = Instantiate(responsePrefab, transform);

        instantiatedResponse.GetComponent<Text>().text = response;
    }
}
