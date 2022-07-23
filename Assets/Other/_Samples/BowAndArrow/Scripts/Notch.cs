using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(PullMeasurer))]
public class Notch : XRSocketInteractor
{
    [Range(0, 1)] public float releaseThreshold = 0.25f;

    public PullMeasurer PullMeasurer { get; private set; } = null;

    public bool IsReady { get; set; } = false;

    protected override void Awake()
    {
        base.Awake();
        PullMeasurer = GetComponent<PullMeasurer>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        // Arrow is released once the puller is released
        PullMeasurer.selectExited.AddListener(ReleaseArrow);

        // Move the point where the arrow is attached
        PullMeasurer.Pulled.AddListener(MoveAttach);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        PullMeasurer.selectExited.RemoveListener(ReleaseArrow);
        PullMeasurer.Pulled.RemoveListener(MoveAttach);
    }

    public void ReleaseArrow(SelectExitEventArgs args)
    {
        // Only release if the target is an arrow using custom deselect
        if (hasSelection & PullMeasurer.PullAmount > releaseThreshold)
            interactionManager.SelectExit(this, firstInteractableSelected);
    }

    public void MoveAttach(Vector3 pullPosition, float pullAmount)
    {
        // Move attach when bow is pulled, this updates the renderer as well
        attachTransform.position = pullPosition;
    }

    public override bool CanSelect(IXRSelectInteractable interactable)
    {
        // We check for the hover here too, since it factors in the recycle time of the socket
        // We also check that notch is ready, which is set once the bow is picked up
        return QuickSelect(interactable) && CanAndHasHover(interactable) && IsReady;
    }

    private bool QuickSelect(IXRSelectInteractable interactable)
    {
        return !hasSelection || IsSelecting(interactable);
    }

    private bool CanAndHasHover(IXRSelectInteractable interactable)
    {
        if (interactable is IXRHoverInteractable hoverInteractable)
            return CanHover(hoverInteractable);

        return false;
    }
}
