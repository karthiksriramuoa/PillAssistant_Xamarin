using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PillAssistant_Xamarin.iOS.CustomRenderers;
using Xamarin.Forms;
using UIKit;
using Foundation;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PillAssistant_Xamarin.CustomControls.CustomImageCell), typeof(CustomImageCellRenderer))]
namespace PillAssistant_Xamarin.iOS.CustomRenderers
{
    public class CustomImageCellRenderer : Xamarin.Forms.Platform.iOS.ImageCellRenderer
    {
        public override UITableViewCell GetCell(Xamarin.Forms.Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.ImageView.Layer.CornerRadius = 15.0f;
            cell.ImageView.Layer.MasksToBounds = true;
            cell.ImageView.Layer.BorderColor = UIColor.LightGray.CGColor;
            cell.ImageView.Layer.BorderWidth = 1.0f;
            return cell;
        }
    }
}