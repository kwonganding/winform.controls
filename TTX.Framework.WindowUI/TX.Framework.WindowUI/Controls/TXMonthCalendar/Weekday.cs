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
	public enum mcDayFormat {Short = 0, Long}
	public enum mcTextAlign {Left = 0, Center, Right}
	
	public enum mcWeekdayProperty 
	{
		BorderColor = 0, BackColor1, BackColor2, GradientMode, Font, TextColor, Format, Align }
	
	#region Delegates
	
	public delegate void WeekdayPropertyEventHandler(object sender, WeekdayPropertyEventArgs e);
	public delegate void WeekdayClickEventHandler(object sender, WeekdayClickEventArgs e);
	
	#endregion


	/// <summary>
	/// Summary description for Weekday.
	/// </summary>
	[TypeConverter(typeof(WeekdayTypeConverter))]
	public class Weekday : IDisposable
	{
		#region private class members
		
		private bool disposed;
		private MonthCalendar m_calendar;
		private Color m_backColor1;
        private Color m_backColor2;
        private mcGradientMode m_gradientMode; 
		private Color m_textColor;
		private Color m_borderColor;
		private Font m_font;
		private mcDayFormat m_dayFormat;
		private mcTextAlign m_align;
		private Rectangle m_rect;
		private Region m_region;
				
		#endregion

		#region EventHandlers
		
		internal event WeekdayClickEventHandler Click;
		internal event WeekdayClickEventHandler DoubleClick;
		internal event WeekdayPropertyEventHandler PropertyChanged;
		

		#endregion
		
		#region Constructor

		public Weekday(MonthCalendar calendar)
		{
			m_calendar = calendar;
			m_backColor1 = Color.White;
            m_backColor2 = Color.White;
            m_gradientMode = mcGradientMode.None;
            m_textColor = Color.FromArgb(0,84,227);
			m_font = new Font("Microsoft Sans Serif",(float)8.25);
			m_dayFormat = mcDayFormat.Short; 
			m_align = mcTextAlign.Center; 
			m_borderColor = Color.Black;
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
		
		[Description("Color used border.")]
		[DefaultValue(typeof(Color),"Black")]
		public Color BorderColor
		{
			get
			{
				return m_borderColor;
			}
			set
			{
				if (m_borderColor!=value)
				{
					m_borderColor = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new WeekdayPropertyEventArgs(mcWeekdayProperty.BorderColor)); 
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
						PropertyChanged(this,new WeekdayPropertyEventArgs(mcWeekdayProperty.BackColor1));
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
                        PropertyChanged(this, new WeekdayPropertyEventArgs(mcWeekdayProperty.BackColor2));
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
                        PropertyChanged(this, new WeekdayPropertyEventArgs(mcWeekdayProperty.GradientMode ));
                    m_calendar.Invalidate();
                }
            }
        }
		
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

		[Description("Indicates wether the name of the day should be displayed using a long or short format.")]
		[DefaultValue(typeof(mcDayFormat),"Short")]
		public mcDayFormat Format
		{
			get
			{
				return m_dayFormat;
			}
			set
			{
				if (m_dayFormat!=value)
				{
					m_dayFormat = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new WeekdayPropertyEventArgs(mcWeekdayProperty.Format)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Determines the position for the text.")]
		[DefaultValue(typeof(mcTextAlign),"Center")]
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
						PropertyChanged(this,new WeekdayPropertyEventArgs(mcWeekdayProperty.Align)); 
					m_calendar.Invalidate();
				}
			}
		}
		
		[Description("The font used for weekdays.")]
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
						PropertyChanged(this,new WeekdayPropertyEventArgs(mcWeekdayProperty.Font)); 
					m_calendar.Invalidate();
				}
			}
		}
		
		[Description("Color used for text.")]
		[DefaultValue(typeof(Color),"0,84,227")]
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
						PropertyChanged(this,new WeekdayPropertyEventArgs(mcWeekdayProperty.TextColor)); 
					m_calendar.Invalidate();
				}
			}
		}

		#endregion

		#region Methods
		
		internal void MouseMove(Point mouseLocation)
		{
			if (m_region.IsVisible(mouseLocation))
			{
				m_calendar.ActiveRegion = mcCalendarRegion.Weekdays;  
			}
		}


		internal void MouseClick(Point mouseLocation,MouseButtons button, mcClickMode mode)
		{
			if (m_region.IsVisible(mouseLocation))
			{
				int day;
				day = (mouseLocation.X / (int)m_calendar.Month.DayWidth);
				if (mode == mcClickMode.Single)
				{
					if (this.Click!=null)
						this.Click(this,new WeekdayClickEventArgs(day,button)); 
				}
				else
				{
					if (this.DoubleClick!=null)
						this.DoubleClick(this,new WeekdayClickEventArgs(day,button));
				}
		
			}

		}

		internal string[] GetWeekDays()
		{
			int index = 0;
			string[] sysNames;
			string[] weekdays = new string[7] ;
			int FirstDayOfWeek = (int)m_calendar.m_dateTimeFormat.FirstDayOfWeek;
					 
			// Get system names for weekdays
			if (Format == mcDayFormat.Short)  
				sysNames = m_calendar.m_dateTimeFormat.AbbreviatedDayNames;
			else
				sysNames = m_calendar.m_dateTimeFormat.DayNames;
			
			weekdays.Initialize(); 
			
			// Arrange weekdays starting with first day of week
			for (int i =FirstDayOfWeek;i<weekdays.Length;i++)
			{
				weekdays[index] = sysNames[i];
				index++;
			}
			for (int i =0;i<FirstDayOfWeek;i++)
			{
				weekdays[index] = sysNames[i];
				index++;
			}

			return weekdays;
		}

		internal bool IsVisible(Rectangle clip)
		{
			return m_region.IsVisible(clip); 	
		}

		internal void Draw(Graphics e)
		{
			Pen linePen = new Pen(m_borderColor,1);
			StringFormat textFormat = new StringFormat();
			Rectangle dayRect = new Rectangle();
			int dayWidth;			
			string[] weekdays;
			
			weekdays = GetWeekDays();

			Brush headerBrush = new SolidBrush(this.BackColor1);
			Brush headerTextBrush = new SolidBrush(this.TextColor); 
			
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




            if (m_gradientMode == mcGradientMode.None)
                e.FillRectangle(headerBrush, 0, m_rect.Top, m_calendar.Width, m_rect.Height);
            else
                m_calendar.DrawGradient(e, m_rect, m_backColor1, m_backColor2, m_gradientMode);
  
            dayWidth = (int)m_calendar.Month.DayWidth;	
			
			for (int i = 0;i<7;i++)
			{
				dayRect.Y = m_rect.Y;
				dayRect.Width = dayWidth; 
				dayRect.Height = m_rect.Height;
				dayRect.X =(dayWidth*i) + m_rect.X;
                dayRect.X += (i + 1) * m_calendar.Month.Padding.Horizontal;
                if (i==6)
                    dayRect.Width = m_rect.Width - (int)(m_calendar.Month.Padding.Horizontal * 8) - (int)(dayWidth * 6) - 1;
         		
				e.DrawString(weekdays[i],this.Font,headerTextBrush,dayRect,textFormat);
			}
			e.DrawLine(linePen,m_rect.X,m_rect.Bottom-1,m_rect.Right,m_rect.Bottom-1); 
			
			// tidy up
			headerBrush.Dispose(); 
			headerTextBrush.Dispose();
			linePen.Dispose();
		}

		#endregion

	}

	#region WeekdayClickEventArgs
	
	public class WeekdayClickEventArgs : EventArgs
	{
		#region Class Data
			
		private int m_day;
		private MouseButtons m_button;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DayClickEventArgs class with default settings
		/// </summary>
		public WeekdayClickEventArgs()
		{
			m_button = MouseButtons.Left;
		}

		public WeekdayClickEventArgs(int day, MouseButtons button)
		{
			this.m_day =day;
			this.m_button = button;
		}

		#endregion


		#region Properties

		public int Day
		{
			get
			{
				return this.m_day;
			}
		}

		public MouseButtons Button
		{
			get
			{
				return this.m_button; 
			}
		}

		#endregion
	}


	#endregion
	
	#region WeekdayPropertyEventArgs
	
	public class WeekdayPropertyEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcWeekdayProperty m_property;

		#endregion

		#region Constructor

		public WeekdayPropertyEventArgs()
		{
			m_property = 0;
		}

		public WeekdayPropertyEventArgs(mcWeekdayProperty property)
		{
			this.m_property = property;
		}

		#endregion


		#region Properties

		public mcWeekdayProperty Property
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
