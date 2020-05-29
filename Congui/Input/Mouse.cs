// <copyright file="Mouse.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.Input {
    using System;
    using System.ComponentModel;
    using System.Threading.Tasks;

    using Conhics;

    /// <summary>
    /// Contains functionality for mouse input.
    /// </summary>
    public static class Mouse {
        private static bool isEnabled;
        private static Task inputListener;

        /// <summary>
        /// Gets a value indicating whether mouse input can be enabled.
        /// </summary>
        /// <value>A value indicating whether mouse input can be enabled.</value>
        public static bool CanBeEnabled {
            get {
                // Returns whether IsCompleted is either true - implying completed, or null - implying inputListener is not initialised.
                return inputListener?.IsCompleted != false;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether mouse input is enabled.
        /// </summary>
        /// <value>A value indicating whether mouse input is enabled.</value>
        public static bool IsEnabled {
            get {
                return isEnabled;
            }

            set {
                if (value && inputListener?.IsCompleted == false) {
                    throw new Exception($"Mouse input can only be enabled if property CanBeEnabled is true. Property CanBeEnabled is {CanBeEnabled}.");
                }
                else if (value && value != isEnabled) {
                    EnableMouseInput();
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
            IntPtr inputHandle = Integration.GetStdHandle((int)Integration.StdHandle.InputHandle);
            ConfigureConsoleMode(inputHandle);
            inputListener = Task.Run(() => ListenForInput(inputHandle));
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

        private static void ListenForInput(IntPtr inputHandle) {
            Integration.INPUT_RECORD inputRecord = default;
            uint numberOfInputEvents = 0;
            uint numberOfInputEventsRead = 0;
            while (isEnabled) {
                if (numberOfInputEvents == 0) {
                    Integration.ManageNativeReturnValue(
                        returnValue: Integration.GetNumberOfConsoleInputEvents(
                            hConsoleInput: inputHandle,
                            lpcNumberOfEvents: out numberOfInputEvents));
                    if (!isEnabled) {
                        break;
                    }
                    else if (numberOfInputEvents == 0) {
                        continue;
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
}