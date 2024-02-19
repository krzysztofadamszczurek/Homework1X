using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] GameObject wheelFL;
    [SerializeField] GameObject wheelFR;
    [SerializeField] GameObject wheelBL;
    [SerializeField] GameObject wheelBR;

    [SerializeField] float wheelFLSpeed;
    [SerializeField] float wheelFRSpeed;
    [SerializeField] float wheelBLSpeed;
    [SerializeField] float wheelBRSpeed;

    public float forwardInput;
    public float backwardInput;
    public float leftInput;
    public float rightInput;
    public float rotateLeftInput;
    public float rotateRightInput;

    Controls controls;

    private void Awake()
    {
        controls = new Controls();
        controls.Movement.Forward.started += ctx => { forwardInput = 1; };
        controls.Movement.Forward.canceled += ctx => { forwardInput = 0; };
        controls.Movement.Backward.started += ctx => { backwardInput = 1; };
        controls.Movement.Backward.canceled += ctx => { backwardInput = 0; };
        controls.Movement.Left.started += ctx => { leftInput = 1; };
        controls.Movement.Left.canceled += ctx => { leftInput = 0; };
        controls.Movement.Right.started += ctx => { rightInput = 1; };
        controls.Movement.Right.canceled += ctx => { rightInput = 0; };
        controls.Movement.RotateLeft.started += ctx => { rotateLeftInput = 1; };
        controls.Movement.RotateLeft.canceled += ctx => { rotateLeftInput = 0; };
        controls.Movement.RotateRight.started += ctx => { rotateRightInput = 1; };
        controls.Movement.RotateRight.canceled += ctx => { rotateRightInput = 0; };
    }

    private void Start()
    {
        controls.Movement.Enable();
    }
    void Update()
    {
        wheelFLSpeed = -forwardInput + backwardInput + leftInput - rightInput + rotateLeftInput - rotateRightInput;
        wheelFL.transform.Rotate(new Vector3(0, 0, -1), wheelFLSpeed / 2);
        wheelFRSpeed = -forwardInput + backwardInput - leftInput + rightInput - rotateLeftInput + rotateRightInput;
        wheelFR.transform.Rotate(new Vector3(0, 0, -1), wheelFRSpeed / 2);

        wheelBLSpeed = -forwardInput + backwardInput - leftInput + rightInput + rotateLeftInput - rotateRightInput;
        wheelBL.transform.Rotate(new Vector3(0, 0, -1), wheelBLSpeed / 2);

        wheelBRSpeed = -forwardInput + backwardInput + leftInput - rightInput - rotateLeftInput + rotateRightInput;
        wheelBR.transform.Rotate(new Vector3(0, 0, -1), wheelBRSpeed / 2);

        transform.Translate(new Vector3(-(forwardInput-backwardInput) * Time.deltaTime, 0, (rightInput - leftInput) * Time.deltaTime));
        double deltaRotation = Time.deltaTime * 200 / (2 * 3.14);
        transform.Rotate(new Vector3(0, 1, 0), (float)deltaRotation * (-rotateLeftInput + rotateRightInput));
    }
}
