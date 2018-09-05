/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using UnityEngine;
using UnityEngine.UI;

namespace HomewreckersStudio
{
    public sealed partial class PlatformTests : MonoBehaviour
    {
        /** Used to initialize the platform. */
        [SerializeField]
        private PlatformManager m_platformManager;

        /** Used to diplay the user's username. */
        [SerializeField]
        private Text m_nameText;

        /** Used to diplay the user's avatar. */
        [SerializeField]
        private Image m_avatarImage;

        /**
         * Runs the unit tests.
         */
        private void Start()
        {
            Debug.Log("Running unit tests");

            TestInitialize();
        }

        /**
         * Finishes the unit tests.
         */
        private void Finish()
        {
            Debug.Log("Unit tests complete");
        }

        /**
         * Initializes the platform.
         */
        private void TestInitialize()
        {
            Debug.Log("Testing initialize");

            m_platformManager.Initialize(OnPlatformSuccess, OnPlatformFailure);
        }

        /**
         * Updates the UI and finishes.
         */
        private void OnPlatformSuccess()
        {
            m_nameText.text = m_platformManager.User.Name;
            m_avatarImage.sprite = m_platformManager.User.Image;

            Finish();
        }

        /**
         * Logs an error and finishes.
         */
        private void OnPlatformFailure()
        {
            Debug.LogError("Initialize failed");

            Finish();
        }
    }
}
