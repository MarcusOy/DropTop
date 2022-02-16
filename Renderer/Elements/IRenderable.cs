using System.Drawing;

namespace DropTop.Renderer.Elements
{
    public interface IRenderable
    {
        void Render(Graphics g, Point pos, float angle);
    }
}