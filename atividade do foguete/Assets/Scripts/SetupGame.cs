using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

public class SetupGame : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("JogoShooter/Montar Cena do Zero")]
    public static void CreateFullScene()
    {
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        // 1. Câmera
        GameObject camObj = new GameObject("Main Camera");
        Camera cam = camObj.AddComponent<Camera>();
        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = new Color(0.15f, 0.2f, 0.3f);
        cam.orthographic = true;
        cam.orthographicSize = 5f;
        camObj.tag = "MainCamera";

        // 2. Prefab da Bala
        GameObject bulletObj = new GameObject("BulletObj");
        SpriteRenderer bulletSr = bulletObj.AddComponent<SpriteRenderer>();
        bulletSr.sprite = CreateSquareSprite(Color.yellow, 8, 16);
        CircleCollider2D bulletCol = bulletObj.AddComponent<CircleCollider2D>();
        bulletCol.isTrigger = true;
        Rigidbody2D bulletRb = bulletObj.AddComponent<Rigidbody2D>();
        bulletRb.bodyType = RigidbodyType2D.Kinematic;
        bulletObj.AddComponent<Bullet>();
        bulletObj.SetActive(false);

        // 3. Player
        GameObject player = new GameObject("Player");
        player.transform.position = new Vector3(0, -3.5f, 0);
        SpriteRenderer playerSr = player.AddComponent<SpriteRenderer>();
        playerSr.sprite = CreateTriangleSprite(Color.cyan, 32, 32);
        BoxCollider2D playerCol = player.AddComponent<BoxCollider2D>();
        playerCol.isTrigger = true;
        Rigidbody2D playerRb = player.AddComponent<Rigidbody2D>();
        playerRb.bodyType = RigidbodyType2D.Kinematic;
        player.AddComponent<PlayerMovement>();
        player.AddComponent<PlayerShoot>();
        player.AddComponent<PlayerHealth>();

        // 4. Boss
        GameObject boss = new GameObject("Boss");
        boss.transform.position = new Vector3(0, 3f, 0);
        SpriteRenderer bossSr = boss.AddComponent<SpriteRenderer>();
        bossSr.sprite = CreateSquareSprite(Color.red, 48, 48);
        BoxCollider2D bossCol = boss.AddComponent<BoxCollider2D>();
        bossCol.isTrigger = true;
        Rigidbody2D bossRb = boss.AddComponent<Rigidbody2D>();
        bossRb.bodyType = RigidbodyType2D.Kinematic;
        boss.AddComponent<Animator>();
        boss.AddComponent<BossHealth>();
        boss.AddComponent<BossPoolManager>();

        // 5. Canvas e UI
        GameObject canvasObj = new GameObject("Canvas");
        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        CanvasScaler scaler = canvasObj.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        canvasObj.AddComponent<GraphicRaycaster>();

        // Barra de Vida
        GameObject healthBar = new GameObject("HealthBar");
        healthBar.transform.SetParent(canvasObj.transform, false);
        RectTransform hbRect = healthBar.AddComponent<RectTransform>();
        hbRect.anchorMin = new Vector2(0.5f, 1f);
        hbRect.anchorMax = new Vector2(0.5f, 1f);
        hbRect.pivot = new Vector2(0.5f, 1f);
        hbRect.anchoredPosition = new Vector2(0, -20);
        hbRect.sizeDelta = new Vector2(400, 30);

        Image bgImg = healthBar.AddComponent<Image>();
        bgImg.color = Color.gray;

        GameObject fillObj = new GameObject("Fill");
        fillObj.transform.SetParent(healthBar.transform, false);
        RectTransform fillRect = fillObj.AddComponent<RectTransform>();
        fillRect.anchorMin = Vector2.zero;
        fillRect.anchorMax = Vector2.one;
        fillRect.sizeDelta = Vector2.zero;

        Image fillImg = fillObj.AddComponent<Image>();
        fillImg.color = Color.green;

        // Painel de Derrota
        GameObject defeatPanel = CreatePanel(canvasObj.transform, "DefeatPanel", "GAME OVER", Color.red);
        defeatPanel.SetActive(false);

        // Painel de Vitória
        GameObject victoryPanel = CreatePanel(canvasObj.transform, "VictoryPanel", "VITÓRIA!", Color.green);
        victoryPanel.SetActive(false);

        Debug.Log("Cena montada com sucesso!");
    }

    private static GameObject CreatePanel(Transform parent, string name, string text, Color textColor)
    {
        GameObject panel = new GameObject(name);
        panel.transform.SetParent(parent, false);
        RectTransform rect = panel.AddComponent<RectTransform>();
        rect.anchorMin = Vector2.zero;
        rect.anchorMax = Vector2.one;
        rect.sizeDelta = Vector2.zero;

        Image img = panel.AddComponent<Image>();
        img.color = new Color(0, 0, 0, 0.8f);

        GameObject textObj = new GameObject("Text");
        textObj.transform.SetParent(panel.transform, false);
        Text txt = textObj.AddComponent<Text>();
        txt.text = text;
        txt.font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        txt.fontSize = 50;
        txt.color = textColor;
        txt.alignment = TextAnchor.MiddleCenter;

        RectTransform textRect = textObj.GetComponent<RectTransform>();
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.sizeDelta = Vector2.zero;

        return panel;
    }

    private static Sprite CreateSquareSprite(Color color, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height);
        Color[] pixels = new Color[width * height];
        for (int i = 0; i < pixels.Length; i++) pixels[i] = color;
        tex.SetPixels(pixels);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
    }

    private static Sprite CreateTriangleSprite(Color color, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (x >= (width / 2) - y && x <= (width / 2) + y)
                    tex.SetPixel(x, y, color);
                else
                    tex.SetPixel(x, y, Color.clear);
            }
        }
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));
    }
#endif
}