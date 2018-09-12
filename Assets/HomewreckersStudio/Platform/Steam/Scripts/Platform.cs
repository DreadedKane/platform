/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_STANDALONE_WIN

using UnityEngine;

namespace HomewreckersStudio
{
    /**
     * Verifies Steamworks has been initialised.
     */
    public sealed partial class Platform
    {
        /**
         * Adds the required components.
         */
        partial void AwakePartial()
        {
            gameObject.AddComponent<SteamManager>();
        }

        /**
         * Checks if Steamworks is initialised.
         */
        partial void InitialisePartial()
        {
            if (SteamManager.Initialized)
            {
                Debug.Log("Platform initialised");

                m_request.OnSuccess();
            }
            else
            {
                Debug.LogError("Couldn't initialise platform: Steamworks not initialised");

                m_request.OnFailure();
            }
        }
    }
}

#endif // UNITY_STANDALONE_WIN
