/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed partial class User : Request
    {
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

        /** Implemented in platform module. */
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

        /** Implemented in platform module. */
        partial void GetImagePartial(ref Sprite image);

        /**
         * Gets the user's data.
         */
        public void Initialize(Action success, Action failure)
        {
            Debug.Log("Initializing user");

            SetEvents(success, failure);

            InitializePartial();
        }

        /** Implemented in platform module. */
        partial void InitializePartial();
    }
}
