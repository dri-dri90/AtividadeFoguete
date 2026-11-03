using UnityEngine;

public static class SpriteGenerator
{
    // Cria uma textura de cor sólida
    public static Sprite CreateSquareSprite(Color color, int width = 32, int height = 32)
    {
        Texture2D texture = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = color;
        texture.SetPixels(pixels);
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 100f);
    }

    // Cria um triângulo simples para a nave do jogador
    public static Sprite CreateTriangleSprite(Color color, int size = 32)
    {
        Texture2D texture = new Texture2D(size, size);
        Color transparent = new Color(0, 0, 0, 0);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float leftBound = (size / 2f) - (y / 2f);
                float rightBound = (size / 2f) + (y / 2f);
                if (x >= leftBound && x <= rightBound)
                    texture.SetPixel(x, y, color);
                else
                    texture.SetPixel(x, y, transparent);
            }
        }
        texture.Apply();
        return Sprite.Create(texture, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), 100f);
    }
}