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
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.Collections;
using System.Drawing.Drawing2D;   


namespace TX.Framework.WindowUI.Controls
{
	
	public enum mcMonthProperty 
	{
		Transparency = 0, FormatTrailing, Padding, DateAlign, ShowMonthInDay,
        TextAlign, ImageAlign, DateFont, TextFont, BackgroundImage, ImageClick
 	}
	
	public enum mcMonthColor
	{
		SelectedBackColor = 0,SelectedBorder, SelectedText, SelectedDate,
        FocusBackColor,FocusBorder, FocusText,FocusDate,
        BackColor1, BackColor2, Gradient,
        TrailingText, TrailingDate, TrailingBackColor1,TrailingBackColor2,TrailingGradient,
		WeekendBackColor1, WeekendBackColor2,WeekendDate, WeekendText,WeekendGradient,
        DisabledBackColor1, DisabledBackColor2, DisabledText, DisabledDate, DisabledGradient,
        DayBackColor1, DayBackColor2, DayText, DayDate, DayGradient, DayBorder
    }
    
	public enum mcMonthBorderStyle
	{
		Normal = 0, Selected, Focus
	}

    public enum mcGradientMode { None = 0, Vertical, Horizontal, BackwardDiagonal, ForwardDiagonal }

	#region Delegates

	public delegate void MonthPropertyEventHandler(object sender, MonthPropertyEventArgs e);
	public delegate void MonthColorEventHandler(object sender, MonthColorEventArgs e);
	public delegate void MonthBorderStyleEventHandler(object sender, MonthBorderStyleEventArgs e);

	#endregion
	
	
	/// <summary>
	/// Summary description for DayProperties.
	/// </summary>
	[TypeConverter(typeof(MonthTypeConverter))]
	public class Month : IDisposable
	{	
		#region private class members
		
		private const int NO_AREA = -2;
	     	
		private bool disposed;
		private MonthCalendar m_calendar;
		private Font m_dateFont;
		private Font m_textFont;
		private Rectangle m_rect;
		private Region m_region;
       	
		int m_selLeft;
		int m_selRight;
		int m_selTop;
		int m_selBottom;

        Point oldMouseLocation;
		
		private bool m_newSelection;
		internal Day[] m_days;
				
		private MonthPadding m_padding;
        private TransparencyCollection m_transparency; 
        private mcItemAlign m_dateAlign;
		private mcItemAlign m_textAlign;
		private mcItemAlign m_imageAlign;
		
		private bool m_showMonth;
		private bool m_mouseDown;
		private bool m_formatTrailing;
		private bool m_imageClick;

        private Image m_backgroundImage;
		internal ArrayList m_selArea = new ArrayList(); // Collection of selected areas.

		private MonthColors m_colors;
		private MonthBorderStyles m_borderStyles;

		private DateTime m_selectedMonth;
		private bool m_selected;		
		private int m_dayInFocus;

		private float m_dayWidth;
		private float m_dayHeight;

		#endregion
		
		#region Events
		
		internal event DayRenderEventHandler DayRender;
        internal event DayQueryInfoEventHandler DayQueryInfo;
        internal event DayEventHandler DayLostFocus;
		internal event DayEventHandler DayGotFocus;
        internal event DayMouseMoveEventHandler DayMouseMove;
        internal event DayClickEventHandler ImageClick;
        internal event DayClickEventHandler DayClick;
        internal event DayClickEventHandler DayDoubleClick;
        internal event DaySelectedEventHandler DaySelected;
        internal event DaySelectedEventHandler DayDeselected;
        internal event MonthPropertyEventHandler PropertyChanged;
        internal event MonthColorEventHandler ColorChanged;
        internal event MonthBorderStyleEventHandler BorderStyleChanged;
        internal event DayStateChangedEventHandler BeforeDaySelected;
        internal event DayStateChangedEventHandler BeforeDayDeselected;
      
		#endregion
		
		#region constructor

		public Month(MonthCalendar calendar)
		{
			m_calendar = calendar;
			m_dateFont = new Font("Microsoft Sans Serif",(float)8.25);
			m_textFont = new Font("Microsoft Sans Serif",(float)8.25);

			m_dayInFocus = -1;
			m_selArea.Clear();
			
            m_formatTrailing = true;
			m_imageAlign = mcItemAlign.TopLeft;
			m_dateAlign = mcItemAlign.Center;
			m_textAlign = mcItemAlign.BottomLeft;
			m_imageClick = false;
			
			// we need 42 (7 * 6) days for display
			m_days = new Day[42];
			for (int i = 0;i<42;i++)
			{
				m_days[i] = new Day();
				m_days[i].Month = this;
				m_days[i].Calendar = m_calendar;
            }

			m_colors = new MonthColors(this); 
			m_borderStyles = new MonthBorderStyles(this); 
			m_padding = new MonthPadding(this);
            m_transparency = new TransparencyCollection(this);
        }

        

		#endregion
                        

        #region Dispose

        protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
                                        
                    m_dateFont.Dispose();
					m_textFont.Dispose();
					m_region.Dispose();
                    if (m_backgroundImage != null)
                        m_backgroundImage.Dispose();
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
		
		#region Poperties


        [Browsable(false)]
        internal int DayInFocus
        {
            get
            {
                return m_dayInFocus;
            }
            set
            {
                m_dayInFocus = value;
            }
        }

		internal bool MouseDown
		{
			get
			{
				return m_mouseDown;
			}
		}

		internal MonthCalendar Calendar
		{
			get
			{
				return m_calendar;
			}
		}

		internal DateTime SelectedMonth
		{
			get
			{
				return m_selectedMonth;
			}
			set
			{
				m_selectedMonth = value;
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

		internal float DayWidth
		{
			get
			{
				return m_dayWidth;
			}
		}

		internal float DayHeight
		{
			get
			{
				return m_dayHeight;
			}
		}

		[Browsable(true)]
		[Description("Indicates wether formatting should be applied to trailing dates.")]
		[DefaultValue(true)]
		public bool FormatTrailing
		{
			get
			{
				return m_formatTrailing;
			}
			set
			{
				if (m_formatTrailing!=value)
				{
					m_formatTrailing = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.FormatTrailing)); 
					Calendar.Invalidate(); 
				}
			}
		}

