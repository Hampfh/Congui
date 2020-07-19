// <copyright file="Label.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.UI {
    using Congui.Graphics;

    /// <summary>
    /// Provides functionality for displaying text in a <see cref="UserInterface"/>.
    /// </summary>
    public class Label : Control {
        private string contents;
        private float fontSize = 20f;

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        public Label()
        : this(contents: string.Empty) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="contents">The text of which the <see cref="Label"/> should contain.</param>
        public Label(string contents)
        : this(contents, fontSize: 20f) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="contents">The text of which the <see cref="Label"/> should contain.</param>
        /// <param name="fontSize">The em font size of the <see cref="Label"/>.</param>
        public Label(string contents, float fontSize)
        : this(contents, fontSize, x: 0, y: 0) {
        }

        public Label(string contents, float fontSize, int x, int y)
        : this(contents, fontSize, x, y, name: typeof(Label).Name) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="contents">The text that this <see cref="Label"/> should initially contain.</param>
        /// <param name="name">The unique identifier of this <see cref="Label"/> control.</param>
        public Label(string contents, float fontSize, int x, int y, string name)
        : base(width: 0, height: 0, x, y, name) {
            this.Contents = contents;
            this.FontSize = fontSize;
        }

        /// <summary>
        /// Gets or sets the contents of this <see cref="Label"/> control.
        /// </summary>
        /// <value>The contents of this <see cref="Label"/> control.</value>
        public string Contents {
            get {
                return this.contents;
            }

            set {
                this.contents = value;
                this.WasChanged = true;
            }
        }

        /// <summary>
        /// Gets or sets the font size of the contents in this <see cref="Label"/> control.
        /// </summary>
        /// <value>The font size of the contents in this <see cref="Label"/> control.</value>
        public float FontSize {
            get {
                return this.fontSize;
            }

            set {
                this.fontSize = value;
                this.WasChanged = true;
            }
        }

        /// <summary>
        /// Gets the <see cref="Renderable"/> for this <see cref="Label"/> control.
        /// </summary>
        /// <returns>The <see cref="Renderable"/> for this <see cref="Label"/> control.</returns>
        public override Renderable GetRenderable() {
            if (!this.WasChanged) {
                return this.Renderable;
            }

            this.WasChanged = false;
            this.Renderable = Text.CreateRenderable(this.contents, this.fontSize);
            return this.Renderable;
        }
    }
}