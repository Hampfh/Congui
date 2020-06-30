// <copyright file="Renderable.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.UI {
    using System;

    /// <summary>
    /// Contains the data to be rendered.
    /// </summary>
    public struct Renderable {
        /// <summary>
        /// The width of this <see cref="Renderable"/> measured in the parent's(???) set number of columns and rows.
        /// </summary>
        public int Width;

        /// <summary>
        /// The height of this <see cref="Renderable"/> measured in the parent's(???) set number of columns and rows.
        /// </summary>
        public int Height;

        /// <summary>
        /// The buffer of which to be rendered.
        /// </summary>
        internal readonly char[] Buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderable"/> struct.
        /// </summary>
        /// <param name="width">The width of this <see cref="Renderable"/> measured in the parent's(???) set number of columns and rows.</param>
        /// <param name="height">The height of this <see cref="Renderable"/> measured in the parent's(???) set number of columns and rows.</param>
        public Renderable(int width, int height) {
            this.Width = width;
            this.Height = height;
            this.Buffer = new char[width * height];
        }

        /// <summary>
        /// Gets the item at the specified coordinate from this <see cref="Renderable"/>.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <returns>The item at the specified coordinate.</returns>
        public char GetAt(int x, int y) {
            return this.Buffer[this.CalculateIndex(x, y)];
        }

        /// <summary>
        /// Sets an item at the specified coordinate in this <see cref="Renderable"/>.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        /// <param name="newValue">The new value to be set at the specified coordinate.</param>
        public void SetAt(int x, int y, char newValue) {
            this.Buffer[this.CalculateIndex(x, y)] = newValue;
        }

        /// <summary>
        /// Appends a <see cref="Renderable"/> to this <see cref="Renderable"/>.
        /// </summary>
        /// <param name="renderable">The <see cref="Renderable"/> to append.</param>
        public void Append(Renderable renderable) {
            if (renderable.Width > this.Width || renderable.Height > this.Height) {
                throw new Exception("The renderable exceeds the size of the renderable it is to be appended to.");
            }

            // TODO: implement location-awareness for renderables/controls
            for (int x = 0; x < renderable.Width; x++) {
                for (int y = 0; y < renderable.Height; y++) {
                    char toAppend = renderable.GetAt(x, y);
                    this.SetAt(x, y, newValue: toAppend);
                }
            }
        }

        private int CalculateIndex(int x, int y) {
            int index = x + (y * this.Width);
            if (index > this.Buffer.Length) {
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