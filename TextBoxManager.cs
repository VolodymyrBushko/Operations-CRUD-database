using System.Collections.Generic;
using System.Windows.Forms;

namespace VolodEF
{
    public static class TextBoxManager
    {
        public static bool IsNotNullOrWhiteSpaceTextBoxes(IEnumerable<TextBox> textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
                if (textBox.ReadOnly == false && string.IsNullOrWhiteSpace(textBox.Text))
                    return false;
            return true;
        }

        public static void ClearTextBoxes(IEnumerable<TextBox> textBoxes)
        {
            foreach (TextBox textBox in textBoxes)
                if (textBox.ReadOnly == false)
                    textBox.Clear();
        }
    }
}