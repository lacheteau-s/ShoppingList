using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShoppingList.Droid.Renderers;
using ShoppingList.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ShoppingList.Renderers.CheckBox), typeof(CheckBoxRenderer))]
namespace ShoppingList.Droid.Renderers
{
	public class CheckBoxRenderer : ButtonRenderer
	{
		public static readonly BindableProperty IsCheckedProperty = BindableProperty.CreateAttached("IsChecked", typeof(bool), typeof(ShoppingList.Renderers.CheckBox), false);

		public CheckBoxRenderer(Context context) : base(context)
		{
		}

		public static void SetIsCheckedProperty(BindableObject view, bool isChecked)
		{
			view.SetValue(IsCheckedProperty, isChecked);
		}

		public static bool GetIsCheckedProperty(BindableObject view)
		{
			return (bool)view.GetValue(IsCheckedProperty);
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

			var control = new Android.Widget.CheckBox(this.Context);
			control.CheckedChange += (s, evt) =>
			{
				((ShoppingList.Renderers.CheckBox)Element).IsChecked = evt.IsChecked;
			};
			control.Click += (s, evt) =>
			{
				this.Element.SendClicked();
			};
			this.SetNativeControl(control);
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			var formsControl = ((ShoppingList.Renderers.CheckBox)sender);

			if (e.PropertyName == nameof(formsControl.IsChecked))
				((Android.Widget.CheckBox)this.Control).Checked = formsControl.IsChecked;
		}
	}
}