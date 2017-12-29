using System;
using System.Collections;
using System.Linq;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Forms9PatchHtmlTag.iOS.Renderers
{
    public class ProductInfoListSource : UITableViewSource
    {
        private const double DefaultRowHeight = 70;
        private readonly View _formsListView;
        private readonly IEnumerable _itemsSource;

        private readonly DataTemplate _template;

        public ProductInfoListSource(View formsListView, DataTemplate template, IEnumerable itemsSource)
        {
            _template = template;
            _itemsSource = itemsSource;
            _formsListView = formsListView;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var context = GetContextForIndexPath(indexPath);

            if (context == null)
                throw new IndexOutOfRangeException();

            var template = GetTemplateForItemSource(context);

            var content = template.CreateContent();

            if (!(content is ViewCell) && content != null)
                throw new InvalidCastException($"Cell {content.GetType().FullName} should be {nameof(ViewCell)}");

            var viewCell = (ViewCell) content;

            var container = viewCell.View;

            if (container == null)
                return new UITableViewCell();

            container.Parent = _formsListView;

            var renderer = Platform.GetRenderer(container);
            if (renderer == null)
            {
                renderer = Platform.CreateRenderer(container);
                Platform.SetRenderer(container, renderer);
            }

            var nativeCell = new UITableViewCell(UITableViewCellStyle.Default, "just_cell");
            nativeCell.ContentView.AddSubview(renderer.NativeView);

            container.BindingContext = viewCell.BindingContext = context;
            Layout.LayoutChildIntoBoundingRegion(container,
                new Rectangle(0, 0, tableView.Bounds.Width, DefaultRowHeight));

            return nativeCell;
        }

        public override nfloat GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return new nfloat(DefaultRowHeight);
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return _itemsSource.Cast<object>().ToList().Count;
        }

        private object GetContextForIndexPath(NSIndexPath indexPath)
        {
            if (_itemsSource == null || _template == null)
                return null;

            var row = indexPath.Row;
            var items = _itemsSource.Cast<object>().ToList();

            if (items.Count <= row)
                return null;

            var item = items.ElementAt(row);

            return item;
        }

        private DataTemplate GetTemplateForItemSource(object item)
        {
            var template = _template;

            if (template is DataTemplateSelector)
                template = ((DataTemplateSelector) template).SelectTemplate(item, _formsListView);

            return template;
        }
    }
}