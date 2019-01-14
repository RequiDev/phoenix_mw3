using System;

using SharpDX.DirectWrite;

namespace Phoenix.Overlay
{
    internal class LayoutBuffer
    {
        public string Text;
        public TextLayout TextLayout;

        public LayoutBuffer(string text, TextLayout layout)
        {
            this.Text = text;
            this.TextLayout = layout;
            this.TextLayout.TextAlignment = TextAlignment.Leading;
            this.TextLayout.WordWrapping = WordWrapping.NoWrap;
        }
        ~LayoutBuffer()
        {
            this.TextLayout.Dispose();
        }

        public void Dispose()
        {
            this.TextLayout.Dispose();
            this.Text = null;
        }
    }
}
