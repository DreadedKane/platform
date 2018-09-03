/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class Platform : Request
    {
        /** Implemented in platform-specific partial class. */
        partial void Internal();

        /**
         * Defers to platform-specific method.
         */
        public void Initialize(Action success, Action failure)
        {
            Debug.Log("Initializing platform");

            SetEvents(success, failure);

            Internal();
        }
    }
}
