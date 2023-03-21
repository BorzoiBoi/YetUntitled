using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCamera : MonoBehaviour
{
    [Tooltip("The rotation acceleration, in degrees/second")]
    [SerializeField] private Vector2 acceleration;
    [Tooltip("An input multiplier. Describes max speed in degrees")]
    [SerializeField] private Vector2 sensitivity;
    [Tooltip("The period to wait until resetting the input value. (Too low value will cause some stuttering)")]
    [SerializeField] private float inputLagPeriod;

    private Vector2 velocity;
    private Vector2 rotation;
    private Vector2 lastInputEvent;
    private float inputLagTimer;

    private Vector2 GetInput()
    {
        inputLagTimer += Time.deltaTime;
        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
            );

        if ((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod)
        {
            lastInputEvent = input;
            inputLagTimer = 0;
        }

        return lastInputEvent;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 wantedVelocity = GetInput() * sensitivity;

        velocity = new Vector2(
            Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime),
            Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.deltaTime)
        );
        rotation += velocity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(rotation.y, rotation.x, 0);
    }
}
