/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class Platform : MonoBehaviour
    {
        /** Invoked when the platform has been initialized. */
        private event Action m_success;

        /** Invoked when there is an error initializing the platform. */
        private event Action m_failure;

        /** Implemented in platform-specific partial class. */
        partial void Internal();

        /**
         * Defers to platform-specific method.
         */
        public void Initialize(Action success, Action failure)
        {
            Debug.Log("Initializing platform");

            m_success = success;
            m_failure = failure;

            Internal();
        }

        /**
         * Invokes the success event.
         */
        private void OnSuccess()
        {
            Debug.Log("Platform initialized");

            Event.Invoke(m_success);

            m_success = null;
            m_failure = null;
        }

        /**
         * Invokes the failure event.
         */
        private void OnFailure(string error)
        {
            Debug.LogError("Couldn't initialize platform: " + error);

            Event.Invoke(m_failure);

            m_success = null;
            m_failure = null;
        }
    }
}
