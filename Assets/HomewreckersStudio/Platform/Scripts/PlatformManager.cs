/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;

namespace HomewreckersStudio
{
    public sealed class PlatformManager : Request
    {
        /** Used to initialize the platform. */
        private Platform m_platform;

        /** Used to verify the entitlements. */
        private Entitlements m_entitlements;

        /** Used to get the user's data. */
        private User m_user;

        /**
         * Gets the entitlements component.
         */
        public Entitlements Entitlements
        {
            get
            {
                return m_entitlements;
            }
        }

        /**
         * Gets the user component.
         */
        public User User
        {
            get
            {
                return m_user;
            }
        }

        /**
         * Adds the required components.
         */
        private void Awake()
        {
            m_platform = gameObject.AddComponent<Platform>();
            m_entitlements = gameObject.AddComponent<Entitlements>();
            m_user = gameObject.AddComponent<User>();
        }

        /**
         * Initializes the platform.
         */
        public void Initialize(Action success, Action failure)
        {
            SetEvents(success, failure);

            m_platform.Initialize(OnInitializeSuccess, OnFailure);
        }

        /**
         * Verifies the entitlements.
         */
        private void OnInitializeSuccess()
        {
            m_entitlements.Verify(OnEntitlementsSuccess, OnFailure);
        }

        /**
         * Gets the user's data.
         */
        private void OnEntitlementsSuccess()
        {
            m_user.Initialize(OnSuccess, OnFailure);
        }
    }
}
