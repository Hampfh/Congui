// <copyright file="Text.cs" company="Hampfh and haholm">
// Copyright (c) Hampfh and haholm. All rights reserved.
// </copyright>

namespace Congui.Graphics {
    using System;
    using System.Drawing;

    using Congui.UI;

    /// <summary>
    /// Provides functionality for rendering text.
    /// </summary>
    internal class Text {
        private static Bitmap bitmap = new Bitmap(width: Console.WindowWidth, height: Console.WindowHeight);
        private static Graphics graphics = Graphics.FromImage(bitmap);

        /// <summary>
        /// Creates a <see cref="Renderable"/> from a string and a float font size.
        /// </summary>
        /// <param name="text">The text to be rendered onto a <see cref="Renderable"/>.</param>
        /// <param name="fontSize">The em font size of the text.</param>
        /// <returns>A <see cref="Renderable"/> with the specified text rendered onto it in the specified font size.</returns>
        internal static Renderable CreateRenderable(string text, float fontSize) {
            var font = new Font(familyName: "Arial", emSize: fontSize);
            graphics.DrawString(text, font, brush: Brushes.White, x: 0, y: 0);
            SizeF textSize = graphics.MeasureString(text, font);
            Renderable renderable = new Renderable((int)textSize.Width, (int)textSize.Height, 0, 0);
            for (int x = 0; x < (int)textSize.Width; x++) {
                for (int y = 0; y < (int)textSize.Height; y++) {
                    char pixel = bitmap.GetPixel(x, y).GetBrightness() > 0 ? '█' : ' ';
                    renderable.SetAt(x, y, newValue: pixel);
                }
            }

            graphics.Clear(Color.Black);
            return renderable;
        }
    }
}