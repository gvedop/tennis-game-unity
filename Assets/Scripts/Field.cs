using UnityEngine;

namespace TennisGame.Assets.Scripts
{
    public class Field
    {
        private Vector2[] _corners = new Vector2[4];
        private Vector2 _center;
        private Vector2 _topCenter;
        private Vector2 _bottomCenter;
        private Vector2 _leftCenter;
        private Vector2 _rightCenter;
        private float _width;
        private float _height;
        private float _widthScale;
        private float _heightScale;

        public Field(Canvas canvas, RectTransform rect, Camera camera)
        {
            Init(canvas, rect, camera);
        }

        public Vector2 TopLeftCorner
        {
            get { return _corners[1]; }
        }

        public Vector2 TopRightCorner
        {
            get { return _corners[2]; }
        }

        public Vector2 BottomLeftCorner
        {
            get { return _corners[0]; }
        }

        public Vector2 BottomRightCorner
        {
            get { return _corners[3]; }
        }

        public Vector2 Center
        {
            get { return _center; }
        }

        public Vector2 TopCenter
        {
            get { return _topCenter; }
        }

        public Vector2 BottomCenter
        {
            get { return _bottomCenter; }
        }

        public Vector2 LeftCenter
        {
            get { return _leftCenter; }
        }

        public Vector2 RightCenter
        {
            get { return _rightCenter; }
        }

        public float Width
        {
            get { return _width; }
        }

        public float Height
        {
            get { return _height; }
        }

        public float WidthScale
        {
            get { return _widthScale; }
        }

        public float HeightScale
        {
            get { return _heightScale; }
        }

        private void Init(Canvas canvas, RectTransform rect, Camera camera)
        {
            var cornerPositions = new Vector3[4];
            rect.GetWorldCorners(cornerPositions);
            for (int i = 0; i < cornerPositions.Length; i++)
            {
                _corners[i] = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? camera.ScreenToWorldPoint(cornerPositions[i]) : cornerPositions[i];
            }
            _center = GetCenter();
            _topCenter = GetTopCenter();
            _bottomCenter = GetBottomCenter();
            _leftCenter = GetLeftCenter();
            _rightCenter = GetRightCenter();
            _width = GetWidth();
            _height = GetHeight();
            _widthScale = GetWidthScale();
            _heightScale = GetHeightScale();
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
