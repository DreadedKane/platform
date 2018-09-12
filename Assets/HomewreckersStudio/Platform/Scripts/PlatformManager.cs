/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using System;

namespace HomewreckersStudio
{
    /**
     * Manages the platform, entitlements, and user components.
     */
    public sealed class PlatformManager : Singleton<PlatformManager>
    {
        /** Used to invoke callbacks. */
        private Request m_request;

        /** Used to initialise the platform. */
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
        protected override void Awake()
        {
            base.Awake();

            m_request = new Request();

            m_platform = gameObject.AddComponent<Platform>();
            m_entitlements = gameObject.AddComponent<Entitlements>();
            m_user = gameObject.AddComponent<User>();
        }

        /**
         * Initialises the platform.
         */
        public void Initialise(Action success, Action failure)
        {
            m_request.SetListeners(success, failure);

            m_platform.Initialise(OnInitialiseSuccess, m_request.OnFailure);
        }

        /**
         * Verifies the entitlements.
         */
        private void OnInitialiseSuccess()
        {
            m_entitlements.Verify(OnEntitlementsSuccess, m_request.OnFailure);
        }

        /**
         * Gets the user's data.
         */
        private void OnEntitlementsSuccess()
        {
            m_user.Initialise(m_request.OnSuccess, m_request.OnFailure);
        }
    }
}
