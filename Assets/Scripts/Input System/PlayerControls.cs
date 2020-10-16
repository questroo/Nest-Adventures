// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input System/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""ActionMap"",
            ""id"": ""c9c4f5ab-ebbc-4b7c-bbae-10c5839a23d5"",
            ""actions"": [
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""59233ea5-1927-4e62-8641-f4b615e5cca6"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""DodgeRoll"",
                    ""type"": ""Button"",
                    ""id"": ""d5c03d16-8305-4774-afeb-ec34b424564b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""LockOn"",
                    ""type"": ""Button"",
                    ""id"": ""be0daf1f-f848-4470-b97f-ca76586106f2"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Health Potion"",
                    ""type"": ""Button"",
                    ""id"": ""6b79913d-bcd8-452e-879c-5009289b828d"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""a8cb8abf-b70a-4286-b5c4-0be0bf180ced"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""27fc3cd0-ec95-445c-8b65-ed31c504e588"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CharacterSwap"",
                    ""type"": ""Button"",
                    ""id"": ""23c6a0dd-2628-48b3-8be3-d0862b1e8c0c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""17abe301-c350-4286-97fa-fea5af25d099"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4 Controller"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7eee5226-5b20-49c6-8a9d-a7edd7f3d3ee"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7871f02b-9213-4505-8318-95ec10641f82"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4 Controller"",
                    ""action"": ""DodgeRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a1da1e1-786e-45c9-a6e9-4b04d48b3e31"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""DodgeRoll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38c81d79-cad7-4406-ae4f-f27edfecba66"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4 Controller"",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3e2a31e9-5c3c-4d1e-96f3-a2831516d288"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LockOn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad54847b-8b3b-4d63-9316-e6d84e8d0c15"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4 Controller"",
                    ""action"": ""Health Potion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4609c6fb-371a-4367-bc43-d67c16acf84d"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Health Potion"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35853fcd-6814-465c-92f6-b623dfb712b6"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4 Controller"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4798e611-5dca-4385-83a6-2e106fd48d7c"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""135c2910-cca9-4931-8bdd-7c58faa36c9c"",
                    ""path"": ""<DualShockGamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4 Controller"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bafbacd2-8262-41b5-b399-90007632a6cf"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""329f1540-dfe2-4233-a002-ebd441638aed"",
                    ""path"": ""<DualShockGamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PS4 Controller"",
                    ""action"": ""CharacterSwap"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PS4 Controller"",
            ""bindingGroup"": ""PS4 Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // ActionMap
        m_ActionMap = asset.FindActionMap("ActionMap", throwIfNotFound: true);
        m_ActionMap_Attack = m_ActionMap.FindAction("Attack", throwIfNotFound: true);
        m_ActionMap_DodgeRoll = m_ActionMap.FindAction("DodgeRoll", throwIfNotFound: true);
        m_ActionMap_LockOn = m_ActionMap.FindAction("LockOn", throwIfNotFound: true);
        m_ActionMap_HealthPotion = m_ActionMap.FindAction("Health Potion", throwIfNotFound: true);
        m_ActionMap_Move = m_ActionMap.FindAction("Move", throwIfNotFound: true);
        m_ActionMap_MoveCamera = m_ActionMap.FindAction("MoveCamera", throwIfNotFound: true);
        m_ActionMap_CharacterSwap = m_ActionMap.FindAction("CharacterSwap", throwIfNotFound: true);
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

    // ActionMap
    private readonly InputActionMap m_ActionMap;
    private IActionMapActions m_ActionMapActionsCallbackInterface;
    private readonly InputAction m_ActionMap_Attack;
    private readonly InputAction m_ActionMap_DodgeRoll;
    private readonly InputAction m_ActionMap_LockOn;
    private readonly InputAction m_ActionMap_HealthPotion;
    private readonly InputAction m_ActionMap_Move;
    private readonly InputAction m_ActionMap_MoveCamera;
    private readonly InputAction m_ActionMap_CharacterSwap;
    public struct ActionMapActions
    {
        private @PlayerControls m_Wrapper;
        public ActionMapActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Attack => m_Wrapper.m_ActionMap_Attack;
        public InputAction @DodgeRoll => m_Wrapper.m_ActionMap_DodgeRoll;
        public InputAction @LockOn => m_Wrapper.m_ActionMap_LockOn;
        public InputAction @HealthPotion => m_Wrapper.m_ActionMap_HealthPotion;
        public InputAction @Move => m_Wrapper.m_ActionMap_Move;
        public InputAction @MoveCamera => m_Wrapper.m_ActionMap_MoveCamera;
        public InputAction @CharacterSwap => m_Wrapper.m_ActionMap_CharacterSwap;
        public InputActionMap Get() { return m_Wrapper.m_ActionMap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionMapActions set) { return set.Get(); }
        public void SetCallbacks(IActionMapActions instance)
        {
            if (m_Wrapper.m_ActionMapActionsCallbackInterface != null)
            {
                @Attack.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnAttack;
                @Attack.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnAttack;
                @Attack.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnAttack;
                @DodgeRoll.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnDodgeRoll;
                @DodgeRoll.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnDodgeRoll;
                @DodgeRoll.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnDodgeRoll;
                @LockOn.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLockOn;
                @LockOn.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLockOn;
                @LockOn.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnLockOn;
                @HealthPotion.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnHealthPotion;
                @HealthPotion.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnHealthPotion;
                @HealthPotion.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnHealthPotion;
                @Move.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMove;
                @MoveCamera.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnMoveCamera;
                @CharacterSwap.started -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnCharacterSwap;
                @CharacterSwap.performed -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnCharacterSwap;
                @CharacterSwap.canceled -= m_Wrapper.m_ActionMapActionsCallbackInterface.OnCharacterSwap;
            }
            m_Wrapper.m_ActionMapActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Attack.started += instance.OnAttack;
                @Attack.performed += instance.OnAttack;
                @Attack.canceled += instance.OnAttack;
                @DodgeRoll.started += instance.OnDodgeRoll;
                @DodgeRoll.performed += instance.OnDodgeRoll;
                @DodgeRoll.canceled += instance.OnDodgeRoll;
                @LockOn.started += instance.OnLockOn;
                @LockOn.performed += instance.OnLockOn;
                @LockOn.canceled += instance.OnLockOn;
                @HealthPotion.started += instance.OnHealthPotion;
                @HealthPotion.performed += instance.OnHealthPotion;
                @HealthPotion.canceled += instance.OnHealthPotion;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @CharacterSwap.started += instance.OnCharacterSwap;
                @CharacterSwap.performed += instance.OnCharacterSwap;
                @CharacterSwap.canceled += instance.OnCharacterSwap;
            }
        }
    }
    public ActionMapActions @ActionMap => new ActionMapActions(this);
    private int m_PS4ControllerSchemeIndex = -1;
    public InputControlScheme PS4ControllerScheme
    {
        get
        {
            if (m_PS4ControllerSchemeIndex == -1) m_PS4ControllerSchemeIndex = asset.FindControlSchemeIndex("PS4 Controller");
            return asset.controlSchemes[m_PS4ControllerSchemeIndex];
        }
    }
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IActionMapActions
    {
        void OnAttack(InputAction.CallbackContext context);
        void OnDodgeRoll(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
        void OnHealthPotion(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnCharacterSwap(InputAction.CallbackContext context);
    }
}
