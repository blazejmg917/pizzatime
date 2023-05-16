//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/New Controls.inputactions
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

public partial class @NewControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @NewControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""New Controls"",
    ""maps"": [
        {
            ""name"": ""Vehicle"",
            ""id"": ""0fdb1e23-b9ee-45e9-b6d3-aacd4c503c9e"",
            ""actions"": [
                {
                    ""name"": ""AccelerateBrake"",
                    ""type"": ""PassThrough"",
                    ""id"": ""0b1d7d66-5c5b-4702-95d3-d761cb5f95ff"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Flippy"",
                    ""type"": ""PassThrough"",
                    ""id"": ""c94924ca-fb59-4c33-a217-67ac97099992"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Turn"",
                    ""type"": ""PassThrough"",
                    ""id"": ""74d648bc-ec7d-4142-8612-d6605ae65dbd"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""2be92ae1-db37-4b5e-8379-bdc17234dd45"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Stunt"",
                    ""type"": ""Button"",
                    ""id"": ""6ecd4086-4b38-44d8-9129-910bf3ad1a7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5999b9ad-c78d-4742-a44e-ce3bc8b19c30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Turbo"",
                    ""type"": ""Button"",
                    ""id"": ""7bb0c0a5-33bb-487a-b41a-cc757acbf7f4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""bcbc31e6-cd4c-4e78-8834-9681b88d9bb8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""ff92130a-7a53-4470-be0e-d649fc38b24d"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""a5a88805-cf6f-42c7-9778-1343d9dfb684"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""57dbb507-bcb5-44a0-a81f-8d9f3d36390e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f5dc487d-dda5-47b5-875a-b93a08df8077"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d949e440-4f4a-4a1f-afbf-42abaa18ee9c"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""6843dea2-0ea9-452d-a873-c5e4dd422b59"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""380831ad-30b7-48ae-8cb3-fae773768109"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f02cdf97-334f-4e29-a537-b3090e4424fe"",
                    ""path"": ""<DualShockGamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""714fc327-b515-4c66-808f-fe8abb56a08d"",
                    ""path"": ""<DualShockGamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AccelerateBrake"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""49e2481d-2aef-4741-918e-e2eee9e9a81a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3de4b6dc-6763-4f45-be15-deb755cb538a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""83062c5d-652a-4c27-8ea4-deedad814ceb"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""07faaca9-5e1b-4efa-bbd6-4b2432be0fe6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""cf5e4263-9700-4ec2-b967-b8989a121606"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""105c8b5f-dc03-4a0a-a4d5-e4529410b207"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""0ed07ad2-5a67-4d3f-87dd-0c432d154ba2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""d545a97b-5ad9-4840-84b7-22918047ee3c"",
                    ""path"": ""<DualShockGamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""f2a98d9c-d082-4b52-b76e-0459d1df99b5"",
                    ""path"": ""<DualShockGamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""03257f78-bd81-4252-8b7c-678fb8f06317"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69e1d373-53e4-429b-9506-cb7d006b5a12"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""11e9e553-2b5d-4ec9-bafd-6a3350f1dbc2"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stunt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""960f0ebc-e174-4edc-90c1-ec42bd3b812b"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Stunt"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""d86811fe-e4a2-4798-8cb8-52ccada860b2"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fb8bd88e-4543-4c36-b674-efa436195cd2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""3857e42e-0509-4353-95a8-5b9ba2a24cc4"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""42c8eccc-0861-4b10-bdc3-30f2a4dfb80e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""c59bba9e-66ce-413e-b161-17664e7fb501"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8b411028-5aed-4ace-aafc-bfb26679ad5e"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""16bd6c5f-72b3-4eea-bf09-2f7377aa2b5a"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""f5b73cf2-de00-4cc2-a6e0-7195a0c37136"",
                    ""path"": ""<DualShockGamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""4755a2e7-66ce-4fca-a0ee-47c265f278ff"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Flippy"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""94d0d188-881e-4fbe-8a11-ee1f7007b455"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""58c7406b-f8dd-40cb-b638-bbb74433e4a2"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd5fca17-713b-4622-b7dd-2208564bd474"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turbo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""baa4ba11-0b94-47ee-a131-c0b9625c72bc"",
                    ""path"": ""<DualShockGamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Turbo"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b56af354-a6d4-468d-ba9d-24fb2afd95c2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ccfe1b4e-9f63-4c57-a310-01afeadaa10d"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Vehicle
        m_Vehicle = asset.FindActionMap("Vehicle", throwIfNotFound: true);
        m_Vehicle_AccelerateBrake = m_Vehicle.FindAction("AccelerateBrake", throwIfNotFound: true);
        m_Vehicle_Flippy = m_Vehicle.FindAction("Flippy", throwIfNotFound: true);
        m_Vehicle_Turn = m_Vehicle.FindAction("Turn", throwIfNotFound: true);
        m_Vehicle_Pause = m_Vehicle.FindAction("Pause", throwIfNotFound: true);
        m_Vehicle_Stunt = m_Vehicle.FindAction("Stunt", throwIfNotFound: true);
        m_Vehicle_Jump = m_Vehicle.FindAction("Jump", throwIfNotFound: true);
        m_Vehicle_Turbo = m_Vehicle.FindAction("Turbo", throwIfNotFound: true);
        m_Vehicle_Interact = m_Vehicle.FindAction("Interact", throwIfNotFound: true);
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

    // Vehicle
    private readonly InputActionMap m_Vehicle;
    private IVehicleActions m_VehicleActionsCallbackInterface;
    private readonly InputAction m_Vehicle_AccelerateBrake;
    private readonly InputAction m_Vehicle_Flippy;
    private readonly InputAction m_Vehicle_Turn;
    private readonly InputAction m_Vehicle_Pause;
    private readonly InputAction m_Vehicle_Stunt;
    private readonly InputAction m_Vehicle_Jump;
    private readonly InputAction m_Vehicle_Turbo;
    private readonly InputAction m_Vehicle_Interact;
    public struct VehicleActions
    {
        private @NewControls m_Wrapper;
        public VehicleActions(@NewControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AccelerateBrake => m_Wrapper.m_Vehicle_AccelerateBrake;
        public InputAction @Flippy => m_Wrapper.m_Vehicle_Flippy;
        public InputAction @Turn => m_Wrapper.m_Vehicle_Turn;
        public InputAction @Pause => m_Wrapper.m_Vehicle_Pause;
        public InputAction @Stunt => m_Wrapper.m_Vehicle_Stunt;
        public InputAction @Jump => m_Wrapper.m_Vehicle_Jump;
        public InputAction @Turbo => m_Wrapper.m_Vehicle_Turbo;
        public InputAction @Interact => m_Wrapper.m_Vehicle_Interact;
        public InputActionMap Get() { return m_Wrapper.m_Vehicle; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(VehicleActions set) { return set.Get(); }
        public void SetCallbacks(IVehicleActions instance)
        {
            if (m_Wrapper.m_VehicleActionsCallbackInterface != null)
            {
                @AccelerateBrake.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnAccelerateBrake;
                @AccelerateBrake.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnAccelerateBrake;
                @AccelerateBrake.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnAccelerateBrake;
                @Flippy.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnFlippy;
                @Flippy.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnFlippy;
                @Flippy.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnFlippy;
                @Turn.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnTurn;
                @Turn.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnTurn;
                @Turn.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnTurn;
                @Pause.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnPause;
                @Stunt.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnStunt;
                @Stunt.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnStunt;
                @Stunt.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnStunt;
                @Jump.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnJump;
                @Turbo.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnTurbo;
                @Turbo.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnTurbo;
                @Turbo.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnTurbo;
                @Interact.started -= m_Wrapper.m_VehicleActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_VehicleActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_VehicleActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_VehicleActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AccelerateBrake.started += instance.OnAccelerateBrake;
                @AccelerateBrake.performed += instance.OnAccelerateBrake;
                @AccelerateBrake.canceled += instance.OnAccelerateBrake;
                @Flippy.started += instance.OnFlippy;
                @Flippy.performed += instance.OnFlippy;
                @Flippy.canceled += instance.OnFlippy;
                @Turn.started += instance.OnTurn;
                @Turn.performed += instance.OnTurn;
                @Turn.canceled += instance.OnTurn;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
                @Stunt.started += instance.OnStunt;
                @Stunt.performed += instance.OnStunt;
                @Stunt.canceled += instance.OnStunt;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Turbo.started += instance.OnTurbo;
                @Turbo.performed += instance.OnTurbo;
                @Turbo.canceled += instance.OnTurbo;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public VehicleActions @Vehicle => new VehicleActions(this);
    public interface IVehicleActions
    {
        void OnAccelerateBrake(InputAction.CallbackContext context);
        void OnFlippy(InputAction.CallbackContext context);
        void OnTurn(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
        void OnStunt(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnTurbo(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
