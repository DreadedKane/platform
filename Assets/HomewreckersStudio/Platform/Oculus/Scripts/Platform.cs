/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_ANDROID

using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class Platform
    {
        /**
         * Initializes the Oculus platform.
         */
        partial void Internal()
        {
            Core.AsyncInitialize().OnComplete(OnComplete);
        }

        /**
         * Checks the message for errors.
         */
        private void OnComplete(Message<PlatformInitialize> message)
        {
            if (message.IsError)
            {
                // Initialize failed.
                Error error = message.GetError();

                Debug.LogError("Couldn't initialize platform: " + error.Message);

                OnFailure();
            }
            else
            {
                // Initialize succeeded.
                Debug.Log("Platform initialized");

                OnSuccess();
            }
        }
    }
}

#endif // UNITY_ANDROID
