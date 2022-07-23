using UnityEngine.XR.Interaction.Toolkit;

public class OffsetInteractable : XRGrabInteractable
{
    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        base.OnSelectEntering(args);
        // MatchAttachmentPoints(args.interactorObject);
    }

    protected void MatchAttachmentPoints(IXRInteractor interactor)
    {
        if(IsFirstSelecting(interactor))
        {
            // TODO: Because the attach transforms change orientation when the move, that complicates this a bit

            // Do we match the position, but then correct the direction?

            bool IsDirect = interactor is XRDirectInteractor;
            attachTransform.position = IsDirect ? interactor.GetAttachTransform(this).position : transform.position;
            attachTransform.rotation = IsDirect ? interactor.GetAttachTransform(this).rotation : transform.rotation;
        }
    }

    private bool IsFirstSelecting(IXRInteractor interactor)
    {
        return interactor == firstInteractorSelecting;
    }
}