		[Browsable(true)]
		[Description("Indicates wether the calendar should respond to image click.")]
		[DefaultValue(false)]
		public bool EnableImageClick
		{
			get
			{
				return m_imageClick;
			}
			set
			{
				if (m_imageClick!=value)
				{
					m_imageClick = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.ImageClick)); 
					Calendar.Invalidate(); 
				}
			}
		}


		[Browsable(true)]
		[Description("Padding (Horizontal, Vertical)")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MonthPadding Padding
		{
			get
			{
				return m_padding;
			}
            set
            {
                if (value != m_padding)
                {
                    if (value != null) m_padding = value;
                    SetupDays();
                    Calendar.Invalidate();
                }

            }
		}

        [Browsable(true)]
        [Description("Transparency (Background, Text)")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TransparencyCollection Transparency
        {
            get
            {
                return m_transparency;
            }
            set
            {
                if (value != m_transparency)
                {
                    if (value != null) m_transparency = value;
                    SetupDays();
                    Calendar.Invalidate();
                }

            }
        }


        [Description("Image used as background.")]
        public Image BackgroundImage
        {
            get
            {
                return m_backgroundImage;
            }
            set
            {
                if (m_backgroundImage != value)
                {
                    m_backgroundImage = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new MonthPropertyEventArgs(mcMonthProperty.BackgroundImage));
                    m_calendar.Invalidate();
                }
            }

        }
		
		[Editor(typeof(AlignEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Determines the position of the date within the day.")]
		[DefaultValue(typeof(mcItemAlign),"Center")]
		public mcItemAlign DateAlign
		{
			get
			{
				return m_dateAlign;
			}
			set
			{
				if (m_dateAlign!=value)
				{
					m_dateAlign = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.DateAlign)); 
					m_calendar.Invalidate();
				}
			}

		}

		[Description("Indicates wether the month should be displayed for the first and last day.")]
		[DefaultValue(false)]
		public bool ShowMonthInDay
		{
			get
			{
				return m_showMonth;
			}
			set
			{
				if (m_showMonth!=value)
				{
					m_showMonth = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.ShowMonthInDay)); 
					m_calendar.Invalidate();			
				}
			}
		}
		
		[Editor(typeof(AlignEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Determines the position for the text within the day.")]
		[DefaultValue(typeof(mcItemAlign),"BottomLeft")]
		public mcItemAlign TextAlign
		{
			get
			{
				return m_textAlign;
			}
			set
			{
				if (m_textAlign!=value)
				{
					m_textAlign = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.TextAlign)); 
					m_calendar.Invalidate();
				}
			}

		}

		[Editor(typeof(AlignEditor),typeof(System.Drawing.Design.UITypeEditor))]
		[Description("Determines the position of the image within the day.")]
		[DefaultValue(typeof(mcItemAlign),"TopLeft")]
		public mcItemAlign ImageAlign
		{
			get
			{
				return m_imageAlign;
			}
			set
			{
				if (m_imageAlign!=value)
				{
					m_imageAlign = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.ImageAlign)); 
					m_calendar.Invalidate();
				}
			}

		}
			
		[Description("Borders used in calendar.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MonthBorderStyles BorderStyles
		{
			get
			{
				return m_borderStyles;
			}
		}

		[Description("Colors used in calendar.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MonthColors Colors
		{
			get
			{
				return m_colors;
			}
		}
	
		[Description("Font used for date.")]
		[DefaultValue(typeof(Font),"Microsoft Sans Serif; 8,25pt")]
		public Font DateFont
		{
			get
			{
				return m_dateFont;
			}
			set
			{
				if (m_dateFont!=value)
				{
					m_dateFont = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.DateFont)); 
					m_calendar.Invalidate();
				}
			}
		}

		[Description("Font used for text.")]
		[DefaultValue(typeof(Font),"Microsoft Sans Serif; 8,25pt")]
		public Font TextFont
		{
			get
			{
				return m_textFont;
			}
			set
			{
				if (m_textFont!=value)
				{
					m_textFont = value;
					if (PropertyChanged!=null)
						PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.TextFont)); 
					m_calendar.Invalidate();
				}
			}
		}
	
	
		#endregion

		#region Methods
		
		private string[] DaysInSelection(int sel)
		{
			string[] days;
			days = new string[0];
			days.Initialize();
			for (int i = 0;i<42;i++)
			{
				if ( (sel == m_days[i].SelectionArea) || ((sel==NO_AREA) && (m_days[i].State == mcDayState.Selected)) )
				{
					days = AddDate(m_days[i].Date.ToShortDateString(),days);  
				}
			}
			return days;
		}
		
		private bool IsDateEnabled(DateTime dt)
		{
			DateItem[] info;
			bool enabled = true;
			info = m_calendar.GetDateInfo(dt);
			for (int i = 0;i<info.Length;i++)
			{
				if (info[i].Enabled == false)
				{
					enabled = false;
					break;
				}
			}
			return enabled;
		}

		private int SelectionDayCount(int sel)
		{	
			int nr = 0;
			for (int i = 0;i<42;i++)
			{
				if (sel == m_days[i].SelectionArea)
					nr++;
			}
			return nr;
		}

		internal void RemoveDay(int day, bool raiseEvent)
		{
			string[] d;
			
			// retrieve the days area
			int sel = m_days[day].SelectionArea;
			d = DaysInSelection(sel);
			
			SelectionArea area = (SelectionArea)m_selArea[sel];
			
			// reset the area
			area.Begin = -1;
			area.End = -1;
					
			// reset the day
			m_days[day].State = mcDayState.Normal;
			m_days[day].SelectionArea = -1; 
			
			if (raiseEvent)
			{
				// Raise event
				if ((this.DayDeselected!=null) && (d.Length>0))
					this.DayDeselected(this,new DaySelectedEventArgs(d));
			}
				
			for (int i = 0;i<42;i++)
			{
				// We dont want to add the day we are removing
				if (i!=day)
				{
					// Check if day belong to the same area as the day we are removing
					if (m_days[i].SelectionArea == sel)
					{
						// Create new selected day
						m_days[i].State = mcDayState.Normal;
						NewSelectedDay(i);
					}
				}
			}
			m_newSelection = true;
			
		}
	
		internal void NewSelectedDay(int day)
		{
			NewSelectedArea(day,day);
		}
				
		internal void NewSelectedRange(int from, int to)
		{
			if ((m_days[from].Rectangle.Bottom == m_days[to].Rectangle.Bottom ))
			{
				// dates are in the same week , treat as area
				NewSelectedArea(from,to);	
			}
			else
			{
				// days are not in same week , select individually
				if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				{
					for (int i = from;i<=to;i++)
					{
						if (m_days[i].State !=mcDayState.Selected) 
							NewSelectedDay(i);
					}
				}
			}
		}

		/*internal void Remove(int day)
		{
			if (m_days[day].State == mcDayState.Selected)
			{
				if (SelectionDayCount(m_days[day].SelectionArea) ==1)
				{
					RemoveSelection(false,m_days[day].SelectionArea);
				}
				else
				{
					RemoveDay(day,false);
				}
			}
		}*/


        internal void Remove(int day)
        {
            if (SelectionDayCount(m_days[day].SelectionArea) == 1)
            {
                RemoveSelection(true, m_days[day].SelectionArea);
                m_days[day].State = mcDayState.Focus;
            }
            else if ((m_calendar.SelectionMode == mcSelectionMode.MultiExtended))
            {
                if (m_calendar.ExtendedKey)
                {
                    RemoveDay(day, true);
                    m_days[day].State = mcDayState.Focus;
                }
                else
                {
                    NewSelectedDay(day);
                }
            }
            else
            {
                NewSelectedDay(day);
            }
        }

		internal void NewSelectedArea(int topLeft, int bottomRight)
		{	
			
			if ((!m_calendar.ExtendedKey) || (m_calendar.SelectionMode<mcSelectionMode.MultiExtended))
			{
				// clear selection and start over
				RemoveSelection(true);
				m_selArea.Clear();
			}
			else if ((m_calendar.SelectionMode==mcSelectionMode.MultiExtended))
			{
				// Add new area
				//m_selIndex++;
			}
			
			m_selArea.Add(new SelectionArea(topLeft,bottomRight,this));
		
			// Mark area as selected
			MarkAreaAsSelected(topLeft,bottomRight,m_selArea.Count-1);
			
			m_selected = true;
			m_newSelection = false;
		}

		internal void DeselectRange(int from, int to)
		{
			string[] dates = new string[0];
			
			// if MultiExtended , press CTRL to enable extended select
			if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				m_calendar.ExtendedKey = true; 
			
			for (int i = from;i<=to;i++)
			{
				if (m_days[i].State==mcDayState.Selected)
				{
					Remove(i);
					dates = AddDate(m_days[i].Date.ToShortDateString(),dates); 
				}
			}
						
			// raise dayselected event
			if ((this.DayDeselected!=null) && (dates.Length>0))
				this.DayDeselected(this,new DaySelectedEventArgs(dates));
			
			m_calendar.ExtendedKey = false;
		}
		
		internal void DeselectArea(int topLeft, int bottomRight)
		{
			ArrayList days;
			string[] dates = new string[0];
			int index;

			days = DaysInArea(topLeft,bottomRight);

			// if MultiExtended , press CTRL to enable extended select
			if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				m_calendar.ExtendedKey = true; 
			
			for (int i = 0;i<days.Count;i++)
			{
				index = (int)days[i];
				if (m_days[index].State==mcDayState.Selected)
				{
					Remove(index);
					dates = AddDate(m_days[index].Date.ToShortDateString(),dates); 
				}
			}
						
			// raise dayselected event
			if ((this.DayDeselected!=null) && (dates.Length>0))
				this.DayDeselected(this,new DaySelectedEventArgs(dates));
			
			m_calendar.ExtendedKey = false;
		}
			
		internal void SelectArea(int topLeft, int bottomRight)
		{
			// if MultiExtended , press CTRL to enable extended select
			if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				m_calendar.ExtendedKey = true; 
				
			NewSelectedArea(topLeft,bottomRight);
			
			// raise dayselected event
			if (this.DaySelected!=null)
				this.DaySelected(this,new DaySelectedEventArgs(DaysInSelection(NO_AREA)));
			
			m_calendar.ExtendedKey = false;
		}
		
		internal void SelectRange(int from, int to)
		{
			string[] selBefore;
			string[] selAfter;
			
			// if MultiExtended , press CTRL to enable extended select
			if (m_calendar.SelectionMode==mcSelectionMode.MultiExtended)
				m_calendar.ExtendedKey = true; 
		
			selBefore = DaysInSelection(NO_AREA);
			NewSelectedRange(from,to);
			selAfter = DaysInSelection(NO_AREA);
			
			if (selAfter.Length>selBefore.Length)
			{
				// raise dayselected event
				if (this.DaySelected!=null)
					this.DaySelected(this,new DaySelectedEventArgs(selAfter));
			}

			// Release CTRL key
			m_calendar.ExtendedKey = false; 
		}
		
		internal void DoubleClick(Point mouseLocation, MouseButtons button)
		{
			Region monthRgn = new Region(m_rect);

			if (monthRgn.IsVisible(mouseLocation))
			{
				for (int i = 0;i<42;i++)
				{
					if (m_days[i].HitTest(mouseLocation))
					{
						if (this.DayDoubleClick!=null)
							this.DayDoubleClick(this,new DayClickEventArgs(m_days[i].Date.ToShortDateString() ,button,
                                                mouseLocation.X - m_days[i].Rectangle.Left, mouseLocation.Y - m_days[i].Rectangle.Top,
                                                mouseLocation.X,mouseLocation.Y,m_days[i].Rectangle));
					}
				}
			}
		}
		
		internal void MouseUp()
		{
			m_mouseDown = false;
			string[] days;
			days = DaysInSelection(NO_AREA);
			if ((days.Length>0) && (m_selected))
			{
				Array.Sort(days);
				if (this.DaySelected!=null)
					this.DaySelected(this,new DaySelectedEventArgs(days));
				m_selected = false;
			}
		}

		internal void Click(Point mouseLocation,MouseButtons button)
		{
						
			if (m_region.IsVisible(mouseLocation))
			{
				for (int i = 0;i<42;i++)
				{
					if (m_days[i].HitTest(mouseLocation))
					{

                        DaySelect(i, button, mouseLocation);
                        break;
					}
				}
			}
		}

        internal void DaySelect(int i, MouseButtons button, Point mouseLocation)
        {
            bool dayEnabled = true;

            if (m_calendar.SelectionMode > mcSelectionMode.None)
            {
                m_dayInFocus = i;
                // Check if proper button is used
                if (button == m_calendar.SelectButton) 
                {

                    if (!m_days[i].ImageHitTest(mouseLocation))
                    {

                        dayEnabled = IsDateEnabled(m_days[i].Date);

                        if (((m_calendar.SelectTrailingDates) || (SelectedMonth.Month == m_days[i].Date.Month)) &&
                            ((m_calendar.MinDate <= m_days[i].Date) && (m_calendar.MaxDate >= m_days[i].Date)) && (dayEnabled))
                        {

                            // If day is already selected and number of days in selection = 1
                            // or selectionMode = MultiExtended, toggle to focus
                            if (m_days[i].State == mcDayState.Selected)
                            {
                                DayStateChangedEventArgs args = new DayStateChangedEventArgs(m_days[i].Date.ToShortDateString(), mcDayState.Selected, mcDayState.Normal);
                                if (BeforeDayDeselected != null)
                                    BeforeDayDeselected(this, args);
                                if (!args.Cancel)
                                {
                                    Remove(i);
                                }

                            }
                            else
                            {
                                DayStateChangedEventArgs args = new DayStateChangedEventArgs(m_days[i].Date.ToShortDateString(), mcDayState.Normal, mcDayState.Selected);
                                if (BeforeDaySelected != null)
                                    BeforeDaySelected(this, args);

                                if (!args.Cancel)
                                {
                                    NewSelectedDay(i);
                                }
                            }

                            m_mouseDown = true;
                            m_calendar.Invalidate();
                        }
                    }
                    else
                    {
                        if (this.ImageClick != null)
                            this.ImageClick(this, new DayClickEventArgs(m_days[i].Date.ToShortDateString(), button,
                                            mouseLocation.X - m_days[i].Rectangle.Left, mouseLocation.Y - m_days[i].Rectangle.Top,
                                            mouseLocation.X, mouseLocation.Y, m_days[i].Rectangle));
             
                    }

                }
                //either way ceate DayClick event
                if (this.DayClick != null)
                    this.DayClick(this, new DayClickEventArgs(m_days[i].Date.ToShortDateString(), button,
                                  mouseLocation.X - m_days[i].Rectangle.Left, mouseLocation.Y - m_days[i].Rectangle.Top,
                                  mouseLocation.X, mouseLocation.Y, m_days[i].Rectangle));
            }
					
        }

		private string[] AddDate(string dt, string[] old)
		{
			int l =  old.Length;
			int i;
			bool exist = false;
			string[] n = new string[l+1];
			n.Initialize(); 
			for (i = 0;i<l;i++)
			{
				n[i] = old[i];
				if (old[i]==dt)
				{
					exist = true;
					break;
				}
			}
			n[i] = dt;
			if (!exist)
				// if already selected return new array
				return n;
			else
				// else return old
				return old;
		}

		internal void MouseMove (Point mouseLocation)
		{
	
            if (mouseLocation != oldMouseLocation)
            {
                oldMouseLocation = mouseLocation;
                // is mouse pointer inside month region
                if (m_region.IsVisible(mouseLocation))
                {
                    m_calendar.ActiveRegion = mcCalendarRegion.Month;
                    // Check which day has focus
                    for (int i = 0; i < 42; i++)
                    {
                        if (m_days[i].HitTest(mouseLocation))
                        {

                            // Raise DayMouseMove event
                            if (this.DayMouseMove != null)
                                this.DayMouseMove(this, new DayMouseMoveEventArgs(m_days[i].Date.ToShortDateString(),
                                              mouseLocation.X - m_days[i].Rectangle.Left, mouseLocation.Y - m_days[i].Rectangle.Top,
                                              mouseLocation.X, mouseLocation.Y, m_days[i].Rectangle));

                            // check if its a new day
                            if (m_dayInFocus != i)
                            {
                                FocusMoved(i);                            
                            }
                            break;
                        }
                    }
                }
                else
                {
                    RemoveFocus();
                }
            }
			
		}

        internal void FocusMoved(int i)
        {
            bool dayEnabled = true;

            if ( (!m_mouseDown) && (!m_calendar.SelectKeyDown) )  
            {

                dayEnabled = IsDateEnabled(m_days[i].Date);

                if (((m_calendar.SelectTrailingDates) || (SelectedMonth.Month == m_days[i].Date.Month)) && (dayEnabled))
                {
                    if (m_days[i].State != mcDayState.Selected)
                        m_days[i].State = mcDayState.Focus;
                    if ((m_dayInFocus != -1) && (m_days[m_dayInFocus].State != mcDayState.Selected))
                        m_days[m_dayInFocus].State = mcDayState.Normal;

                    // raise events
                    if ((DayLostFocus != null) && (m_dayInFocus != -1))
                        DayLostFocus(this, new DayEventArgs(m_days[m_dayInFocus].Date.ToShortDateString()));
                    if (DayGotFocus != null)
                        DayGotFocus(this, new DayEventArgs(m_days[i].Date.ToShortDateString()));
                }
                else
                {
                    if ((m_dayInFocus != -1) && (m_days[m_dayInFocus].State != mcDayState.Selected))
                        m_days[m_dayInFocus].State = mcDayState.Normal;
                }

                if (m_calendar.ShowFocus)
                    m_calendar.Invalidate(m_rect);
                m_dayInFocus = i;
            }
            else if (m_calendar.SelectionMode >= mcSelectionMode.MultiSimple)
            {
                DayStateChangedEventArgs args;
                if (m_days[i].State == mcDayState.Normal)
                {
                    args = new DayStateChangedEventArgs(m_days[i].Date.ToShortDateString(), mcDayState.Normal, mcDayState.Selected);
                    if (BeforeDaySelected != null)
                        BeforeDaySelected(this, args);
                }
                else
                {
                    if (m_dayInFocus == -1)
                        m_dayInFocus = i;
                    args = new DayStateChangedEventArgs(m_days[m_dayInFocus].Date.ToShortDateString(), mcDayState.Selected, mcDayState.Normal);
                     if (BeforeDayDeselected != null)
                         BeforeDayDeselected(this, args);
                }

                m_dayInFocus = i;

                if (((m_calendar.SelectTrailingDates) || (SelectedMonth.Month == m_days[i].Date.Month)) &&
                    ((m_calendar.MinDate <= m_days[i].Date) && (m_calendar.MaxDate >= m_days[i].Date)) && (dayEnabled) && (!args.Cancel))
                {

                    if (m_newSelection)
                    {
                        NewSelectedDay(i);
                        m_newSelection = false;
                    }
                    else
                    {
                        SelectionArea area = (SelectionArea)m_selArea[m_selArea.Count - 1];
                        area.End = i;
                    }

                    m_selected = true;
                    RemoveSelection(false);
                    // loop through number of selections
                    for (int y = 0; y < m_selArea.Count; y++)
                    {
                        SelectionArea area = (SelectionArea)m_selArea[y];
                        if ((area.Begin != -1) && (area.End != -1))
                            MarkAreaAsSelected(area.Begin, area.End, y);
                    }
                    // Force repaint of calendar
                    m_calendar.Invalidate(m_rect);

                }
            }
            else
            {
                // init dagdrop
                //m_calendar.DoDragDrop(m_days[i].Date.ToString(),DragDropEffects.Copy);   
            }
        }
			
		internal string DateInFocus()
		{
			return m_days[m_dayInFocus].Date.ToShortDateString(); 
		}

		internal int GetDay(Point mouseLocation)
		{
			int day = -1;
			for (int i = 0;i<42;i++)
				if (m_days[i].HitTest(mouseLocation))
					day = i;
			return day;
		}

		internal void RemoveFocus()
		{
			
			if ((DayLostFocus!=null) && (m_dayInFocus!=-1))
				DayLostFocus(this,new DayEventArgs(m_days[m_dayInFocus].Date.ToShortDateString()));	
			
			m_dayInFocus = -1;
			for (int i = 0;i<42;i++)
				if (m_days[i].State != mcDayState.Selected)
					m_days[i].State = mcDayState.Normal; 
		}
		
		private ArrayList DaysInArea(int topLeft,int bottomRight)
		{
			ArrayList days = new ArrayList();
		
			// Get Coordinates for selection rectangle
			m_selRight = System.Math.Max(m_days[bottomRight].Rectangle.Right,m_days[topLeft].Rectangle.Right); 
			m_selLeft = System.Math.Min(m_days[bottomRight].Rectangle.Left,m_days[topLeft].Rectangle.Left);
			m_selTop = System.Math.Min(m_days[bottomRight].Rectangle.Top,m_days[topLeft].Rectangle.Top); 
			m_selBottom = System.Math.Max(m_days[bottomRight].Rectangle.Bottom,m_days[topLeft].Rectangle.Bottom); 	
				
			for (int t = 0;t<42;t++)
			{
				if ((m_days[t].Rectangle.Left >= m_selLeft) &&
					(m_days[t].Rectangle.Right <= m_selRight) &&
					(m_days[t].Rectangle.Top >= m_selTop) &&
					(m_days[t].Rectangle.Bottom <= m_selBottom))
				{
					days.Add(t);
				}
			}  
			return days;
		}

		private void MarkAreaAsSelected(int topLeft,int bottomRight, int area)
		{
				
			ArrayList days;
			int index = 0;
			
			SelectionArea a = (SelectionArea)m_selArea[area];

			a.Begin = topLeft;
			a.End = bottomRight;
			
			days = DaysInArea(topLeft,bottomRight);
			for (int i = 0;i<days.Count;i++)
			{
				index = (int)days[i];
				if ( (m_calendar.SelectTrailingDates) || (SelectedMonth.Month  == m_days[index].Date.Month) &&
					(m_days[index].State != mcDayState.Selected) )
				{
					m_days[index].State = mcDayState.Selected;
					m_days[index].SelectionArea = area; 
				}
			}
			  
		}

		internal void RemoveSelection(bool raiseEvent, int sel)
		{
			string[] days;
			
			// Get selected days
			days = DaysInSelection(sel); 
					
			for (int i = 0;i<42;i++)
			{
				// Reset all days or days within a selection to "Normal"
				if ( (m_days[i].SelectionArea == sel) || (sel == NO_AREA) && (m_days[i].State == mcDayState.Selected)  )
				{
					m_days[i].State = mcDayState.Normal;
					m_days[i].SelectionArea = -1;
				}
			}

			// if a selection is specified , "reset" its start and stop day
			if ((sel!=NO_AREA)) 
			{
				SelectionArea area = (SelectionArea)m_selArea[sel];
				area.Begin = -1;
				area.End = -1;
				// Make sure moving the mouse creates a new selection
				m_newSelection = true;
			}
							
			//raise deselect event
			if (raiseEvent)
			{
				if (days.Length !=0)
				{
					Array.Sort(days);
					if (this.DayDeselected!=null)
						this.DayDeselected(this,new DaySelectedEventArgs(days));
				}
				
				// reset arrays and index
				if (sel==NO_AREA)
					m_selArea.Clear(); 
				
			}
		}
		
		internal void RemoveSelection(bool raiseEvent)
		{
			RemoveSelection(raiseEvent,NO_AREA);	
		}

		internal void Setup()
		{
			int startPos=0;
			DateTime currentDate;
			string[] weekdays;
			string lblDay;
			int i = 0;

			weekdays = m_calendar.Weekdays.GetWeekDays();  
			   
			if (m_calendar.Weekdays.Format == mcDayFormat.Short)   
				lblDay = m_calendar.m_dateTimeFormat.GetAbbreviatedDayName(m_selectedMonth.DayOfWeek);
			else
				lblDay = m_calendar.m_dateTimeFormat.GetDayName(m_selectedMonth.DayOfWeek);
		
			for (i = 0;i<weekdays.Length;i++)
			{
				if (weekdays[i] == lblDay)
					break;
   
			}
			startPos = i;
			if (startPos == 0) startPos = 7;

			currentDate = m_selectedMonth;
			for (i = startPos;i<42;i++)
			{
				m_days[i].Date = currentDate;
				currentDate = currentDate.AddDays(1); 
			}
			currentDate = m_selectedMonth;
			for (i= startPos;i>=0;i--)
			{
				m_days[i].Date = currentDate;
				currentDate = currentDate.AddDays(-1); 
			}
		}

		internal bool IsVisible(Rectangle clip)
		{
			return m_region.IsVisible(clip); 	
		}
		
		internal void Draw(Graphics e)
		{
			
			int today = -1;
			string[] selectedDays;

            Brush bgBrush = new SolidBrush(Colors.BackColor1);    
			Brush selBrush = new SolidBrush(Color.FromArgb(125,Colors.Selected.BackColor));   
			Brush focusBrush = new SolidBrush(Color.FromArgb(125,Colors.Focus.BackColor));
			Pen todayPen = new Pen(Color.FromArgb(150,Calendar.TodayColor),2);
			
			try
			{
                if (BackgroundImage != null)
                    e.DrawImage(BackgroundImage, Rect);
                else
                {
                    if (Colors.GradientMode != mcGradientMode.None)
                        m_calendar.DrawGradient(e, m_rect, Colors.BackColor1, Colors.BackColor2, Colors.GradientMode);
                    else
                        e.FillRectangle(bgBrush, m_rect);
                }
				// Draw days
				
                for (int i = 0;i<42;i++)
				{
					
                    // only draw days that are visible...
                    if ((m_days[i].Rectangle.Height > 0) && (m_days[i].Rectangle.Width > 0))
                    {
                        // Create new graphics object
                        Graphics d = m_calendar.CreateGraphics();
                        // Create bitmap..
                    
                        Bitmap bmp = new Bitmap(m_days[i].Rectangle.Width, m_days[i].Rectangle.Height, d);
                        // link graphics object to bitmap
                        d = Graphics.FromImage(bmp);
                        DayRenderEventArgs args = new DayRenderEventArgs(d, m_days[i].Rectangle, m_days[i].Date, m_days[i].State);
                        DayRender(this, args);
                        if (!args.OwnerDraw)
                        {
                            // day is not user drawn
                            m_days[i].UserDrawn = false;
                            DateItem dayInfo = new DateItem();
                            dayInfo.Calendar = m_calendar;
                            DayQueryInfoEventArgs info = new DayQueryInfoEventArgs(dayInfo, m_days[i].Date, m_days[i].State);
                            DayQueryInfo(this, info);
                            if (!info.OwnerDraw)
                                dayInfo = null;
                            m_days[i].Draw(e, dayInfo);
                            if (dayInfo != null)
                                dayInfo.Dispose();
                        }
                        else
                        {
                            // Draw user rendered day
                            m_days[i].UserDrawn = true;
                            e.DrawImage(bmp, m_days[i].Rectangle);
                        }

                        // Check if day has focus and if focus should be drawn
                        if ((m_days[i].State == mcDayState.Focus) && (m_calendar.ShowFocus))
                        {
                            e.FillRectangle(focusBrush, m_days[i].Rectangle);
                            ControlPaint.DrawBorder(e, m_days[i].Rectangle, Colors.Focus.Border, BorderStyles.Focus);
                        }

                        if ((m_days[i].Date == DateTime.Today) && (!args.OwnerDraw))
                            today = i;

                        d.Dispose();
                        bmp.Dispose();
                    }
				}
			
				// check if date is "today" and if it should be marked..
				if ( (m_calendar.ShowToday) && (today !=-1) && 
					((m_calendar.ShowTrailingDates) || (m_days[today].Date.Month == m_calendar.ActiveMonth.Month)) )  
				{

                    RectangleF dateRect = m_days[today].DateRegion[0].GetBounds(e);

                    dateRect.Inflate(5, 5);
                    e.DrawEllipse(todayPen, dateRect); 
	  
                    
                }

				// Check if a selection exist
			
				selectedDays = DaysInSelection(NO_AREA);
				if (selectedDays.Length>0)
				{
					// Check how many selection areas there are
					if (m_selArea.Count<=1) 
					{
						for (int i = 0;i<m_selArea.Count;i++)
						{
							SelectionArea area = (SelectionArea)m_selArea[i];
							if ((area.Begin!=-1) && (area.End !=-1))
							{
								// Get Coordinates for selection rectangle
						
								m_selRight = System.Math.Max(m_days[area.End].Rectangle.Right,m_days[area.Begin].Rectangle.Right); 
								m_selLeft = System.Math.Min(m_days[area.End].Rectangle.Left,m_days[area.Begin].Rectangle.Left);
								m_selTop = System.Math.Min(m_days[area.End].Rectangle.Top,m_days[area.Begin].Rectangle.Top); 
								m_selBottom = System.Math.Max(m_days[area.End].Rectangle.Bottom,m_days[area.Begin].Rectangle.Bottom); 	
				
								// Draw selection
								Rectangle selRect = new Rectangle(m_selLeft,m_selTop,m_selRight-m_selLeft,m_selBottom-m_selTop);
								e.FillRectangle(selBrush,selRect); 
								ControlPaint.DrawBorder(e,selRect,Colors.Selected.Border,BorderStyles.Selected);  	
							}
						
						}
					}
						// Multiple selection areas, we dont use border so we 
						// draw each day individually to not overlap regions
					else
					{
						for (int i =0;i<42;i++)
						{
							if ((m_days[i].State==mcDayState.Selected) && (m_days[i].SelectionArea!=-1))
							{
								e.FillRectangle(selBrush,m_days[i].Rectangle);
							}
						}
					}
			
				}
			}
			catch (Exception)
			{

			}
		
			bgBrush.Dispose();
			selBrush.Dispose();
			todayPen.Dispose();
			focusBrush.Dispose();
		}

		internal void SetupDays()
		{
			int row = 0;
			int col = 0;
			int index;
			
			Rectangle dayRect = new Rectangle(); 
			
			m_dayHeight = (float)((m_rect.Height - (m_padding.Vertical*7))  / 6);
			m_dayWidth =  (float)((m_rect.Width - (m_padding.Horizontal*8)) / 7);
			
			// setup rectangles for days
			row = 0;
			index = 0;
						
			for (int i = 0;i<6;i++)  // rows
			{
				col = 0;
				for (int j = 0;j<7;j++)  // colums
				{
					dayRect.X = (int)(m_dayWidth * col)+(col+1)*m_padding.Horizontal+ m_rect.Left;
					dayRect.Y = (int)(m_dayHeight *row)+(row+1)*m_padding.Vertical + m_rect.Top;
					if (j ==6)
						dayRect.Width = m_rect.Width - (int)(m_padding.Horizontal*8) - (int)(m_dayWidth*6)-1;
					else
						dayRect.Width = (int)m_dayWidth;
					if ( i==5)
						dayRect.Height = m_rect.Height - (int)(m_padding.Vertical*7) - (int)(m_dayHeight*5)-1;
					else
						dayRect.Height = (int)m_dayHeight;
									
					m_days[index].Rectangle = dayRect;
					index++;
					col++;
				}
				row++;
			}
		}

		#endregion

		#region  MonthColors

		[TypeConverter(typeof(ColorsTypeConverter))]
		public class MonthColors
		{
			private Color m_backColor1;
            private Color m_backColor2;
            private mcGradientMode m_gradientMode;
			            
            private TrailingColors m_trailingColors;
            private WeekendColors m_weekendColors;
            private DisabledColors m_disabledColors;
            private SelectedColors m_selectedColors;
            private FocusColors m_focusColors;
            private DayColors m_dayColors;

			internal Month m_month;
			
			public MonthColors(Month month)
			{
				m_month = month;
                m_trailingColors = new TrailingColors(this);
                m_weekendColors = new WeekendColors(this);
                m_disabledColors = new DisabledColors(this);
                m_selectedColors = new SelectedColors(this);
                m_focusColors = new FocusColors(this);
                m_dayColors = new DayColors(this);


				// Default values
				                
                m_backColor1 = Color.White;
                m_backColor2 = Color.White;
                m_gradientMode = mcGradientMode.None;
        		                
			}
				

            [Description("Trailing Colors.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public TrailingColors Trailing
            {
                get
                {
                    return m_trailingColors;
                }
            }

            [Description("Trailing Colors.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public DayColors Days
            {
                get
                {
                    return m_dayColors;
                }
            }

            [Description("Weekend Colors.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public WeekendColors Weekend
            {
                get
                {
                    return m_weekendColors;
                }
            }

            [Description("Disabled Colors.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public DisabledColors Disabled
            {
                get
                {
                    return m_disabledColors;
                }
            }

            [Description("Selected Colors.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public SelectedColors Selected
            {
                get
                {
                    return m_selectedColors;
                }
            }

            [Description("Focus Colors.")]
            [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
            public FocusColors Focus
            {
                get
                {
                    return m_focusColors;
                }
            }
                        

			[Description("Background color when day is not selected or has focus.")]
			[DefaultValue(typeof(Color),"White")]
			public Color BackColor1
			{
				get
				{
					return m_backColor1;
				}
				set
				{
                    if (m_backColor1 != value)
					{
                        m_backColor1 = value;
						if (m_month.ColorChanged!=null)
							m_month.ColorChanged(this,new MonthColorEventArgs(mcMonthColor.BackColor1));  
						m_month.m_calendar.Invalidate();
					}
				}
			}

            [Description("Background color when day is not selected or has focus.")]
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
                        if (m_month.ColorChanged != null)
                            m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.BackColor2));
                        m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Type of gradient for month.")]
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
                        if (m_month.ColorChanged != null)
                            m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.Gradient));
                        m_month.m_calendar.Invalidate();
                    }
                }
            }
					
		}
		
		#endregion

        #region DisabledColors

        [TypeConverter(typeof(ColorsTypeConverter))]
        public class DisabledColors
        {
            private Color m_backColor1;
            private Color m_backColor2;
            private Color m_textColor;
            private Color m_dateColor;
            private mcGradientMode m_gradientMode;
            private MonthColors m_parent;

            public DisabledColors(MonthColors parent)
            {
                m_parent = parent;
                m_textColor = Color.LightGray;
                m_dateColor = Color.LightGray;
                m_backColor1 = Color.FromArgb(233, 233, 233);
                m_backColor2 = Color.White;
                m_gradientMode = mcGradientMode.None;
            }


            #region Properties


            [Description("Text Color for disabled days.")]
            [DefaultValue(typeof(Color), "LightGray")]
            public Color Text
            {
                get
                {
                    return m_textColor;
                }
                set
                {
                    if (m_textColor != value)
                    {
                        m_textColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DisabledText));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Date color for disabled days.")]
            [DefaultValue(typeof(Color), "LightGray")]
            public Color Date
            {
                get
                {
                    return m_dateColor;
                }
                set
                {
                    if (m_dateColor != value)
                    {
                        m_dateColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DisabledDate));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Backgrund color for disabled days.")]
            [DefaultValue(typeof(Color), "233,233,233")]
            public Color BackColor1
            {
                get
                {
                    return m_backColor1;
                }
                set
                {
                    if (m_backColor1 != value)
                    {
                        m_backColor1 = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DisabledBackColor1));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Second background color for disabled days when using gradient.")]
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
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DisabledBackColor2));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Type of gradient used for disabled days.")]
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
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DisabledGradient));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            #endregion

        }


        #endregion

        #region WeekendColors

        [TypeConverter(typeof(ColorsTypeConverter))]
        public class WeekendColors
        {
            private Color m_backColor1;
            private Color m_backColor2;
            private Color m_textColor;
            private Color m_dateColor;
            private mcGradientMode m_gradientMode;
            private MonthColors m_parent;

            private bool m_sunday;
            private bool m_saturday;

            public WeekendColors(MonthColors parent)
            {
                m_parent = parent;
                m_textColor = Color.Black;
                m_dateColor = Color.Black;
                m_backColor1 = Color.White;
                m_backColor2 = Color.White;
                m_gradientMode = mcGradientMode.None;
                m_saturday = true;
                m_sunday = true;
            }


            #region Properties


            [Description("Text color for weekends.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Text
            {
                get
                {
                    return m_textColor;
                }
                set
                {
                    if (m_textColor != value)
                    {
                        m_textColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.WeekendText));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Date color for weekends.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Date
            {
                get
                {
                    return m_dateColor;
                }
                set
                {
                    if (m_dateColor != value)
                    {
                        m_dateColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.WeekendDate));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Backgrund color for weekends.")]
            [DefaultValue(typeof(Color), "White")]
            public Color BackColor1
            {
                get
                {
                    return m_backColor1;
                }
                set
                {
                    if (m_backColor1 != value)
                    {
                        m_backColor1 = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.WeekendBackColor1));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Second background color for weeekends when using gradient.")]
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
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.WeekendBackColor2 ));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Type of gradient used for weekends.")]
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
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.WeekendGradient));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Indicates wether the weekend colors are applied to saturdays.")]
            [DefaultValue(typeof(bool), "True")]
            public bool Saturday
            {
                get
                {
                    return m_saturday;
                }
                set
                {
                    if (m_saturday != value)
                    {
                        m_saturday = value;
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Indicates wether the weekend colors are applied to sundays.")]
            [DefaultValue(typeof(bool), "True")]
            public bool Sunday
            {
                get
                {
                    return m_sunday;
                }
                set
                {
                    if (m_sunday != value)
                    {
                        m_sunday = value;
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            #endregion

        }
        

        #endregion

        #region TrailingColors

        [TypeConverter(typeof(ColorsTypeConverter))]
        public class TrailingColors
        {
            private Color m_backColor1;
            private Color m_backColor2;
            private Color m_textColor;
            private Color m_dateColor;
            private mcGradientMode m_gradientMode;
            private MonthColors m_parent;

            public TrailingColors(MonthColors parent)
            {
                m_parent = parent;
                m_textColor = Color.LightGray;
                m_dateColor = Color.LightGray;
                m_backColor1 = Color.White;
                m_backColor2 = Color.White;
                m_gradientMode = mcGradientMode.None;
            }


            #region Properties


            [Description("Text color for trailing days.")]
            [DefaultValue(typeof(Color), "LightGray")]
            public Color Text
            {
                get
                {
                    return m_textColor;
                }
                set
                {
                    if (m_textColor != value)
                    {
                        m_textColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.TrailingText));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Date color for for trailing days.")]
            [DefaultValue(typeof(Color), "LightGray")]
            public Color Date
            {
                get
                {
                    return m_dateColor;
                }
                set
                {
                    if (m_dateColor != value)
                    {
                        m_dateColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.TrailingDate));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Backgrund color for trailing days.")]
            [DefaultValue(typeof(Color), "White")]
            public Color BackColor1
            {
                get
                {
                    return m_backColor1;
                }
                set
                {
                    if (m_backColor1 != value)
                    {
                        m_backColor1 = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.TrailingBackColor1));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Second background color for trailing days when using gradient.")]
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
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.TrailingBackColor2));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Type of gradient used for trailing days.")]
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
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.TrailingGradient));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }



            #endregion


        }


        #endregion

        #region SelectedColors

        [TypeConverter(typeof(ColorsTypeConverter))]
        public class SelectedColors
        {
            private Color m_backColor;
            private Color m_textColor;
            private Color m_dateColor;
            private Color m_borderColor;

            private MonthColors m_parent;

            public SelectedColors(MonthColors parent)
            {
                m_parent = parent;
                m_textColor = Color.Black;
                m_dateColor = Color.Black;
                m_backColor = Color.FromArgb(193, 210, 238);
                m_borderColor = Color.FromArgb(49, 106, 197);
             
            
            }


            #region Properties


            [Description("Text color for selected days.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Text
            {
                get
                {
                    return m_textColor;
                }
                set
                {
                    if (m_textColor != value)
                    {
                        m_textColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.SelectedText));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Date color for selected days.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Date
            {
                get
                {
                    return m_dateColor;
                }
                set
                {
                    if (m_dateColor != value)
                    {
                        m_dateColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.SelectedDate));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Border color for selected days.")]
            [DefaultValue(typeof(Color), "49, 106, 197")]
            public Color Border
            {
                get
                {
                    return m_borderColor;
                }
                set
                {
                    if (m_borderColor != value)
                    {
                        m_borderColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.SelectedBorder));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Backgrund color for selected days.")]
            [DefaultValue(typeof(Color), "193, 210, 238")]
            public Color BackColor
            {
                get
                {
                    return m_backColor;
                }
                set
                {
                    if (m_backColor != value)
                    {
                        m_backColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.SelectedBackColor));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            
            #endregion

        }


        #endregion

        #region FocusColors

        [TypeConverter(typeof(ColorsTypeConverter))]
        public class FocusColors
        {
            private Color m_backColor;
            private Color m_textColor;
            private Color m_dateColor;
            private Color m_borderColor;

            private MonthColors m_parent;

            public FocusColors(MonthColors parent)
            {
                m_parent = parent;
                m_textColor = Color.Black;
                m_dateColor = Color.Black;
                m_backColor = Color.FromArgb(224, 232, 246);
                m_borderColor = Color.FromArgb(152, 180, 226);
				
            }


            #region Properties


            [Description("Text color used for days with focus.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Text
            {
                get
                {
                    return m_textColor;
                }
                set
                {
                    if (m_textColor != value)
                    {
                        m_textColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.FocusText));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Date color for days with focus.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Date
            {
                get
                {
                    return m_dateColor;
                }
                set
                {
                    if (m_dateColor != value)
                    {
                        m_dateColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.FocusDate));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Border color for days with focus.")]
            [DefaultValue(typeof(Color), "152, 180, 226")]
            public Color Border
            {
                get
                {
                    return m_borderColor;
                }
                set
                {
                    if (m_borderColor != value)
                    {
                        m_borderColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.FocusBorder));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Backgrund color for days with focus.")]
            [DefaultValue(typeof(Color), "224, 232, 246")]
            public Color BackColor
            {
                get
                {
                    return m_backColor;
                }
                set
                {
                    if (m_backColor != value)
                    {
                        m_backColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.FocusBackColor));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }


            #endregion

        }


        #endregion

        #region DayColors

        [TypeConverter(typeof(ColorsTypeConverter))]
        public class DayColors
        {
            private Color m_backColor1;
            private Color m_backColor2;
            private Color m_textColor;
            private Color m_dateColor;
            private Color m_borderColor;
            private mcGradientMode m_gradientMode;
            private MonthColors m_parent;

            public DayColors(MonthColors parent)
            {
                m_parent = parent;
                m_textColor = Color.Black;
                m_dateColor = Color.Black;
                m_backColor1 = Color.White;
                m_backColor2 = Color.White;
                m_borderColor = Color.Black;
                m_gradientMode = mcGradientMode.None;
            }


            #region Properties


            [Description("Text color for regular days.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Text
            {
                get
                {
                    return m_textColor;
                }
                set
                {
                    if (m_textColor != value)
                    {
                        m_textColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DayText));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Date color for regular days.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Date
            {
                get
                {
                    return m_dateColor;
                }
                set
                {
                    if (m_dateColor != value)
                    {
                        m_dateColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DayDate));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Border color for days.")]
            [DefaultValue(typeof(Color), "Black")]
            public Color Border
            {
                get
                {
                    return m_borderColor;
                }
                set
                {
                    if (m_borderColor != value)
                    {
                        m_borderColor = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DayBorder));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Backgrund color for regular days..")]
            [DefaultValue(typeof(Color), "White")]
            public Color BackColor1
            {
                get
                {
                    return m_backColor1;
                }
                set
                {
                    if (m_backColor1 != value)
                    {
                        m_backColor1 = value;
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DayBackColor1));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Second background color for regular days. when using gradient.")]
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
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DayBackColor2));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            [Description("Type of gradient used for regular days..")]
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
                        if (m_parent.m_month.ColorChanged != null)
                            m_parent.m_month.ColorChanged(this, new MonthColorEventArgs(mcMonthColor.DayGradient));
                        m_parent.m_month.m_calendar.Invalidate();
                    }
                }
            }

            #endregion

        }


        #endregion



        #region  MonthBorderStyles


        [TypeConverter(typeof(BorderStylesTypeConverter))]
		public class MonthBorderStyles
		{
			private Month m_month;
			
			private ButtonBorderStyle m_borderStyle;
			private ButtonBorderStyle m_focusBorderStyle;
			private ButtonBorderStyle m_selectedBorderStyle;

			public MonthBorderStyles(Month month)
			{
				m_month = month;
				m_borderStyle = ButtonBorderStyle.None;
				m_focusBorderStyle = ButtonBorderStyle.Solid;
				m_selectedBorderStyle = ButtonBorderStyle.Solid;
			}
				
			[Description("Border style when item has no focus.")]
			[DefaultValue(typeof(ButtonBorderStyle),"None")]
			public ButtonBorderStyle Normal
			{
				get
				{
					return m_borderStyle;
				}
				set
				{
					if (m_borderStyle!=value)
					{
						m_borderStyle = value;
						if (m_month.BorderStyleChanged!=null)
							m_month.BorderStyleChanged(this,new MonthBorderStyleEventArgs(mcMonthBorderStyle.Normal));  
						m_month.m_calendar.Invalidate();
					}
				}
				
			}
			
			[Description("Border style when item has focus.")]
			[DefaultValue(typeof(ButtonBorderStyle),"Solid")]
			public ButtonBorderStyle Focus
			{
				get
				{
					return m_focusBorderStyle;
				}
				set
				{
					if (m_focusBorderStyle!=value)
					{
						m_focusBorderStyle = value;
						if (m_month.BorderStyleChanged!=null)
							m_month.BorderStyleChanged(this,new MonthBorderStyleEventArgs(mcMonthBorderStyle.Focus));  
						m_month.m_calendar.Invalidate();
					}
				}
				
			}
			
			[Description("Border style when item is selected.")]
			[DefaultValue(typeof(ButtonBorderStyle),"Solid")]
			public ButtonBorderStyle Selected
			{
				get
				{
					return m_selectedBorderStyle;
				}
				set
				{
					if (m_selectedBorderStyle!=value)
					{
						m_selectedBorderStyle = value;
						if (m_month.BorderStyleChanged!=null)
							m_month.BorderStyleChanged(this,new MonthBorderStyleEventArgs(mcMonthBorderStyle.Selected));  
						m_month.m_calendar.Invalidate(); 
					}
				}
	
			}
		
		}
		

		#endregion

		#region  MonthPadding

		[TypeConverter(typeof(MonthPaddingTypeConverter))]		
		public class MonthPadding
		{
			private Month m_month;
			private int m_horizontal;
			private int m_vertical;
			
			public MonthPadding(Month month)
			{
				// set the control to which the collection belong
				m_month = month;
				// Default values
				m_horizontal = 2;
				m_vertical = 2;
			}
			
			[RefreshProperties(System.ComponentModel.RefreshProperties.All)]
			[Description("Horizontal padding.")]
			[DefaultValue(2)]
			public int Horizontal
			{
				get
				{
					return m_horizontal;
				}
				set
				{
					if (m_horizontal!=value)
					{
						m_horizontal = value;
						if (m_month!=null)
						{
							// padding has changed , force DoLayout
							m_month.SetupDays();
							m_month.Calendar.Invalidate();  
							if (m_month.PropertyChanged!=null)
								m_month.PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.Padding));  
				
						}
					}
				}
			}
			
			[RefreshProperties(System.ComponentModel.RefreshProperties.All)]
			[Description("Vertical padding.")]
			[DefaultValue(2)]
			public int Vertical
			{
				get
				{	
					return m_vertical;
				}
				set
				{
					if (m_vertical!=value)
					{
						m_vertical = value;
						if (m_month!=null)
						{						
							m_month.SetupDays();
							m_month.Calendar.Invalidate();  
							if (m_month.PropertyChanged!=null)
								m_month.PropertyChanged(this,new MonthPropertyEventArgs(mcMonthProperty.Padding));  
						}
					}
				}
			}

		}
		
		#endregion


        #region  TransparencyCollection

        [TypeConverter(typeof(TransparencyTypeConverter))]
        public class TransparencyCollection
        {
            private Month m_month;
            private int m_background;
            private int m_text;

            public TransparencyCollection(Month month)
            {
                // set the control to which the collection belong
                m_month = month;
                // Default values
                m_background = 175;
                m_text = 175;
            }

            [RefreshProperties(System.ComponentModel.RefreshProperties.All)]
            [Description("Transparency used for background.")]
            [DefaultValue(175)]
            public int Background
            {
                get
                {
                    return m_background;
                }
                set
                {
                    if (m_background != value)
                    {
                        m_background = value;
                        if (m_month != null)
                        {
                            // padding has changed , force DoLayout
                            m_month.SetupDays();
                            m_month.Calendar.Invalidate();
                            if (m_month.PropertyChanged != null)
                                m_month.PropertyChanged(this, new MonthPropertyEventArgs(mcMonthProperty.Transparency));

                        }
                    }
                }
            }

            [RefreshProperties(System.ComponentModel.RefreshProperties.All)]
            [Description("Transparency used for text.")]
            [DefaultValue(175)]
            public int Text
            {
                get
                {
                    return m_text;
                }
                set
                {
                    if (m_text != value)
                    {
                        m_text = value;
                        if (m_month != null)
                        {
                            m_month.SetupDays();
                            m_month.Calendar.Invalidate();
                            if (m_month.PropertyChanged != null)
                                m_month.PropertyChanged(this, new MonthPropertyEventArgs(mcMonthProperty.Transparency));
                        }
                    }
                }
            }

        }

        #endregion
		
		#region MonthPaddingTypeConverter

		public class MonthPaddingTypeConverter : ExpandableObjectConverter
		{
			        	
			public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
			{
				if(sourceType == typeof(string))
					return true;
				return base.CanConvertFrom (context, sourceType);
			}

			public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
			{
				if(destinationType == typeof(string))
					return true;
				return base.CanConvertTo (context, destinationType);
			}

			public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
			{
				
				if(value.GetType() == typeof(string))
				{
					// Parse property string
					string[] ss = value.ToString().Split(new char[] {';'}, 2);
					if (ss.Length==2)
					{
						// Create new PaddingCollection
						MonthPadding item = new MonthPadding((Month)context.Instance); 
						// Set properties
						item.Horizontal = int.Parse(ss[0]);
						item.Vertical = int.Parse(ss[1]); 
						return item;				
					}
				}
				return base.ConvertFrom (context, culture, value);
			}

			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
			{
									
				MonthPadding dest = value as Month.MonthPadding;
				if (destinationType == typeof(string) && (dest !=null) )
				{
					// create property string
					return dest.Horizontal.ToString()+"; "+dest.Vertical.ToString();
				}
				return base.ConvertTo (context, culture, value, destinationType);
			}

		}


		#endregion

        #region TransparencyTypeConverter

        public class TransparencyTypeConverter : ExpandableObjectConverter
        {

            public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            {
                if (sourceType == typeof(string))
                    return true;
                return base.CanConvertFrom(context, sourceType);
            }

            public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
            {
                if (destinationType == typeof(string))
                    return true;
                return base.CanConvertTo(context, destinationType);
            }

            public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
            {

                if (value.GetType() == typeof(string))
                {
                    // Parse property string
                    string[] ss = value.ToString().Split(new char[] { ';' }, 2);
                    if (ss.Length == 2)
                    {
                        // Create new PaddingCollection
                        TransparencyCollection item = new TransparencyCollection((Month)context.Instance);
                        // Set properties
                        item.Background = int.Parse(ss[0]);
                        item.Text = int.Parse(ss[1]);
                                                
                        if (item.Text > 255)
                            item.Text = 255;
                        if (item.Text < 0)
                            item.Text = 0;
                        if (item.Background > 255)
                            item.Background = 255;
                        if (item.Background < 0)
                            item.Background = 0;

                        return item;
                    }
                }
                return base.ConvertFrom(context, culture, value);
            }

            public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
            {

                TransparencyCollection dest = value as Month.TransparencyCollection;
                if (destinationType == typeof(string) && (dest != null))
                {
                    // create property string
                    if (dest.Text > 255)
                        dest.Text = 255;
                    if (dest.Text < 0)
                        dest.Text = 0;
                    if (dest.Background > 255)
                        dest.Background = 255;
                    if (dest.Background < 0)
                        dest.Background = 0;

                    return dest.Background.ToString() + "; " + dest.Text.ToString();
                }
                return base.ConvertTo(context, culture, value, destinationType);
            }

        }


        #endregion
		
		
		#region ColorsTypeConverter
	
		public class ColorsTypeConverter : ExpandableObjectConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
			{
				return ""; 
			}

		}

		#endregion
               

        #region BorderStylesTypeConverter

        public class BorderStylesTypeConverter : ExpandableObjectConverter
		{
			public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
			{
				return ""; 
			}

		}

		#endregion
	}

	#region MonthPropertyEventArgs
	
	public class MonthPropertyEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcMonthProperty m_property;

		#endregion

		#region Constructor

		public MonthPropertyEventArgs()
		{
			m_property = 0;
		}

		public MonthPropertyEventArgs(mcMonthProperty property)
		{
			this.m_property = property;
		}

		#endregion


		#region Properties

		public mcMonthProperty Property
		{
			get
			{
				return this.m_property;
			}
		}

		#endregion
	}


	#endregion
	
	#region MonthColorEventArgs
	
	public class MonthColorEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcMonthColor m_color;

		#endregion

		#region Constructor

		public MonthColorEventArgs()
		{
			m_color = 0;
		}

		public MonthColorEventArgs(mcMonthColor color)
		{
			this.m_color = color;
		}

		#endregion


		#region Properties

		public mcMonthColor Color
		{
			get
			{
				return this.m_color;
			}
		}

		#endregion
	}


	#endregion
	
	#region MonthBorderStyleEventArgs
	
	public class MonthBorderStyleEventArgs : EventArgs
	{
		#region Class Data

		/// <summary>
		/// The property that has changed
		/// </summary>
		private mcMonthBorderStyle m_borderStyle;

		#endregion

		#region Constructor

		public MonthBorderStyleEventArgs()
		{
			m_borderStyle = 0;
		}

		public MonthBorderStyleEventArgs(mcMonthBorderStyle style)
		{
			this.m_borderStyle = style;
		}

		#endregion


		#region Properties

		public mcMonthBorderStyle BorderStyle
		{
			get
			{
				return this.m_borderStyle;
			}
		}

		#endregion
	}


	#endregion


}
