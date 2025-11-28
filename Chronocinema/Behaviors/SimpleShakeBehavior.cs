using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Chronocinema.Behaviors
{
    public static class SimpleShakeBehavior
    {
        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.RegisterAttached(
                "InEnabled",
                typeof(bool),
                typeof(SimpleShakeBehavior),
                new PropertyMetadata(false, OnIsEnabledChanged)
            );

        public static bool GetIsEnabled(DependencyObject obj)
            => (bool)obj.GetValue(IsEnabledProperty);
        public static void SetIsEnabled(DependencyObject obj, bool value)
            => obj.SetValue(IsEnabledProperty, value);

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is FrameworkElement element)
            {
                if ((bool)e.NewValue)
                {
                    element.RenderTransform = new TranslateTransform();
                    element.RenderTransformOrigin = new Point(0.5, 0.5);
                }
            }
        }

        public static void Shake(FrameworkElement element)
        {
            if (element == null) return;

            try
            {
                var transform = element.RenderTransform as TranslateTransform;
                if (transform == null)
                {
                    transform = new TranslateTransform();
                    element.RenderTransform = transform;
                    element.RenderTransformOrigin = new Point(0.5, 0.5);
                }

                var storyBoard = new Storyboard();
                var shakeAnimation = new DoubleAnimationUsingKeyFrames();

                shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(15, TimeSpan.FromSeconds(0.1)));
                shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(-12, TimeSpan.FromSeconds(0.2)));
                shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(9, TimeSpan.FromSeconds(0.3)));
                shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(-6, TimeSpan.FromSeconds(0.4)));
                shakeAnimation.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0.5)));

                Storyboard.SetTarget(shakeAnimation, element);
                Storyboard.SetTargetProperty(shakeAnimation, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.X)"));
                storyBoard.Children.Add(shakeAnimation);
                storyBoard.Begin();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in Shake animation: {ex.Message}");
            }
        }
    }
}
