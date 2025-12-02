using UnityEngine;

public class SkyboxCycle : MonoBehaviour
{
    [Header("Skyboxes")]
    public Material daySkybox;
    public Material nightSkybox;
    public Material overcastSkybox;

    [Header("Directional Light (Sun)")]
    public Light sunLight;

    [Header("Cycle Settings")]
    [Tooltip("Time in seconds for a full day/night rotation of the sun.")]
    public float dayLength = 600f; // 10 minutes

    [Tooltip("Chance 0–1 that sky switches to overcast instead of normal night/day.")]
    public float overcastChance = 0.2f;

    [Tooltip("Duration for skybox blend transitions.")]
    public float transitionDuration = 5f;

    private Material _targetSkybox;
    private Material _currentSkybox;

    private float _transitionTimer = 0f;
    private bool _transitioning = false;

    private float _sunAngle = 120f; // 0–360 degrees

    private void Start()
    {
        if (sunLight == null)
        {
            Debug.LogWarning("SkyboxCycle: No sunLight set. Rotation and lighting will not work.");
            enabled = false;
            return;
        }

        RenderSettings.skybox = daySkybox;
        _currentSkybox = daySkybox;

        _sunAngle = 120f;

        InvokeRepeating(nameof(SwitchSkybox), dayLength / 2f, dayLength / 2f);
    }

    private void Update()
    {
        RotateSun();

        if (_transitioning)
        {
            _transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(_transitionTimer / transitionDuration);
            RenderSettings.skybox.Lerp(_currentSkybox, _targetSkybox, t);

            UpdateLightingTransition(t);

            if (t >= 1f)
                FinishTransition();
        }
    }

    private void RotateSun()
    {
        // Degrees per second
        float rotationSpeed = 360f / dayLength;
        _sunAngle += rotationSpeed * Time.deltaTime;

        if (_sunAngle > 360f)
            _sunAngle -= 360f;

        // Pitch the sun over the horizon (X rotation)
        float xRot = _sunAngle - 90f; // -90 = sunrise at angle 0

        sunLight.transform.rotation = Quaternion.Euler(xRot, 0f, 0f);

        // Adjust intensity dynamically based on angle
        // (below horizon = night, above = day)
        float normalized = Mathf.Clamp01(Mathf.InverseLerp(-20f, 20f, xRot));
        sunLight.intensity = Mathf.Lerp(0.05f, 1.1f, normalized);
    }

    private void SwitchSkybox()
    {
        bool goOvercast = Random.value < overcastChance;

        if (goOvercast)
        {
            BeginTransition(overcastSkybox);
            return;
        }

        if (_currentSkybox == daySkybox)
            BeginTransition(nightSkybox);
        else
            BeginTransition(daySkybox);
    }

    private void BeginTransition(Material next)
    {
        _targetSkybox = next;
        _transitionTimer = 0f;
        _transitioning = true;
    }

    private void FinishTransition()
    {
        _currentSkybox = _targetSkybox;
        RenderSettings.skybox = _currentSkybox;
        _transitioning = false;
    }

    private void UpdateLightingTransition(float t)
    {
        if (_targetSkybox == overcastSkybox)
        {
            sunLight.intensity = Mathf.Lerp(sunLight.intensity, 0.3f, t);
            sunLight.color = Color.Lerp(sunLight.color, new Color(0.8f, 0.8f, 0.8f), t);
        }
        else if (_targetSkybox == daySkybox)
        {
            sunLight.color = Color.Lerp(new Color(0.3f, 0.4f, 0.6f), Color.white, t);
        }
        else if (_targetSkybox == nightSkybox)
        {
            sunLight.color = Color.Lerp(Color.white, new Color(0.2f, 0.3f, 0.6f), t);
        }
    }
}
