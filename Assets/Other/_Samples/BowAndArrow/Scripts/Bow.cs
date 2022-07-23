using UnityEngine.XR.Interaction.Toolkit;

public class Bow : XRGrabInteractable
{
    private Notch notch = null;

    protected override void Awake()
    {
        base.Awake();
        notch = GetComponentInChildren<Notch>();
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        notch.IsReady = true;
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        notch.IsReady = false;
    }
}
