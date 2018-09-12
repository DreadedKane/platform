/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_STANDALONE_WIN

using UnityEngine;

namespace HomewreckersStudio
{
    /**
     * Verifies the session ticket.
     */
    public sealed partial class Entitlements
    {
        /** Used to verify entitlements. */
        private SessionTicket m_sessionTicket;

        /**
         * Gets the session ticket in string format.
         */
        public string SessionTicket
        {
            get
            {
                return m_sessionTicket.Ticket;
            }
        }

        /**
         * Adds the required components.
         */
        partial void AwakePartial()
        {
            m_sessionTicket = gameObject.AddComponent<SessionTicket>();
        }

        /**
         * Attempts to get a session ticket.
         */
        partial void VerifyPartial()
        {
            m_sessionTicket.Initialise(OnTicketSuccess, OnTicketFailure);
        }

        /**
         * Calls success.
         */
        private void OnTicketSuccess()
        {
            Debug.Log("Entitlements verified");

            m_request.OnSuccess();
        }

        /**
         * Calls failure.
         */
        private void OnTicketFailure()
        {
            Debug.LogError("Couldn't get session ticket");

            m_request.OnFailure();
        }
    }
}

#endif // UNITY_STANDALONE_WIN
