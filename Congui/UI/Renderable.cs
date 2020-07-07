// <copyright file="Renderable.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.UI {
    using System;
    using System.Drawing;

    /// <summary>
    /// Contains the data to be rendered.
    /// </summary>
    public struct Renderable {
        /// <summary>
        /// The width of this <see cref="Renderable"/> measured in the parent's(???) set number of columns and rows.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// The height of this <see cref="Renderable"/> measured in the parent's(???) set number of columns and rows.
        /// </summary>
        public readonly int Height;

        public readonly Point Position;

        private readonly char[] buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderable"/> struct.
        /// </summary>
        /// <param name="width">The width of this <see cref="Renderable"/> measured in the parent's(???) set number of columns and rows.</param>
        /// <param name="height">The height of this <see cref="Renderable"/> measured in the parent's(???) set number of columns and rows.</param>
        /// <param name="x">The x position of this <see cref="Renderable"/>.</param>
        /// <param name="y">The y position of this <see cref="Renderable"/>.</param>
        public Renderable(int width, int height, int x, int y) {
            this.Width = width;
            this.Height = height;
            this.Position = new Point(x, y);
            this.buffer = new char[width * height];
        }

        /// <summary>
        /// Gets the item at the specified coordinate from this <see cref="Renderable"/>.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The item at the specified coordinate.</returns>
        public char GetAt(int x, int y) {
            return this.buffer[this.CalculateIndex(x, y)];
        }

        /// <summary>
        /// Sets an item at the specified coordinate in this <see cref="Renderable"/>.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="newValue">The new value to be set at the specified coordinate.</param>
        public void SetAt(int x, int y, char newValue) {
            this.buffer[this.CalculateIndex(x, y)] = newValue;
        }

        /// <summary>
        /// Appends a <see cref="Renderable"/> to this <see cref="Renderable"/>.
        /// </summary>
        /// <param name="renderable">The <see cref="Renderable"/> to append.</param>
        public void Append(Renderable renderable) {
            int renderableWidthBounds = renderable.Width + renderable.Position.X;
            int renderableHeightBounds = renderable.Height + renderable.Position.Y;
            if (renderableWidthBounds > this.Width || renderableHeightBounds > this.Height ||
                renderable.Position.X < 0 || renderable.Position.Y < 0) {
                throw new Exception("The renderable exceeds the bounds of the renderable it is to be appended to.");
            }

            // TODO: implement(improve?) location-awareness for renderables/controls
            // fetching renderable.Position etc. may cause artifacts as it may be changed while this runs? (to be tested)
            for (int x = renderable.Position.X; x < renderableWidthBounds; x++) {
                for (int y = renderable.Position.Y; y < renderableHeightBounds; y++) {
                    char toAppend = renderable.GetAt(x - renderable.Position.X, y - renderable.Position.Y);
                    this.SetAt(x, y, newValue: toAppend);
                }
            }
        }

        private int CalculateIndex(int x, int y) {
            int index = x + (y * this.Width);
            if (index > this.buffer.Length) {
                string outOfRangeParam;
                if (x > this.Width) {
                    outOfRangeParam = nameof(x);
                }
                else {
                    outOfRangeParam = nameof(y);
                }

                throw new ArgumentOutOfRangeException(paramName: outOfRangeParam, message: $"The given coordinate was out of range of the {this.GetType().FullName} buffer.");
            }

            return index;
        }
   }
}