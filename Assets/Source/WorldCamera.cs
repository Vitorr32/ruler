﻿using System;
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
    [SerializeField] private float m_ZoomSensitivity = 500.0f;
    [SerializeField] private float m_CameraSensitivity = 500.0f;
    [SerializeField] private float m_MaxCameraZoom = 50.0f;
    [SerializeField] private float m_MinCameraZoom = 5.0f;
    [SerializeField] private float m_MaxCameraPitch = 90.0f;
    [SerializeField] private float m_MinCameraPitch = 30.0f;
    //Distance from the border of the terrain that the camera will stop moving
    [SerializeField] private float m_OffsetFromBorder = 30.0f;
    //Distance between the camera and the height of the terrain where the camera is located
    [SerializeField] private float m_OffsetFromGround = 30.0f;
    //Setters for camera maximum moviment in the cartesian X/Y axis, they depend on the terrain size to be calculated
    private Vector3 m_MaxCameraBorders;
    private Vector3 m_MinCameraBorders;

    //The terrain in which this camera is restricted to look, the camera will not go beyond it's borders
    [SerializeField] private Terrain terrain = null;
    //The gameobject in which the camera is currently following along.
    [SerializeField] private GameObject target = null;

    // Start is called before the first frame update
    void Start() {
        m_Camera = GetComponent<Camera>();

        if (!terrain) {
            Debug.Log("No terrain reference set, search for one in gameobject");
            terrain = FindObjectOfType<Terrain>();
        }

        startUpTerrainCorners();
        snapCameraToCenterOfTerrain();
    }

    // Update is called once per frame
    void LateUpdate() {
        m_CameraVelocity = Vector3.zero;
        m_CameraRotation = Vector3.zero;

        //Update the camera position and rotation in relation to the Mouse scroll input
        updateCameraZoom(Input.GetAxis("Mouse ScrollWheel"));
        //Use the WASD and Arrow Keys mapping to move the camera
        navigateAxisX(Input.GetAxis("Horizontal"));
        navigateAxisZ(Input.GetAxis("Vertical"));

        //After all camera position/rotation processing, assing new values to the transform of the camera
        updateCameraPosition();
        updateCameraRotation();
    }

    private void snapCameraToCenterOfTerrain() {
        float xPosition = (m_MinCameraBorders.x + m_MaxCameraBorders.x) / 2;
        float zPosition = (m_MinCameraBorders.z + m_MaxCameraBorders.z) / 2;

        transform.position = new Vector3(xPosition, m_MaxCameraBorders.y, zPosition);
        transform.eulerAngles = new Vector3(m_MaxCameraPitch, 0.0f, 0.0f);
    }

    private void startUpTerrainCorners() {
        m_MinCameraBorders = new Vector3(terrain.transform.position.x, m_MinCameraZoom, terrain.transform.position.z);
        m_MaxCameraBorders = new Vector3(m_MinCameraBorders.x + terrain.terrainData.size.x, m_MaxCameraZoom, m_MinCameraBorders.z + terrain.terrainData.size.z);
    }

    private void updateCameraZoom(float axis) {
        if (axis != 0) {
            target = null;
            //The axis values is inverted so that mouse scroll up zoom in, and mouse scroll down will zoom out
            m_CameraVelocity.y += -axis * m_ZoomSensitivity;
        }
    }
    private void navigateAxisX(float axis) {
        if (axis != 0) {
            target = null;
            m_CameraVelocity.x = m_CameraVelocity.x + (axis * m_CameraSensitivity);
        }
    }
    private void navigateAxisZ(float axis) {
        if (axis != 0) {
            target = null;
            m_CameraVelocity.z = m_CameraVelocity.z + (axis * m_CameraSensitivity);
        }
    }
    private float calculatePitchRotationByHeight(float height, float minHeight, float maxHeight) {
        //Get the percentage of height from the max by decreasing both from the minheight, then dividing the current by the maximum
        float percentageOfHeight = (height - minHeight) / (maxHeight - minHeight);
        float newPitchTarget = (m_MaxCameraPitch - m_MinCameraPitch) * percentageOfHeight;

        return newPitchTarget + m_MinCameraPitch;
    }

    private void updateCameraPosition() {
        if (m_CameraVelocity == Vector3.zero) { return; }
        //Get new position based in the current tick movement
        Vector3 newPosition = transform.position + (m_CameraVelocity * Time.deltaTime);

        /**
         * Clamp the values of the axis so the camera position does not overflows from the terrain area
         * Also, add the offset to the minimum borders, so the user can't see the end of the terrain
         * Decrease the maximum borders by a offset too, for the same motive
         */
        newPosition.x = Mathf.Clamp(newPosition.x, m_MinCameraBorders.x + m_OffsetFromBorder, m_MaxCameraBorders.x - m_OffsetFromBorder);
        //Get the Height from the current position in comparation to the terrain sampe height, so the camera does not clip trough the terrain
        newPosition.y = Mathf.Clamp(newPosition.y, Mathf.Max(m_MinCameraZoom, terrain.SampleHeight(newPosition) + m_OffsetFromGround), m_MaxCameraZoom);
        newPosition.z = Mathf.Clamp(newPosition.z, m_MinCameraBorders.z + m_OffsetFromBorder, m_MaxCameraBorders.z - m_OffsetFromBorder);

        transform.position = newPosition;
    }
    private void updateCameraRotation() {
        Vector3 newRotation = m_CameraRotation * Time.deltaTime;

        if (!IsCameraOnTheGround()) {
            newRotation.x = Mathf.Clamp(
                calculatePitchRotationByHeight(transform.position.y, m_MinCameraBorders.y, m_MaxCameraBorders.y),
                m_MinCameraPitch,
                m_MaxCameraPitch
            );
        }

        transform.eulerAngles = newRotation;
    }

    private bool IsCameraOnTheGround() {
        return m_MinCameraZoom == terrain.SampleHeight(transform.position) + m_OffsetFromGround;
    }
}
