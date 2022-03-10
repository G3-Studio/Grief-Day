/*
MIT License

Copyright(c) 2019 Mitchel Thompson
www.angryarugula.com

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.InputSystem.Controls;


[RequireComponent(typeof(PlayerInputManager))]
public class KeyboardSplitter : MonoBehaviour
{
    [System.Serializable]
    public class KeyRemap
    {
        public Key original;
        public Key remapped;

        public KeyRemap(InputBinding binding)
        {
            KeyControl keyControl = Keyboard.current.allKeys.FirstOrDefault(x => x.name == binding.path.Split('/').Last());
            original = keyControl.keyCode;
            remapped = original;
        }
    }

    [System.Serializable]
    public class Player
    {
        public string name;
        public List<KeyRemap> routes = new List<KeyRemap>();
        internal Keyboard device;
        internal KeyboardState state;
        bool isDirty = false;

        public void AddDevice() => device = InputSystem.AddDevice<Keyboard>(name);
        public void RemoveDevice() => InputSystem.RemoveDevice(device);
        public void Set(Key key, bool pressed)
        {
            state.Set(key, pressed);
            isDirty = true;
        }

        public bool Push(bool ignoreDirty = false)
        {
            if (ignoreDirty || isDirty)
            {
                InputSystem.QueueStateEvent(device, state, Time.time + 1f);
                isDirty = false;
                return true;
            }

            return false;
        }
    }


    // Helper class to be a sneaky input pirate and steal actions from PlayerInputManager
    private class ActionCollection : IInputActionCollection
    {
        public InputBinding? bindingMask { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public ReadOnlyArray<InputDevice>? devices { get => m_devices; set => m_devices = value; }

        public ReadOnlyArray<InputControlScheme> controlSchemes => throw new System.NotImplementedException();

        List<InputAction> m_actions = new List<InputAction>();
        ReadOnlyArray<InputDevice>? m_devices;

        public ActionCollection(InputDevice device, InputAction action)
        {
            m_devices = new ReadOnlyArray<InputDevice>(new InputDevice[] { device });
            m_actions.Add(action);
        }

        public bool Contains(InputAction action)
        {
            return m_actions.Contains(action);
        }

        public void Disable()
        {
            throw new System.NotImplementedException();
        }

        public void Enable()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<InputAction> GetEnumerator()
        {
            return m_actions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_actions.GetEnumerator();
        }
    }

    public List<Player> players = new List<Player>();
    InputUser consumer;
    Keyboard keyboard;

    Dictionary<Key, System.Tuple<Player, Key>> routingTable = new Dictionary<Key, System.Tuple<Player, Key>>();

    Dictionary<Key, Key> keyTable = new Dictionary<Key, Key>();
    Dictionary<Key, Player> playerTable = new Dictionary<Key, Player>();

    private void Awake()
    {
        keyboard = Keyboard.current;
        consumer = InputUser.PerformPairingWithDevice(keyboard, consumer);

        foreach (var p in players)
            p.AddDevice();

        //Hijack the PlayerInputManager Join actions! Yarrr!
        var arr = new ActionCollection(Keyboard.current, GetComponent<PlayerInputManager>().joinAction.action);
        consumer.AssociateActionsWithUser(arr);

        UpdateRoutes();
    }

    private void OnEnable()
    {
        InputSystem.onEvent += OnEvent;
        InputSystem.onBeforeUpdate += PushPlayerStates;
    }


    private void OnDisable()
    {
        InputSystem.onEvent -= OnEvent;
        InputSystem.onBeforeUpdate -= PushPlayerStates;
    }

    private void OnDestroy()
    {
        consumer.UnpairDevicesAndRemoveUser();
        foreach (var p in players)
            p.RemoveDevice();
    }

    private void PushPlayerStates()
    {
        foreach (var p in players)
            p.Push();
    }

    private unsafe void OnEvent(InputEventPtr eventPtr, InputDevice device)
    {
        if (device == keyboard)
        {
            if (!eventPtr.IsA<StateEvent>())
                return;

            int controlCount = device.allControls.Count;
            var controls = device.allControls;

            for (int i = 0; i < controlCount; i++)
            {
                var control = controls[i];

                var statePtr = control.GetStatePtrFromStateEvent(eventPtr);
                if (statePtr == null)
                    continue;

                if (control is KeyControl)
                {
                    KeyControl keyControl = (KeyControl)control;
                    Key a = keyControl.keyCode;
                    if (keyTable.ContainsKey(a))
                    {
                        Key b = keyTable[a];
                        playerTable[a].Set(b, keyControl.ReadValueFromState(statePtr) > 0);
                    }

                }
            }

            eventPtr.handled = true;
        }

    }

    public void UpdateRoutes()
    {
        keyTable.Clear();
        playerTable.Clear();

        foreach (var p in players)
        {
            foreach (var route in p.routes)
            {
                keyTable[route.remapped] = route.original;
                playerTable[route.remapped] = p;
            }
        }
    }
}