// <copyright file="Control.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.UI {
    using System;
    using System.Text;

    public abstract class Control {
        internal bool WasChanged { get; set; }

        public string Name { get; internal set; }

        public Control(string name) {
            Name = name;
        }

        public abstract StringBuilder GetRenderable();
    }
}
