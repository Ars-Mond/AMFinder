using System;
using System.Collections.Generic;
using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.MathTools;

namespace AMFinder
{
    public class FindBlockCommand : ClientChatCommand
    {
        public ICoreClientAPI Api;

        public FindBlockCommand()
        {
            Command = "FindBlock";
            Description = "Shows the NameId of the block the character is looking at. ";
            Syntax = ".findblock <NameID>";
        }

        public override void CallHandler(IPlayer player, int groupId, CmdArgs args)
        {
            try
            {
                Api.ShowChatMessage("======== FindBlock ========");
                if (args.Length != 1)
                {
                    Api.ShowChatMessage("Error: The Argument is null!");
                    Api.ShowChatMessage($"Syntax: {Syntax}");
                    
                    return;
                }
                var worldBlockAccessor = Api.World.BlockAccessor;
                var blockPos = Api.World.Player.CurrentBlockSelection.Position;

                BlockPos temp = new BlockPos(blockPos.X - 10, blockPos.Y - 10, blockPos.Z - 10);
                List<string> list = new List<string>();
                
                for (int i = 0; i < 20; i++)
                {
                    for (int j = 0; j < 20; j++)
                    {
                        for (int k = 0; k < 20; k++)
                        {
                            var t = worldBlockAccessor.GetBlock(temp.X + i, temp.Y + j, temp.Z + k).Code.Path;
                            
                            if (t == args[0]) list.Add(SubPos(temp, i, j, k, blockPos).ToString());
                        }
                    }
                }
                
                if (list.Count == 0)
                {
                    Api.ShowChatMessage($"Error: The {args[0]} block was not found!");
                    return;
                }
                foreach (var item in list)
                {
                    Api.ShowChatMessage(item);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        private string Format<T> (string text, T a) => $"{text}: {a}";

        private BlockPos AddPos(BlockPos blockPos, int x, int y, int z)
        {
            return new BlockPos(x, y, z) + blockPos;
        }

        private BlockPos Pos(int x, int y, int z)
        {
            return new BlockPos(x, y, z);
        }

        private BlockPos SubPos(BlockPos temp, int x, int y, int z, BlockPos blockPos)
        {
            return temp + new BlockPos(x, y, z) - blockPos;
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