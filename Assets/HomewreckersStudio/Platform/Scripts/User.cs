/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    /**
     * Gets the user's username and avatar.
     */
    public sealed partial class User : MonoBehaviour
    {
        /** Used to invoke callbacks. */
        private Request m_request;

        /**
         * Creates the request object.
         */
        private void Awake()
        {
            m_request = new Request();

            AwakePartial();
        }

        /** Implemented in platform-specific module. */
        partial void AwakePartial();

        /**
         * Gets the user's name.
         */
        public string Name
        {
            get
            {
                string name = null;

                GetNamePartial(ref name);

                return name;
            }
        }

        /** Implemented in platform-specific module. */
        partial void GetNamePartial(ref string name);

        /**
         * Gets the user's avatar.
         */
        public Sprite Image
        {
            get
            {
                Sprite image = null;

                GetImagePartial(ref image);

                return image;
            }
        }

        /** Implemented in platform-specific module. */
        partial void GetImagePartial(ref Sprite image);

        /**
         * Gets the user's data.
         */
        public void Initialise(Action success, Action failure)
        {
            Debug.Log("Initialising user");

            m_request.SetListeners(success, failure);

            InitialisePartial();
        }

        /** Implemented in platform-specific module. */
        partial void InitialisePartial();
    }
}
