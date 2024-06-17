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
        
        public KeyBindings(Dictionary<VirtualKey, GameplayAction> mapping)
        {
            inputKeyToGameplayActionMap = mapping;
        }

        public static Dictionary<VirtualKey, GameplayAction> GetDefaultControlsPlayer1()
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

        public static Dictionary<VirtualKey, GameplayAction> GetDefaultControlsPlayer2()
        {
            return new Dictionary<VirtualKey, GameplayAction>
            {
                [VirtualKey.T] = GameplayAction.Up,
                [VirtualKey.G] = GameplayAction.Down,
                [VirtualKey.F] = GameplayAction.Left,
                [VirtualKey.H] = GameplayAction.Right,
                [VirtualKey.W] = GameplayAction.FireUp,
                [VirtualKey.S] = GameplayAction.FireDown,
                [VirtualKey.A] = GameplayAction.FireLeft,
                [VirtualKey.D] = GameplayAction.FireRight,
                [VirtualKey.Z] = GameplayAction.Speed,
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
