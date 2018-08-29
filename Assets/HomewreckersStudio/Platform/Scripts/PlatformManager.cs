/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    [RequireComponent(typeof(Platform))]
    [RequireComponent(typeof(Entitlements))]
    public sealed class PlatformManager : MonoBehaviour
    {
        /** Invoked when the platform has been intialized. */
        private event Action m_success;

        /** Invoked when the platform fails to initialize. */
        private event Action m_failure;

        /** Used to initialize the platform. */
        private Platform m_platform;

        /** Used to verify the entitlements. */
        private Entitlements m_entitlements;

        /**
         * Gets the required components.
         */
        private void Awake()
        {
            m_platform = GetComponent<Platform>();
            m_entitlements = GetComponent<Entitlements>();
        }

        /**
         * Initializes the platform.
         */
        public void Initialize(Action success, Action failure)
        {
            m_success = success;
            m_failure = failure;

            m_platform.Initialize(OnInitializeSuccess, OnFailure);
        }

        /**
         * Verifies the entitlements.
         */
        private void OnInitializeSuccess()
        {
            m_entitlements.Verify(OnSuccess, OnFailure);
        }

        /**
         * Invokes the success event.
         */
        private void OnSuccess()
        {
            Event.Invoke(m_success);

            m_success = null;
            m_failure = null;
        }

        /**
         * Invokes the failure event.
         */
        private void OnFailure()
        {
            Event.Invoke(m_failure);

            m_success = null;
            m_failure = null;
        }
    }
}
