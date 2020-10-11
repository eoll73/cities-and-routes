﻿using Accessibility;
using DesktopApp.Services.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopApp.UserControllers
{
    /// <summary>
    /// Логика взаимодействия для MaoControl.xaml
    /// </summary>
    public partial class MapControl : UserControl
    {
        private Vector _RelativeTransformPosition;
        private Vector _RelativeOffsetValue;

        public MapControl()
        {
            InitializeComponent();
            TransformPosition = new Point(0.5, 0.5);
            _RelativeTransformPosition = new Vector();
            _RelativeTransformPosition.X += TransformPosition.X;
            _RelativeTransformPosition.Y += TransformPosition.Y;
            this.MouseDown += MapControl_MouseDown;
            this.MouseWheel += MapControl_MouseWheel;
            SetBinding(ZoomCommandProperty, new Binding("ZoomCommand"));
            SetBinding(NavigateCommandProperty, new Binding("NavigateCommand"));
        }

        private void MapControl_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (ScaleValue >= 1 && ScaleValue < 16) ZoomCommand.Execute(2); 
            }
            else {

                if (ScaleValue > 1 && ScaleValue <= 16)
                {
                    ZoomCommand.Execute(0.5);
                }
            }
        }

        #region DragActions
        private void MapControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _RelativeOffsetValue = e.GetPosition(this) - new Point();
            this.MouseMove += MapControl_MouseMove;
            this.LostMouseCapture += MapControl_LostMouseCapture;
            this.MouseUp += MapControl_MouseUp;
            Mouse.Capture(this);
        }
        private void MapControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            FinishDrag(e);
        }
        private void UpdatePosition(MouseEventArgs e)
        {
            NavigateCommand.Execute(e.GetPosition(this) - _RelativeOffsetValue);
        }
        private void FinishDrag(MouseEventArgs e)
        {
            UpdatePosition(e);
            TransformPosition = MapHelper.GetRelativeCurrentPosition(OffsetValue, ActualHeight, ActualWidth, ScaleValue,out _RelativeTransformPosition);
            OffsetValue = default;
            this.MouseMove -= MapControl_MouseMove;
            this.LostMouseCapture -= MapControl_LostMouseCapture;
            this.MouseUp -= MapControl_MouseUp;
            Mouse.Capture(null);
        }
        private void MapControl_LostMouseCapture(object sender, MouseEventArgs e)
        {
            Mouse.Capture(null);
            FinishDrag(e);
        }
        private void MapControl_MouseMove(object sender, MouseEventArgs e)
        {
            UpdatePosition(e);
        }
        #endregion

        #region ImageSource
        public ImageSource MapImageSource
        {
            get { return (ImageSource)GetValue(MapImageSourceProperty); }
            set { SetValue(MapImageSourceProperty, value); }
        }

        public static readonly DependencyProperty MapImageSourceProperty =
            DependencyProperty.Register("MapImageSource", typeof(ImageSource), typeof(MapControl));
        #endregion

        #region ScaleValue
        public double ScaleValue
        {
            get { return (double)GetValue(ScaleValueProperty); }
            set { SetValue(ScaleValueProperty, value); }
        }

        public static readonly DependencyProperty ScaleValueProperty =
            DependencyProperty.Register("ScaleValue", typeof(double), typeof(MapControl), new PropertyMetadata(0.0));
        #endregion

        #region OffsetValue
        public Point OffsetValue
        {
            get { return (Point)GetValue(OffsetValueProperty); }
            set { SetValue(OffsetValueProperty, value); }
        }

        public static readonly DependencyProperty OffsetValueProperty =
            DependencyProperty.Register("OffsetValue", typeof(Point), typeof(MapControl), new PropertyMetadata(new Point(0,0)));
        #endregion

        #region TransformPosition
        public Point TransformPosition
        {
            get { return (Point)GetValue(TransformPositionProperty); }
            set { SetValue(TransformPositionProperty, value); }
        }

        public static readonly DependencyProperty TransformPositionProperty =
            DependencyProperty.Register("TransformPosition", typeof(Point), typeof(MapControl));
        #endregion

        #region NavigateCommand
        public ICommand NavigateCommand
        {
            get { return (ICommand)GetValue(NavigateCommandProperty); }
            set { SetValue(NavigateCommandProperty, value); }
        }

       
        public static readonly DependencyProperty NavigateCommandProperty =
            DependencyProperty.Register("NavigateCommand", typeof(ICommand), typeof(MapControl));
        #endregion

        #region ZoomCommand

        public ICommand ZoomCommand
        {
            get { return (ICommand)GetValue(ZoomCommandProperty); }
            set { SetValue(ZoomCommandProperty, value); }
        }

        public static readonly DependencyProperty ZoomCommandProperty =
            DependencyProperty.Register("ZoomCommand", typeof(ICommand), typeof(MapControl));


        #endregion

    }
}