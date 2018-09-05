/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_ANDROID

using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

using OculusEntitlements = Oculus.Platform.Entitlements;

namespace HomewreckersStudio
{
    public sealed partial class Entitlements
    {
        /**
         * Verifies entitlements.
         */
        partial void Internal()
        {
            OculusEntitlements.IsUserEntitledToApplication().OnComplete(OnComplete);
        }

        /**
         * Checks the message for errors.
         */
        private void OnComplete(Message message)
        {
            if (message.IsError)
            {
                // Entitlement check failed.
                Error error = message.GetError();

                Debug.LogError("Couldn't verify entitlements: " + error.Message);

                OnFailure();
            }
            else
            {
                // Entitlement check succeeded.
                Debug.Log("Entitlements verified");

                OnSuccess();
            }
        }
    }
}

#endif // UNITY_ANDROID
