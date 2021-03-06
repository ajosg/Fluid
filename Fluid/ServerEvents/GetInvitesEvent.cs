﻿using PlayerIOClient;
using System.Collections.Generic;

namespace Fluid.ServerEvents
{
    public class GetInvitesEvent : IServerEvent
    {
        /// <summary>
        /// Gets the raw message
        /// </summary>
        public Message Raw { get; set; }

        /// <summary>
        /// Gets the list of friend invites
        /// </summary>
        public List<Friend> Invites { get; internal set; }
    }
}
