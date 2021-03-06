﻿using PlayerIOClient;
using System.Collections.Generic;

namespace Fluid.ServerEvents
{
    public class GetFriendsEvent : IServerEvent
    {
        /// <summary>
        /// Gets the list of friends
        /// </summary>
        public List<Friend> Friends { get; internal set; }

        /// <summary>
        /// Gets the raw message
        /// </summary>
        public Message Raw { get; set; }
    }
}
