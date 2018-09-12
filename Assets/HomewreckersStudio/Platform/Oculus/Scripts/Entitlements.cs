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
    /**
     * Verifies the user's entitlements.
     */
    public sealed partial class Entitlements
    {
        /**
         * Verifies entitlements.
         */
        partial void VerifyPartial()
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

                m_request.OnFailure();
            }
            else
            {
                // Entitlement check succeeded.
                Debug.Log("Entitlements verified");

                m_request.OnSuccess();
            }
        }
    }
}

#endif // UNITY_ANDROID
