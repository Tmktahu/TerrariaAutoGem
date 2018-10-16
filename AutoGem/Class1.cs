using System;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace AutoGem {
    [ApiVersion(2, 1)]
    public class AutoGem : TerrariaPlugin {
        public override string Author => "Fryke";
        public override string Description => "Automatically gives gems on spawn.";
        public override string Name => "AutoGem";
        public override Version Version => new Version(1, 1, 0, 3);

        /// Initializes a new instance of the TestPlugin class.
        /// This is where you set the plugin's order and perfrom other constructor logic
        public AutoGem(Main game) : base(game) {

        }

        /// Handles plugin initialization. 
        /// Fired when the server is started and the plugin is being loaded.
        /// You may register hooks, perform loading procedures etc here.
        public override void Initialize() {
            ServerApi.Hooks.NetSendData.Register(this, OnSendData);
        }

        /// Handles plugin disposal logic.
        /// *Supposed* to fire when the server shuts down.
        /// You should deregister hooks and free all resources here.
        protected override void Dispose(bool disposing) {
            if (disposing) {
                // Deregister hooks here
                ServerApi.Hooks.NetSendData.Deregister(this, OnSendData);
            }
            base.Dispose(disposing);
        }

       
        public void OnSendData(SendDataEventArgs args) {
            if (args.MsgId == PacketTypes.PlayerSpawn) {

                TSPlayer thePlayer = TShock.Players[args.number]; // Get the player
                Group playerGroup = thePlayer.Group; // Get the player's group
                thePlayer.Group = new SuperAdminGroup(); // Set them to superadmin

                if (playerGroup.Name == "red") { // If they are on red
                    Commands.HandleCommand(thePlayer, "/i large ruby"); // Give them a red gem
                } else if(playerGroup.Name == "green") { // If they are on green
                    Commands.HandleCommand(thePlayer, "/i large emerald"); // Give them a green gem
                } else if (playerGroup.Name == "yellow") { // If they are on yellow
                    Commands.HandleCommand(thePlayer, "/i large topaz"); // Give them a yellow gem
                } else if (playerGroup.Name == "blue") { // If they are on blue
                    Commands.HandleCommand(thePlayer, "/i large sapphire"); // Give them a blue gem
                }

                thePlayer.Group = playerGroup; // Set them back to their normal group
            }
        }
    }
}