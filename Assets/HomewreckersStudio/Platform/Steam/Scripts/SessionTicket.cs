/**
 * Copyright (c) Eugene Bridger. All rights reserved.
 * Licensed under the MIT License. See LICENSE file in the project root for full license information.
 */

#if UNITY_STANDALONE_WIN

using Steamworks;
using System;
using System.Collections;
using UnityEngine;

namespace HomewreckersStudio
{
    public sealed class SessionTicket : Request
    {
        /** Seconds to wait before timing out. */
        [SerializeField]
        private float m_timeout = 10f;

        /** The coroutine that invokes the ticket request. */
        private IEnumerator m_coroutine;

        /** The session ticket. */
        private byte[] m_ticket;

        /** The length of the session ticket in bytes. */
        private uint m_length;

        /** The session ticket request. */
        private HAuthTicket m_authTicket;

        /** The session ticket in string format. */
        private string m_ticketString;

        /** The session ticket callback. */
#pragma warning disable 0414
        private Callback<GetAuthSessionTicketResponse_t> m_callback;
#pragma warning restore 0414

        /**
         * Gets the session ticket in string format.
         */
        public string Ticket
        {
            get
            {
                return m_ticketString;
            }
        }

        /**
         * Creates a callback for the session ticket.
         */
        private void OnEnable()
        {
            m_callback = Callback<GetAuthSessionTicketResponse_t>.Create(OnResponse);
        }

        /**
         * Attempts to get a session ticket.
         */
        public void Initialize(Action success, Action failure)
        {
            SetEvents(success, failure);

            if (!m_active)
            {
                m_active = true;

                m_coroutine = SessionTicketCoroutine();

                StartCoroutine(m_coroutine);
            }
        }

        /**
         * Gets a session ticket or times out.
         */
        private IEnumerator SessionTicketCoroutine()
        {
            // Attempts to get a session ticket
            m_ticket = new byte[1024];
            m_authTicket = SteamUser.GetAuthSessionTicket(m_ticket, 1024, out m_length);

            yield return new WaitForSeconds(m_timeout);

            // Times out
            OnFailure("Couldn't get session ticket");
        }

        /**
         * Checks the session ticket response.
         */
        private void OnResponse(GetAuthSessionTicketResponse_t callback)
        {
            // Stops the coroutine
            StopCoroutine(m_coroutine);

            // Checks the response result
            if (callback.m_eResult == EResult.k_EResultOK)
            {
                Debug.Log("Session ticket valid");

                m_ticketString = String.FromByteArray(m_ticket, (int)m_length);

                OnSuccess();
            }
            else
            {
                OnFailure("Invalid session ticket: " + callback.ToString());
            }
        }

        /**
         * Cancels the ticket and calls failure.
         */
        private void OnFailure(string message)
        {
            Debug.LogError(message);

            CancelTicket();
            OnFailure();
        }

        /**
         * Cancels the auth ticket.
         */
        private void CancelTicket()
        {
            SteamUser.CancelAuthTicket(m_authTicket);
        }
    }
}

#endif // UNITY_STANDALONE_WIN
