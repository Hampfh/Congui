// <copyright file="Control.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.UI {
    using System;
    using System.Drawing;
    using System.Text;

    /// <summary>
    /// The base class for any <see cref="UserInterface"/> control.
    /// </summary>
    public abstract class Control {
        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="width">The width of the <see cref="Control"/>.</param>
        /// <param name="height">The height of the <see cref="Control"/>.</param>
        /// <param name="x">The x position of the <see cref="Control"/>.</param>
        /// <param name="y">The y position of the <see cref="Control"/>.</param>
        /// <param name="name">The unique identifier of this control.</param>
        protected Control(int width, int height, int x, int y, string name) {
            this.Name = name;
            this.Renderable = new Renderable(width, height, x, y);
        }

        public int Width {
            get {
                return this.Renderable.Width;
            }

            set {
                this.Renderable = new Renderable(
                    width: value,
                    height: this.Renderable.Height,
                    x: this.Renderable.Position.X,
                    y: this.Renderable.Position.Y);
            }
        }

        public int Height {
            get {
                return this.Renderable.Height;
            }

            set {
                this.Renderable = new Renderable(
                    width: this.Renderable.Width,
                    height: value,
                    x: this.Renderable.Position.X,
                    y: this.Renderable.Position.Y);
            }
        }

        public Point Position {
            get {
                return this.Renderable.Position;
            }

            set {
                this.Renderable = new Renderable(
                    width: this.Renderable.Width,
                    height: this.Renderable.Height,
                    x: value.X,
                    y: value.Y);
            }
        }

        /// <summary>
        /// Gets the unique identifier of this control.
        /// </summary>
        /// <value>The unique identifier of this control.</value>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets or sets a value indicating whether this control was changed or not.
        /// </summary>
        /// <value>A value indicating whether this control was changed or not.</value>
        internal bool WasChanged { get; set; }

        /// <summary>
        /// Gets or sets a provided <see cref="UI.Renderable"/> property for storing a changed <see cref="UI.Renderable"/>.
        /// </summary>
        /// <value>The <see cref="UI.Renderable"/> representing this control.</value>
        protected Renderable Renderable { get; set; }

        /// <summary>
        /// Gets the <see cref="UI.Renderable"/> for this control and should only construct a new <see cref="UI.Renderable"/> if necessary.
        /// <code>
        /// Example implementation:
        /// if (!this.WasChanged) {
        ///     return this.Renderable;
        /// }
        ///
        /// this.WasChanged = false;
        /// this.Renderable = new Congui.UI.Renderable();
        /// return this.Renderable;
        /// </code>
        /// </summary>
        /// <returns>The <see cref="Renderable"/> for this control.</returns>
        public abstract Renderable GetRenderable();
    }
}
