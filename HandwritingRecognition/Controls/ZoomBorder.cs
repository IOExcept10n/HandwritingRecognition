﻿// Copyright 2024 (c) PedroTeam (contact https://github.com/IOExcept10n)
// Distributed under CC BY-NC 4.0 license. See LICENSE.md file in the project root for more information
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HandwritingRecognition.Controls
{
    /// <summary>
    /// Represents a content control with support of move and zoom contents.
    /// </summary>
    public class ZoomBorder : Border
    {
        private UIElement? child = null;
        private Size initialSize;
        private Point origin;
        private double scale = 1;
        private Point start;

        /// <summary>
        /// Gets or sets the child element to scale and move.
        /// </summary>
        public override UIElement Child
        {
            get => base.Child;
            set
            {
                if (value != null && value != Child)
                    Initialize(value);
                base.Child = value;
            }
        }

        /// <summary>
        /// Gets or sets the maximum content scaling factor.
        /// </summary>
        public double MaxScale { get; set; } = 3;

        /// <summary>
        /// Gets or sets the minimum content scaling factor.
        /// </summary>
        public double MinScale { get; set; } = 1;

        /// <summary>
        /// Gets or sets the current scaling factor.
        /// </summary>
        public double Scale
        {
            get => scale;
            set
            {
                if (child == null) return;
                value = Math.Clamp(value, MinScale, MaxScale);
                var st = GetScaleTransform(child);
                st.ScaleX = st.ScaleY = scale = value;
            }
        }

        /// <summary>
        /// Initializes the component properties according to content change.
        /// </summary>
        /// <param name="element">New content element to bind properties to.</param>
        public void Initialize(UIElement element)
        {
            child = element;
            if (child != null)
            {
                initialSize = child.RenderSize;
                TransformGroup group = new TransformGroup();
                TranslateTransform tt = new TranslateTransform();
                ScaleTransform st = new ScaleTransform();
                group.Children.Add(st);
                group.Children.Add(tt);
                child.RenderTransform = group;
                child.RenderTransformOrigin = new Point(0.0, 0.0);
                MouseWheel += child_MouseWheel;
                MouseLeftButtonDown += child_MouseLeftButtonDown;
                MouseLeftButtonUp += child_MouseLeftButtonUp;
                MouseMove += child_MouseMove;
                PreviewMouseRightButtonDown += new MouseButtonEventHandler(
                  child_PreviewMouseRightButtonDown);
            }
        }

        /// <summary>
        /// Resets the scale and move state.
        /// </summary>
        public void Reset()
        {
            if (child != null)
            {
                // reset zoom
                Scale = 1;

                // reset pan
                var tt = GetTranslateTransform(child);
                tt.X = 0.0;
                tt.Y = 0.0;
            }
        }

        private ScaleTransform GetScaleTransform(UIElement element)
        {
            return (ScaleTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is ScaleTransform);
        }

        private TranslateTransform GetTranslateTransform(UIElement element)
        {
            return (TranslateTransform)((TransformGroup)element.RenderTransform)
              .Children.First(tr => tr is TranslateTransform);
        }

#pragma warning disable
        #region Child Events

        private void child_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (child != null && e.Delta > 0)
            {
                var st = GetScaleTransform(child);
                var tt = GetTranslateTransform(child);

                double zoom = 0.2;

                Point relative = e.GetPosition(child);
                double absoluteX;
                double absoluteY;

                absoluteX = relative.X * st.ScaleX + tt.X;
                absoluteY = relative.Y * st.ScaleY + tt.Y;

                Scale += zoom;

                tt.X = absoluteX - relative.X * st.ScaleX;
                tt.Y = absoluteY - relative.Y * st.ScaleY;
            }
        }
        private Rect RecBounds(UIElement item)
        {
            return item.TransformToAncestor(Application.Current.MainWindow).TransformBounds(new Rect(0, 0, item.RenderSize.Width, item.RenderSize.Height));
        }
        private void child_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                var tt = GetTranslateTransform(child);
                start = e.GetPosition(this);
                origin = new Point(tt.X, tt.Y);
                Cursor = Cursors.Hand;
                child.CaptureMouse();
            }
        }

        private void child_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (child != null)
            {
                child.ReleaseMouseCapture();
                Cursor = Cursors.Arrow;
            }
        }

        void child_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Reset();
        }

        private void child_MouseMove(object sender, MouseEventArgs e)
        {
            if (child != null)
            {
                if (child.IsMouseCaptured && Scale > 1)
                {
                    var tt = GetTranslateTransform(child);
                    var bounds = RecBounds(this);
                    var childBounds = RecBounds(child);
                    Point mouse = e.GetPosition(this);
                    Vector v = start - mouse;
                    childBounds = Rect.Offset(childBounds, -v);
                    if (v.X < 0 && childBounds.Left > bounds.Left) v.X = 0;
                    if (v.Y < 0 && childBounds.Top > bounds.Top) v.Y = 0;
                    if (v.X > 0 && childBounds.Right < bounds.Right) v.X = 0;
                    if (v.Y > 0 && childBounds.Bottom < bounds.Bottom) v.Y = 0;
                    tt.X = origin.X - v.X;
                    tt.Y = origin.Y - v.Y;
                    start = mouse;
                    origin = new Point(tt.X, tt.Y);
                }
            }
        }

        #endregion
    }
}
