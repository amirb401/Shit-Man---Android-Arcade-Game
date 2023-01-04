using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeBehaviour : MonoBehaviour
{

    // Credits to:
    // https://medium.com/@mattThousand/basic-2d-screen-shake-in-unity-9c27b56b516

    private Transform transformVariable;              // Transform of the GameObject you want to shake
    private static float shakeDuration = 0f; // Desired duration of the shake effect
    private float shakeMagnitude = 0.7f;    // A measure of magnitude for the shake. Tweak based on your preference
    private float dampingSpeed = 1.0f;     // A measure of how quickly the shake effect should evaporate
    Vector3 initialPosition;              // The initial position of the GameObject

    void Awake()
    {
        //store your camera’s transform
        if (transformVariable == null)
        {
            transformVariable = GetComponent(typeof(Transform)) as Transform;
        }
    }
    void OnEnable()
    {
        //store the initial position of your camera’sGameObject
        initialPosition = transform.localPosition;
    }

    public static void TriggerShake()
    {
        shakeDuration = 0.25f;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            transformVariable.localPosition = initialPosition + Random.insideUnitSphere * shakeMagnitude;

            shakeDuration -= Time.deltaTime * dampingSpeed;
        }
        else
        {
            shakeDuration = 0f;
            transformVariable.localPosition = initialPosition;
        }
        //The logic is simple. If shakeDuration is positive, adjust the game object’s transform by a random factor (that’s our shake).
        // Decrement shakeDuration so that your camera doesn’t shake forever.
        // Once shakeDuration reaches 0, return your camera’s GameObjectto its initial position:
    }
}
