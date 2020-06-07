using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Conhics;

namespace Congui {
    public class Ui {
        private List<List<UiModule>> canvas;
        private readonly List<List<UiModuleSettings>> layout;
        private const ConsoleColor selectedColumn = ConsoleColor.DarkCyan;
        private const ConsoleColor selectedItem = ConsoleColor.Green;
        public int currentWindowWidth;
        public int currentWindowHeight;

        public Ui(List<List<UiModuleSettings>> layout) {
            this.layout = layout;
            this.currentWindowWidth = Console.WindowWidth;
            this.currentWindowHeight = Console.WindowHeight;
            this.canvas = GenerateCanvas(layout);
        }

        public static void Setup(string title, int columns = 120, int rows = 30, short charWidth = 8, short charHeight = 16) {
            Window.Setup(title, columns, rows, charWidth, charHeight, activeEventCapture: false);
        }

        private List<List<UiModule>> GenerateCanvas(IReadOnlyCollection<List<UiModuleSettings>> layout) {
            var generatedCanvas = new List<List<UiModule>>();

            // Calculate modules heights
            var defaultHeight = currentWindowHeight;
            var remainingHeight = defaultHeight;
            var remainingModulesY = layout.Count;

            // Add all vertical layers of ui blocks
            foreach (var row in layout) {
                var canvasRow = new List<UiModule>();
                
                // Calculate row modules width
                var moduleWidth = currentWindowWidth / row.Count;
                int moduleHeight;
                if (row[0].heightPercentage == 0)
                    moduleHeight = remainingHeight / remainingModulesY;
                else {
                    if (row[0] .heightPercentage > 1)
                        throw new Exception("Module cannot be more than 100% of screen");
                    var calculatedHeight = (int) (remainingHeight * row[0].heightPercentage);
                    if (calculatedHeight <= remainingHeight)
                        moduleHeight = calculatedHeight;
                    else 
                        throw new Exception("Module exceeds height of console");
                }

                // Add the modules
                foreach (var uiModuleSettings in row) {
                    var uiModule = new UiModule(moduleWidth, moduleHeight, uiModuleSettings);
                    canvasRow.Add(uiModule);
                }

                remainingModulesY--;
                remainingHeight -= moduleHeight;
                generatedCanvas.Add(canvasRow);
            }

            return generatedCanvas;
        }

        public void Add(
            string message, ConsoleColor color = ConsoleColor.White, int column = 0, int row = 0,
            bool isCentered = false) {
            if (canvas.Count - 1 < row || canvas[row].Count - 1 < column) 
                throw new Exception("Out of range");
            canvas[row][column].Add(message, color, isCentered);
        }

        public void Flush() {
            UpdateDimensions();

            // Perform a printout of everything
            var currentY = 0;
            var cursorEndX = 0;
            var cursorEndY = 0;

            // Vertically iterate through all UiModule
            foreach (var rowUiModules in canvas) {
                var compiledRow = new List<List<Tuple<string, ConsoleColor, int>>>();
                var currentX = 0;

                // Retrieve data from all modules
                foreach (var uiModule in rowUiModules) {
                    var result = uiModule.Flush();
                    compiledRow.Add(result);
                }

                // Printout 
                // Rows in modules
                for (var y = 0; y < rowUiModules[0].GetOuterHeight(); y++) {
                    foreach (var uiSegment in compiledRow) {
                        var (text, color, rememberIndex) = uiSegment[y];
                        Window.Print(text, currentX, currentY, color);
                        currentX += uiSegment[0].Item1.Length;
                    }

                    currentX = 0;
                    currentY++;
                }
            }
            Window.Flush();
        }

        public void UpdateDimensions() {

            if (currentWindowWidth == Console.WindowWidth && currentWindowHeight == Console.WindowHeight)
                return;

            currentWindowWidth = Console.WindowWidth;
            currentWindowHeight = Console.WindowHeight;

            Window.Clear();

            // Re-Calculate modules heights
            var defaultHeight = currentWindowHeight;
            var remainingHeight = defaultHeight;
            var remainingModulesY = layout.Count;

            // Add all vertical layers of ui blocks
            for (var y = 0; y < layout.Count; y++) {
                var row = layout[y];
                int moduleHeight;
                if (row[0].heightPercentage == 0)
                    moduleHeight = remainingHeight / remainingModulesY;
                else if (y == layout.Count - 1)
                    moduleHeight = remainingHeight;
                else {
                    if (row[0].heightPercentage > 1)
                        throw new Exception("Module cannot be more than 100% of screen");
                    var calculatedHeight = (int) (remainingHeight * row[0].heightPercentage);
                    if (calculatedHeight <= remainingHeight)
                        moduleHeight = calculatedHeight;
                    else
                        throw new Exception("Module exceeds height of console");
                }

                // Calculate module width
                var widthRemaining = currentWindowWidth;
                // Add horizontal ui modules for each line
                for (var x = 0; x < row.Count; x++) {
                    int moduleWidth;
                    if (x == row.Count - 1)
                        moduleWidth = widthRemaining;
                    else
                        moduleWidth = widthRemaining / (row.Count - x);

                    canvas[y][x].UpdateDimensions(moduleWidth, moduleHeight);
                    widthRemaining -= moduleWidth;
                }
            }
        }

        public void Clear() {
            foreach (var row in canvas) {
                foreach (var uiModule in row) {
                    uiModule.Clear();
                }
            }
        }

        public void Clear(int column, int row) {
            if (canvas.Count < row || canvas[row].Count < column)
                throw new Exception("Out of range");
            canvas[row][column].Clear();
        }

        public UiModule GetModule(int column, int row) {
            if (canvas.Count < row || canvas[row].Count < column)
                throw new Exception("Out of range");

            return canvas[row][column];
        }
    }
}
