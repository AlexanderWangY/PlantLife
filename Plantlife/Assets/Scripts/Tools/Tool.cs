using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tool : MonoBehaviour
{
    /// <summary>
    /// Trigger that activates when a tool is used
    /// </summary>
    public SphereCollider toolUseTrigger;

    [SerializeField] 
    private XRGrabInteractable grabInteractable;

    public float pressThreshold = 0.1f;

    [SerializeField]
    private InputActionProperty useAction;


    void Start()
    {
        toolUseTrigger.enabled = false;
        if (grabInteractable == null)
            grabInteractable = GetComponent<XRGrabInteractable>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (!grabInteractable.isSelected)
            return;

        // this is what we're using instead of VR stuff? was trying to get something quick and dirty
        float v = useAction.action.ReadValue<float>();

        if (v > 0.5f)
            Use();
    }

/*    private void CheckHand(XRNode hand)
    {
        bool pressed;
        var device = InputDevices.GetDeviceAtXRNode(hand);
        InputHelpers.IsPressed(device, button, out pressed);

        if (pressed)
        {
            Debug.Log("f");
            Use();
        }
    }*/

    public virtual void Use()
    {
        Debug.Log("Using " + name);
        if (toolUseTrigger == null)
            toolUseTrigger = GetComponent<SphereCollider>();

        StopAllCoroutines();
        StartCoroutine(PulseRoutine());
    }

    private System.Collections.IEnumerator PulseRoutine()
    {
        toolUseTrigger.enabled = true;
        yield return new WaitForSeconds(3f);
        toolUseTrigger.enabled = false;
    }
}
