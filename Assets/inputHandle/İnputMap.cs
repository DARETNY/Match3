//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.1
//     from Assets/inputHandle/İnputMap.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @İnputMap: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @İnputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""İnputMap"",
    ""maps"": [
        {
            ""name"": ""Swipe"",
            ""id"": ""0ae87d60-e9f8-4d3f-9a92-9c8e0898d270"",
            ""actions"": [
                {
                    ""name"": ""mapper"",
                    ""type"": ""Value"",
                    ""id"": ""f66e8b32-f6db-4d21-8bea-ae29a83ba23b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6f2f5da1-a2fc-44fc-a6bd-2ce2fd2bfa27"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mapper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""46d12715-9b21-4226-9697-6eef7e7e6a0a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mapper"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3dee117d-e736-444a-804f-1854966d9e34"",
                    ""path"": ""w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mapper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4432f808-23ed-497f-b854-575c81bb2fc3"",
                    ""path"": ""s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mapper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d3a1e495-ebd0-450e-9071-426201b0d254"",
                    ""path"": ""a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mapper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""43eddd01-102d-48bf-95c6-1ad75e795310"",
                    ""path"": ""d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""mapper"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Swipe
        m_Swipe = asset.FindActionMap("Swipe", throwIfNotFound: true);
        m_Swipe_mapper = m_Swipe.FindAction("mapper", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Swipe
    private readonly InputActionMap m_Swipe;
    private List<ISwipeActions> m_SwipeActionsCallbackInterfaces = new List<ISwipeActions>();
    private readonly InputAction m_Swipe_mapper;
    public struct SwipeActions
    {
        private @İnputMap m_Wrapper;
        public SwipeActions(@İnputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @mapper => m_Wrapper.m_Swipe_mapper;
        public InputActionMap Get() { return m_Wrapper.m_Swipe; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SwipeActions set) { return set.Get(); }
        public void AddCallbacks(ISwipeActions instance)
        {
            if (instance == null || m_Wrapper.m_SwipeActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_SwipeActionsCallbackInterfaces.Add(instance);
            @mapper.started += instance.OnMapper;
            @mapper.performed += instance.OnMapper;
            @mapper.canceled += instance.OnMapper;
        }

        private void UnregisterCallbacks(ISwipeActions instance)
        {
            @mapper.started -= instance.OnMapper;
            @mapper.performed -= instance.OnMapper;
            @mapper.canceled -= instance.OnMapper;
        }

        public void RemoveCallbacks(ISwipeActions instance)
        {
            if (m_Wrapper.m_SwipeActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ISwipeActions instance)
        {
            foreach (var item in m_Wrapper.m_SwipeActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_SwipeActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public SwipeActions @Swipe => new SwipeActions(this);
    public interface ISwipeActions
    {
        void OnMapper(InputAction.CallbackContext context);
    }
}
