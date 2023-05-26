using System;
using System.Collections.Generic;
using AMFinder.Properties;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace AMFinder
{
    public class FindBlockCommand : ClientChatCommand
    {
        public ICoreClientAPI Api;
        
        
        private long _timerForUpdateBlock;
        private bool _start;

        private BlockPos _spawnPosition;
        private BlockPos _playerPosition;

        public FindBlockCommand()
        {
            Command = "FindBlock";
            Description = "Shows the NameId of the block the character is looking at. ";
            Syntax = ".findblock <NameID> <Size>";

            _start = false;
        }

        public override void CallHandler(IPlayer player, int groupId, CmdArgs args)
        {
            try
            {
                if (_start == false) RegisterListener(true);

                Api.ShowChatMessage("======== FindBlock ========");
                
                if (args.Length != 2)
                {
                    Api.ShowChatMessage("Error: There are not enough arguments or they are not defined correctly!\n");
                    return;
                }
                
                var worldBlockAccessor = Api.World.BlockAccessor;
                var spawnPosition = _spawnPosition; //Api.World.DefaultSpawnPosition.AsBlockPos;
                var playerPosition = _playerPosition; //Api.World.Player.Entity.Pos.AsBlockPos; //Api.World.Player.CurrentBlockSelection.Position;
                
                
                Api.ShowChatMessage($"{spawnPosition}, {playerPosition}");

                spawnPosition.Y = 0;

                int size = 0;
                try
                {
                    size = Int32.Parse(args[1]);
                }
                catch (Exception e)
                {
                    Api.ShowChatMessage("Error: The size must be an integer number.!\n");
                    Api.World.Logger.Debug($"{e}");
                }

                BlockPos centerSearchPosition = new BlockPos(playerPosition.X - size / 2, playerPosition.Y - size / 4 * 3, playerPosition.Z - size / 2);
                List<string> list = new List<string>();
                
                for (int x = 0; x < size; x++)
                {
                    for (int y = 0; y < size; y++)
                    {
                        for (int z = 0; z < size; z++)
                        {
                            var t = worldBlockAccessor.GetBlock(centerSearchPosition.X + x, centerSearchPosition.Y + y, centerSearchPosition.Z + z).Code.Path;
                            
                            if (t == args[0]) list.Add((centerSearchPosition + new BlockPos(x, y, z) - spawnPosition).ToString());
                        }
                    }
                }
                
                if (list.Count == 0)
                {
                    Api.ShowChatMessage($"Error: The {args[0]} block was not found!\n");
                    return;
                }
                
                foreach (var item in list)
                {
                    Api.ShowChatMessage($"Cords: [ {item} ]");
                }
            }
            catch (Exception e)
            {
                Api.World.Logger.Fatal($"Crush mod: {e}");
            }
        }
        
        private void UpdatePositions(float temp)
        {
            _spawnPosition = Api.World.DefaultSpawnPosition.AsBlockPos;
            _spawnPosition.Y = 0;
            _playerPosition = Api.World.Player.Entity.Pos.AsBlockPos;
        }

        private void RegisterListener(bool state)
        {
            if (state)
            {
                _timerForUpdateBlock = Api.World.RegisterGameTickListener(UpdatePositions, 1000, 0);
                UpdatePositions(0);
                _start = true;
            }
            else
            {
                Api.World.UnregisterGameTickListener(_timerForUpdateBlock);
                _start = false;
            }
        }
    }
}

/*//Api.ShowChatMessage("x> " + (temp.X + i) + "y> " +  (temp.Y + j) + "x> " +  (temp.Z + k));
if (t == 5626 || t == 5629)
{
    list.Add(SubPos(temp, i, j, k, blockPos).ToString() + " Lime");
}
else if (t == 2489 || t == 2491)
{
    list.Add(SubPos(temp, i, j, k, blockPos).ToString() + " Clay");
}*/