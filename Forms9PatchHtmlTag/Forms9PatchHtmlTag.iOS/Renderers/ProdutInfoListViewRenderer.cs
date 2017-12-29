using System.ComponentModel;
using Forms9PatchHtmlTag;
using Forms9PatchHtmlTag.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ProductInfoListView), typeof(ProdutInfoListViewRenderer))] //TODO: Comment this line and ActionTagTapped will work
namespace Forms9PatchHtmlTag.iOS.Renderers
{
	public class ProdutInfoListViewRenderer : ViewRenderer<ProductInfoListView, UITableView>
	{
		private ProductInfoListSource _tableViewSource;

		protected override void OnElementChanged(ElementChangedEventArgs<ProductInfoListView> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				if (Control == null)
				{
					var control = new UITableView();
					control.RowHeight = UITableView.AutomaticDimension;
					control.SeparatorStyle = UITableViewCellSeparatorStyle.None;
					SetNativeControl(control);
				}

				UpdateTableViewSource();
			}
		}

		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			switch (e.PropertyName)
			{
				case nameof(Element.ItemsSource):
				case nameof(Element.ItemTemplate):
					UpdateTableViewSource();
					break;
			}
		}

		private void UpdateTableViewSource()
		{
			var element = Element;
			var control = Control;

			if(element == null || control == null)
				return;

			control.Source = null;
			_tableViewSource?.Dispose();

			var itemTemplate = Element.ItemTemplate;
			var itemsSource = Element.ItemsSource;

			if (itemTemplate == null|| itemsSource == null)
				return;

			_tableViewSource = new ProductInfoListSource(element, itemTemplate, itemsSource);
			control.Source = _tableViewSource;
			control.ReloadData();
		}
	}
}