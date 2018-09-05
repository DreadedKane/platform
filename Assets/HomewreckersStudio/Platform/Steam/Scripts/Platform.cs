/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_STANDALONE_WIN

using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class Platform
    {
        /**
         * Adds the required components.
         */
        private void Awake()
        {
            gameObject.AddComponent<SteamManager>();
        }

        /**
         * Checks if Steamworks is initialized.
         */
        partial void Internal()
        {
            if (SteamManager.Initialized)
            {
                Debug.Log("Platform initialized");

                OnSuccess();
            }
            else
            {
                Debug.LogError("Couldn't initialize platform: Steamworks not initialized");

                OnFailure();
            }
        }
    }
}

#endif // UNITY_STANDALONE_WIN
