/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

using UnityEngine;
using UnityEngine.UI;

namespace HomewreckersStudio
{
    /**
     * Performs unit tests on the module.
     */
    public sealed partial class PlatformTests : MonoBehaviour
    {
        [Header("Required Components")]

        [SerializeField]
        [Tooltip("Used to diplay the user's username.")]
        private Text m_nameText;

        [SerializeField]
        [Tooltip("Used to diplay the user's avatar.")]
        private Image m_avatarImage;

        /**
         * Runs the unit tests.
         */
        private void Start()
        {
            Debug.Log("Running unit tests");

            TestInitialise();
        }

        /**
         * Finishes the unit tests.
         */
        private void Finish()
        {
            Debug.Log("Unit tests complete");
        }

        /**
         * Initialises the platform.
         */
        private void TestInitialise()
        {
            Debug.Log("Testing initialise");

            PlatformManager.Instance.Initialise(OnPlatformSuccess, OnPlatformFailure);
        }

        /**
         * Updates the UI and finishes.
         */
        private void OnPlatformSuccess()
        {
            User user = PlatformManager.Instance.User;

            m_nameText.text = user.Name;
            m_avatarImage.sprite = user.Image;

            Finish();
        }

        /**
         * Logs an error and finishes.
         */
        private void OnPlatformFailure()
        {
            Debug.LogError("Initialise failed");

            Finish();
        }
    }
}
