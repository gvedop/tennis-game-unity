using UnityEngine;

namespace TennisGame.Core
{
    public class Field
    {
        public Field(Canvas canvas, RectTransform rect, Camera camera)
        {
            Init(canvas, rect, camera);
        }
        
        public Vector2 TopLeftCorner { get; private set; }
        
        public Vector2 TopRightCorner { get; private set; }
        
        public Vector2 BottomLeftCorner { get; private set; }
        
        public Vector2 BottomRightCorner { get; private set; }

        public Vector2 Center { get; private set; }

        public Vector2 TopCenter { get; private set; }

        public Vector2 BottomCenter { get; private set; }

        public Vector2 LeftCenter { get; private set; }

        public Vector2 RightCenter { get; private set; }

        public float Width { get; private set; }

        public float Height { get; private set; }

        public float WidthScale { get; private set; }

        public float HeightScale { get; private set; }

        private void Init(Canvas canvas, RectTransform rect, Camera camera)
        {
            InitMasterFields(canvas, rect, camera);
            InitSlaveFields();
        }

        private void InitMasterFields(Canvas canvas, RectTransform rect, Camera camera)
        {
            var cornerPositions = new Vector3[4];
            rect.GetWorldCorners(cornerPositions);
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                for (int i = 0; i < cornerPositions.Length; i++)
                {
                    cornerPositions[i] = camera.ScreenToWorldPoint(cornerPositions[i]);
                }
            }
            TopLeftCorner = cornerPositions[1];
            TopRightCorner = cornerPositions[2];
            BottomLeftCorner = cornerPositions[0];
            BottomRightCorner = cornerPositions[3];
        }

        private void InitSlaveFields()
        {
            Center = GetCenter();
            TopCenter = GetTopCenter();
            BottomCenter = GetBottomCenter();
            LeftCenter = GetLeftCenter();
            RightCenter = GetRightCenter();
            Width = GetWidth();
            Height = GetHeight();
            WidthScale = GetWidthScale();
            HeightScale = GetHeightScale();
        }

        private Vector2 GetCenter()
        {
            return new Vector2((BottomLeftCorner.x + BottomRightCorner.x) / 2f, (TopLeftCorner.y + BottomLeftCorner.y) / 2f);
        }

        private Vector2 GetTopCenter()
        {
            return new Vector2((BottomLeftCorner.x + BottomRightCorner.x) / 2f, TopLeftCorner.y);
        }

        private Vector2 GetBottomCenter()
        {
            return new Vector2((BottomLeftCorner.x + BottomRightCorner.x) / 2f, BottomLeftCorner.y);
        }

        private Vector2 GetLeftCenter()
        {
            return new Vector2(BottomLeftCorner.x, (TopLeftCorner.y + BottomLeftCorner.y) / 2f);
        }

        private Vector2 GetRightCenter()
        {
            return new Vector2(BottomRightCorner.x, (TopLeftCorner.y + BottomLeftCorner.y) / 2f);
        }

        private float GetWidth()
        {
            return Mathf.Abs(BottomLeftCorner.x - BottomRightCorner.x);
        }

        private float GetHeight()
        {
            return Mathf.Abs(BottomLeftCorner.y - TopLeftCorner.y);
        }

        private float GetWidthScale()
        {
            return GetWidth() / Screen.width;
        }

        private float GetHeightScale()
        {
            return GetHeight() / Screen.height;
        }
    }
}
