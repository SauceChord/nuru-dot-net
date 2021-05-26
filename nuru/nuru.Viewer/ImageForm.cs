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
            NUIFile nuiFile = NUIFile.FromFile("nui/nuru_cat.nui");
            NUPFile nupGlyphFile = NUPFile.FromFile("nup/nurustd.nup");
            NUPFile nupColorFile = null;
            FileImage image = new FileImage(nuiFile, nupGlyphFile, nupColorFile);
            Render(image);
        }

        private void Render(IImage image)
        {
            for (int y = 0; y < image.Height; ++y)
            {
                for (int x = 0; x < image.Width; ++x)
                {
                    var ch = image.GetGlyph(x, y);
                    var fg = image.GetForeground(x, y);
                    var bg = image.GetBackground(x, y);
                    output.AppendChar(ch, fg, bg);
                }
                output.AppendText("\n");
            }
        }
    }
    public static class RichTextBoxExtensions
    {
        public static void AppendChar(this RichTextBox box, char c, RGB fgRGB, RGB bgRGB)
        {
            var fg = System.Drawing.Color.FromArgb(fgRGB.R, fgRGB.G, fgRGB.B);
            var bg = System.Drawing.Color.FromArgb(bgRGB.R, bgRGB.G, bgRGB.B);
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionBackColor = bg;
            box.SelectionColor = fg;
            box.AppendText(c.ToString());
            box.SelectionColor = box.ForeColor;
            box.SelectionBackColor = box.BackColor;
        }
    }
}
