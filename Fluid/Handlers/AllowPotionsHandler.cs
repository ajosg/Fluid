﻿using Fluid.ServerEvents;
using PlayerIOClient;
using System.Collections.Generic;

namespace Fluid.Handlers
{
    public class AllowPotionsHandler : IMessageHandler
    {
        /// <summary>
        /// Gets the handled types
        /// </summary>
        public string[] HandleTypes
        {
            get { return new string[] { "allowpotions" }; }
        }

        /// <summary>
        /// Processes the message
        /// </summary>
        /// <param name="connectionBase">The connection base</param>
        /// <param name="message">The playerio message</param>
        /// <param name="handled">Whether the message was already handled</param>
        public void Process(ConnectionBase connectionBase, Message message, bool handled)
        {
            bool allowed = message.GetBoolean(0);

            WorldConnection worldCon = (WorldConnection)connectionBase;

            if (worldCon != null)
            {
                //Update each player's potions
                foreach (KeyValuePair<int, WorldPlayer> playerEntry in worldCon.Players.GetDictionary())
                {
                    foreach (KeyValuePair<Potion, PotionState> potionEntry in playerEntry.Value.Potions)
                    {
                        if (allowed)
                        {
                            if (potionEntry.Value == PotionState.Suspended)
                            {
                                playerEntry.Value.SetPotion(potionEntry.Key, PotionState.Active);
                            }
                        }
                        else
                        {
                            if (potionEntry.Value == PotionState.Active)
                            {
                                playerEntry.Value.SetPotion(potionEntry.Key, PotionState.Suspended);
                            }
                        }
                    }
                }
            }

            List<Potion> potions = new List<Potion>();
            for (uint i = 1; i < message.Count; i++)
            {
                int potionType = -1;
                if (int.TryParse(message.GetString(i), out potionType))
                {
                    Potion p = (Potion)potionType;
                    potions.Add(p);

                    if (!handled)
                    {
                        worldCon.Potions.SetEnabled(p, allowed);
                    }
                }
            }

            AllowPotionEvent allowPotionEvent = new AllowPotionEvent()
            {
                Raw = message,
                Potions = potions,
                AreEnabled = allowed
            };

            connectionBase.RaiseServerEvent<AllowPotionEvent>(allowPotionEvent);
        }
    }
}
