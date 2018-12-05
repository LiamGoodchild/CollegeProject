using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour {

    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform playerBody;

    private float xAxisClamp;

    public bool activateTrigger;
    public Text collectableText;
    public Text PressE;
    public int collected;

    public bool hasPickedUp;

    public void Start()
    {
        PressE.text = ("");
    }

    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
    }


    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        CameraRotation();

        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 1.5f;

        if (Physics.Raycast(transform.position, (forward), out hit, 3))
        {
            if (hit.collider.gameObject.tag == "CollectableItem" && Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.gameObject.SetActive(false);

                collected = collected + 1;

                collectableText.text = ("Objects Collected: " + collected + "/20");

                activateTrigger = false;

                hasPickedUp = true;

                PressE.text = ("");

            } else if(hit.collider.gameObject.tag == "CollectableItem" && hasPickedUp == false)
            {
                PressE.text = ("Press 'E' To Pick Up");

            } else if (hit.collider.gameObject.tag != "CollectableItem" && hasPickedUp == false)
            {
                PressE.text = ("");
            }
        }

        if (collected == 20)
        {
            //
        }

    }

    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

}
