/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class Entitlements : MonoBehaviour
    {
        /** Invoked when the entitlements have been verified. */
        private event Action m_success;

        /** Invoked when there is an error verifying the entitlements. */
        private event Action m_failure;

        /** Implemented in platform-specific partial class. */
        partial void Internal();

        /**
         * Defers to platform-specific method.
         */
        public void Verify(Action success, Action failure)
        {
            Debug.Log("Verifying entitlements");

            m_success = success;
            m_failure = failure;

            Internal();
        }

        /**
         * Invokes the success event.
         */
        private void OnSuccess()
        {
            Debug.Log("Entitlements verified");

            Event.Invoke(m_success);

            m_success = null;
            m_failure = null;
        }

        /**
         * Invokes the failure event.
         */
        private void OnFailure(string message)
        {
            Debug.LogError("Couldn't verify entitlements: " + message);

            Event.Invoke(m_failure);

            m_success = null;
            m_failure = null;
        }
    }
}
