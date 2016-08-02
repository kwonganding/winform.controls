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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;  

namespace TX.Framework.WindowUI.Controls
{
	
	public enum mcTodayFormat {Short = 0, Long}
	
	public enum mcFooterProperty {Align = 0, Text, ShowToday, Format, BackColor1, BackColor2, GradientMode,
								Font, TextColor}														
	
	#region Delegates
	
	public delegate void FooterPropertyEventHandler(object sender, FooterPropertyEventArgs e);

	#endregion


	/// <summary>
	/// Summary description for Footer.
	/// </summary>
	[TypeConverter(typeof(FooterTypeConverter))]
	public class Footer : IDisposable
	{
		#region private class members
		
		private bool disposed;
		private MonthCalendar m_calendar;
		private Color m_backColor1;
        private Color m_backColor2;
        private mcGradientMode m_gradientMode;
		private Color m_textColor;
		private Font m_font;
		private mcTodayFormat m_format;
		private string m_text;
		private bool m_showToday;
		private Rectangle m_rect;
		private Region m_region;
		private mcTextAlign m_align;
		#endregion
	
		#region EventHandlers
		
		internal event ClickEventHandler Click;
		internal event ClickEventHandler DoubleClick;
		internal event FooterPropertyEventHandler PropertyChanged;

		#endregion

		#region Constructor

		public Footer(MonthCalendar calendar)
		{
			m_calendar = calendar;
			m_backColor1 = Color.White;
            m_backColor2 = Color.White;
            m_gradientMode = mcGradientMode.None;
            m_textColor = Color.Black;
			m_font = new Font("Microsoft Sans Serif",(float)8.25,FontStyle.Bold);
			m_format = mcTodayFormat.Short; 
			m_text = "";
			m_showToday = true;
			m_align = mcTextAlign.Left;
		}

		#endregion

		#region Methods
		
		internal void MouseMove(Point mouseLocation)
		{
			if (m_region.IsVisible(mouseLocation))
			{
				m_calendar.ActiveRegion = mcCalendarRegion.Footer;  
			}
		}

		internal void MouseClick(Point mouseLocation, MouseButtons button,mcClickMode mode)
		{
			if (m_region.IsVisible(mouseLocation))
			{
				if (mode == mcClickMode.Single)
				{
					if (this.Click!=null)
						this.Click(this,new ClickEventArgs(button));
				}
				else
				{
					if (this.DoubleClick!=null)
						this.DoubleClick(this,new ClickEventArgs(button));
				}
			}
		}
		
		internal bool IsVisible(Rectangle clip)
		{
			return m_region.IsVisible(clip); 	
		}

		internal void Draw(Graphics e)
		{
			StringFormat textFormat = new StringFormat();
			Brush textBrush = new SolidBrush(TextColor); 
			Brush bgBrush = new SolidBrush(BackColor1); 
			textFormat.Alignment = StringAlignment.Near;   
			textFormat.LineAlignment = StringAlignment.Center;
			Rectangle txtRect;

            if (m_gradientMode == mcGradientMode.None)
                e.FillRectangle(bgBrush, m_rect);
            else
                m_calendar.DrawGradient(e, m_rect, m_backColor1, m_backColor2, m_gradientMode);  

			textFormat.LineAlignment = StringAlignment.Center;
			// Draw header
			switch (Align)
			{
				case mcTextAlign.Left:
				{
					textFormat.Alignment = StringAlignment.Near;
					break;
				}
				case mcTextAlign.Center:
				{
					textFormat.Alignment = StringAlignment.Center;
					break;
				}
				case mcTextAlign.Right:
				{
					textFormat.Alignment = StringAlignment.Far;
					break;
				}
			}

			txtRect = new Rectangle(m_rect.Left + 2,m_rect.Top,m_rect.Width - (2*2),m_rect.Height);
						
			if (m_showToday)
			{
				if (m_format == mcTodayFormat.Short)
					e.DrawString(DateTime.Now.ToShortDateString() ,Font,textBrush,txtRect,textFormat);
				else
					e.DrawString(DateTime.Now.ToLongDateString() ,Font,textBrush,txtRect,textFormat);
								
			}
			else
				e.DrawString(m_text ,Font,textBrush,txtRect,textFormat);

			// Clean up
			textBrush.Dispose();
			bgBrush.Dispose();
		}
		
