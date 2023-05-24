using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    [SerializeField] bool autoSet = true; //If autoSet --> Require a gamObject parent of all floaters
    public Rigidbody rb; //Boat Rigidbody, not the floater's
    public Transform parent; //Parent of this gameObject --> Contains every Floater of the boat (used only to get the RigidBody)

    //Default values for a 4floater boat
    public float depthBeforeSubmerged = 1f;
    public float displacementAmount = 3f;
    public float waterDrag = 1f;
    public float waterAngularDrag = 4f;

    //Except this would be 4
    public int floaterCount = 1;

    private void Awake()
    {
        #region AutoSet

        if (!autoSet)
            return;
        
        parent = gameObject.transform.parent;
        floaterCount = parent.transform.childCount;

        rb = parent.parent.GetComponent<Rigidbody>(); //Original parent Rigidbody (the boat / floating object) (if there are more parents, do more .parent :)

        #endregion

    }

    private void FixedUpdate()
    {
        rb.AddForceAtPosition(Physics.gravity / floaterCount, transform.position, ForceMode.Acceleration);
        float waveHeight = WaveManager.Instance.GetWaveHeight(transform.position.x);

        if (transform.position.y < waveHeight)
        {
            float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            rb.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),transform.position, ForceMode.Acceleration);
            rb.AddTorque(displacementMultiplier * Time.fixedDeltaTime * waterDrag * -rb.velocity, ForceMode.VelocityChange);
            rb.AddTorque(displacementMultiplier * Time.fixedDeltaTime * waterAngularDrag * -rb.angularVelocity, ForceMode.VelocityChange);
        }
    }
}
