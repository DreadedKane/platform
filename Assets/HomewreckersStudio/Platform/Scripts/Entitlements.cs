/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    /**
     * Verifies the user's entitlements.
     */
    public sealed partial class Entitlements : MonoBehaviour
    {
        /** Used to invoke callbacks. */
        private Request m_request;

        /**
         * Creates the request object.
         */
        private void Awake()
        {
            m_request = new Request();

            AwakePartial();
        }

        /** Implemented in platform-specific module. */
        partial void AwakePartial();

        /**
         * Defers to platform-specific module.
         */
        public void Verify(Action success, Action failure)
        {
            Debug.Log("Verifying entitlements");

            m_request.SetListeners(success, failure);

            VerifyPartial();
        }

        /** Implemented in platform-specific module. */
        partial void VerifyPartial();
    }
}
