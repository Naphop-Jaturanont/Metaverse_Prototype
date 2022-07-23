using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SteeringWheel : XRBaseInteractable
{
    [SerializeField] private Transform wheelTransform;

    private float currentAngle = 0.0f;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        currentAngle = FindWheelAngle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        currentAngle = FindWheelAngle();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
                RotateWheel();
        }
    }

    private void RotateWheel()
    {
        // Convert that direction to an angle, then rotation
        float newAngle = FindWheelAngle();

        // Apply difference in angle to wheel
        float angleDifference = currentAngle - newAngle;
        wheelTransform.Rotate(transform.forward, -angleDifference);
            
        // Store angle for next process
        currentAngle = newAngle;
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0;

        // Combine directions of current interactors
        foreach (IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindDirection(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }

        return totalAngle;
    }

    private Vector2 FindDirection(Vector3 position)
    {
        // Convert the hand positions to local, so we can find the angle easier
        return transform.InverseTransformPoint(position).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        // TODO: Maybe we do transform.up?
        return Vector2.SignedAngle(Vector2.up, direction);
    }

    private float FindRotationSensitivity()
    {
        return 1.0f / interactorsSelecting.Count;
    }
}
