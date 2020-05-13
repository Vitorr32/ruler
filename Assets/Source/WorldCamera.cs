using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCamera : MonoBehaviour
{

    private Camera m_Camera;

    //Velocity in which the camera is moving in the current tick
    private Vector3 m_CameraVelocity = new Vector3();
    //Rotation in which the camera is moving in the current tick
    private Vector3 m_CameraRotation = new Vector3();

    //Setters for camera defaults
    [SerializeField] private float m_MouseSensitivity = 500.0f;
    [SerializeField] private float m_MaxCameraZoom = 50.0f;
    [SerializeField] private float m_MinCameraZoom = 5.0f;
    [SerializeField] private float m_MaxCameraPitch = 90.0f;
    [SerializeField] private float m_MinCameraPitch = 30.0f;
    //Distance from the border of the terrain that the camera will stop moving
    [SerializeField] private float m_OffsetFromBorder = 30.0f;
    //Distance between the camera and the height of the terrain where the camera is located
    [SerializeField] private float m_OffsetFromGround = 30.0f;
    //Setters for camera maximum moviment in the cartesian X/Y axis, they depend on the terrain size to be calculated
    private float m_MaxCameraXAxis;
    private float m_MinCameraXAxis;
    private float m_MaxCameraZAxis;
    private float m_MinCameraZAxis;

    //The terrain in which this camera is restricted to look, the camera will not go beyond it's borders
    [SerializeField] private Terrain terrain;
    //The gameobject in which the camera is currently following along.
    [SerializeField] private GameObject target;

    // Start is called before the first frame update
    void Start() {
        m_Camera = GetComponent<Camera>();

        startUpTerrainCorners();
        snapCameraToCenterOfTerrain();
    }

    // Update is called once per frame
    void Update() {
        m_CameraVelocity = new Vector3();
        m_CameraRotation = new Vector3();

        //Update the camera position and rotation in relation to the Mouse scroll input
        updateCameraZoom(Input.GetAxis("Mouse ScrollWheel"));

        //After all camera position/rotation processing, assing new values to the transform of the camera
        updateCameraPosition();
        updateCameraRotation();
    }

    private void snapCameraToCenterOfTerrain() {
        float xPosition = (m_MinCameraXAxis + m_MaxCameraXAxis) / 2;
        float zPosition = (m_MinCameraZAxis + m_MaxCameraZAxis) / 2;

        transform.position = new Vector3(xPosition, m_MaxCameraZoom, zPosition);
    }

    private void startUpTerrainCorners() {
        m_MinCameraXAxis = terrain.transform.position.x;
        m_MaxCameraXAxis = m_MinCameraXAxis + terrain.terrainData.size.x;

        m_MinCameraZAxis = terrain.transform.position.z;
        m_MaxCameraZAxis = m_MinCameraZAxis + terrain.terrainData.size.z;
    }

    private void updateCameraZoom(float axis) {
        if (axis != 0) {
            m_CameraVelocity.y = m_CameraVelocity.y + (axis * m_MouseSensitivity);
        }
    }

    private void updateCameraPosition() {
        //Get new position based in the current tick movement
        Vector3 newPosition = transform.position + (m_CameraVelocity * Time.deltaTime);

        //Clamp the values of the axis so no overflow occurs, add offset to the minimum border and decrease if from the maximum border
        newPosition.x = Mathf.Clamp(newPosition.x, m_MinCameraXAxis + m_OffsetFromBorder, m_MaxCameraXAxis - m_OffsetFromBorder);
        newPosition.y = Mathf.Clamp(newPosition.y, Mathf.Max(m_MinCameraZoom, terrain.SampleHeight(newPosition) + m_OffsetFromGround), m_MaxCameraZoom);
        newPosition.z = Mathf.Clamp(newPosition.z, m_MinCameraZAxis + m_OffsetFromBorder, m_MaxCameraZAxis - m_OffsetFromBorder);

        transform.position = newPosition;
    }
    private void updateCameraRotation() {
        Vector3 newRotation = transform.rotation.eulerAngles + (m_CameraRotation * Time.deltaTime);

        newRotation.x = Mathf.Clamp(newRotation.x, m_MinCameraPitch, m_MaxCameraPitch);

        transform.rotation = Quaternion.Euler(newRotation);
    }
}
