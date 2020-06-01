using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResponseController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject responsePrefab;

    public void ShowResponse(string response) {
        gameObject.SetActive(true);

        GameObject instantiatedResponse = Instantiate(responsePrefab, transform);

        instantiatedResponse.GetComponent<Text>().text = response;
    }
}