		#endregion
		
		#region Dispose

		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					m_font.Dispose();
					m_region.Dispose();
				
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

		#region Properties
		
		internal Rectangle Rect
		{
			get
			{
				return m_rect;
			}
			set
			{
				m_rect = value;
				m_region = new Region(m_rect);
			}

		}
		
		[Description("Determines the position for the text.")]
		[DefaultValue(typeof(mcTextAlign),"Left")]
		public mcTextAlign Align
		{
			get
			{
				return m_align;
			}
			set
			{
				if (m_align!=value)
				{
					m_align = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new FooterPropertyEventArgs(mcFooterProperty.Align));   
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Text to be displayed in the footer.")]
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return m_text;
			}
			set
			{
				if (value!=m_text)
				{
					m_text = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new FooterPropertyEventArgs(mcFooterProperty.Text));   
					m_calendar.Invalidate(); 
				}
			}

		}

		[Description("Determines wether todays date should be displayed.")]
		[DefaultValue(true)]
		public bool ShowToday
		{
			get
			{
				return m_showToday;
			}
			set
			{
				if (value!=m_showToday)
				{
					m_showToday = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new FooterPropertyEventArgs(mcFooterProperty.ShowToday));   
					m_calendar.Invalidate(); 
				}
			}

		}

		[Description("Determines the format used to display todays date.")]
		[DefaultValue(typeof(mcTodayFormat),"Short")]
		public mcTodayFormat Format
		{
			get
			{
				return m_format;
			}
			set
			{
				if (value!=m_format)
				{
					m_format = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new FooterPropertyEventArgs(mcFooterProperty.Format));   
					m_calendar.Invalidate(); 
				}
			}

		}

		[Description("Color used for background.")]
		[DefaultValue(typeof(Color),"White")]
		public Color BackColor1
		{
			get
			{
				return m_backColor1;
			}
			set
			{
				if (m_backColor1!=value)
				{
					m_backColor1 = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new FooterPropertyEventArgs(mcFooterProperty.BackColor1));   
					m_calendar.Invalidate();
				}
			}
		}

        [Description("Second background color when using gradient.")]
        [DefaultValue(typeof(Color), "White")]
        public Color BackColor2
        {
            get
            {
                return m_backColor2;
            }
            set
            {
                if (m_backColor2 != value)
                {
                    m_backColor2 = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new FooterPropertyEventArgs(mcFooterProperty.BackColor2));
                    m_calendar.Invalidate();
                }
            }
        }

        [Description("Type of gradient used.")]
        [DefaultValue(typeof(mcGradientMode), "None")]
        public mcGradientMode GradientMode
        {
            get
            {
                return m_gradientMode;
            }
            set
            {
                if (m_gradientMode != value)
                {
                    m_gradientMode = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new FooterPropertyEventArgs(mcFooterProperty.GradientMode));
                    m_calendar.Invalidate();
                }
            }
        }

		[Description("Font used for footer.")]
		[DefaultValue(typeof(Font),"Microsoft Sans Serif; 8,25pt")]
		public Font Font
		{
			get
			{
				return m_font;
			}
			set
			{
				if (m_font!=value)
				{
					m_font = value;
					m_calendar.DoLayout();
					if (PropertyChanged!=null)
						PropertyChanged(this,new FooterPropertyEventArgs(mcFooterProperty.Font));   
					m_calendar.Invalidate();
				}
			}
		}
		
		[Description("Color used for text.")]
		[DefaultValue(typeof(Color),"Black")]
		public Color TextColor
		{
			get
			{
				return m_textColor;
			}
			set
			{
				if (m_textColor!=value)
				{
					m_textColor = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new FooterPropertyEventArgs(mcFooterProperty.Text));   
					m_calendar.Invalidate();
				}
			}
		}

		#endregion

	}

	#region FooterPropertyEventArgs
	
	public class FooterPropertyEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcFooterProperty m_property;

		#endregion

		#region Constructor

		public FooterPropertyEventArgs()
		{
			m_property = 0;
		}

		public FooterPropertyEventArgs(mcFooterProperty property)
		{
			this.m_property = property;
		}

		#endregion


		#region Properties

		public mcFooterProperty Property
		{
			get
			{
				return this.m_property;
			}
		}

		#endregion
	}


	#endregion

}
