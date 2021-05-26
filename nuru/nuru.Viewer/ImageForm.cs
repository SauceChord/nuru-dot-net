using nuru.IO.NUI;
using nuru.IO.NUP;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nuru.Viewer
{
    public partial class ImageForm : Form
    {
        public ImageForm()
        {
            InitializeComponent();
        }

        private void ImageForm_Load(object sender, EventArgs e)
        {
            var nuiFile = NUIFile.FromFile("nui/nuru_dot_net_alt.nui");
            var nupGlyphFile = NUPFile.FromFile("nup/nurustd.nup");
            var nupColorFile = NUPFile.FromFile("nup/aurora.nup");
            
            for (int y = 0; y < nuiFile.Header.Height; ++y)
            {
                for (int x = 0; x < nuiFile.Header.Width; ++x)
                {
                    var nuiGlyph = nuiFile.Payload[x, y].Glyph;
                    var nupGlyph = nupGlyphFile.UnicodePayload[(byte)nuiGlyph].ToString();
                    var nuiColor = nuiFile.Payload[x, y].Color;
                    var rgbFG = Palette.ANSI8.ToRGB(nuiColor.Foreground);
                    var rgbBG = Palette.ANSI8.ToRGB(nuiColor.Background);

                    output.AppendText(nupGlyph, rgbFG, rgbBG);
                }
                output.AppendText("\n");
            }
        }
    }
    public static class RichTextBoxExtensions
    {
        public static void AppendText(this RichTextBox box, string text, RGB fgRGB, RGB bgRGB)
        {
            var fg = System.Drawing.Color.FromArgb(fgRGB.R, fgRGB.G, fgRGB.B);
            var bg = System.Drawing.Color.FromArgb(bgRGB.R, bgRGB.G, bgRGB.B);
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionBackColor = bg;
            box.SelectionColor = fg;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
            box.SelectionBackColor = box.BackColor;
        }
    }
}
