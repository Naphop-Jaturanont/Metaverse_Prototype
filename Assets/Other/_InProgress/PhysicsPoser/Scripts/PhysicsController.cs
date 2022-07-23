using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PhysicsController : ActionBasedController
{
    public float physicsRange = 0.1f;
    public LayerMask physicsMask = ~0;

    [Range(0, 1)] public float slowDownVelocity = 0.75f;
    [Range(0, 1)] public float slowDownAngularVelocity = 0.75f;

    [Range(0, 100)] public float maxPositionChange = 75.0f;
    [Range(0, 100)] public float maxRotationChange = 75.0f;

    private Rigidbody rigidBody = null;
    private XRDirectInteractor interactor = null;

    private Vector3 TargetPosition => currentControllerState.position;
    private Quaternion TargetRotation => currentControllerState.rotation;

    protected override void Awake()
    {
        base.Awake();
        rigidBody = GetComponent<Rigidbody>();
        interactor = GetComponent<XRDirectInteractor>();
    }

    // TODO: I think we can just try and inject all the info here instead?

    // We can also do the tracking ourselves to simplify that process

    /*
    protected override void UpdateTrackingInput(XRControllerState controllerState)
    {
        base.UpdateTrackingInput(controllerState);

        targetPosition = positionAction.action.ReadValue<Vector3>();
        targetRotation = rotationAction.action.ReadValue<Quaternion>();
    }
    */

    private void FixedUpdate()
    {
        if (IsHoldingObject() || !WithinPhysicsRange())
        {
            MoveUsingTransform();
            RotateUsingTransform();
        }
        else
        {
            MoveUsingPhysics();
            RotateUsingPhysics();
        }
    }

    public bool IsHoldingObject()
    {
        return interactor.hasSelection;
    }

    public bool WithinPhysicsRange()
    {
        return Physics.CheckSphere(transform.position, physicsRange, physicsMask, QueryTriggerInteraction.Ignore);
    }

    private void MoveUsingPhysics()
    {
        rigidBody.velocity *= slowDownVelocity;
        Vector3 velocity = FindNewVelocity();

        if (IsValidVelocity(velocity.x))
        {
            float maxChange = maxPositionChange * Time.deltaTime;
            rigidBody.velocity = Vector3.MoveTowards(rigidBody.velocity, velocity, maxChange);
        }
    }

    private Vector3 FindNewVelocity()
    {
        Vector3 worldPosition = transform.root.TransformPoint(TargetPosition);
        Vector3 difference = worldPosition - rigidBody.position;
        return difference / Time.deltaTime;
    }

    private void RotateUsingPhysics()
    {
        rigidBody.angularVelocity *= slowDownAngularVelocity;
        Vector3 angularVelocity = FindNewAngularVelocity();

        if (IsValidVelocity(angularVelocity.x))
        {
            float maxChange = maxRotationChange * Time.deltaTime;
            rigidBody.angularVelocity = Vector3.MoveTowards(rigidBody.angularVelocity, angularVelocity, maxChange);
        }
    }

    private Vector3 FindNewAngularVelocity()
    {
        Quaternion worldRotation = transform.root.rotation * TargetRotation;
        Quaternion difference = worldRotation * Quaternion.Inverse(rigidBody.rotation);

        difference.ToAngleAxis(out float angleInDegrees, out Vector3 rotationAxis);
        angleInDegrees -= angleInDegrees > 180 ? 360 : 0;

        /*
        if (angleInDegrees > 180)
            angleInDegrees -= 360;
        */

        return (rotationAxis * angleInDegrees * Mathf.Deg2Rad) / Time.deltaTime;
    }

    private bool IsValidVelocity(float value)
    {
        return !float.IsNaN(value) && !float.IsInfinity(value);
    }

    private void MoveUsingTransform()
    {
        rigidBody.velocity = Vector3.zero;
        transform.localPosition = TargetPosition;
    }

    private void RotateUsingTransform()
    {
        rigidBody.angularVelocity = Vector3.zero;
        transform.localRotation = TargetRotation;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, physicsRange);
    }

    private void OnValidate()
    {
        if (TryGetComponent(out Rigidbody rigidBody))
            rigidBody.useGravity = false;
    }
}
