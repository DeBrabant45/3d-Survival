using Pinwheel.Poseidon;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.PrefabScripts
{
    public class Floater : MonoBehaviour
    {
        [SerializeField] private float depthBeforeSubMerged = 0.4f;
        [SerializeField] private float displacementAmount = 3f;
        [SerializeField] private int floaterCount = 1;
        [SerializeField] private float waterDrag = 0.5f;
        [SerializeField] private float waterAngularDrag = 0.5f;
        [SerializeField] private bool floaterWorking;
        private float waterHeight;
        private float waveHeight;

        [SerializeField] private PWater water;
        [SerializeField] private bool applyRipple;
        [SerializeField] Rigidbody rigidbody;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            waterHeight = water.transform.position.y;
        }

        public void FixedUpdate()
        {
            if (water != null)
            {
                if (transform.position.y < waterHeight + 0.2)
                {
                    floaterWorking = true;
                    if (water == null)
                    {
                        return;
                    }
                    Vector3 localPos = water.transform.InverseTransformPoint(transform.position);
                    localPos.y = 0;
                    localPos = water.GetLocalVertexPosition(localPos, applyRipple);
                    Vector3 worldPos = water.transform.TransformPoint(localPos);
                    waveHeight = worldPos.y;

                    if (transform.position.y < waveHeight)
                    {
                        float displacementMultiplier = Mathf.Clamp01((waveHeight - transform.position.y) / depthBeforeSubMerged) * displacementAmount / floaterCount;
                        rigidbody.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
                        rigidbody.AddForce(displacementMultiplier * -rigidbody.velocity * waterDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
                        rigidbody.AddTorque(displacementMultiplier * -rigidbody.angularVelocity * waterAngularDrag * Time.fixedDeltaTime, ForceMode.VelocityChange);
                    }
                }
            }
            else
            {
                floaterWorking = false;
            }
        }
    }
}