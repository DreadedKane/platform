/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_ANDROID

using Oculus.Platform;
using Oculus.Platform.Models;
using UnityEngine;

using OculusUser = Oculus.Platform.Models.User;

namespace HomewreckersStudio
{
    /**
     * Gets the user's username and avatar.
     */
    public sealed partial class User
    {
        /** Used to download the user's avatar. */
        private SpriteDownload m_spriteDownload;

        /** The user's username. */
        private string m_name;

        /** The user's avatar. */
        private Sprite m_image;

        /**
         * Adds the required components.
         */
        partial void AwakePartial()
        {
            m_spriteDownload = gameObject.AddComponent<SpriteDownload>();
        }

        /**
         * Gets the user's username.
         */
        partial void GetNamePartial(ref string name)
        {
            name = m_name;
        }

        /**
         * Gets the user's avatar.
         */
        partial void GetImagePartial(ref Sprite image)
        {
            image = m_image;
        }

        /**
         * Gets the user's data.
         */
        partial void InitialisePartial()
        {
            Users.GetLoggedInUser().OnComplete(OnComplete);
        }

        /**
         * Checks for errors and downloads the user's avatar.
         */
        private void OnComplete(Message<OculusUser> message)
        {
            if (message.IsError)
            {
                // Logs a warning and calls failure.
                Error error = message.GetError();

                Debug.LogError("Get user failed: " + error.Message);

                m_request.OnFailure();
            }
            else
            {
                // Gets the user's name and downloads their avatar
                OculusUser user = message.Data;

                m_name = user.OculusID;

                if (string.IsNullOrEmpty(user.ImageURL))
                {
                    m_request.OnSuccess();
                }
                else
                {
                    m_spriteDownload.Download(user.ImageURL, OnSpriteSuccess, m_request.OnFailure);
                }
            }
        }

        /**
         * Sets the user's avatar and calls success.
         */
        private void OnSpriteSuccess()
        {
            m_image = m_spriteDownload.Sprite;

            m_request.OnSuccess();
        }
    }
}

#endif // UNITY_ANDROID
