﻿using System;
using System.Net.NetworkInformation;

namespace Fluid
{
    public class ConnectionValue
    {
        /// <summary>
        /// Gets the pure connection lag
        /// </summary>
        /// <returns>The lag in milliseconds if successful; otherwise -1</returns>
        public long GetLag()
        {
            Ping playerIOPing = new Ping();
            PingReply reply = playerIOPing.Send("api.playerio.com");
            if (reply.Status == IPStatus.Success)
            {
                return reply.RoundtripTime / 2;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Gets the suggested block throttle
        /// </summary>
        public long GetBlockThrottle()
        {
            long lag = GetLag();
            if (lag != -1)
            {
                return Math.Max(10, (long)Math.Round(lag * 0.2));
            }
            else
            {
                return 20;
            }
        }
    }
}
