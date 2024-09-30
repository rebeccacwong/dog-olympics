using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    [System.Serializable]
    public class BaseMaple : MonoBehaviour
    {

        #region Cached Components
        protected Animator cc_animator;
        protected Rigidbody cc_rb;
        protected BoxCollider cc_boxCollider;
        #endregion

        #region Instance variables
        protected Vector3 m_forceVect = Vector3.zero;
        #endregion

        protected void Awake()
        {
            this.cc_animator = gameObject.GetComponent<Animator>();
            Debug.Assert(cc_animator != null);

            cc_rb = GetComponent<Rigidbody>();
            Debug.Assert(cc_rb != null);

            this.cc_boxCollider = gameObject.GetComponent<BoxCollider>();
            Debug.Assert(cc_boxCollider != null);
        }

        // Start is called before the first frame update
        private void Start()
        {

        }

        // Update is called once per frame
        private void Update()
        {

        }

        protected float getPlayerHeight()
        {
            return cc_boxCollider.size.y;
        }

        // Get force vector applied on this object.
        public Vector3 getForceVector()
        {
            return m_forceVect;
        }

    }

}
