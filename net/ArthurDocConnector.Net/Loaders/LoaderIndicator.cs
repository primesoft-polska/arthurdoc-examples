using System.Windows;
using System.Windows.Controls;

namespace ArthurDocConnector.Loaders
{
    [TemplatePart(Name = "Border", Type = typeof(Border))]
    public class LoaderIndicator: Control
    {
        /// <summary>
        /// Identifies the <see cref="SpeedRatio"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(LoaderIndicator), new PropertyMetadata(1d, (o, e) => {
                LoaderIndicator li = (LoaderIndicator)o;

                if (li.ArcsBorder == null || li.IsActive == false)
                {
                    return;
                }

                foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(li.ArcsBorder))
                {
                    if (group.Name == "ActiveStates")
                    {
                        foreach (VisualState state in group.States)
                        {
                            if (state.Name == "Active")
                            {
                                state.Storyboard.SetSpeedRatio(li.ArcsBorder, (double)e.NewValue);
                            }
                        }
                    }
                }
            }));


        /// <summary>
        /// Identifies the <see cref="IsActive"/> dependency property.
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(LoaderIndicator), new PropertyMetadata(true, (o, e) => {
                LoaderIndicator li = (LoaderIndicator)o;

                if (li.ArcsBorder == null)
                {
                    return;
                }

                if ((bool)e.NewValue == false)
                {
                    VisualStateManager.GoToElementState(li.ArcsBorder, "Inactive", false);
                    li.ArcsBorder.Visibility = Visibility.Collapsed;
                }
                else
                {
                    VisualStateManager.GoToElementState(li.ArcsBorder, "Active", false);
                    li.ArcsBorder.Visibility = Visibility.Visible;

                    foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(li.ArcsBorder))
                    {
                        if (group.Name == "ActiveStates")
                        {
                            foreach (VisualState state in group.States)
                            {
                                if (state.Name == "Active")
                                {
                                    state.Storyboard.SetSpeedRatio(li.ArcsBorder, li.SpeedRatio);
                                }
                            }
                        }
                    }
                }
            }));
        protected Border ArcsBorder;
        public LoaderIndicator()
        {

        }


        /// <summary>
        /// When overridden in a derived class, is invoked whenever application code
        /// or internal processes call System.Windows.FrameworkElement.ApplyTemplate().
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            ArcsBorder = (Border)GetTemplateChild("ArcsBorder");

            if (ArcsBorder != null)
            {
                VisualStateManager.GoToElementState(ArcsBorder, (this.IsActive ? "Active" : "Inactive"), false);
                foreach (VisualStateGroup group in VisualStateManager.GetVisualStateGroups(ArcsBorder))
                {
                    if (group.Name == "ActiveStates")
                    {
                        foreach (VisualState state in group.States)
                        {
                            if (state.Name == "Active")
                            {
                                state.Storyboard.SetSpeedRatio(ArcsBorder, this.SpeedRatio);
                            }
                        }
                    }
                }

                ArcsBorder.Visibility = (this.IsActive ? Visibility.Visible : Visibility.Collapsed);
            }
        }

        /// <summary>
        /// Get/set whether the loading indicator is active.
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// Get/set the speed ratio of the animation.
        /// </summary>
        public double SpeedRatio
        {
            get { return (double)GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }

        
    }

}
