using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject floaters;

    public float depthBeforeSubmerged;
    public float displacementAmount;
    public float waterDrag = 0.99f;
    public float waterAngularDrag = 0.5f;
    private int floaterCount = 1;

    private void Start()
    {
        floaterCount = floaters.transform.childCount;
    }

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        float waveHeight = WaveManager.Instance.GetWaveHeight(transform.position.x);
        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            rb.AddTorque(displacementMultiplier * Time.fixedDeltaTime * waterDrag * -rb.velocity, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * Time.fixedDeltaTime * waterAngularDrag * -rb.angularVelocity, ForceMode.VelocityChange);
        }
    }
}
