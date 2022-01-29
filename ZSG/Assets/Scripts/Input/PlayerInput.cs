// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""b4dbb086-b3b1-4f88-b6c9-084b279797b2"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""e624753e-3bc4-4022-be49-e1444b18411f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""43566daf-9147-4291-8db8-1ecf30f5ed6e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press(behavior=2)""
                },
                {
                    ""name"": ""ReloadWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""cd096bab-5458-4f3b-9ef1-890f656c4b4e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""PassThrough"",
                    ""id"": ""7af84db9-c7e6-4a32-b1b3-290c448c552b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""HealPlayer"",
                    ""type"": ""Button"",
                    ""id"": ""306a8f1c-cf78-49e6-94fe-05a28f5d964b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""GameMenu"",
                    ""type"": ""Button"",
                    ""id"": ""26331139-aaf1-42d3-9234-32fe7540aa96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""4f75fc06-fda7-4f9b-8984-cd39920ded91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""bfcb0505-3547-4e31-9b43-ef38ec46fc9c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""KnifeWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""76e85076-846e-4fd9-b5f3-0f044aa3890a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""43c09b57-a132-491e-8cdf-cd2ff202bafe"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""77eb54f2-6ec9-4bf0-8be7-065c0e7fbe75"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""53b839c8-1924-4ea8-93bd-90d9c11ceb03"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""5ca5f1f4-822b-4a35-9959-f8a9fadcc4a0"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""4b16eebb-1773-4fb1-9f8c-a1d14e08643d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""07ef83eb-a0c7-4889-a95b-260c80cdd0d0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""2fe53784-fde2-4807-8b5a-c3720ed8b686"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""3e313ee7-9c6b-46a4-9669-245eeb31433c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9ec4b77d-1229-4518-95c8-bc6685da3858"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c205aed1-f45e-4546-98a1-112d633fe63f"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6efd383c-d03c-45ee-b2b2-e02e9013ee50"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""dea9d56b-c258-46b8-9c48-8f849abbd471"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fedb9e8b-d7b1-49ac-b907-cd9aca62d487"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ReloadWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32dc0837-70b4-4d22-9cfc-155d3dfb637d"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2dffb268-2d7c-4512-908d-bcba09b127eb"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""HealPlayer"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""991f5424-4899-4529-93cb-d01d1cde1eaa"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GameMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7e15fc40-7a1d-47ba-9e2b-3edbc77acadc"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""568e99ea-671b-4f2c-9089-82958d2a794d"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee2c0116-cf92-49d9-8ddc-8bdd5e798a8c"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KnifeWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83a47600-1093-4210-bba3-e60ebd80662a"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Shoot = m_Gameplay.FindAction("Shoot", throwIfNotFound: true);
        m_Gameplay_ReloadWeapon = m_Gameplay.FindAction("ReloadWeapon", throwIfNotFound: true);
        m_Gameplay_CameraZoom = m_Gameplay.FindAction("CameraZoom", throwIfNotFound: true);
        m_Gameplay_HealPlayer = m_Gameplay.FindAction("HealPlayer", throwIfNotFound: true);
        m_Gameplay_GameMenu = m_Gameplay.FindAction("GameMenu", throwIfNotFound: true);
        m_Gameplay_PrimaryWeapon = m_Gameplay.FindAction("PrimaryWeapon", throwIfNotFound: true);
        m_Gameplay_SecondaryWeapon = m_Gameplay.FindAction("SecondaryWeapon", throwIfNotFound: true);
        m_Gameplay_KnifeWeapon = m_Gameplay.FindAction("KnifeWeapon", throwIfNotFound: true);
        m_Gameplay_Inventory = m_Gameplay.FindAction("Inventory", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Shoot;
    private readonly InputAction m_Gameplay_ReloadWeapon;
    private readonly InputAction m_Gameplay_CameraZoom;
    private readonly InputAction m_Gameplay_HealPlayer;
    private readonly InputAction m_Gameplay_GameMenu;
    private readonly InputAction m_Gameplay_PrimaryWeapon;
    private readonly InputAction m_Gameplay_SecondaryWeapon;
    private readonly InputAction m_Gameplay_KnifeWeapon;
    private readonly InputAction m_Gameplay_Inventory;
    public struct GameplayActions
    {
        private @PlayerInput m_Wrapper;
        public GameplayActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Shoot => m_Wrapper.m_Gameplay_Shoot;
        public InputAction @ReloadWeapon => m_Wrapper.m_Gameplay_ReloadWeapon;
        public InputAction @CameraZoom => m_Wrapper.m_Gameplay_CameraZoom;
        public InputAction @HealPlayer => m_Wrapper.m_Gameplay_HealPlayer;
        public InputAction @GameMenu => m_Wrapper.m_Gameplay_GameMenu;
        public InputAction @PrimaryWeapon => m_Wrapper.m_Gameplay_PrimaryWeapon;
        public InputAction @SecondaryWeapon => m_Wrapper.m_Gameplay_SecondaryWeapon;
        public InputAction @KnifeWeapon => m_Wrapper.m_Gameplay_KnifeWeapon;
        public InputAction @Inventory => m_Wrapper.m_Gameplay_Inventory;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Shoot.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnShoot;
                @ReloadWeapon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReloadWeapon;
                @ReloadWeapon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReloadWeapon;
                @ReloadWeapon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReloadWeapon;
                @CameraZoom.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCameraZoom;
                @HealPlayer.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHealPlayer;
                @HealPlayer.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHealPlayer;
                @HealPlayer.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHealPlayer;
                @GameMenu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGameMenu;
                @GameMenu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGameMenu;
                @GameMenu.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnGameMenu;
                @PrimaryWeapon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimaryWeapon;
                @PrimaryWeapon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimaryWeapon;
                @PrimaryWeapon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPrimaryWeapon;
                @SecondaryWeapon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondaryWeapon;
                @SecondaryWeapon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondaryWeapon;
                @SecondaryWeapon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSecondaryWeapon;
                @KnifeWeapon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKnifeWeapon;
                @KnifeWeapon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKnifeWeapon;
                @KnifeWeapon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnKnifeWeapon;
                @Inventory.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @ReloadWeapon.started += instance.OnReloadWeapon;
                @ReloadWeapon.performed += instance.OnReloadWeapon;
                @ReloadWeapon.canceled += instance.OnReloadWeapon;
                @CameraZoom.started += instance.OnCameraZoom;
                @CameraZoom.performed += instance.OnCameraZoom;
                @CameraZoom.canceled += instance.OnCameraZoom;
                @HealPlayer.started += instance.OnHealPlayer;
                @HealPlayer.performed += instance.OnHealPlayer;
                @HealPlayer.canceled += instance.OnHealPlayer;
                @GameMenu.started += instance.OnGameMenu;
                @GameMenu.performed += instance.OnGameMenu;
                @GameMenu.canceled += instance.OnGameMenu;
                @PrimaryWeapon.started += instance.OnPrimaryWeapon;
                @PrimaryWeapon.performed += instance.OnPrimaryWeapon;
                @PrimaryWeapon.canceled += instance.OnPrimaryWeapon;
                @SecondaryWeapon.started += instance.OnSecondaryWeapon;
                @SecondaryWeapon.performed += instance.OnSecondaryWeapon;
                @SecondaryWeapon.canceled += instance.OnSecondaryWeapon;
                @KnifeWeapon.started += instance.OnKnifeWeapon;
                @KnifeWeapon.performed += instance.OnKnifeWeapon;
                @KnifeWeapon.canceled += instance.OnKnifeWeapon;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnReloadWeapon(InputAction.CallbackContext context);
        void OnCameraZoom(InputAction.CallbackContext context);
        void OnHealPlayer(InputAction.CallbackContext context);
        void OnGameMenu(InputAction.CallbackContext context);
        void OnPrimaryWeapon(InputAction.CallbackContext context);
        void OnSecondaryWeapon(InputAction.CallbackContext context);
        void OnKnifeWeapon(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
    }
}
