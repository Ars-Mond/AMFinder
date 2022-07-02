using System;
using Vintagestory.API.Client;
using Vintagestory.API.Common;

namespace AMFinder
{
    public class CheckBlockCommand : ClientChatCommand
    {
        public ICoreClientAPI Api;
        public CheckBlockCommand()
        {
            Command = "CheckBlock";
            Description = "Shows the \"NameId\" of the block the character is looking at.";
            Syntax = ".checkblock";
        }

        public override void CallHandler(IPlayer player, int groupId, CmdArgs args)
        { 
            try
            {
               var worldBlockAccessor = Api.World.BlockAccessor;
               var blockPos = Api.World.Player.CurrentBlockSelection.Position;
               var block = worldBlockAccessor.GetBlock(blockPos);
               ;
               Api.ShowChatMessage("========CheckBlock========");
               Api.ShowChatMessage( Format("NameID block", block.Code.Path) );
               Api.ShowChatMessage( Format("NumberID block", block.Id.ToString()) );
            }
            catch (Exception e)
            {
               Console.WriteLine(e);
            }
        }

        private string Format<T> (string text, T a) => $"{text}: {a}";
    }
}