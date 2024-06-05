using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Tanks.Controlls
{
    class KeyBindings
    {
        public string PlayerId { get; set; }
        public Dictionary<VirtualKey, GameplayAction> inputKeyToGameplayActionMap { get; }
        
        public KeyBindings()
        {
            inputKeyToGameplayActionMap = GetDefaultControls();
        }
        
        private Dictionary<VirtualKey, GameplayAction> GetDefaultControls()
        {
            return new Dictionary<VirtualKey, GameplayAction>
            {
                [VirtualKey.Up] = GameplayAction.Up,
                [VirtualKey.Down] = GameplayAction.Down,
                [VirtualKey.Left] = GameplayAction.Left,
                [VirtualKey.Right] = GameplayAction.Right,
                [VirtualKey.I] = GameplayAction.FireUp,
                [VirtualKey.K] = GameplayAction.FireDown,
                [VirtualKey.J] = GameplayAction.FireLeft,
                [VirtualKey.L] = GameplayAction.FireRight,
                [VirtualKey.Shift] = GameplayAction.Speed,
            };
        }

        private Dictionary<VirtualKey, GameplayAction> ControlsFromConfig()
        {
            throw new NotImplementedException();
        }

        public GameplayAction? GetGamePlayAction(VirtualKey key)
        {
            if(inputKeyToGameplayActionMap.TryGetValue(key, out GameplayAction value))
            {
                return value;
            }
            return null;
        }
    }
}
