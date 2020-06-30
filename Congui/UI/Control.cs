// <copyright file="Control.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.UI {
    using System;
    using System.Text;

    public abstract class Control {
        public Control(string name, int width, int height) {
            this.Name = name;
            this.Renderable = new Renderable(width, height);
        }

        public string Name { get; internal set; }

        internal bool WasChanged { get; set; }

        protected Renderable Renderable { get; set; }

        public abstract Renderable GetRenderable();
    }
}
