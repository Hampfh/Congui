using System;
using System.Collections.Generic;
using System.Threading;
using Congui;

namespace Example_Project {
    class Program {
        static void Main(string[] args) {
            Ui.Setup("Test project");

            var standardModule = new UiModuleSettings {
                borderColor = ConsoleColor.Cyan,
                borderThickness = 1,
                bottomBorderActive = true,
                heightPercentage = 0,
                horizontalBorderCharacter = '#',
                leftBorderActive = true,
                rightBorderActive = true,
                topBorderActive = true,
                verticalBorderCharacter = '#'
            };

            Ui ui = new Ui(new List<List<UiModuleSettings>> {
                new List<UiModuleSettings>{ standardModule, standardModule },
                new List<UiModuleSettings>{ standardModule , standardModule , standardModule },
                new List<UiModuleSettings>{ standardModule }
            });

            ui.Add("This is the center in this current layout", ConsoleColor.DarkCyan, 1, 1);
            ui.Add("This is another test", ConsoleColor.DarkGray, 0, 1, true);
            ui.Add("Hello", ConsoleColor.Magenta, 1, 0);
            ui.Add("First", ConsoleColor.Yellow, 1, 0);
            ui.Add("Second", ConsoleColor.Blue, 1, 0);
            ui.Add("Third", ConsoleColor.Green, 1, 0);
            ui.Add("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec interdum aliquam quam vitae blandit. Vivamus eu ipsum sit amet tortor pretium blandit. Pellentesque accumsan augue eget magna lacinia consequat. Aliquam molestie dui vel erat vestibulum, varius blandit nisl pellentesque. Fusce at tellus aliquam, ultrices metus in, lobortis quam. Donec id consectetur ex. Pellentesque nec maximus dui, a dictum diam. Nunc sed tempus felis. Pellentesque scelerisque lacus ac est fringilla maximus. Donec augue massa, rhoncus in risus non, congue pharetra nunc. Quisque quis quam quis mauris pulvinar imperdiet. Nulla finibus nec massa a pulvinar. Etiam commodo felis eget leo malesuada, eu egestas odio scelerisque. Morbi sed fermentum urna. Aenean ut mi id elit mattis molestie non vel odio.\r\n\r\nDuis a massa vitae est tincidunt imperdiet eu nec turpis. Quisque justo dui, facilisis vulputate ante a, interdum vehicula mauris. Quisque sed lobortis risus, vitae consequat nisi. Pellentesque sed ipsum turpis. Duis enim nibh, consequat a enim et, gravida aliquet arcu. Vivamus viverra, erat sit amet porttitor molestie, ipsum magna mattis lectus, a ultrices lacus lectus non enim. Suspendisse pharetra ligula vitae scelerisque dictum. Aliquam erat nisl, vehicula sit amet pellentesque et, vestibulum a nibh. Maecenas mauris nunc, viverra at vehicula vitae, mattis eu elit.\r\n\r\nDuis rhoncus purus euismod tellus ornare, ac faucibus quam semper. Donec aliquam molestie felis, a finibus libero lobortis sed. Quisque dictum suscipit viverra. Donec molestie commodo augue. Quisque at dolor sit amet elit sollicitudin tempus. Vivamus et urna ut magna lacinia condimentum eu ut metus. In hac habitasse platea dictumst. Fusce ornare mi vitae ante bibendum, non pulvinar ipsum aliquam. Curabitur a placerat tellus. ", ConsoleColor.DarkCyan, 0, 2);
            ui.Flush();
        }
    }
}
