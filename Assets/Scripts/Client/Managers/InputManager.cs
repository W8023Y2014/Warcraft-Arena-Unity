﻿using Core;

namespace Client
{
    public class InputManager : SingletonGameObject<InputManager>
    {
        public Player OriginalPlayer { get; private set; }

        private WorldManager worldManager;

        public void Initialize(WorldManager worldManager)
        {
            this.worldManager = worldManager;

            worldManager.WorldEntityManager.EventEntityAttached += OnEventEntityAttached;
            worldManager.WorldEntityManager.EventEntityDetach += OnEventEntityDetach;
        }

        public void Deinitialize()
        {
            worldManager.WorldEntityManager.EventEntityAttached -= OnEventEntityAttached;
            worldManager.WorldEntityManager.EventEntityDetach -= OnEventEntityDetach;

            worldManager = null;
        }

        private void OnEventEntityAttached(WorldEntity worldEntity)
        {
            var player = worldEntity as Player;
            if (player != null && player.IsOwner)
                OriginalPlayer = player;
        }

        private void OnEventEntityDetach(WorldEntity worldEntity)
        {
            if (worldEntity == OriginalPlayer)
                OriginalPlayer = null;
        }
    }
}