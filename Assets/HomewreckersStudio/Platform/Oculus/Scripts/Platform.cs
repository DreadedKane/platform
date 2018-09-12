/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_ANDROID

using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

using OculusCore = Oculus.Platform.Core;

namespace HomewreckersStudio
{
    /**
     * Initialises the platform.
     */
    public sealed partial class Platform
    {
        /**
         * Initialises the Oculus platform.
         */
        partial void InitialisePartial()
        {
            OculusCore.AsyncInitialize().OnComplete(OnComplete);
        }

        /**
         * Checks the message for errors.
         */
        private void OnComplete(Message<PlatformInitialize> message)
        {
            if (message.IsError)
            {
                // Initialise failed.
                Error error = message.GetError();

                Debug.LogError("Couldn't initialise platform: " + error.Message);

                m_request.OnFailure();
            }
            else
            {
                // Initialise succeeded.
                Debug.Log("Platform initialised");

                m_request.OnSuccess();
            }
        }
    }
}

#endif // UNITY_ANDROID
