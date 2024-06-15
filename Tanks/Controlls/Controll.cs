using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace Tanks.Controlls
{
    internal class Controll
    {
        public Controll(KeyBindings keyBindings)
        {
            _keyBindings = keyBindings;
            GamePlayActionState = GetInitialDictionary();
        }
        private KeyBindings _keyBindings;
        public Dictionary<GameplayAction, bool> GamePlayActionState { get; }


        public static HashSet<GameplayAction> MovementGamePlayActions { get; } = new HashSet<GameplayAction>()
        {
            GameplayAction.Up,
            GameplayAction.Down,
            GameplayAction.Left,
            GameplayAction.Right,
        };

        public void SetControll(VirtualKey key)
        {
            UpdateControl(key, true);
        }

        public void UnsetControll(VirtualKey key)
        {
            UpdateControl(key, false);
        }

        public void UpdateControl(VirtualKey key, bool value)
        {
            GameplayAction? mappedAction = _keyBindings.GetGamePlayAction(key);
            if (mappedAction is GameplayAction correspondingAction)
            {
                GamePlayActionState[correspondingAction] = value;
            }
        }

        public bool GetActionState(GameplayAction gameplayAction)
        {

            return GamePlayActionState[gameplayAction];
        }

        private Dictionary<GameplayAction, bool> GetInitialDictionary()
        {
            return Enum.GetValues(typeof(GameplayAction))
                .Cast<GameplayAction>()
                .ToDictionary((enumValue) => enumValue, (_) => false);
        }
    }
}
