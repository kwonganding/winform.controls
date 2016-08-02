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
using System.Globalization;
using System.Windows.Forms;   

namespace TX.Framework.WindowUI.Controls
{
	
	public enum mcWeeknumberProperty 
	{
		BorderColor = 0, BackColor1, BackColor2, GradientMode, Font, TextColor, Align
    }
    
    public enum mcWeeknumberAlign
    {
        Top = 0, Center, Bottom
    }
	
	#region Delegates
	
	public delegate void WeeknumberPropertyEventHandler(object sender, WeeknumberPropertyEventArgs e);
	public delegate void WeeknumberClickEventHandler(object sender, WeeknumberClickEventArgs e);

	#endregion

	
	/// <summary>
	/// Summary description for WeekNumber.
	/// </summary>
	[TypeConverter(typeof(WeeknumberTypeConverter))]
	public class Weeknumber : IDisposable
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
		private Rectangle m_rect;
		private Region m_region;
        private mcWeeknumberAlign m_align; 

		#endregion

		#region EventHandler

		internal event WeeknumberClickEventHandler Click;
		internal WeeknumberClickEventHandler DoubleClick;
		internal event WeeknumberPropertyEventHandler PropertyChanged;

		#endregion

		#region Constructor

		public Weeknumber(MonthCalendar calendar)
		{
			m_calendar = calendar;
			m_backColor1 = Color.White;
            m_backColor2 = Color.White;
            m_gradientMode = mcGradientMode.None;
            m_textColor = Color.FromArgb(0,84,227); 
			m_borderColor = Color.Black;
			m_font = new Font("Microsoft Sans Serif",(float)8.25);
            m_align = mcWeeknumberAlign.Top; 	
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
						PropertyChanged(this,new WeeknumberPropertyEventArgs(mcWeeknumberProperty.BorderColor)); 
					m_calendar.Invalidate();
				}
			}
		}

        [Description("Determines the position for the text.")]
        [DefaultValue(typeof(mcWeeknumberAlign), "Top")]
        public mcWeeknumberAlign Align
        {
            get
            {
                return m_align;
            }
            set
            {
                if (m_align != value)
                {
                    m_align = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new WeeknumberPropertyEventArgs(mcWeeknumberProperty.Align));
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
						PropertyChanged(this,new WeeknumberPropertyEventArgs(mcWeeknumberProperty.BackColor1)); 
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
                        PropertyChanged(this, new WeeknumberPropertyEventArgs(mcWeeknumberProperty.BackColor2));
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
                        PropertyChanged(this, new WeeknumberPropertyEventArgs(mcWeeknumberProperty.GradientMode));
                    m_calendar.Invalidate();
                }
            }
        }
		
		[Description("Font used for week numbers.")]
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
						PropertyChanged(this,new WeeknumberPropertyEventArgs(mcWeeknumberProperty.Font)); 
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
						PropertyChanged(this,new WeeknumberPropertyEventArgs(mcWeeknumberProperty.TextColor)); 
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
				m_calendar.ActiveRegion = mcCalendarRegion.Weeknumbers;  
			}
		}

		internal void MouseClick(Point mouseLocation,MouseButtons button, mcClickMode mode)
		{
			GregorianCalendar gCalendar = new GregorianCalendar();
	
			if (m_region.IsVisible(mouseLocation))
			{
				int week = 0;
				
				int i = ((mouseLocation.Y-m_rect.Top) / (int)m_calendar.Month.DayHeight);				
				week = gCalendar.GetWeekOfYear(m_calendar.Month.m_days[i*7].Date,m_calendar.m_dateTimeFormat.CalendarWeekRule,m_calendar.m_dateTimeFormat.FirstDayOfWeek);
				if (mode == mcClickMode.Single)
				{
					if (this.Click!=null)
						this.Click(this,new WeeknumberClickEventArgs(week,button));
				}
				else
				{
					if (this.DoubleClick!=null)
						this.DoubleClick(this,new WeeknumberClickEventArgs(week,button));
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
			Pen linePen = new Pen(m_borderColor,1);
			Rectangle weekRect = new Rectangle();
			 
			int weeknr=0;	
			Brush weekBrush = new SolidBrush(this.BackColor1);
			Brush weekTextBrush = new SolidBrush(this.TextColor); 
			int dayHeight;
			
			// Draw header
            textFormat.Alignment = StringAlignment.Center;
		    switch (m_align)
            {
                case mcWeeknumberAlign.Top:
                {

                    textFormat.LineAlignment = StringAlignment.Near;
                    break;
                }
                case mcWeeknumberAlign.Center:
                {

                    textFormat.LineAlignment = StringAlignment.Center;
                    break;
                }
                case mcWeeknumberAlign.Bottom:
                {

                    textFormat.LineAlignment = StringAlignment.Far;
                    break;
                }



            }

            if (m_gradientMode == mcGradientMode.None)
                e.FillRectangle(weekBrush, m_rect);
            else
                m_calendar.DrawGradient(e, m_rect, m_backColor1, m_backColor2, m_gradientMode);  
			
			dayHeight = (int)m_calendar.Month.DayHeight; 			
			for (int i = 0;i<6;i++)
			{
				weekRect.Y = m_rect.Y + dayHeight*i;
                weekRect.Y += (i+1)* m_calendar.Month.Padding.Vertical;    
                weekRect.Width = m_rect.Width; 
				weekRect.X =0;
                if (i == 5)
                    weekRect.Height = m_rect.Height - (m_calendar.Month.Padding.Vertical*7) - (int)(dayHeight*5)-1;
                else
                    weekRect.Height = dayHeight;
				
				weeknr = GetWeek(m_calendar.Month.m_days[i*7].Date);
				
				e.DrawString(weeknr.ToString(),this.Font,weekTextBrush,weekRect,textFormat);
					  
			}
			e.DrawLine(linePen,m_rect.Right-1,m_rect.Top,m_rect.Right-1,m_rect.Bottom); 
			// tidy up
			weekBrush.Dispose(); 
			weekTextBrush.Dispose();
			linePen.Dispose(); 
			
		}

		internal int GetWeek(DateTime dt)
		{
			int weeknr;
					
			try
			{
				// retrieve week by calling weeknumber callback
				weeknr = m_calendar.WeeknumberCallBack(dt);	
			}
			catch(Exception)
			{
				//if callback fails , call CalcWeek 
				weeknr = CalcWeek(dt);		
			}
			return weeknr;
		}

		internal int CalcWeek(DateTime dt)
		{
			int weeknr = 0;
			GregorianCalendar gCalendar = new GregorianCalendar();

			if ((m_calendar.m_dateTimeFormat.CalendarWeekRule == CalendarWeekRule.FirstFourDayWeek) &&
				(m_calendar.m_dateTimeFormat.FirstDayOfWeek == DayOfWeek.Monday))
				// Get ISO week
				weeknr = GetISO8601Weeknumber(dt); 	
			else
				// else get Microsoft week
				weeknr = gCalendar.GetWeekOfYear(dt,m_calendar.m_dateTimeFormat.CalendarWeekRule, m_calendar.m_dateTimeFormat.FirstDayOfWeek);
			
			return weeknr;
		}

        private int GetISO8601Weeknumber(DateTime dt)
        {
            DateTime week1;
            int IsoYear = dt.Year;
            int IsoWeek;
            if (dt >= new DateTime(IsoYear, 12, 29))
            {
                week1 = GetIsoWeekOne(IsoYear + 1);
                if (dt < week1)
                {
                    week1 = GetIsoWeekOne(IsoYear);
                }
                else
                {
                    IsoYear++;
                }
            }
            else
            {
                week1 = GetIsoWeekOne(IsoYear);
                if (dt < week1)
                {
                    week1 = GetIsoWeekOne(--IsoYear);
                }
            }

            IsoWeek = (IsoYear * 100) + ((dt - week1).Days / 7 + 1);
            return IsoWeek % 100;
        }

        private DateTime GetIsoWeekOne(int Year)
        {
            // get the date for the 4-Jan for this year
            DateTime dt = new DateTime(Year, 1, 4);

            // get the ISO day number for this date 1==Monday, 7==Sunday
            int dayNumber = (int)dt.DayOfWeek; // 0==Sunday, 6==Saturday
            if (dayNumber == 0)
            {
                dayNumber = 7;
            }

            // return the date of the Monday that is less than or equal
            // to this date
            return dt.AddDays(1 - dayNumber);
        }
	 		

		#endregion

	}

	
	#region WeeknumberClickEventArgs
	
	public class WeeknumberClickEventArgs : EventArgs
	{
		#region Class Data
			
		private int m_week;
		private MouseButtons m_button;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DayClickEventArgs class with default settings
		/// </summary>
		public WeeknumberClickEventArgs()
		{
			m_button = MouseButtons.Left;
		}

		public WeeknumberClickEventArgs(int week, MouseButtons button)
		{
			this.m_week =week;
			this.m_button = button;
		}

		#endregion


		#region Properties

		public int Week
		{
			get
			{
				return this.m_week;
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

	#region WeeknumberPropertyEventArgs
	
	public class WeeknumberPropertyEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcWeeknumberProperty m_property;

		#endregion

		#region Constructor

		public WeeknumberPropertyEventArgs()
		{
			m_property = 0;
		}

		public WeeknumberPropertyEventArgs(mcWeeknumberProperty property)
		{
			this.m_property = property;
		}

		#endregion


		#region Properties

		public mcWeeknumberProperty Property
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
