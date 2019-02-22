using System;
using System.Collections.Generic;
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

[assembly: ExportRenderer(typeof(MyCheckBox), typeof(CheckBoxRenderer))]
namespace ShoppingList.Droid.Renderers
{
	public class CheckBoxRenderer : ButtonRenderer
	{
		public CheckBoxRenderer(Context context) : base(context)
		{
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
		{
			base.OnElementChanged(e);

			var control = new Android.Widget.CheckBox(this.Context);
			this.SetNativeControl(control);
		}
	}
}