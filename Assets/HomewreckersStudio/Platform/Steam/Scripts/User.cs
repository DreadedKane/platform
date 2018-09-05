/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_STANDALONE_WIN

using Steamworks;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class User
    {
        /** The user's username. */
        private string m_name;

        /** The user's avatar. */
        private Sprite m_image;

        /** Invoked when the avatar has loaded. */
#pragma warning disable 0414
        private Callback<AvatarImageLoaded_t> m_avatarLoaded;
#pragma warning restore 0414

        /**
         * Creates a callback for when the avatar has loaded.
         */
        private void OnEnable()
        {
            m_avatarLoaded = Callback<AvatarImageLoaded_t>.Create(OnAvatarLoaded);
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
         * Gets the user's username and avatar.
         */
        partial void InitializePartial()
        {
            m_name = SteamFriends.GetPersonaName();

            // Get avatar
            CSteamID id = SteamUser.GetSteamID();
            int avatar = SteamFriends.GetLargeFriendAvatar(id);

            if (avatar == 0)
            {
                // There is no avatar for this user.
                OnSuccess();
            }
            else if (avatar != -1)
            {
                // The avatar is available.
                m_image = CreateAvatar(avatar);

                OnSuccess();
            }
        }

        /**
         * Creates the avatar and calls success.
         */
        private void OnAvatarLoaded(AvatarImageLoaded_t callback)
        {
            m_image = CreateAvatar(callback.m_iImage);

            OnSuccess();
        }

        /**
         * Creates a sprite from the avatar image.
         */
        private Sprite CreateAvatar(int avatar)
        {
            // Get avatar texture
            Vector2 size;
            Texture2D texture = SteamUtilsTest.GetSteamImageAsTexture2D(avatar, out size);

            // Create avatar sprite
            Rect rect = new Rect(Vector2.zero, size);
            Vector2 pivot = new Vector2(.5f, .5f);

            return Sprite.Create(texture, rect, pivot);
        }
    }
}

#endif // UNITY_STANDALONE_WIN
