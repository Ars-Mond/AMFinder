using Vintagestory.API.Client;
using Vintagestory.API.Common;
using Vintagestory.API.Server;

namespace AMFinder
{
    public class AmFinderMod : ModSystem
    {
        private ICoreClientAPI _coreClientApi;
        private readonly FindBlockCommand _findBlockCommand;
        private readonly CheckBlockCommand _checkBlockCommand;

        public AmFinderMod()
        {
            _findBlockCommand = new FindBlockCommand();
            _checkBlockCommand = new CheckBlockCommand();
        }

        public override void StartClientSide(ICoreClientAPI api)
        {
            base.StartClientSide(api);

            _coreClientApi = api;
            _findBlockCommand.Api = api;
            _checkBlockCommand.Api = api;

            _coreClientApi.RegisterCommand(_findBlockCommand);
            _coreClientApi.RegisterCommand(_checkBlockCommand);
            
            _coreClientApi.ShowChatMessage("AM~Finder load!\n");
        }
    }
}