using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;


namespace Snowboarding
{
    public enum SnowMapleAnimationState
    {
        IDLE = 0,
        SKIING = 1,
        LEFT_LEAN = 2,
        RIGHT_LEAN = 3,
        JUMP = 4
    }

    [System.Serializable]
    public class SnowMaple : BaseMaple
    {

        #region Private variables
        [SerializeField]
        [Tooltip("Character speed")]
        private float m_speed;

        [SerializeField]
        [Tooltip("Rotation speed")]
        private float m_rotationSpeed;

        [SerializeField]
        [Tooltip("Threshold for rotation modification. Should be between 0 and 1.")]
        private float m_rotationThreshold;
        #endregion

        private void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        private void Start()
        {
            
        }

        // Update is called once per frame
        private void Update()
        {
            if (this.getState() != SnowMapleAnimationState.IDLE)
            {
                RaycastHit slopeHit;
                if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, Mathf.Infinity))
                {
                    // Adjust angle according to slope
                    float angleBufferInDegrees = 10f;
                    float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
                    Vector3 projectedVect = Vector3.ProjectOnPlane(gameObject.transform.forward, slopeHit.normal).normalized;

                    if (Vector3.Distance(projectedVect, Vector3.up) < m_rotationThreshold)
                    {
                        Quaternion rotation = Quaternion.LookRotation(projectedVect, Vector3.up);
                        transform.rotation = rotation;
                    }

                    // Apply force to accelerate
                    if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
                    {
                        if (Mathf.Abs(angle) > angleBufferInDegrees)
                        {
                            cc_rb.AddForce(projectedVect * this.m_speed, ForceMode.Force);
                            m_forceVect = projectedVect * this.m_speed;
                        }
                        else
                        {
                            cc_rb.AddForce(gameObject.transform.forward * this.m_speed, ForceMode.Force);
                            m_forceVect = gameObject.transform.forward * this.m_speed;
                        }
                    }
                    else
                    {
                        m_forceVect = Vector3.zero;
                    }
                }
                else
                {
                    Debug.LogWarning("Could not find slope hit.");
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                setState(SnowMapleAnimationState.SKIING);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                gameObject.transform.Rotate(0, -m_rotationSpeed * Time.deltaTime, 0, Space.World);
                setState(SnowMapleAnimationState.LEFT_LEAN);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                gameObject.transform.Rotate(0, m_rotationSpeed * Time.deltaTime, 0, Space.World);
                setState(SnowMapleAnimationState.RIGHT_LEAN);
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                setState(SnowMapleAnimationState.SKIING);
            }
        }

        // Returns the animation state the character is currently in
        private SnowMapleAnimationState getState()
        {
            return (SnowMapleAnimationState) this.cc_animator.GetInteger("State");
        }

        private void setState(SnowMapleAnimationState state)
        {
            if (this.cc_animator != null)
            {
                this.cc_animator.SetInteger("State", (int) state);
            }
        }
    }
}