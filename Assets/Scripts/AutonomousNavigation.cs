using System.Collections;
using UnityEngine;

public class AutonomousNavigation : MonoBehaviour
{
    public Vector2 TargetPosition;
    public float TargetRotation;

    [SerializeField] GameObject robot;

    Movement movementScript;

    void Start()
    {
        movementScript = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 200, 30), new GUIContent("Generate and follow next target")))
        {
            TargetPosition = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            TargetRotation = Random.Range(0, 359);
            StopMovement();
            StopCoroutine(nameof(FollowTarget));
            StartCoroutine(nameof(FollowTarget));
        }
        GUI.Label(new Rect(10, 50, 1200, 60), "Target: " + TargetPosition.ToString() + ", " + TargetRotation + "deg" 
            + " , current robot position: " + "" + robot.transform.position.x + ", " + robot.transform.position.z 
            + ", " + robot.transform.rotation.eulerAngles.y + "deg");

        GUI.Label(new Rect(10, 120, 1000, 60), "To move manually, use WSADQE");

    }

    IEnumerator FollowTarget()
    {
        var robotCurrentRotation = robot.transform.rotation.eulerAngles.y;
        var rotationDifference = Mathf.Abs(0 - robotCurrentRotation);
        while (true)
        {
            robotCurrentRotation = robot.transform.rotation.eulerAngles.y;
            rotationDifference = Mathf.Abs(0 - robotCurrentRotation);
            if (rotationDifference < 1)
            {
                break;
            }
            movementScript.rotateLeftInput = 1;
            yield return new WaitForSeconds(.02f);
        }
        movementScript.rotateLeftInput = 0;


        Vector2 robotCurrentPosition = new Vector2(robot.transform.position.x, robot.transform.position.z);
        float distance = (TargetPosition - robotCurrentPosition).magnitude;
        while (true)
        {
            robotCurrentPosition = new Vector2(robot.transform.position.x, robot.transform.position.z);
            if (TargetPosition.x - robotCurrentPosition.x > 0.01)
            {
                movementScript.backwardInput = 1;
                movementScript.forwardInput = 0;
            }
            else if (TargetPosition.x - robotCurrentPosition.x < -0.01)
            {
                movementScript.backwardInput = 0;
                movementScript.forwardInput = 1;
            }
            else
            {
                movementScript.backwardInput = 0;
                movementScript.forwardInput = 0;
            }

            if (TargetPosition.y - robotCurrentPosition.y > 0.01)
            {
                movementScript.leftInput = 0;
                movementScript.rightInput = 1;
            }
            else if (TargetPosition.y - robotCurrentPosition.y < -0.01)
            {
                movementScript.leftInput = 1;
                movementScript.rightInput = 0;
            }
            else
            {
                movementScript.leftInput = 0;
                movementScript.rightInput = 0;
            }
            distance = (TargetPosition - robotCurrentPosition).magnitude;
            if (distance <= 0.01)
            {
                break;
            }
            yield return new WaitForSeconds(.02f);
        }

        robotCurrentRotation = robot.transform.rotation.eulerAngles.y;
        rotationDifference = Mathf.Abs(TargetRotation - robotCurrentRotation);
        while (true)
        {
            robotCurrentRotation = robot.transform.rotation.eulerAngles.y;
            rotationDifference = Mathf.Abs(TargetRotation - robotCurrentRotation);
            if (rotationDifference < 1)
            {
                break;
            }
            movementScript.rotateLeftInput = 1;
            yield return new WaitForSeconds(.02f);
        }
        movementScript.rotateLeftInput = 0;
    }

    void StopMovement()
    {
        movementScript.backwardInput = 0;
        movementScript.forwardInput = 0;
        movementScript.leftInput = 0;
        movementScript.rightInput = 0;
        movementScript.rotateLeftInput = 0;
        movementScript.rotateRightInput = 0;
    }
}
