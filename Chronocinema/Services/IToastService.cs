using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Chronocinema.Services
{
    public interface IToastService
    {
        void ShowToast(string message, int duration = 3);
    }

    public class ToastService : IToastService
    {
        public void ShowToast(string message, int duration = 3)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var border = new Border
                {
                    Background = System.Windows.Media.Brushes.Green,
                    CornerRadius = new CornerRadius(8),
                    Padding = new Thickness(16, 10, 16, 10),
                    Opacity = 0,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom,
                    Margin = new Thickness(0, 0, 0, 80)
                };
                var textBlock = new TextBlock
                {
                    Text = message,
                    Foreground = System.Windows.Media.Brushes.White,
                    FontSize = 14,
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Center
                };
                border.Child = textBlock;

                var mainWindow = Application.Current.MainWindow;
                if (mainWindow != null)
                {
                    if (mainWindow.Content is Grid grid)
                    {
                        grid.Children.Add(border);
                        Grid.SetRow(border, 0);
                        Grid.SetColumn(border, 0);
                        Grid.SetColumnSpan(border, int.MaxValue);

                        var fadeInAnimation = new System.Windows.Media.Animation.DoubleAnimation
                        {
                            To = 1,
                            Duration = TimeSpan.FromSeconds(0.3)
                        };

                        var fadeOutAnimation = new System.Windows.Media.Animation.DoubleAnimation
                        {
                            To = 0,
                            Duration = TimeSpan.FromSeconds(0.3),
                            BeginTime = TimeSpan.FromSeconds(duration)
                        };

                        var storyBoard = new System.Windows.Media.Animation.Storyboard();
                        storyBoard.Children.Add(fadeInAnimation);
                        storyBoard.Children.Add(fadeOutAnimation);

                        System.Windows.Media.Animation.Storyboard.SetTarget(fadeInAnimation, border);
                        System.Windows.Media.Animation.Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(Border.OpacityProperty));
                        System.Windows.Media.Animation.Storyboard.SetTarget(fadeOutAnimation, border);
                        System.Windows.Media.Animation.Storyboard.SetTargetProperty(fadeOutAnimation, new PropertyPath(Border.OpacityProperty));

                        storyBoard.Completed += (s, e) =>
                        {
                            grid.Children.Remove(border);
                        };
                        storyBoard.Begin();

                    }
                }
            });
        }
    }
}
