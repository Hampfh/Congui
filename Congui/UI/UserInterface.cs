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

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInterface"/> class.
        /// </summary>
        public UserInterface()
        : this(width: Console.WindowWidth, height: Console.WindowHeight, controls: new List<Control>()) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserInterface"/> class.
        /// </summary>
        /// <param name="width">The width of the <see cref="UserInterface"/>.</param>
        /// <param name="height">The height of the <see cref="UserInterface"/>.</param>
        /// <param name="controls">A <see cref="List{Control}"/> containing the controls to be added to the <see cref="UserInterface"/>.</param>
        public UserInterface(int width, int height, List<Control> controls)
        : base(width, height, name: "UserInterface") {
            this.nameDictionary = new Dictionary<string, int>();
            foreach (var control in controls) {
                this.RegisterControlName(control);
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
        public override Renderable GetRenderable() {
            foreach (var control in this.controls) {
                this.Renderable.Append(renderable: control.GetRenderable());
            }

            return this.Renderable;
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
