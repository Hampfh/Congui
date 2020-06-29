// <copyright file="UserInterface.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.UI {
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Provides functionality for managing a Congui user interface.
    /// </summary>
    public sealed class UserInterface : Control {
        private Dictionary<string, int> nameDictionary;
        private List<Control> controls;
        private StringBuilder renderable;   // I have no better name for this atm

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInterface"/> class.
        /// </summary>
        public UserInterface() : this(controls: new List<Control>()) {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInterface"/> class.
        /// </summary>
        /// <param name="controls">A <see cref="List{Control}"/> containing the controls to be added to the <see cref="UserInterface"/>.</param>
        public UserInterface(List<Control> controls) : base(name: "UserInterface") {
            nameDictionary = new Dictionary<string, int>();
            foreach (var control in controls) {
                RegisterControlName(control);
            }

            this.controls = controls;
        }

        /// <summary>
        /// Adds a specified <see cref="Control"/> to this <see cref="UserInterface"/>.
        /// </summary>
        /// <param name="control">The <see cref="Control"/> to be added to this <see cref="UserInterface"/>.</param>
        public void AddControl(Control control) {
            this.RegisterControlName(control);
            this.controls.Add(control);
        }

        /// <summary>
        /// Gets a renderable <see cref="StringBuilder"/> reference containing the current <see cref="UserInterface"/> state.
        /// </summary>
        /// <returns>A <see cref="StringBuilder"/> reference containing the current <see cref="UserInterface"/> state.</returns>
        public override StringBuilder GetRenderable() {
            // foreach (var control in controls) {
            //     renderable.
            // }

            return null;
        }

        private void RegisterControlName(Control control) {
            string name = control.Name;
            if (this.nameDictionary.TryGetValue(name, out int count)) {
                this.nameDictionary[name] = ++count;
                name += count;
            }
            else {
                this.nameDictionary.Add(name, 0);
            }

            control.Name = name;
        }
    }
}
