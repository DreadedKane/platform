/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class Entitlements : Request
    {
        /** Implemented in platform-specific partial class. */
        partial void Internal();

        /**
         * Defers to platform-specific method.
         */
        public void Verify(Action success, Action failure)
        {
            Debug.Log("Verifying entitlements");

            SetEvents(success, failure);

            Internal();
        }
    }
}
