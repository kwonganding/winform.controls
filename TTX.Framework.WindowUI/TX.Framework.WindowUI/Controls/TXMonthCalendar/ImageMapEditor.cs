/*
 * Copyright ?2005, Patrik Bohman
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification, 
 * are permitted provided that the following conditions are met:
 *
 *    - Redistributions of source code must retain the above copyright notice, 
 *      this list of conditions and the following disclaimer.
 * 
 *    - Redistributions in binary form must reproduce the above copyright notice, 
 *      this list of conditions and the following disclaimer in the documentation 
 *      and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
 * NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, 
 * OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY 
 * OF SUCH DAMAGE.
 */

using System;
using System.Windows.Forms;
using System.Windows.Forms.Design; 
using System.Drawing.Design; 
using System.Drawing;
using System.ComponentModel; 

namespace TX.Framework.WindowUI.Controls
{
	/// <summary>
	/// Summary description for ImageEditor.
	/// </summary>
	[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	internal class ImageMapEditor : System.Drawing.Design.UITypeEditor , IDisposable   
	{
		
		#region private members
		
		private bool disposed;

		#endregion

		#region properties
		
		private IWindowsFormsEditorService wfes;
		private int m_selectedIndex = -1 ;
		private ImageListPanel m_imagePanel;
	
		#endregion
		
		#region constructor

		public ImageMapEditor()
		{
			m_imagePanel = new ImageListPanel(); 
		}

		#endregion

		#region Methods

		protected virtual ImageList GetImageList(object component) 
		{
			DateItem item = component as DateItem;
			if (item != null) 
			{
				return item.GetImageList();
			}

			return null ;
		}

		#endregion
		
		#region overrides

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			wfes = (IWindowsFormsEditorService)	provider.GetService(typeof(IWindowsFormsEditorService));
			if((wfes == null) || (context == null))
				return null ;
			
			ImageList imageList = GetImageList(context.Instance) ;
			if ((imageList == null) || (imageList.Images.Count==0))
				return -1 ;
						
			m_imagePanel.BackgroundColor = Color.FromArgb(241,241,241);
			m_imagePanel.BackgroundOverColor = Color.FromArgb(102,154,204);
			m_imagePanel.HLinesColor = Color.FromArgb(182,189,210);
			m_imagePanel.VLinesColor = Color.FromArgb(182,189,210);
			m_imagePanel.BorderColor = Color.FromArgb(0,0,0);
			m_imagePanel.EnableDragDrop = true;
			m_imagePanel.Init(imageList,12,12,6,(int)value);
			
			// add listner for event
			m_imagePanel.ItemClick += new ImageListPanelEventHandler(OnItemClicked);
			
			// set m_selectedIndex to -1 in case the dropdown is closed without selection
			m_selectedIndex = -1;
			// show the popup as a drop-down
			wfes.DropDownControl(m_imagePanel) ;
				
			// return the selection (or the original value if none selected)
			return (m_selectedIndex != -1) ? m_selectedIndex : (int) value ;
		}

		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
		{
			if(context != null && context.Instance != null ) 
			{
				return UITypeEditorEditStyle.DropDown ;
			}
			return base.GetEditStyle (context);
		}
		

		public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
		{
			return true;
		}

		public override void PaintValue(System.Drawing.Design.PaintValueEventArgs e)
		{
			int imageIndex = -1 ;	
			// value is the image index
			if(e.Value != null) 
			{
				try 
				{
					imageIndex = (int)Convert.ToUInt16( e.Value.ToString() ) ;
				}
				catch
				{
				}
			}
			// no instance, or the instance represents an undefined image
			if((e.Context.Instance == null) || (imageIndex < 0))
				return ;
			// get the image set
			ImageList imageList = GetImageList(e.Context.Instance) ;
			// make sure everything is valid
			if((imageList == null) || (imageList.Images.Count == 0) || (imageIndex >= imageList.Images.Count))
				return ;
			// Draw the preview image
			e.Graphics.DrawImage(imageList.Images[imageIndex],e.Bounds);
		}

		#endregion

		#region EventHandlers

		private void OnItemClicked(object sender, ImageListPanelEventArgs e)
		{
			m_selectedIndex = ((ImageListPanelEventArgs) e).SelectedItem;
			
			//remove listner
			m_imagePanel.ItemClick -= new ImageListPanelEventHandler(OnItemClicked);
			
			// close the drop-dwon, we are done
			wfes.CloseDropDown() ;
		}

		#endregion

		#region IDisposable Members
		
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					m_imagePanel.Dispose();
				}
				// shared cleanup logic
				disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);	
		}

		#endregion
	}

	#region ImageTypeConverter

	internal class ImageTypeConverter : TypeConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context,System.Globalization.CultureInfo culture,object value,Type destinationType)
		{
			if (value.ToString() == "-1")
			{
				return "(none)";
			}
			else
			{
				return value.ToString();
			}
			
		}
        	
		public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
		{

			if(destinationType == typeof(string))
				return true;
			return base.CanConvertTo (context, destinationType);

		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if(sourceType == typeof(string))
				return true;
			return base.CanConvertFrom (context, sourceType);

		}

		public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
		{
			if(value.GetType() == typeof(string))
			{
				// none = -1 = no image
				if (( value.ToString().ToUpper().IndexOf("NONE") >=0) || (value.ToString().Length==0))
					return -1;
				return
					Convert.ToInt16(value.ToString()); 
			}
			return base.ConvertFrom (context, culture, value);
			
		}

	}

	#endregion

}
