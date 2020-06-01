// <copyright file="Mouse.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.Input {
    using System;
    using System.Threading.Tasks;

    using Congui.Events;

    using Conhics;

    /// <summary>
    /// Contains functionality for mouse input.
    /// </summary>
    public static class Mouse {
        private static bool isEnabled;
        private static IntPtr inputHandle;
        private static EventParameters mouseEventParameters = new EventParameters(
            condition: () => IsEnabled,
            subscribingMethod: UpdateInput);

        private static Integration.INPUT_RECORD inputRecord = default;
        private static uint numberOfInputEvents = 0;
        private static uint numberOfInputEventsRead = 0;

        /// <summary>
        /// Gets or sets a value indicating whether mouse input is enabled - true, or disabled - false.
        /// </summary>
        /// <value>A value indicating whether mouse input is enabled - true, or disabled - false.</value>
        public static bool IsEnabled {
            get {
                return isEnabled;
            }

            set {
                if (value == isEnabled) {
                    return;
                }
                else if (value) {
                    EnableMouseInput();
                }
                else if (!value) {
                    DisableMouseInput();
                }

                isEnabled = value;
            }
        }

        /// <summary>
        /// Gets an instance of the <see cref="MouseInput"/> struct with the most recent mouse input.
        /// </summary>
        /// <value>An instance of the <see cref="MouseInput"/> struct with the most recent mouse input.</value>
        public static MouseInput Input { get; private set; }

        private static void EnableMouseInput() {
            inputHandle = Integration.GetStdHandle((int)Integration.StdHandle.InputHandle);
            ConfigureConsoleMode(inputHandle);
            EventManager.RegisterEvent(mouseEventParameters);
        }

        private static void DisableMouseInput() {
            // TODO: might reconfigure console mode...
            EventManager.UnregisterEvent(mouseEventParameters);
        }

        private static void ConfigureConsoleMode(IntPtr inputHandle) {
            int consoleMode = 0;
            Integration.ManageNativeReturnValue(
                returnValue: Integration.GetConsoleMode(
                    hConsoleHandle: inputHandle,
                    lpMode: ref consoleMode));
            consoleMode |= Integration.ENABLE_MOUSE_INPUT;      // Enable mouse input
            consoleMode &= ~Integration.ENABLE_QUICK_EDIT_MODE; // Disable quick edit mode (ability to highlight text)
            consoleMode |= Integration.ENABLE_EXTENDED_FLAGS;   // Enable extended flags for quick edit to take effect
            Integration.ManageNativeReturnValue(
                returnValue: Integration.SetConsoleMode(
                    hConsoleHandle: inputHandle,
                    dwMode: consoleMode));
        }

        private static void UpdateInput() {
            if (numberOfInputEvents == 0) {
                Integration.ManageNativeReturnValue(
                    returnValue: Integration.GetNumberOfConsoleInputEvents(
                        hConsoleInput: inputHandle,
                        lpcNumberOfEvents: out numberOfInputEvents));
                if (!isEnabled || numberOfInputEvents == 0) {
                    return;
                }
            }

            Integration.ManageNativeReturnValue(
                returnValue: Integration.ReadConsoleInput(
                    hConsoleInput: inputHandle,
                    lpBuffer: ref inputRecord,
                    nLength: 1,
                    lpNumberOfEventsRead: ref numberOfInputEventsRead /* always equals 1 if nLength: 1 */));
            numberOfInputEvents -= numberOfInputEventsRead;
            Input = new MouseInput(inputRecord.MouseEvent);
        }
    }
}