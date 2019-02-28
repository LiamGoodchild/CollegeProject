using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public Text YouWin;
    public Text ObjectToCollect;
    public int collected;
    public int collectedcounter;

    public bool hasPickedUp;

    List<string> shuffledItems;

    public void Start()
    {

        PressE.text = ("");
        YouWin.text = ("");

        List<string> collectableitems = new List<string>();

        collectableitems.Add("Old Telephone");
        collectableitems.Add("Modern Phone");
        collectableitems.Add("Laptop");
        collectableitems.Add("Book");
        collectableitems.Add("Blood Stained Weapon");
        collectableitems.Add("Paper");
        collectableitems.Add("Radio");
        collectableitems.Add("Flashlight");
        collectableitems.Add("Key");

        shuffledItems = collectableitems.OrderBy(x => Random.value).ToList();

        ObjectToCollect.text = ("Object To Collect: " + shuffledItems[0]);

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
            if (hit.collider.gameObject.tag == "CollectableItem" && hit.collider.gameObject.name == shuffledItems[collectedcounter] && Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.gameObject.SetActive(false);

                collected = collected + 1;

                collectableText.text = ("Objects Collected: " + collected + "/9");

                activateTrigger = false;

                hasPickedUp = true;

                PressE.text = ("");

                collectedcounter = collectedcounter + 1;

                ObjectToCollect.text = ("Object To Collect: " + shuffledItems[collectedcounter]);

            } else if(hit.collider.gameObject.tag == "CollectableItem" && hasPickedUp == false)
            {
                PressE.text = ("Press 'E' To Pick Up");

            } else if (hit.collider.gameObject.tag != "CollectableItem" && hasPickedUp == false)
            {
                PressE.text = ("");
            }
        }

        if (collected == 9)
        {
            YouWin.text = ("YOU WIN");
            Time.timeScale = 0.000001f;
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
