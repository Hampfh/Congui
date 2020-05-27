using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Congui {
    internal class UtilityMethods {
        public static string CenterText(string text, int lineWidth) {
            if (text.Length > lineWidth)
                throw new Exception("Text is exceeding line length");

            var extra = lineWidth - text.Length;
            var padding = "";
            for (var i = 0; i < extra / 2; i++)
                padding += ' ';
            return padding + text + padding + (extra % 2 == 0 ? "" : " ");
        }
    }
}
