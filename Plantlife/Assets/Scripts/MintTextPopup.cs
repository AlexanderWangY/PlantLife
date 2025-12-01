using System.Collections;
using UnityEngine;

public class MintTextPopup : MonoBehaviour
{
    [Tooltip("The Text (TMP) object you want to show/hide")]
    public GameObject textObject;

    [Tooltip("How long to keep the text visible (seconds)")]
    public float showTime = 50f;

    private Coroutine hideRoutine;

    private void Start()
    {
        if (textObject != null)
            textObject.SetActive(false);   // make sure it's off at start
    }

    // Call this from the Teleportation Anchor
    public void ShowText()
    {
        if (textObject == null) return;

        textObject.SetActive(true);

        if (hideRoutine != null)
            StopCoroutine(hideRoutine);

        hideRoutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(showTime);
        textObject.SetActive(false);
    }
}
