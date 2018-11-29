using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    private float movementSpeed;

    [SerializeField] private float walkSpeed, runSpeed;
    [SerializeField] private float runBuildUpSpeed;
    [SerializeField] private KeyCode runKey;

    private CharacterController charController;

    public bool activateTrigger;
    public Text collectableText;
    public int collected;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();

    }

    private void Update()
    {
        PlayerMovement();
       
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 1;

        if (Physics.Raycast(transform.position, (forward), out hit, 3))
        {
            if (hit.collider.gameObject.tag == "CollectableItem" && Input.GetKeyDown(KeyCode.E))
            {
                hit.collider.gameObject.SetActive(false);

                collected = collected + 1;

                collectableText.text = ("Objects Collected: " + collected + "/20");

                activateTrigger = false;

            }
        }
    }

    private void PlayerMovement()
    {
        float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        charController.SimpleMove(forwardMovement + rightMovement);

        SetMovementSpeed();

    }

    private void SetMovementSpeed()
    {

        if (Input.GetKey(runKey))
        {
            movementSpeed = Mathf.Lerp(movementSpeed, runSpeed, Time.deltaTime * runBuildUpSpeed);
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, walkSpeed, Time.deltaTime * runBuildUpSpeed);
        }
    }

}
