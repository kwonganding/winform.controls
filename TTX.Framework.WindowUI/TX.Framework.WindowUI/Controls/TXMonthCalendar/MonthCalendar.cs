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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Runtime.Serialization;
using System.Globalization;
using System.Threading;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.VisualStyles;
using System.Resources;
using System.Text;
using TX.Framework.WindowUI.Controls.TXMonthCalendar;

namespace TX.Framework.WindowUI.Controls
{

    public enum mcClickMode { Single = 0, Double }
    public enum mcSelectionMode { None = 0, One, MultiSimple, MultiExtended }
    public enum mcExtendedSelectionKey { None = 0, Ctrl, Shift, Alt }
    internal enum mcCalendarRegion { None = 0, Day, Header, Footer, Weekdays, Weeknumbers, Month }
    public enum mcKeyboard { Up = 0, Down, Left, Right, Select, NextMonth, PreviousMonth, NextYear, PreviousYear }
    public enum mcCalendarColor { Border = 0, Today }


    #region Delegates

    public delegate void ClickEventHandler(object sender, ClickEventArgs e);
    public delegate void MonthChangedEventHandler(object sender, MonthChangedEventArgs e);
    public delegate void KeyboardChangedEventHandler(object sender, KeyboardChangedEventArgs e);
    public delegate void BeforeMonthChangedEventHandler(object sender, BeforeMonthChangedEventArgs e);
    public delegate void CalendarColorEventHandler(object sender, CalendarColorEventArgs e);
    public delegate void DayRenderEventHandler(object sender, DayRenderEventArgs e);
    public delegate void DayQueryInfoEventHandler(object sender, DayQueryInfoEventArgs e);

    public delegate int WeekCallBack(DateTime time);

    #endregion

    [DesignerAttribute(typeof(MonthCalendarDesigner))]
    [DefaultProperty("Name")]
    [DefaultEvent("MonthChanged")]
    [ToolboxItem(true)]
    public class MonthCalendar : System.Windows.Forms.Control
    {
        #region Class members

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        private Color m_borderColor;
        private bool m_ctrlKey;
        private GlobalHook m_hook = new GlobalHook();
        private bool m_keyHandled;
        private bool m_selectKeyDown;
        private bool m_keyboardEnabled;

        private mcExtendedSelectionKey m_extendedKey;

        private Weekday m_weekday;
        private Month m_month;
        private Footer m_footer;
        private Weeknumber m_weeknumber;
        private int m_firstDayOfWeek;
        private DayOfWeek m_defaultFirstDayOfWeek;

        private mcCalendarRegion m_activeRegion;
        private Header m_header;
        private KeyboardConfig m_keyboard;
        private bool m_showToday;
        private bool m_showTrailing;
        private bool m_useTheme;
        private IntPtr m_theme;
        private bool m_selectTrailing;
        private mcSelectionMode m_selectionMode;
        private CultureInfo[] m_installedCultures;
        private CultureInfo m_culture;

        private ImageList m_imageList;
        private PrintDocument m_printDoc = new PrintDocument();

        internal DateTimeFormatInfo m_dateTimeFormat = new DateTimeFormatInfo();

        private Rectangle m_weekdaysRect = new Rectangle();
        private Rectangle m_monthRect = new Rectangle();
        private Rectangle m_footerRect = new Rectangle();
        private Rectangle m_headerRect = new Rectangle();
        private Rectangle m_weeknumbersRect = new Rectangle();
        private DateItemCollection m_dateItemCollection;
        private SelectedDatesCollection m_selectedDates;

        private ButtonBorderStyle m_borderStyle;
        private bool m_showFocus;
        private MouseButtons m_selectButton;

        private DateTime m_minDate;
        private DateTime m_maxDate;

        private Color m_todayColor;

        private bool m_showFooter;
        private bool m_showWeekday;
        private bool m_showHeader;
        private bool m_showWeeknumber;

        private ActiveMonth m_activeMonth;

        [field: NonSerialized]
        public WeekCallBack WeeknumberCallBack;

        private Point m_mouseLocation;
        private MouseButtons m_mouseButton;

        #endregion

        #region EventHandler

        #region Events


        [Browsable(true)]
        [Description("Indicates that a day is about to be drawn.")]
        [Category("Calender")]
        public event DayRenderEventHandler DayRender;
        [Browsable(true)]
        [Description("Indicates that a day is about to be drawn.")]
        [Category("Calender")]
        public event DayQueryInfoEventHandler DayQueryInfo;
        [Browsable(true)]
        [Description("Indicates that something was dropped on a day.")]
        [Category("Calender")]
        public event DayDragDropEventHandler DayDragDrop;
        [Browsable(true)]
        [Description("Indicates the month has changed.")]
        [Category("Calender")]
        public event MonthChangedEventHandler MonthChanged;
        [Browsable(true)]
        [Description("Indicates the month is about to change.")]
        [Category("Calender")]
        public event BeforeMonthChangedEventHandler BeforeMonthChanged;
        [Browsable(true)]
        [Description("Indicates that an image has been clicked.")]
        [Category("Calender")]
        public event DayClickEventHandler ImageClick;
        [Browsable(true)]
        [Description("Indicates that the mouse was moved inside a day.")]
        [Category("Calender")]
        public event DayMouseMoveEventHandler DayMouseMove;
        [Browsable(true)]
        [Description("Indicates that a day has been clicked.")]
        [Category("Calender")]
        public event DayClickEventHandler DayClick;
        [Browsable(true)]
        [Description("Indicates that a day has been double clicked.")]
        [Category("Calender")]
        public event DayClickEventHandler DayDoubleClick;
        [Browsable(true)]
        [Description("Indicates that the header has been clicked.")]
        [Category("Calender")]
        public event ClickEventHandler HeaderClick;
        [Browsable(true)]
        [Description("Indicates that the header has been double clicked.")]
        [Category("Calender")]
        public event ClickEventHandler HeaderDoubleClick;
        [Browsable(true)]
        [Description("Indicates that the footer has been clicked.")]
        [Category("Calender")]
        public event ClickEventHandler FooterClick;
        [Browsable(true)]
        [Description("Indicates that the footer has been double clicked.")]
        [Category("Calender")]
        public event ClickEventHandler FooterDoubleClick;
        [Browsable(true)]
        [Description("Indicates that one or more days have been selected.")]
        [Category("Calender")]
        public event DaySelectedEventHandler DaySelected;
        [Browsable(true)]
        [Description("Indicates that one or more days have been deselected.")]
        [Category("Calender")]
        public event DaySelectedEventHandler DayDeselected;
        [Browsable(true)]
        [Description("Trigged before a day becomes selected.")]
        [Category("Calender")]
        public event DayStateChangedEventHandler BeforeDaySelected;
        [Browsable(true)]
        [Description("Trigged before a day becomes deselected.")]
        [Category("Calender")]
        public event DayStateChangedEventHandler BeforeDayDeselected;
        [Browsable(true)]
        [Description("Indicates that a day has lost focus.")]
        [Category("Calender")]
        public event DayEventHandler DayLostFocus;
        [Browsable(true)]
        [Description("Indicates that a day has got focus.")]
        [Category("Calender")]
        public event DayEventHandler DayGotFocus;
        [Browsable(true)]
        [Description("Indicates that a week number has been clicked.")]
        [Category("Calender")]
        public event WeeknumberClickEventHandler WeeknumberClick;
        [Browsable(true)]
        [Description("Indicates that a week number has been double clicked.")]
        [Category("Calender")]
        public event WeeknumberClickEventHandler WeeknumberDoubleClick;
        [Browsable(true)]
        [Description("Indicates that a weekday has been clicked.")]
        [Category("Calender")]
        public event WeekdayClickEventHandler WeekdayClick;
        [Browsable(true)]
        [Description("Indicates that a weekday has been double clicked.")]
        [Category("Calender")]
        public event WeekdayClickEventHandler WeekdayDoubleClick;
        [Browsable(true)]
        [Description("Indicates that the footer received focus.")]
        [Category("Calender")]
        public event EventHandler FooterMouseEnter;
        [Browsable(true)]
        [Description("Indicates that the footer lost focus.")]
        [Category("Calender")]
        public event EventHandler FooterMouseLeave;
        [Browsable(true)]
        [Description("Indicates that the header received focus.")]
        [Category("Calender")]
        public event EventHandler HeaderMouseEnter;
        [Browsable(true)]
        [Description("Indicates that the lost received focus.")]
        [Category("Calender")]
        public event EventHandler HeaderMouseLeave;
        [Browsable(true)]
        [Description("Indicates that weekdays received focus.")]
        [Category("Calender")]
        public event EventHandler WeekdaysMouseEnter;
        [Browsable(true)]
        [Description("Indicates that weekdays lost focus.")]
        [Category("Calender")]
        public event EventHandler WeekdaysMouseLeave;
        [Browsable(true)]
        [Description("Indicates that weeknumbers received focus.")]
        [Category("Calender")]
        public event EventHandler WeeknumbersMouseEnter;
        [Browsable(true)]
        [Description("Indicates that weeknumbers lost focus.")]
        [Category("Calender")]
        public event EventHandler WeeknumbersMouseLeave;

        #endregion

        #region PropertyChanged


        [Browsable(true)]
        [Category("PropertyChanged")]
        [Description("Indicates that the SelectionMode setting was changed.")]
        public event EventHandler SelectionModeChanged;
        [Browsable(true)]
        [Category("PropertyChanged")]
        [Description("Indicates that ExtendedSelectionKey was changed.")]
        public event EventHandler ExtendedSelectionKeyChanged;
        [Browsable(true)]
        [Category("PropertyChanged")]
        [Description("Indicates that the Theme setting was changed.")]
        public event EventHandler UseThemeChanged;
        [Browsable(true)]
        [Description("Indicates that the culture has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler CultureChanged;
        [Browsable(true)]
        [Description("Indicates that ShowFocus has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ShowFocusChanged;
        [Browsable(true)]
        [Description("Indicates that ShowTrailing has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ShowTrailingChanged;
        [Browsable(true)]
        [Description("Indicates that SelectTrailing has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler SelectTrailingChanged;
        [Browsable(true)]
        [Description("Indicates that a calendar color has changed.")]
        [Category("PropertyChanged")]
        public event CalendarColorEventHandler CalendarColorChanged;
        [Browsable(true)]
        [Description("Indicates that ShowToday has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ShowTodayChanged;
        [Browsable(true)]
        [Description("Indicates that the ImageList has been changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ImageListChanged;
        [Browsable(true)]
        [Description("Indicates that the SelectButton has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler SelectButtonChanged;
        [Browsable(true)]
        [Description("Indicates that the MinDate has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler MinDateChanged;
        [Browsable(true)]
        [Description("Indicates that the maxdate has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler MaxDateChanged;
        [Browsable(true)]
        [Description("Indicates that ShowWeeknumber has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ShowWeeknumberChanged;
        [Browsable(true)]
        [Description("Indicates that the border style has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler BorderStyleChanged;
        [Browsable(true)]
        [Description("Indicates that ShowFooter has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ShowFooterChanged;
        [Browsable(true)]
        [Description("Indicates that ShowHeader has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ShowHeaderChanged;
        [Browsable(true)]
        [Description("Indicates that ShowWeekdays has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler ShowWeekdaysChanged;
        [Browsable(true)]
        [Description("Indicates that a Footer property has changed.")]
        [Category("PropertyChanged")]
        public event FooterPropertyEventHandler FooterPropertyChanged;
        [Browsable(true)]
        [Description("Indicates that a weeknumber property has changed.")]
        [Category("PropertyChanged")]
        public event WeeknumberPropertyEventHandler WeeknumberPropertyChanged;
        [Browsable(true)]
        [Description("Indicates that a weekday property has changed.")]
        [Category("PropertyChanged")]
        public event WeekdayPropertyEventHandler WeekdayPropertyChanged;
        [Browsable(true)]
        [Description("Indicates that a header property has changed.")]
        [Category("PropertyChanged")]
        public event HeaderPropertyEventHandler HeaderPropertyChanged;
        [Browsable(true)]
        [Description("Indicates that a month property has changed.")]
        [Category("PropertyChanged")]
        public event MonthPropertyEventHandler MonthPropertyChanged;
        [Browsable(true)]
        [Description("Indicates that a month color has changed.")]
        [Category("PropertyChanged")]
        public event MonthColorEventHandler MonthColorChanged;
        [Browsable(true)]
        [Description("Indicates that a month borderstyle has changed.")]
        [Category("PropertyChanged")]
        public event MonthBorderStyleEventHandler MonthBorderStyleChanged;
        [Browsable(true)]
        [Description("Indicates that the keyboard enabled property has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler KeyboardEnabledChanged;
        [Browsable(true)]
        [Description("Indicates that the first day of week has changed.")]
        [Category("PropertyChanged")]
        public event EventHandler FirstDayOfWeekChanged;
        [Browsable(true)]
        [Description("Indicates that the keyboard configuration has been changed.")]
        [Category("PropertyChanged")]
        public event KeyboardChangedEventHandler KeyboardChanged;

        #endregion

        #endregion

        #region Constructor

        public MonthCalendar()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            // TODO: Add any initialization after the InitComponent call
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            m_borderColor = Color.Black;
            m_selectButton = MouseButtons.Left;
            m_extendedKey = mcExtendedSelectionKey.Ctrl;

            m_activeRegion = mcCalendarRegion.None;
            m_selectionMode = mcSelectionMode.MultiSimple;
            m_dateTimeFormat = DateTimeFormatInfo.CurrentInfo;
            m_theme = IntPtr.Zero;

            m_installedCultures = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures);
            m_culture = CultureInfo.CurrentCulture;

            m_showToday = true;
            m_showTrailing = true;
            m_showFocus = true;
            m_todayColor = Color.Red;
            //initiate regions	
            m_weekday = new Weekday(this);
            m_month = new Month(this);
            m_footer = new Footer(this);
            m_weeknumber = new Weeknumber(this);
            m_header = new Header(this);

            m_keyboard = new KeyboardConfig(this);
            m_keyboardEnabled = true;
            m_activeMonth = new ActiveMonth(this);
            m_dateItemCollection = new DateItemCollection(this);
            m_selectedDates = new SelectedDatesCollection(this);

            // setup callback for weeknumbers
            WeeknumberCallBack = new WeekCallBack(m_weeknumber.CalcWeek);

            // setup internal events
            m_hook.KeyDown += new KeyEventHandler(m_hook_KeyDown);
            m_hook.KeyUp += new KeyEventHandler(m_hook_KeyUp);

            m_dateItemCollection.DateItemModified += new DateItemEventHandler(m_dateItemCollection_DateItemModified);

            m_month.DayRender += new DayRenderEventHandler(m_month_DayRender);
            m_month.DayQueryInfo += new DayQueryInfoEventHandler(m_month_DayQueryInfo);
            m_month.DayLostFocus += new DayEventHandler(m_month_DayLostFocus);
            m_month.DayGotFocus += new DayEventHandler(m_month_DayGotFocus);
            m_month.ImageClick += new DayClickEventHandler(m_month_ImageClick);
            m_month.DayMouseMove += new DayMouseMoveEventHandler(m_month_DayMouseMove);
            m_month.DayClick += new DayClickEventHandler(m_month_DayClick);
            m_month.DayDoubleClick += new DayClickEventHandler(m_month_DayDoubleClick);
            m_month.DaySelected += new DaySelectedEventHandler(m_month_DaySelected);
            m_month.DayDeselected += new DaySelectedEventHandler(m_month_DayDeselected);
            m_month.ColorChanged += new MonthColorEventHandler(m_month_ColorChanged);
            m_month.BorderStyleChanged += new MonthBorderStyleEventHandler(m_month_BorderStyleChanged);
            m_month.PropertyChanged += new MonthPropertyEventHandler(m_month_PropertyChanged);
            m_month.BeforeDaySelected += new DayStateChangedEventHandler(m_month_BeforeDaySelected);
            m_month.BeforeDayDeselected += new DayStateChangedEventHandler(m_month_BeforeDayDeselected);

            m_footer.Click += new ClickEventHandler(m_footer_Click);
            m_footer.DoubleClick += new ClickEventHandler(m_footer_DoubleClick);
            m_footer.PropertyChanged += new FooterPropertyEventHandler(m_footer_PropertyChanged);

            m_weeknumber.PropertyChanged += new WeeknumberPropertyEventHandler(m_weeknumber_PropertyChanged);
            m_weeknumber.Click += new WeeknumberClickEventHandler(m_weeknumber_Click);
            m_weeknumber.DoubleClick += new WeeknumberClickEventHandler(m_weeknumber_DoubleClick);

            m_weekday.PropertyChanged += new WeekdayPropertyEventHandler(m_weekday_PropertyChanged);
            m_weekday.Click += new WeekdayClickEventHandler(m_weekday_Click);
            m_weekday.DoubleClick += new WeekdayClickEventHandler(m_weekday_DoubleClick);

            m_header.PropertyChanged += new HeaderPropertyEventHandler(m_header_PropertyChanged);
            m_header.Click += new ClickEventHandler(m_header_Click);
            m_header.DoubleClick += new ClickEventHandler(m_header_DoubleClick);
            m_header.PrevMonthButtonClick += new EventHandler(m_header_PrevMonthButtonClick);
            m_header.NextMonthButtonClick += new EventHandler(m_header_NextMonthButtonClick);
            m_header.PrevYearButtonClick += new EventHandler(m_header_PrevYearButtonClick);
            m_header.NextYearButtonClick += new EventHandler(m_header_NextYearButtonClick);


            m_activeMonth.MonthChanged += new MonthChangedEventHandler(m_activeMonth_MonthChanged);
            m_activeMonth.BeforeMonthChanged += new BeforeMonthChangedEventHandler(m_activeMonth_BeforeMonthChanged);

            m_printDoc.BeginPrint += new PrintEventHandler(m_printDoc_BeginPrint);
            m_printDoc.PrintPage += new PrintPageEventHandler(m_printDoc_PrintPage);
            m_printDoc.QueryPageSettings += new QueryPageSettingsEventHandler(m_printDoc_QueryPageSettings);

            m_borderStyle = ButtonBorderStyle.Solid;

            m_printDoc.DocumentName = "MonthCalendar";

            m_showFooter = false;
            m_showHeader = true;
            m_showWeekday = true;

            m_selectTrailing = true;
            m_selectKeyDown = false;

            m_activeMonth.Month = DateTime.Today.Month;
            m_activeMonth.Year = DateTime.Today.Year;

            m_minDate = DateTime.Now.AddYears(-10);
            m_maxDate = DateTime.Now.AddYears(10);

            m_month.SelectedMonth = DateTime.Parse(m_activeMonth.Year + "-" + m_activeMonth.Month + "-01");

            m_hook.InstallKeyboardHook();

            this.Width = 176;
            this.Height = 184;
            m_keyHandled = false;
            Setup();
            /* 
             * init control
             * add by ryan
           */
            //this.FirstDayOfWeek = 2;
            //
            this.Weekdays.Align = mcTextAlign.Center;
            this.Weekdays.BackColor1 = Color.Transparent;
            this.Weekdays.BackColor2 = Color.SeaShell;
            this.Weekdays.BorderColor = SkinManager.CurrentSkin.BorderColor;
            this.Weekdays.GradientMode = mcGradientMode.Vertical;
            this.Weekdays.TextColor = Color.FromArgb(0, 65, 0);
            this.Weekdays.Font = new Font("Tahoma", 11.5F);
            //this.ShowFooter = false;
            this.Month.BorderStyles.Normal = ButtonBorderStyle.Dotted;
            this.Month.Colors.Weekend.Date = Color.DarkRed;
            this.Month.DateAlign = mcItemAlign.TopCenter;
            this.Month.DateFont = new Font("Tahoma", 14.25f);
            this.Month.ShowMonthInDay = true;
            this.Month.TextAlign = mcItemAlign.BottomCenter;
            this.Month.TextFont = new Font("Tahoma", 9.75F);
            this.Month.Colors.Days.Text = Color.Red;
            this.Month.Colors.Trailing.Date = Color.FromArgb(32, 32, 32);
            this.Month.Colors.Trailing.Text = Color.Red;
            //Í¸Ã÷¶È
            this.Month.Transparency.Background = 190;
            this.Month.Transparency.Text = 200;
            //selected border color
            this.Month.Colors.Selected.Border = Color.Red;
            this.Month.BorderStyles.Selected = ButtonBorderStyle.Outset;
            //min max
            this.MinDate = new DateTime(1986, 11, 15);
            this.MaxDate = new DateTime(2086, 11, 15);
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {

            // Remove hook
            m_hook.RemoveKeyboardHook();

            if (disposing)
            {
                if (components != null)
                    components.Dispose();

                // delete internal events

                m_hook.KeyDown -= new KeyEventHandler(m_hook_KeyDown);
                m_hook.KeyUp -= new KeyEventHandler(m_hook_KeyUp);

                m_activeMonth.MonthChanged -= new MonthChangedEventHandler(m_activeMonth_MonthChanged);
                m_activeMonth.BeforeMonthChanged -= new BeforeMonthChangedEventHandler(m_activeMonth_BeforeMonthChanged);

                m_dateItemCollection.DateItemModified -= new DateItemEventHandler(m_dateItemCollection_DateItemModified);

                m_month.DayRender -= new DayRenderEventHandler(m_month_DayRender);
                m_month.DayQueryInfo -= new DayQueryInfoEventHandler(m_month_DayQueryInfo);
                m_month.DayLostFocus -= new DayEventHandler(m_month_DayLostFocus);
                m_month.DayGotFocus -= new DayEventHandler(m_month_DayGotFocus);
                m_month.ImageClick -= new DayClickEventHandler(m_month_ImageClick);
                m_month.DayMouseMove -= new DayMouseMoveEventHandler(m_month_DayMouseMove);
                m_month.DayClick -= new DayClickEventHandler(m_month_DayClick);
                m_month.DayDoubleClick -= new DayClickEventHandler(m_month_DayDoubleClick);
                m_month.DaySelected -= new DaySelectedEventHandler(m_month_DaySelected);
                m_month.DayDeselected -= new DaySelectedEventHandler(m_month_DayDeselected);
                m_month.ColorChanged -= new MonthColorEventHandler(m_month_ColorChanged);
                m_month.BorderStyleChanged -= new MonthBorderStyleEventHandler(m_month_BorderStyleChanged);
                m_month.PropertyChanged -= new MonthPropertyEventHandler(m_month_PropertyChanged);
                m_month.BeforeDaySelected -= new DayStateChangedEventHandler(m_month_BeforeDaySelected);
                m_month.BeforeDayDeselected -= new DayStateChangedEventHandler(m_month_BeforeDayDeselected);


                m_footer.Click -= new ClickEventHandler(m_footer_Click);
                m_footer.DoubleClick -= new ClickEventHandler(m_footer_DoubleClick);
                m_footer.PropertyChanged -= new FooterPropertyEventHandler(m_footer_PropertyChanged);

                m_weeknumber.PropertyChanged -= new WeeknumberPropertyEventHandler(m_weeknumber_PropertyChanged);
                m_weeknumber.Click -= new WeeknumberClickEventHandler(m_weeknumber_Click);
                m_weeknumber.DoubleClick -= new WeeknumberClickEventHandler(m_weeknumber_DoubleClick);

                m_weekday.PropertyChanged -= new WeekdayPropertyEventHandler(m_weekday_PropertyChanged);
                m_weekday.Click -= new WeekdayClickEventHandler(m_weekday_Click);
                m_weekday.DoubleClick -= new WeekdayClickEventHandler(m_weekday_DoubleClick);

                m_header.PropertyChanged -= new HeaderPropertyEventHandler(m_header_PropertyChanged);
                m_header.Click -= new ClickEventHandler(m_header_Click);
                m_header.DoubleClick -= new ClickEventHandler(m_header_DoubleClick);
                m_header.PrevMonthButtonClick -= new EventHandler(m_header_PrevMonthButtonClick);
                m_header.NextMonthButtonClick -= new EventHandler(m_header_NextMonthButtonClick);
                m_header.PrevYearButtonClick -= new EventHandler(m_header_PrevYearButtonClick);
                m_header.NextYearButtonClick -= new EventHandler(m_header_NextYearButtonClick);


                m_printDoc.BeginPrint -= new PrintEventHandler(m_printDoc_BeginPrint);
                m_printDoc.PrintPage -= new PrintPageEventHandler(m_printDoc_PrintPage);
                m_printDoc.QueryPageSettings -= new QueryPageSettingsEventHandler(m_printDoc_QueryPageSettings);

                m_printDoc.Dispose();
                m_header.Dispose();
                m_weeknumber.Dispose();
                m_weekday.Dispose();
                m_month.Dispose();
                m_footer.Dispose();

                m_hook.Dispose();

            }
            base.Dispose(disposing);
        }

        #endregion

        #region Public Methods

        public bool IsSelected(DateTime dt)
        {
            bool sel = false;
            for (int i = 0; i < 42; i++)
            {
                if (m_month.m_days[i].Date.ToShortDateString() == dt.ToShortDateString())
                {
                    if (m_month.m_days[i].State == mcDayState.Selected)
                        sel = true;
                    break;
                }
            }
            return sel;
        }

        public void ClearSelection()
        {
            m_month.RemoveSelection(true);
            Invalidate();
        }

        public Bitmap Snapshot()
        {

            Graphics e = this.CreateGraphics();
            // Create a new bitmap
            Bitmap bmp = new Bitmap(this.Width, this.Height, e);
            // Create a graphics context connected to the bitmap
            e = Graphics.FromImage(bmp);
            // Draw the calendar on the bitmap
            Draw(e, this.DisplayRectangle);

            e.Dispose();
            return bmp;
        }

        public void Print()
        {
            m_printDoc.Print();
        }

        public void SaveAsImage(string filename, ImageFormat format)
        {
            Bitmap bmp = Snapshot();
            bmp.Save(filename, format);
        }

        public void Copy()
        {
            try
            {
                Bitmap bmp = Snapshot();
                System.Windows.Forms.Clipboard.SetDataObject(bmp);
            }
            catch (Exception)
            {

            }

        }

        public void AddDateInfo(DateItem[] info)
        {
            for (int i = 0; i < info.Length; i++)
            {
                if (info[i] != null)
                    Dates.Add(info[i]);
            }
            Invalidate();
        }

        public void RemoveDateInfo(DateItem info)
        {
            Dates.Remove(info);
        }

        public void RemoveDateInfo(DateTime info)
        {
            for (int i = 0; i < Dates.Count; i++)
            {
                if (Dates[i].Date.ToShortDateString() == info.ToShortDateString())
                {
                    Dates.RemoveAt(i);
                }
            }
            Invalidate();
        }

        public void AddDateInfo(DateItem info)
        {
            Dates.Add(info);
            Invalidate();
        }

        public void ResetDateInfo()
        {
            Dates.Clear();
            Invalidate();
        }

        public DateItem[] GetDateInfo()
        {
            DateItem[] ret = new DateItem[0];
            ret.Initialize();
            for (int i = 0; i < Dates.Count; i++)
            {
                ret = Dates.AddInfo(Dates[i], ret);
            }
            return ret;
        }

        public DateItem[] GetDateInfo(DateTime dt)
        {
            return Dates.DateInfo(dt);
        }

        public void SelectDate(DateTime date)
        {
            SelectRange(date, date);
        }

        public void DeselectRange(DateTime From, DateTime To)
        {
            int from = -1;
            int to = -1;
            if ((From >= m_minDate) && (From <= m_maxDate) &&
                (To >= m_minDate) && (To <= m_maxDate) &&
                (SelectionMode == mcSelectionMode.MultiExtended))
            {
                for (int i = 0; i < 42; i++)
                {
                    if (m_month.m_days[i].Date.ToShortDateString() == From.ToShortDateString())
                        from = i;
                    if (m_month.m_days[i].Date.ToShortDateString() == To.ToShortDateString())
                        to = i;
                    if ((from != -1) && (to != -1))
                        break;
                }
                if ((from != -1) && (to != -1))
                {
                    m_month.DeselectRange(from, to);
                    this.Invalidate();
                }
            }
        }

        public void SelectArea(DateTime topLeft, DateTime bottomRight)
        {
            int topleft = -1;
            int bottomright = -1;
            if ((topLeft >= m_minDate) && (topLeft <= m_maxDate) &&
                (bottomRight >= m_minDate) && (bottomRight <= m_maxDate) &&
                (SelectionMode >= mcSelectionMode.MultiSimple))
            {

                if ((topLeft.Year.ToString() + topLeft.Month.ToString() == bottomRight.Year.ToString() + bottomRight.Month.ToString()) &&
                     (ActiveMonth.Year.ToString() + ActiveMonth.Month.ToString() != topLeft.Year.ToString() + topLeft.Month.ToString()))
                {
                    // Change month
                    if (ActiveMonth.Year != topLeft.Year)
                        ActiveMonth.Year = topLeft.Year;
                    if (ActiveMonth.Month != topLeft.Month)
                        ActiveMonth.Month = topLeft.Month;

                }

                for (int i = 0; i < 42; i++)
                {
                    if (m_month.m_days[i].Date.ToShortDateString() == topLeft.ToShortDateString())
                        topleft = i;
                    if (m_month.m_days[i].Date.ToShortDateString() == bottomRight.ToShortDateString())
                        bottomright = i;
                    if ((topleft != -1) && (bottomright != -1))
                        break;
                }
                if ((topleft != -1) && (bottomright != -1))
                {
                    m_month.SelectArea(topleft, bottomright);
                    this.Invalidate();
                }
            }
        }

        public void DeselectArea(DateTime topLeft, DateTime bottomRight)
        {
            int topleft = -1;
            int bottomright = -1;
            if ((topLeft >= m_minDate) && (topLeft <= m_maxDate) &&
                (bottomRight >= m_minDate) && (bottomRight <= m_maxDate) &&
                (SelectionMode == mcSelectionMode.MultiExtended))
            {
                for (int i = 0; i < 42; i++)
                {
                    if (m_month.m_days[i].Date.ToShortDateString() == topLeft.ToShortDateString())
                        topleft = i;
                    if (m_month.m_days[i].Date.ToShortDateString() == bottomRight.ToShortDateString())
                        bottomright = i;
                    if ((topleft != -1) && (bottomright != -1))
                        break;
                }
                if ((topleft != -1) && (bottomright != -1))
                {
                    m_month.DeselectArea(topleft, bottomright);
                    this.Invalidate();
                }
            }
        }

        public void SelectRange(DateTime fromDate, DateTime toDate)
        {
            int to = -1;
            int from = -1;

            if (((fromDate >= m_minDate) && (toDate <= m_maxDate) &&
                (toDate >= m_minDate) && (toDate <= m_maxDate)) &&
                ((SelectionMode >= mcSelectionMode.MultiSimple) ||
                ((fromDate == toDate) &&
                (SelectionMode == mcSelectionMode.One))))
            {

                if ((fromDate.Year.ToString() + fromDate.Month.ToString() == toDate.Year.ToString() + toDate.Month.ToString()) &&
                     (ActiveMonth.Year.ToString() + ActiveMonth.Month.ToString() != fromDate.Year.ToString() + fromDate.Month.ToString()))
                {
                    // Change month
                    if (ActiveMonth.Year != fromDate.Year)
                        ActiveMonth.Year = fromDate.Year;
                    if (ActiveMonth.Month != fromDate.Month)
                        ActiveMonth.Month = fromDate.Month;

                }


                for (int i = 0; i < 42; i++)
                {
                    if (m_month.m_days[i].Date.ToShortDateString() == fromDate.ToShortDateString())
                        from = i;
                    if (m_month.m_days[i].Date.ToShortDateString() == toDate.ToShortDateString())
                        to = i;
                    if ((to != -1) && (from != -1))
                        break;
                }
                if ((from != -1) && (to != -1))
                {
                    m_month.SelectRange(from, to);
                    this.Invalidate();
                }

            }

        }

        public void SelectWeekday(DayOfWeek day)
        {

            if (m_selectionMode >= mcSelectionMode.MultiSimple)
            {
                for (int i = 0; i <= 6; i++)
                {
                    if ((int)m_month.m_days[i].Weekday == (int)day)
                    {
                        m_month.SelectArea(i, i + 35);
                        this.Invalidate();
                        break;
                    }
                }
            }
        }

        public void DeselectWeekday(DayOfWeek day)
        {
            if (m_selectionMode == mcSelectionMode.MultiExtended)
            {
                for (int i = 0; i <= 6; i++)
                {
                    if ((int)m_month.m_days[i].Weekday == (int)day)
                    {
                        m_month.DeselectArea(i, i + 35);
                        this.Invalidate();
                        break;
                    }
                }
            }
        }

        public void SelectWeek(int week)
        {
            if (m_selectionMode >= mcSelectionMode.MultiSimple)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (m_month.m_days[i * 7].Week == week)
                    {
                        m_month.SelectRange(i * 7, (i * 7) + 6);
                        this.Invalidate();
                        break;
                    }

                }
            }
        }

        public void DeselectWeek(int week)
        {
            if (m_selectionMode == mcSelectionMode.MultiExtended)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (m_month.m_days[i * 7].Week == week)
                    {
                        m_month.DeselectArea(i * 7, (i * 7) + 6);
                        this.Invalidate();
                        break;
                    }

                }
            }
        }



        #endregion

        #region Private Methods

        private DayOfWeek IntToDayOfWeek(int d)
        {

            switch (d)
            {
                case 0: return DayOfWeek.Sunday;
                case 1: return DayOfWeek.Monday;
                case 2: return DayOfWeek.Tuesday;
                case 3: return DayOfWeek.Wednesday;
                case 4: return DayOfWeek.Thursday;
                case 5: return DayOfWeek.Friday;
                case 6: return DayOfWeek.Saturday;
                default: return DayOfWeek.Friday; // should never be used.  	 
            }
        }

        private void Draw(Graphics e, Rectangle clip)
        {

            if ((ShowHeader) && (m_header.IsVisible(clip)))
                m_header.Draw(e);
            if ((ShowWeekdays) && (m_weekday.IsVisible(clip)))
                m_weekday.Draw(e);
            if ((ShowWeeknumbers) && (m_weeknumber.IsVisible(clip)))
                m_weeknumber.Draw(e);
            if ((ShowFooter) && (m_footer.IsVisible(clip)))
                m_footer.Draw(e);

            if (m_month.IsVisible(clip))
                m_month.Draw(e);

            // Draw border
            ControlPaint.DrawBorder(e, this.ClientRectangle, m_borderColor, m_borderStyle);

        }

        private void GetThemeColors()
        {
            Color selectColor = new Color();
            Color focusColor = new Color();
            Color borderColor = new Color();
            Color itemColor = new Color();
            bool useSystemColors = false;

            VisualStyleElement element;

            // Check if visual styles are used
            if (Application.RenderWithVisualStyles)
            {

                // Get Theme colors..
                element = VisualStyleElement.ExplorerBar.HeaderBackground.Normal;
                VisualStyleRenderer renderer = new VisualStyleRenderer(element);
                selectColor = renderer.GetColor(ColorProperty.GradientColor2);

                itemColor = selectColor;
                borderColor = ControlPaint.Light(selectColor);
                selectColor = ControlPaint.Light(selectColor);
                focusColor = ControlPaint.Light(selectColor);
            }
            else
            {
                useSystemColors = true;
            }

            if (useSystemColors)
            {
                // Get System colors
                selectColor = SystemColors.ActiveCaption;
                focusColor = ControlPaint.Light(selectColor);
                borderColor = SystemColors.ActiveBorder;
                itemColor = selectColor;
            }

            // apply colors..

            m_month.Colors.Selected.Border = ControlPaint.Light(ControlPaint.Dark(selectColor));
            this.BorderColor = borderColor;

            m_month.Colors.Selected.BackColor = selectColor;
            m_month.Colors.Focus.BackColor = focusColor;
            m_month.Colors.Focus.Border = selectColor;
            m_header.BackColor1 = itemColor;
            m_weekday.TextColor = itemColor;
            m_weekday.BackColor1 = Color.White;
            m_weeknumber.TextColor = itemColor;
            m_weeknumber.BackColor1 = Color.White;

            Invalidate();

        }

        internal void Setup()
        {
            m_month.Setup();
        }

        internal string[] AllowedMonths()
        {
            string[] monthList = new string[12];
            string[] months = m_dateTimeFormat.MonthNames;
            monthList.Initialize();

            for (int i = 0; i < 12; i++)
                monthList[i] = months[i];

            return monthList;

        }

        internal void DrawGradient(Graphics e, Rectangle rect, Color color1, Color color2, mcGradientMode mode)
        {
            LinearGradientMode gradient = LinearGradientMode.BackwardDiagonal;
            if (mode == mcGradientMode.Vertical)
                gradient = LinearGradientMode.Vertical;
            else if (mode == mcGradientMode.Horizontal)
                gradient = LinearGradientMode.Horizontal;
            else if (mode == mcGradientMode.BackwardDiagonal)
                gradient = LinearGradientMode.BackwardDiagonal;
            else if (mode == mcGradientMode.ForwardDiagonal)
                gradient = LinearGradientMode.ForwardDiagonal;

            LinearGradientBrush gb = new LinearGradientBrush(rect, color1, color2, gradient);
            e.FillRectangle(gb, rect);
            gb.Dispose();
        }

        internal string[] DayNames()
        {
            string[] dayList = new string[8];
            string[] days = m_dateTimeFormat.DayNames;
            dayList.Initialize();

            dayList[0] = "Default";
            for (int i = 1; i <= 7; i++)
                dayList[i] = days[i - 1];

            return dayList;

        }

        internal bool IsYearValid(string y)
        {
            string[] years = AllowedYears();
            bool ret = false;
            for (int i = 0; i < years.Length; i++)
            {
                if (y == years[i])
                    ret = true;
            }
            return ret;
        }

        internal int MonthNumber(string m)
        {
            int ret = -1;
            string[] months;
            months = AllowedMonths();

            for (int i = 0; i < months.Length; i++)
            {
                if (m.CompareTo(months[i]) == 0)
                    return i + 1;
            }
            if ((Convert.ToInt32(m) >= 1) && (Convert.ToInt32(m) <= 12))
            {
                ret = Convert.ToInt32(m);
            }
            return ret;
        }

        internal int DayNumber(string m)
        {
            int ret = 0;
            string[] days;
            days = DayNames();

            for (int i = 0; i < days.Length; i++)
            {
                if (m.CompareTo(days[i]) == 0)
                    return i;
            }
            if ((Convert.ToInt32(m) >= 0) && (Convert.ToInt32(m) < 8))
            {
                ret = Convert.ToInt32(m);
            }
            return ret;
        }

        internal string MonthName(int m)
        {
            string[] validNames;
            string name = "";
            validNames = AllowedMonths();
            if ((m >= 1) && (m <= 12))
            {
                name = validNames[m - 1];
            }
            return name;
        }

        internal string[] AllowedYears()
        {

            string[] yearList = new string[(m_maxDate.Year - m_minDate.Year) + 1];

            yearList.Initialize();

            int year;

            year = 0;
            for (int i = m_minDate.Year; i <= m_maxDate.Year; i++)
            {
                yearList[year] = i.ToString();
                year++;
            }

            return yearList;
        }

        internal void DoLayout()
        {
            int y = 0;
            int x = 0;

            Graphics g;
            SizeF weekSize = new SizeF();

            g = this.CreateGraphics();
            weekSize = g.MeasureString("99", m_weeknumber.Font);

            if (ShowHeader)
            {
                if (m_header.Font.Height > 31)
                    y = 2 + this.Font.Height + 2;
                else
                    y = 31;

                m_headerRect = new Rectangle(0, 0, this.Width, y);
            }
            else
            {
                m_headerRect = new Rectangle(0, 0, 0, 0);
            }

            if (ShowWeeknumbers)
                x = 2 + (int)weekSize.Width + 2;

            m_weekdaysRect.Height = 2 + m_weekday.Font.Height + 2;
            if (ShowWeekdays)
            {
                m_weekdaysRect.Y = y;
                m_weekdaysRect.Width = this.Width - x;
                m_weekdaysRect.X = x;
                y = y + m_weekdaysRect.Height;
            }
            else m_weekdaysRect = new Rectangle(0, 0, 0, 0);

            if (ShowWeeknumbers)
                m_weeknumbersRect = new Rectangle(0, y - 1, x, this.Height - y + 1);


            m_monthRect.Y = y;
            m_monthRect.X = x;
            m_monthRect.Width = this.Width - x;

            if (ShowFooter)
            {
                m_footerRect.Height = 2 + m_footer.Font.Height + 2;
                m_footerRect.Y = this.Height - m_footerRect.Height;
                m_footerRect.X = 0;
                m_footerRect.Width = this.Width;
                m_monthRect.Height = this.Height - m_footerRect.Height - y;
                m_weeknumbersRect.Height -= m_footerRect.Height;
            }
            else
            {
                m_footerRect = new Rectangle(0, 0, 0, 0);
                m_monthRect.Height = this.Height - y;
            }

            m_month.Rect = m_monthRect;
            m_month.SetupDays();

            m_footer.Rect = m_footerRect;
            m_header.Rect = m_headerRect;
            m_weeknumber.Rect = m_weeknumbersRect;
            m_weekday.Rect = m_weekdaysRect;

            g.Dispose();

        }


        #endregion

        #region Properties

        internal bool SelectKeyDown
        {
            get
            {
                if (!m_keyboardEnabled)
                    return false;
                else
                    return m_selectKeyDown;
            }
        }

        internal bool ExtendedKey
        {
            get
            {
                if (m_extendedKey == mcExtendedSelectionKey.None)
                    return true;
                else
                    return m_ctrlKey;
            }
            set
            {
                m_ctrlKey = value;
            }
        }

        internal mcCalendarRegion ActiveRegion
        {
            get
            {
                return m_activeRegion;
            }
            set
            {
                if (value != m_activeRegion)
                {
                    // raise OnLeave event...
                    switch (m_activeRegion)
                    {
                        case mcCalendarRegion.None:
                        case mcCalendarRegion.Month:
                        case mcCalendarRegion.Day:
                            {
                                break;
                            }
                        case mcCalendarRegion.Header:
                            {
                                if (HeaderMouseLeave != null)
                                    HeaderMouseLeave(this, new EventArgs());
                                break;
                            }
                        case mcCalendarRegion.Weekdays:
                            {
                                if (WeekdaysMouseLeave != null)
                                    WeekdaysMouseLeave(this, new EventArgs());
                                break;
                            }
                        case mcCalendarRegion.Weeknumbers:
                            {
                                if (WeeknumbersMouseLeave != null)
                                    WeeknumbersMouseLeave(this, new EventArgs());
                                break;
                            }
                        case mcCalendarRegion.Footer:
                            {
                                if (FooterMouseLeave != null)
                                    FooterMouseLeave(this, new EventArgs());
                                break;
                            }
                    }
                    m_activeRegion = value;
                    // Raise onEnter event...
                    switch (m_activeRegion)
                    {
                        case mcCalendarRegion.None:
                        case mcCalendarRegion.Month:
                        case mcCalendarRegion.Day:
                            {
                                break;
                            }
                        case mcCalendarRegion.Header:
                            {
                                if (HeaderMouseEnter != null)
                                    HeaderMouseEnter(this, new EventArgs());
                                break;
                            }
                        case mcCalendarRegion.Weekdays:
                            {
                                if (WeekdaysMouseEnter != null)
                                    WeekdaysMouseEnter(this, new EventArgs());
                                break;
                            }
                        case mcCalendarRegion.Weeknumbers:
                            {
                                if (WeeknumbersMouseEnter != null)
                                    WeeknumbersMouseEnter(this, new EventArgs());
                                break;
                            }
                        case mcCalendarRegion.Footer:
                            {
                                if (FooterMouseEnter != null)
                                    FooterMouseEnter(this, new EventArgs());
                                break;
                            }
                    }
                }
            }
        }

        [Browsable(false)]
        public string Version
        {
            get
            {
                int startPos;
                int endPos;
                string ver = "Version=";
                Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
                startPos = myAssembly.FullName.IndexOf(ver);
                startPos += ver.Length;
                endPos = myAssembly.FullName.IndexOf(",", startPos + 1);
                return myAssembly.FullName.Substring(startPos, endPos - startPos);
            }

        }

        [Description("First day of week.")]
        [RefreshProperties(RefreshProperties.All)]
        [Category("Behavior")]
        [DefaultValue(0)]
        [TypeConverter(typeof(FirstDayOfWeekConverter))]
        public int FirstDayOfWeek
        {
            get
            {
                return m_firstDayOfWeek;
            }
            set
            {
                if (m_firstDayOfWeek != value)
                {
                    m_firstDayOfWeek = value;
                    if (m_firstDayOfWeek != 0)
                        m_dateTimeFormat.FirstDayOfWeek = IntToDayOfWeek(m_firstDayOfWeek - 1);
                    else
                        m_dateTimeFormat.FirstDayOfWeek = m_defaultFirstDayOfWeek;

                    if (FirstDayOfWeekChanged != null)
                        FirstDayOfWeekChanged(this, new EventArgs());

                    Setup();
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Description("Indicates wether keyboard support is enabled.")]
        [Category("Behavior")]
        [DefaultValue(typeof(bool), "True")]
        public bool KeyboardEnabled
        {
            get
            {
                return m_keyboardEnabled;
            }
            set
            {
                if (m_keyboardEnabled != value)
                {
                    m_keyboardEnabled = value;
                    if (KeyboardEnabledChanged != null)
                        KeyboardEnabledChanged(this, new EventArgs());

                }
            }
        }


        [Description("Indicates wether the trailing dates should be drawn.")]
        [Category("Behavior")]
        [DefaultValue(true)]
        public bool ShowTrailingDates
        {
            get
            {
                return m_showTrailing;
            }
            set
            {
                if (m_showTrailing != value)
                {
                    m_showTrailing = value;
                    if (value == false)
                        SelectTrailingDates = false;
                    if (ShowTrailingChanged != null)
                        ShowTrailingChanged(this, new EventArgs());
                    Invalidate();
                }
            }
        }

        [Category("Behavior")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("Culture to use for calendar.")]
        public CultureInfo Culture
        {
            get
            {
                return m_culture;
            }
            set
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture = value;
                    m_dateTimeFormat = DateTimeFormatInfo.CurrentInfo;
                }
                catch (Exception)
                {
                }
                finally
                {
                    m_culture = value;
                    m_defaultFirstDayOfWeek = m_dateTimeFormat.FirstDayOfWeek;

                    if (m_firstDayOfWeek != 0)
                        m_dateTimeFormat.FirstDayOfWeek = IntToDayOfWeek(m_firstDayOfWeek - 1);
                    else
                        m_dateTimeFormat.FirstDayOfWeek = m_defaultFirstDayOfWeek;

                    if (this.CultureChanged != null)
                        this.CultureChanged(this, new EventArgs());
                }
                m_month.RemoveSelection(true);
                Setup();
                Invalidate();
            }
        }

        [Category("Behavior")]
        [DefaultValue(null)]
        [Description("Collection with formatted dates.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor(typeof(DateItemCollectionEditor), typeof(UITypeEditor))]
        public DateItemCollection Dates
        {
            get
            {
                return this.m_dateItemCollection;
            }
        }

        [Browsable(false)]
        public SelectedDatesCollection SelectedDates
        {
            get
            {
                SelectedDatesCollection dc = new SelectedDatesCollection(this);
                return UpdateSelectedCollection();
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The color used to mark todays date.")]
        [DefaultValue(typeof(Color), "Red")]
        public Color TodayColor
        {
            get
            {
                return m_todayColor;
            }
            set
            {
                if (value != m_todayColor)
                {
                    m_todayColor = value;
                    if (this.CalendarColorChanged != null)
                        this.CalendarColorChanged(this, new CalendarColorEventArgs(mcCalendarColor.Today));
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Indicates wether todays date should be marked.")]
        [DefaultValue(true)]
        public bool ShowToday
        {
            get
            {
                return m_showToday;
            }
            set
            {
                if (value != m_showToday)
                {
                    m_showToday = value;
                    if (this.ShowTodayChanged != null)
                        this.ShowTodayChanged(this, new EventArgs());
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Indicates wether the focus should be displayed.")]
        [DefaultValue(true)]
        public bool ShowFocus
        {
            get
            {
                return m_showFocus;
            }
            set
            {
                if (value != m_showFocus)
                {
                    m_showFocus = value;
                    if (this.ShowFocusChanged != null)
                        this.ShowFocusChanged(this, new EventArgs());
                    Invalidate();
                }
            }
        }


        [Browsable(true)]
        [Category("Behavior")]
        [Description("Indicates wether its possible to select trailing dates.")]
        [DefaultValue(true)]
        public bool SelectTrailingDates
        {
            get
            {
                return m_selectTrailing;
            }
            set
            {
                if (value != m_selectTrailing)
                {
                    m_selectTrailing = value;
                    if (this.SelectTrailingChanged != null)
                        this.SelectTrailingChanged(this, new EventArgs());
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Indicates if theme colors should be used.")]
        [DefaultValue(false)]
        public bool Theme
        {
            get
            {
                return m_useTheme;
            }
            set
            {
                if (value != m_useTheme)
                {
                    m_useTheme = value;
                    if (this.UseThemeChanged != null)
                        this.UseThemeChanged(this, new EventArgs());
                    if (m_useTheme)
                        GetThemeColors();
                    else
                        Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("ImageList thats contains the images used in the calendar.")]
        public ImageList ImageList
        {
            get
            {
                return m_imageList;
            }
            set
            {
                if (value != m_imageList)
                {
                    m_imageList = value;
                    if (this.ImageListChanged != null)
                        this.ImageListChanged(this, new EventArgs());
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [DefaultValue(typeof(mcExtendedSelectionKey), "Ctrl")]
        [Description("Key used for Extended selection mode.")]
        public mcExtendedSelectionKey ExtendedSelectionKey
        {
            get
            {
                return m_extendedKey;
            }
            set
            {
                if (value != m_extendedKey)
                {
                    m_extendedKey = value;

                    //if ((m_selectionMode == mcSelectionMode.MultiExtended) && (m_extendedKey!=mcExtendedSelectionKey.None))
                    //	m_hook.InstallKeyboardHook();
                    //else
                    //	m_hook.RemoveKeyboardHook(); 

                    if (this.ExtendedSelectionKeyChanged != null)
                        this.ExtendedSelectionKeyChanged(this, new EventArgs());

                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("Indicates the selection mode used.")]
        [DefaultValue(typeof(mcSelectionMode), "MultiSimple")]
        public mcSelectionMode SelectionMode
        {
            get
            {
                return m_selectionMode;
            }
            set
            {
                if (value != m_selectionMode)
                {
                    m_selectionMode = value;

                    // if new selectionMode is more limited than the "old" , clear existing selections
                    if (value < m_selectionMode)
                        ClearSelection();

                    if (this.SelectionModeChanged != null)
                        this.SelectionModeChanged(this, new EventArgs());

                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("The mouse button used for selections.")]
        [DefaultValue(typeof(MouseButtons), "Left")]
        public MouseButtons SelectButton
        {
            get
            {
                return m_selectButton;
            }
            set
            {
                if (value != m_selectButton)
                {
                    if (this.SelectButtonChanged != null)
                        this.SelectButtonChanged(this, new EventArgs());
                    m_selectButton = value;
                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("The minimum date that can be selected.")]
        [TypeConverter(typeof(DateTimeTypeConverter))]
        public DateTime MinDate
        {
            get
            {
                return m_minDate;
            }
            set
            {
                if (value != m_minDate)
                {
                    if (value <= m_maxDate)
                    {
                        if (this.MinDateChanged != null)
                            this.MinDateChanged(this, new EventArgs());
                        m_minDate = value;
                        Invalidate();
                    }
                }
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [Description("The maximum date that can be selected.")]
        [TypeConverter(typeof(DateTimeTypeConverter))]
        public DateTime MaxDate
        {
            get
            {
                return m_maxDate;
            }
            set
            {
                if (value != m_maxDate)
                {
                    if (value >= m_minDate)
                    {
                        m_maxDate = value;
                        if (this.MaxDateChanged != null)
                            this.MaxDateChanged(this, new EventArgs());
                        Invalidate();
                    }
                }
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Indicates wether the calendar should display week numbers.")]
        [DefaultValue(false)]
        public bool ShowWeeknumbers
        {
            get
            {
                return m_showWeeknumber;
            }
            set
            {
                if (value != m_showWeeknumber)
                {
                    m_showWeeknumber = value;
                    DoLayout();
                    if (this.ShowWeeknumberChanged != null)
                        this.ShowWeeknumberChanged(this, new EventArgs());
                    Invalidate();
                }
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Properties for header.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Header Header
        {
            get
            {
                return m_header;
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Properties for weekdays.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Weekday Weekdays
        {
            get
            {
                return m_weekday;
            }
        }

        [Browsable(true)]
        [Category("Behavior")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("")]
        public ActiveMonth ActiveMonth
        {
            get
            {
                return m_activeMonth;
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Properties for week numbers.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Weeknumber Weeknumbers
        {
            get
            {
                return m_weeknumber;
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Properties for month.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Month Month
        {
            get
            {
                return m_month;
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("Properties for footer.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Footer Footer
        {
            get
            {
                return m_footer;
            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The borderstyle used for the calendar.")]
        [DefaultValue(typeof(ButtonBorderStyle), "Solid")]
        public ButtonBorderStyle BorderStyle
        {
            get
            {
                return m_borderStyle;
            }
            set
            {
                if (value != m_borderStyle)
                {
                    m_borderStyle = value;
                    if (this.BorderStyleChanged != null)
                        this.BorderStyleChanged(this, new EventArgs());
                    Invalidate();
                }

            }
        }

        [Browsable(true)]
        [Category("Appearance")]
        [Description("The color used for the border.")]
        [DefaultValue(typeof(Color), "Black")]
        public Color BorderColor
        {
            get
            {
                return m_borderColor;
            }
            set
            {
                if (value != m_borderColor)
                {
                    m_borderColor = value;
                    if (this.CalendarColorChanged != null)
                        this.CalendarColorChanged(this, new CalendarColorEventArgs(mcCalendarColor.Border));
                    Invalidate();
                }

            }
        }


        [Browsable(true)]
        [Category("Appearance")]
        [Description("Indicates wether the calendar should display the footer.")]
        [DefaultValue(true)]
        public bool ShowFooter
        {
            get
            {
                return m_showFooter;
            }
            set
            {
                if (value != m_showFooter)
                {
                    m_showFooter = value;
                    DoLayout();
                    if (this.ShowFooterChanged != null)
                        this.ShowFooterChanged(this, new EventArgs());
                    Invalidate();
                }

            }
        }
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Indicates wether the calendar should display the header.")]
        [DefaultValue(true)]
        public bool ShowHeader
        {
            get
            {
                return m_showHeader;
            }
            set
            {
                if (value != m_showHeader)
                {
                    m_showHeader = value;
                    DoLayout();
                    if (this.ShowHeaderChanged != null)
                        this.ShowHeaderChanged(this, new EventArgs());
                    Invalidate();
                }

            }
        }
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Indicates wether the calendar should display weekdays.")]
        [DefaultValue(true)]
        public bool ShowWeekdays
        {
            get
            {
                return m_showWeekday;
            }
            set
            {
                if (value != m_showWeekday)
                {
                    m_showWeekday = value;
                    DoLayout();
                    if (this.ShowWeekdaysChanged != null)
                        this.ShowWeekdaysChanged(this, new EventArgs());
                    Invalidate();
                }

            }
        }

        [Category("Behavior")]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public KeyboardConfig Keyboard
        {
            get
            {
                return m_keyboard;
            }
        }

        #region Obsolete properties
        // obsolete properties

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ObsoleteAttribute("This property is not supported", true)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ObsoleteAttribute("This property is not supported", true)]
        public override RightToLeft RightToLeft
        {
            get
            {
                return base.RightToLeft;
            }
            set
            {
                base.RightToLeft = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ObsoleteAttribute("This property is not supported", true)]
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ObsoleteAttribute("This property is not supported", true)]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ObsoleteAttribute("This property is not supported", true)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [ObsoleteAttribute("This property is not supported", true)]
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }

        #endregion

        #endregion

        #region Overrides


        public override bool PreProcessMessage(ref Message msg)
        {

            // Check if message is KEY_DOWN
            if (msg.Msg == TXMonthCalendar.NativeMethods.WM_KEYDOWN)
            {
                Keys keyData = ((Keys)(int)msg.WParam) | ModifierKeys;
                Keys keyCode = ((Keys)(int)msg.WParam);
                // Make sure we handle certain keys
                switch (keyCode)
                {

                    default:
                        {
                            if ((keyCode == m_keyboard.Up) ||
                                 (keyCode == m_keyboard.Down) ||
                                 (keyCode == m_keyboard.Left) ||
                                 (keyCode == m_keyboard.Right) ||
                                 (keyCode == m_keyboard.Select) ||
                                 (keyCode == m_keyboard.NextMonth) ||
                                 (keyCode == m_keyboard.NextYear) ||
                                 (keyCode == m_keyboard.PreviousMonth) ||
                                 (keyCode == m_keyboard.PreviousYear))
                            {
                                if (m_keyHandled)
                                    return false;

                            }


                            break;
                        }

                }
            }

            return base.PreProcessMessage(ref msg);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if ((m_month.DayInFocus != -1) && (m_activeRegion != mcCalendarRegion.Month))
            {

                m_month.m_days[m_month.DayInFocus].State = mcDayState.Normal;
                m_month.DayInFocus = -1;
                Invalidate();
            }
            base.OnLostFocus(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            this.Focus();
            if ((m_month.DayInFocus == -1) && (m_activeRegion != mcCalendarRegion.Month))
            {
                for (int i = 0; i < 42; i++)
                {
                    if (m_month.m_days[i].State == mcDayState.Normal)
                    {
                        m_month.DayInFocus = i;
                        m_month.m_days[m_month.DayInFocus].State = mcDayState.Focus;
                        Invalidate();
                        break;
                    }
                }
            }
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnEnter(e);
        }


        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            base.OnDragDrop(drgevent);

            int day = m_month.GetDay(m_mouseLocation);
            if (day != -1)
            {
                if (DayDragDrop != null)
                    DayDragDrop(this, new DayDragDropEventArgs(drgevent.Data, drgevent.KeyState, m_month.m_days[day].Date.ToShortDateString()));

            }
        }

        protected override void OnDragEnter(DragEventArgs drgevent)
        {
            base.OnDragEnter(drgevent);
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            base.OnDragOver(drgevent);
            if ((drgevent.AllowedEffect & DragDropEffects.Move) == DragDropEffects.Move)
            {
                // By default, the drop action should be move, if allowed.
                drgevent.Effect = DragDropEffects.Move;
            }
        }

        protected override void OnDragLeave(EventArgs e)
        {
            base.OnDragLeave(e);
        }


        protected override void OnSystemColorsChanged(EventArgs e)
        {
            base.OnSystemColorsChanged(e);
            if (Theme)
                GetThemeColors();
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {

            base.WndProc(ref m);
            switch (m.Msg)
            {
                case NativeMethods.WM_THEMECHANGED:
                    {
                        // Theme has changed , get new colors if Theme = true
                        if (Theme)
                            GetThemeColors();
                        break;
                    }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // set location and button
            m_mouseLocation = new Point(e.X, e.Y);
            m_mouseButton = e.Button;

            m_month.Click(m_mouseLocation, m_mouseButton);

            if (ShowHeader) m_header.MouseClick(m_mouseLocation, m_mouseButton, mcClickMode.Single);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (ShowHeader) m_header.MouseUp();
            m_month.MouseUp();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            // set location and button
            m_mouseLocation = new Point(e.X, e.Y);
            m_mouseButton = e.Button;

            if (ShowHeader) m_header.MouseMove(m_mouseLocation);
            m_month.MouseMove(m_mouseLocation);
            if (ShowFooter) m_footer.MouseMove(m_mouseLocation);
            if (ShowWeekdays) m_weekday.MouseMove(m_mouseLocation);
            if (ShowWeeknumbers) m_weeknumber.MouseMove(m_mouseLocation);

        }

        protected override void OnMouseLeave(EventArgs e)
        {
            ActiveRegion = mcCalendarRegion.None;
            m_month.RemoveFocus();
            base.OnMouseLeave(e);
            Invalidate();
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            this.Focus();
            if (ShowWeekdays) m_weekday.MouseClick(m_mouseLocation, m_mouseButton, mcClickMode.Single);
            if (ShowWeeknumbers) m_weeknumber.MouseClick(m_mouseLocation, m_mouseButton, mcClickMode.Single);
            if (ShowFooter) m_footer.MouseClick(m_mouseLocation, m_mouseButton, mcClickMode.Single);

        }

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);

            m_month.DoubleClick(m_mouseLocation, m_mouseButton);

            if (ShowWeekdays) m_weekday.MouseClick(m_mouseLocation, m_mouseButton, mcClickMode.Double);
            if (ShowWeeknumbers) m_weeknumber.MouseClick(m_mouseLocation, m_mouseButton, mcClickMode.Double);
            if (ShowHeader) m_header.MouseClick(m_mouseLocation, m_mouseButton, mcClickMode.Double);
            if (ShowFooter) m_footer.MouseClick(m_mouseLocation, m_mouseButton, mcClickMode.Double);

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Draw(e.Graphics, e.ClipRectangle);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            DoLayout();
        }

        #endregion

        #region Events

        private void m_hook_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    {
                        if (m_extendedKey == mcExtendedSelectionKey.Ctrl)
                            m_ctrlKey = true;
                        break;
                    }
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    {
                        if (m_extendedKey == mcExtendedSelectionKey.Shift)
                            m_ctrlKey = true;
                        break;
                    }
                case Keys.LMenu:
                    {
                        if (m_extendedKey == mcExtendedSelectionKey.Alt)
                            m_ctrlKey = true;
                        break;
                    }

                default:
                    {
                        if (m_keyboardEnabled)
                        {
                            if (Focused)
                            {
                                m_keyHandled = false;
                                if (e.KeyCode == m_keyboard.Left)
                                {
                                    if (!m_selectKeyDown)
                                        MoveFocus(-1);
                                    else if ((m_month.DayInFocus - 1 >= 0) && (m_month.m_days[m_month.DayInFocus - 1].Rectangle.Y == m_month.m_days[m_month.DayInFocus].Rectangle.Y))
                                        m_month.FocusMoved(m_month.DayInFocus - 1);
                                    m_keyHandled = true;
                                }
                                if (e.KeyCode == m_keyboard.Right)
                                {
                                    if (!m_selectKeyDown)
                                        MoveFocus(1);
                                    else if ((m_month.DayInFocus + 1 <= 41) && (m_month.m_days[m_month.DayInFocus + 1].Rectangle.Y == m_month.m_days[m_month.DayInFocus].Rectangle.Y))
                                        m_month.FocusMoved(m_month.DayInFocus + 1);
                                    m_keyHandled = true;
                                }
                                if (e.KeyCode == m_keyboard.Up)
                                {
                                    if (!m_selectKeyDown)
                                        MoveFocus(-7);
                                    else if (m_month.DayInFocus - 7 >= 0)
                                        m_month.FocusMoved(m_month.DayInFocus - 7);
                                    m_keyHandled = true;
                                }
                                if (e.KeyCode == m_keyboard.Down)
                                {
                                    if (!m_selectKeyDown)
                                        MoveFocus(7);
                                    else if (m_month.DayInFocus + 7 <= 41)
                                        m_month.FocusMoved(m_month.DayInFocus + 7);
                                    m_keyHandled = true;
                                }
                                if (e.KeyCode == m_keyboard.NextMonth)
                                {
                                    m_keyHandled = true;
                                    ChangeMonth(1);
                                }
                                if (e.KeyCode == m_keyboard.PreviousMonth)
                                {
                                    m_keyHandled = true;
                                    ChangeMonth(-1);
                                }
                                if (e.KeyCode == m_keyboard.NextYear)
                                {
                                    m_keyHandled = true;
                                    ChangeMonth(12);
                                }
                                if (e.KeyCode == m_keyboard.PreviousYear)
                                {
                                    m_keyHandled = true;
                                    ChangeMonth(-12);
                                }
                                if ((e.KeyCode == m_keyboard.Select) && (m_month.DayInFocus != -1))
                                {
                                    m_keyHandled = true;
                                    if (!m_selectKeyDown)
                                    {
                                        m_selectKeyDown = true;


                                        m_month.DaySelect(m_month.DayInFocus, m_selectButton,
                                                          new Point(m_month.m_days[m_month.DayInFocus].Rectangle.X + 1,
                                                                    m_month.m_days[m_month.DayInFocus].Rectangle.Y + 1));
                                    }

                                }

                            }
                        }

                        break;
                    }
            }
        }

        private void m_hook_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    {
                        if (m_extendedKey == mcExtendedSelectionKey.Ctrl)
                            m_ctrlKey = false;
                        break;
                    }
                case Keys.LShiftKey:
                case Keys.RShiftKey:
                    {
                        if (m_extendedKey == mcExtendedSelectionKey.Shift)
                            m_ctrlKey = false;
                        break;
                    }
                case Keys.LMenu:
                    {
                        if (m_extendedKey == mcExtendedSelectionKey.Alt)
                            m_ctrlKey = false;
                        break;
                    }

                default:
                    {
                        if (m_keyboardEnabled)
                        {
                            if (Focused)
                            {
                                if (e.KeyCode == m_keyboard.Select)
                                {
                                    m_selectKeyDown = false;
                                    m_month.MouseUp();
                                }
                            }
                        }
                        break;
                    }

            }
        }

        private void MoveFocus(int step)
        {
            int focus = m_month.DayInFocus;
            if ((focus + step >= 0) && (focus + step <= 41))
            {
                m_keyHandled = true;
                m_month.DayInFocus = focus + step;
                if (m_month.m_days[focus + step].State == mcDayState.Normal)
                    m_month.m_days[focus + step].State = mcDayState.Focus;
                if ((focus <= 41) && (focus >= 0) && (m_month.m_days[focus].State == mcDayState.Focus))
                    m_month.m_days[focus].State = mcDayState.Normal;

                Invalidate();
            }
            else m_keyHandled = false;

        }


        private SelectedDatesCollection UpdateSelectedCollection()
        {
            m_selectedDates.Clear();

            for (int i = 0; i < 42; i++)
            {
                if (m_month.m_days[i].State == mcDayState.Selected)
                {
                    m_selectedDates.Add(m_month.m_days[i].Date);
                }
            }

            return m_selectedDates;
        }

        private void m_printDoc_BeginPrint(object sender, PrintEventArgs e)
        {

        }

        private void m_printDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bmp;
            bmp = Snapshot();
            e.Graphics.DrawImage(bmp, 1, 1, bmp.Width, bmp.Height);
            e.HasMorePages = false;
            bmp.Dispose();
        }

        private void m_printDoc_QueryPageSettings(object sender, QueryPageSettingsEventArgs e)
        {

        }

        private void m_activeMonth_MonthChanged(object sender, MonthChangedEventArgs e)
        {
            m_month.RemoveSelection(true);
            if (this.MonthChanged != null)
                MonthChanged(this, e);
        }

        void m_activeMonth_BeforeMonthChanged(object sender, BeforeMonthChangedEventArgs e)
        {
            if (this.BeforeMonthChanged != null)
                BeforeMonthChanged(this, e);
        }


        private void m_month_DayRender(object sender, DayRenderEventArgs e)
        {
            if (this.DayRender != null)
                this.DayRender(this, e);
        }

        private void m_month_DayQueryInfo(object sender, DayQueryInfoEventArgs e)
        {
            if (this.DayQueryInfo != null)
                this.DayQueryInfo(this, e);
        }

        private void m_month_DaySelected(object sender, DaySelectedEventArgs e)
        {
            if (this.DaySelected != null)
                this.DaySelected(this, e);
        }

        private void m_month_DayDeselected(object sender, DaySelectedEventArgs e)
        {
            if (this.DayDeselected != null)
                this.DayDeselected(this, e);
        }

        void m_month_BeforeDayDeselected(object sender, DayStateChangedEventArgs e)
        {
            if (BeforeDayDeselected != null)
                BeforeDayDeselected(this, e);
        }

        void m_month_BeforeDaySelected(object sender, DayStateChangedEventArgs e)
        {
            if (BeforeDaySelected != null)
                BeforeDaySelected(this, e);

        }

        private void m_month_DayLostFocus(object sender, DayEventArgs e)
        {
            if (this.DayLostFocus != null)
                this.DayLostFocus(this, e);
        }

        private void m_month_DayGotFocus(object sender, DayEventArgs e)
        {
            if (this.DayGotFocus != null)
                this.DayGotFocus(this, e);
        }

        private void m_month_ImageClick(object sender, DayClickEventArgs e)
        {
            if (this.ImageClick != null)
                this.ImageClick(this, e);
        }

        private void m_month_DayClick(object sender, DayClickEventArgs e)
        {
            if (this.DayClick != null)
                this.DayClick(this, e);
        }

        private void m_month_DayMouseMove(object sender, DayMouseMoveEventArgs e)
        {
            if (this.DayMouseMove != null)
                this.DayMouseMove(this, e);
        }

        private void m_month_DayDoubleClick(object sender, DayClickEventArgs e)
        {
            if (this.DayDoubleClick != null)
                this.DayDoubleClick(this, e);
        }

        private void m_footer_Click(object sender, ClickEventArgs e)
        {
            if (this.FooterClick != null)
                this.FooterClick(this, e);
        }

        private void m_footer_DoubleClick(object sender, ClickEventArgs e)
        {
            if (this.FooterDoubleClick != null)
                this.FooterDoubleClick(this, e);
        }

        private void m_header_Click(object sender, ClickEventArgs e)
        {
            if (this.HeaderClick != null)
                this.HeaderClick(this, e);
        }

        private void m_header_DoubleClick(object sender, ClickEventArgs e)
        {
            if (this.HeaderDoubleClick != null)
                this.HeaderDoubleClick(this, e);
        }

        private void m_header_PrevMonthButtonClick(object sender, EventArgs e)
        {
            ChangeMonth(-1);
        }

        private void m_header_NextMonthButtonClick(object sender, EventArgs e)
        {
            ChangeMonth(1);
        }

        private void ChangeMonth(int step)
        {
            int oldMonth = m_month.SelectedMonth.Month;
            int oldYear = m_month.SelectedMonth.Year;
            DateTime m = m_month.SelectedMonth.AddMonths(step);
            int month = m.Month;
            int year = m.Year;
            m_activeMonth.RaiseEvent = false;
            m_activeMonth.Year = year;
            // check if user changed year
            if (oldMonth == month)
                // if so , make sure the BeforeMonthChanged event will trigger for month
                if (month + 1 < 12)
                    m_activeMonth.Month = month + 1;
                else
                    m_activeMonth.Month = month - 1;
            m_activeMonth.RaiseEvent = true;
            m_activeMonth.Month = month;
            if ((m_activeMonth.Month == month) && (m_activeMonth.Year == year))
                m_month.SelectedMonth = m;
            else
            {
                m_activeMonth.RaiseEvent = false;
                m_activeMonth.Month = oldMonth;
                m_activeMonth.Year = oldYear;
                m_activeMonth.RaiseEvent = true;
            }

        }

        private void m_header_PrevYearButtonClick(object sender, EventArgs e)
        {
            ChangeMonth(-12);

        }

        private void m_header_NextYearButtonClick(object sender, EventArgs e)
        {
            ChangeMonth(12);
        }

        private void m_weekday_Click(object sender, WeekdayClickEventArgs e)
        {
            if (this.WeekdayClick != null)
                this.WeekdayClick(this, e);
        }

        private void m_weekday_DoubleClick(object sender, WeekdayClickEventArgs e)
        {
            if (this.WeekdayDoubleClick != null)
                this.WeekdayDoubleClick(this, e);
        }

        private void m_weeknumber_DoubleClick(object sender, WeeknumberClickEventArgs e)
        {
            if (this.WeeknumberDoubleClick != null)
                this.WeeknumberDoubleClick(this, e);
        }

        private void m_weeknumber_Click(object sender, WeeknumberClickEventArgs e)
        {
            if (this.WeeknumberClick != null)
                this.WeeknumberClick(this, e);
        }

        private void m_footer_PropertyChanged(object sender, FooterPropertyEventArgs e)
        {
            if (this.FooterPropertyChanged != null)
                this.FooterPropertyChanged(this, e);
        }

        private void m_weeknumber_PropertyChanged(object sender, WeeknumberPropertyEventArgs e)
        {
            if (this.WeeknumberPropertyChanged != null)
                this.WeeknumberPropertyChanged(this, e);
        }

        private void m_weekday_PropertyChanged(object sender, WeekdayPropertyEventArgs e)
        {
            if (this.WeekdayPropertyChanged != null)
                this.WeekdayPropertyChanged(this, e);
        }

        private void m_header_PropertyChanged(object sender, HeaderPropertyEventArgs e)
        {
            if (this.HeaderPropertyChanged != null)
                this.HeaderPropertyChanged(this, e);
        }

        private void m_month_PropertyChanged(object sender, MonthPropertyEventArgs e)
        {
            if (this.MonthPropertyChanged != null)
                this.MonthPropertyChanged(this, e);
        }

        private void m_month_ColorChanged(object sender, MonthColorEventArgs e)
        {
            if (this.MonthColorChanged != null)
                this.MonthColorChanged(this, e);
        }

        private void m_month_BorderStyleChanged(object sender, MonthBorderStyleEventArgs e)
        {
            if (this.MonthBorderStyleChanged != null)
                this.MonthBorderStyleChanged(this, e);
        }

        private void m_dateItemCollection_DateItemModified(object sender, EventArgs e)
        {
            Invalidate();
        }


        #endregion

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }
        #endregion


        #region KeyboardConfig
        [TypeConverter(typeof(KeyboardTypeConverter))]
        public class KeyboardConfig
        {

            #region variables

            private MonthCalendar m_parent;
            private Keys m_up;
            private Keys m_down;
            private Keys m_left;
            private Keys m_right;
            private Keys m_select;
            private Keys m_nextMonth;
            private Keys m_prevMonth;
            private Keys m_nextYear;
            private Keys m_prevYear;

            #endregion

            #region Contructor

            public KeyboardConfig(MonthCalendar calendar)
            {
                m_up = Keys.Up;
                m_parent = calendar;
                m_down = Keys.Down;
                m_left = Keys.Left;
                m_right = Keys.Right;
                m_select = Keys.Space;
                m_nextMonth = Keys.PageUp;
                m_prevMonth = Keys.PageDown;
                m_nextYear = Keys.Home;
                m_prevYear = Keys.End;

            }


            #endregion

            #region Properties

            [Description("Key used to move up.")]
            [DefaultValue(typeof(Keys), "Up")]
            public Keys Up
            {
                get
                {
                    return m_up;
                }
                set
                {
                    if (m_up != value)
                    {
                        KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.Up, m_up, value);
                        m_up = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);
                    }
                }
            }

            [Description("Key used to move down.")]
            [DefaultValue(typeof(Keys), "Down")]
            public Keys Down
            {
                get
                {
                    return m_down;
                }
                set
                {
                    KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.Down, m_down, value);
                    if (m_down != value)
                    {
                        m_down = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);

                    }
                }
            }

            [Description("Key used to move left.")]
            [DefaultValue(typeof(Keys), "Left")]
            public Keys Left
            {
                get
                {
                    return m_left;
                }
                set
                {
                    KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.Left, m_left, value);
                    if (m_left != value)
                    {
                        m_left = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);

                    }
                }
            }

            [Description("Key used to move right.")]
            [DefaultValue(typeof(Keys), "Right")]
            public Keys Right
            {
                get
                {
                    return m_right;
                }
                set
                {
                    KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.Right, m_right, value);
                    if (m_right != value)
                    {
                        m_right = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);

                    }
                }
            }

            [Description("Key used to select.")]
            [DefaultValue(typeof(Keys), "Space")]
            public Keys Select
            {
                get
                {
                    return m_select;
                }
                set
                {
                    KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.Select, m_select, value);
                    if (m_select != value)
                    {
                        m_select = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);

                    }
                }
            }

            [Description("Key used for next month.")]
            [DefaultValue(typeof(Keys), "PageUp")]
            public Keys NextMonth
            {
                get
                {
                    return m_nextMonth;
                }
                set
                {
                    KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.NextMonth, m_nextMonth, value);
                    if (m_nextMonth != value)
                    {
                        m_nextMonth = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);

                    }
                }
            }

            [Description("Key used for previous month.")]
            [DefaultValue(typeof(Keys), "PageDown")]
            public Keys PreviousMonth
            {
                get
                {
                    return m_prevMonth;
                }
                set
                {
                    KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.PreviousMonth, m_prevMonth, value);
                    if (m_prevMonth != value)
                    {
                        m_prevMonth = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);

                    }
                }
            }

            [Description("Key used for next year.")]
            [DefaultValue(typeof(Keys), "Home")]
            public Keys NextYear
            {
                get
                {
                    return m_nextYear;
                }
                set
                {
                    KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.NextYear, m_nextYear, value);
                    if (m_nextYear != value)
                    {
                        m_nextYear = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);

                    }
                }
            }


            [Description("Key used for previous year.")]
            [DefaultValue(typeof(Keys), "End")]
            public Keys PreviousYear
            {
                get
                {
                    return m_prevYear;
                }
                set
                {
                    KeyboardChangedEventArgs args = new KeyboardChangedEventArgs(mcKeyboard.PreviousYear, m_prevYear, value);
                    if (m_prevYear != value)
                    {
                        m_prevYear = value;
                        if (m_parent.KeyboardChanged != null)
                            m_parent.KeyboardChanged(this, args);

                    }
                }
            }

            #endregion

        }

        #endregion

    }


    #region ActiveMonth

    [TypeConverter(typeof(ActiveMonthTypeConverter))]
    public class ActiveMonth
    {
        private int m_month;
        private int m_year;
        private MonthCalendar m_calendar;
        private bool m_raiseEvent = true;

        internal event MonthChangedEventHandler MonthChanged;
        internal event BeforeMonthChangedEventHandler BeforeMonthChanged;

        [Browsable(false)]
        public MonthCalendar Calendar
        {
            get
            {
                return m_calendar;
            }
        }

        [Browsable(false)]
        internal bool RaiseEvent
        {
            set
            {
                m_raiseEvent = value;
            }
        }

        public ActiveMonth(MonthCalendar calendar)
        {
            m_calendar = calendar;
            m_year = DateTime.Now.Year;
            m_month = DateTime.Now.Month;
        }

        [Description("Current month.")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(MonthConverter))]
        public int Month
        {
            get
            {
                return m_month;
            }
            set
            {
                if (m_month != value)
                {
                    BeforeMonthChangedEventArgs args = new BeforeMonthChangedEventArgs(m_year, value, m_year, m_month);
                    if ((BeforeMonthChanged != null) && (m_raiseEvent))
                        BeforeMonthChanged(this, args);
                    if (!args.Cancel)
                    {
                        m_month = value;
                        ChangeMonth();
                    }
                }
            }
        }

        [Description("Current calendar year.")]
        [RefreshProperties(RefreshProperties.All)]
        [TypeConverter(typeof(YearConverter))]
        public int Year
        {
            get
            {
                return m_year;
            }
            set
            {
                if (m_year != value)
                {
                    BeforeMonthChangedEventArgs args = new BeforeMonthChangedEventArgs(value, m_month, m_year, m_month);
                    if ((BeforeMonthChanged != null) && (m_raiseEvent))
                        BeforeMonthChanged(this, args);
                    if (!args.Cancel)
                    {
                        m_year = value;
                        ChangeMonth();
                    }
                }
            }
        }

        private void ChangeMonth()
        {
            m_calendar.Month.SelectedMonth = DateTime.Parse(m_year.ToString() + "-" + m_month.ToString() + "-01");
            m_calendar.Setup();
            if ((MonthChanged != null) && (m_raiseEvent))
                MonthChanged(this, new MonthChangedEventArgs(m_month, m_year));
            m_calendar.Invalidate();
        }

    }

    #endregion

    #region FirstDayOfWeekConverter

    internal class FirstDayOfWeekConverter : StringConverter
    {

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string[] validNames = new string[8];
                string[] dayNames = culture.DateTimeFormat.DayNames;
                validNames.Initialize();

                validNames[0] = "Default";

                for (int i = 1; i <= 7; i++)
                    validNames[i] = dayNames[i - 1];

                if (value.GetType() == typeof(string))
                {
                    for (int i = 0; i < validNames.Length; i++)
                    {
                        if (value.ToString().CompareTo(validNames[i]) == 0)
                            return validNames[i];
                    }
                }
                else if (value.GetType() == typeof(int))
                {
                    int m = Convert.ToInt32(value);

                    if ((m >= 0) && (m <= 7))
                    {
                        return validNames[m];
                    }
                }
            }
            return new DateTime();

        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            int ret;
            if (value.GetType() == typeof(string))
            {
                MonthCalendar m = (MonthCalendar)context.Instance;
                ret = m.DayNumber(value.ToString());
                if ((ret >= 0) && (ret <= 7))
                    return ret;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // Allow user to type the value
            return false;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection
            GetStandardValues(ITypeDescriptorContext context)
        {

            MonthCalendar m = (MonthCalendar)context.Instance;

            return new StandardValuesCollection(m.DayNames());
        }

    }

    #endregion


    #region MonthConverter

    internal class MonthConverter : StringConverter
    {

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {

                string[] validNames;
                validNames = culture.DateTimeFormat.MonthNames;
                if (value.GetType() == typeof(string))
                {
                    for (int i = 0; i < validNames.Length; i++)
                    {
                        if (value.ToString().CompareTo(validNames[i]) == 0)
                            //if ((value.ToString().ToLower() == validNames[i].ToLower())) 
                            return validNames[i];
                    }
                }
                else if (value.GetType() == typeof(int))
                {
                    int m = Convert.ToInt32(value);

                    if ((m >= 1) && (m <= 12))
                    {
                        return validNames[m - 1];
                    }
                }
            }
            return new DateTime();

        }


        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            int ret = 0;
            if (value.GetType() == typeof(string))
            {
                ActiveMonth m = (ActiveMonth)context.Instance;
                ret = m.Calendar.MonthNumber(value.ToString());
                if ((ret >= 1) && (ret <= 12))
                    return ret;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // Allow user to type the value
            return false;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection
            GetStandardValues(ITypeDescriptorContext context)
        {

            ActiveMonth m = (ActiveMonth)context.Instance;

            return new StandardValuesCollection(m.Calendar.AllowedMonths());
        }

    }
    #endregion

    #region YearConverter

    internal class YearConverter : StringConverter
    {

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {

            if (destinationType == typeof(string))
            {
                return Convert.ToString(value);
            }
            else throw new ArgumentException("Invalid");

        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                ActiveMonth m = (ActiveMonth)context.Instance;
                if (m.Calendar.IsYearValid(value.ToString()))
                    return Convert.ToInt32(value);
            }
            return base.ConvertFrom(context, culture, value);

        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            // Return true to allow standard values.
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // Allow user to type the value
            return false;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection
            GetStandardValues(ITypeDescriptorContext context)
        {

            ActiveMonth m = (ActiveMonth)context.Instance;

            return new StandardValuesCollection(m.Calendar.AllowedYears());
        }
    }

    #endregion

    #region ActiveMonthTypeConverter

    internal class ActiveMonthTypeConverter : ExpandableObjectConverter
    {

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            if (sourceType == typeof(DateTime))
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
                    // Create new ActiveMonth
                    ActiveMonth item;
                    MonthCalendar m = (MonthCalendar)context.Instance;
                    item = m.ActiveMonth;
                    // Set properties
                    item.Month = item.Calendar.MonthNumber(ss[0]);
                    if (item.Calendar.IsYearValid(ss[1].Trim()))
                    {
                        item.Year = Convert.ToInt32(ss[1].Trim());
                        return item;
                    }

                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {

            if (destinationType == typeof(string))
            {
                // cast value to ActiveMonth
                ActiveMonth dest = (ActiveMonth)value;
                // create property string
                return dest.Calendar.MonthName(dest.Month) + "; " + dest.Year;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }


    #endregion

    #region SimpleTypeConverter

    public class SimpleTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return "";
        }

    }

    #endregion

    #region MonthTypeConverter

    internal class MonthTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return "";
        }

    }

    #endregion

    #region WeekdayTypeConverter

    internal class WeekdayTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return "";
        }

    }

    #endregion

    #region WeeknumberTypeConverter

    internal class WeeknumberTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return "";
        }

    }

    #endregion

    #region HeaderTypeConverter

    internal class HeaderTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return "";
        }

    }

    #endregion

    #region FooterTypeConverter

    internal class FooterTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return "";
        }

    }

    #endregion


    #region DateTimeTypeConverter

    internal class DateTimeTypeConverter : ExpandableObjectConverter
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

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                return DateTime.Parse((string)value);
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                MonthCalendar cal = (MonthCalendar)context.Instance;
                DateTime date = (DateTime)value;
                return date.ToString(cal.m_dateTimeFormat.ShortDatePattern);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }

    #endregion

    #region MonthChangedEventArgs

    public class MonthChangedEventArgs : EventArgs
    {
        #region Class Data

        private int m_month;
        private int m_year;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DayClickEventArgs class with default settings
        /// </summary>
        public MonthChangedEventArgs()
        {

        }

        public MonthChangedEventArgs(int month, int year)
        {
            this.m_month = month;
            this.m_year = year;
        }

        #endregion


        #region Properties

        public int Month
        {
            get
            {
                return m_month;
            }
        }

        public int Year
        {
            get
            {
                return m_year;
            }
        }

        #endregion
    }


    #endregion

    #region DayRenderEventArgs

    public class DayRenderEventArgs : EventArgs
    {
        #region Class Data

        private Graphics m_graphics;
        private bool m_ownerDraw;
        private Rectangle m_rect;
        private DateTime m_date;
        private mcDayState m_state;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DayRenderEventArgs class with default settings
        /// </summary>
        public DayRenderEventArgs()
        {
        }

        public DayRenderEventArgs(Graphics graphics, Rectangle rect, DateTime date, mcDayState state)
        {
            this.m_graphics = graphics;
            this.m_rect = rect;
            this.m_date = date;
            this.m_state = state;
        }

        #endregion


        #region Properties

        public Graphics Graphics
        {
            get
            {
                return m_graphics;
            }
        }


        public DateTime Date
        {
            get
            {
                return m_date;
            }
        }

        public int Width
        {
            get
            {
                return m_rect.Width;
            }
        }

        public mcDayState State
        {
            get
            {
                return m_state;
            }
        }

        public int Height
        {
            get
            {
                return m_rect.Height;
            }
        }

        public bool OwnerDraw
        {
            set
            {
                m_ownerDraw = value;
            }
            get
            {
                return m_ownerDraw;
            }
        }

        #endregion
    }


    #endregion

    #region DayQueryInfoEventArgs

    public class DayQueryInfoEventArgs : EventArgs
    {
        #region Class Data

        private DateItem m_info;
        private bool m_ownerDraw;
        private mcDayState m_state;
        private DateTime m_date;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DayRenderEventArgs class with default settings
        /// </summary>
        public DayQueryInfoEventArgs()
        {
        }

        public DayQueryInfoEventArgs(DateItem info, DateTime date, mcDayState state)
        {
            this.m_info = info;
            this.m_date = date;
            this.m_state = state;
        }

        #endregion


        #region Properties

        public DateItem Info
        {
            get
            {
                return m_info;
            }
        }


        public DateTime Date
        {
            get
            {
                return m_date;
            }
        }

        public mcDayState State
        {
            get
            {
                return m_state;
            }
        }

        public bool OwnerDraw
        {
            set
            {
                m_ownerDraw = value;
            }
            get
            {
                return m_ownerDraw;
            }
        }

        #endregion
    }


    #endregion

    #region KeyboardTypeConverter

    public class KeyboardTypeConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            return "";
        }

    }

    #endregion


    #region ClickEventArgs

    public class ClickEventArgs : EventArgs
    {
        #region Class Data

        private MouseButtons m_button;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DayClickEventArgs class with default settings
        /// </summary>
        public ClickEventArgs()
        {
            m_button = MouseButtons.Left;
        }

        public ClickEventArgs(MouseButtons button)
        {
            this.m_button = button;
        }

        #endregion


        #region Properties

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

    #region CalendarColorEventArgs

    public class CalendarColorEventArgs : EventArgs
    {
        #region Class Data

        /// <summary>
        /// The color that has changed
        /// </summary>
        private mcCalendarColor m_color;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the MozItemEventArgs class with default settings
        /// </summary>
        public CalendarColorEventArgs()
        {
            m_color = 0;
        }


        public CalendarColorEventArgs(mcCalendarColor color)
        {
            this.m_color = color;
        }

        #endregion


        #region Properties

        public mcCalendarColor Color
        {
            get
            {
                return this.m_color;
            }
        }

        #endregion
    }


    #endregion

    #region BeforeMonthChangedEventArgs

    public class BeforeMonthChangedEventArgs : EventArgs
    {
        #region Class Data

        private bool m_cancel;
        private int m_oldYear;
        private int m_oldMonth;
        private int m_newYear;
        private int m_newMonth;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DayClickEventArgs class with default settings
        /// </summary>

        public BeforeMonthChangedEventArgs(int newYear, int newMonth, int oldYear, int oldMonth)
        {
            m_newYear = newYear;
            m_newMonth = newMonth;
            m_oldYear = oldYear;
            m_oldMonth = oldMonth;
            m_cancel = false;
        }

        #endregion


        #region Properties

        public int OldYear
        {
            get
            {
                return m_oldYear;
            }
        }

        public int OldMonth
        {
            get
            {
                return m_oldMonth;
            }
        }

        public int NewYear
        {
            get
            {
                return m_newYear;
            }
            set
            {
                m_newYear = value;
            }
        }

        public int NewMonth
        {
            get
            {
                return m_newMonth;
            }
            set
            {
                m_newMonth = value;
            }
        }


        public bool Cancel
        {
            get
            {
                return m_cancel;
            }
            set
            {
                m_cancel = value;
            }
        }




        #endregion
    }


    #endregion

    #region KeyboardChangeChangedEventArgs

    public class KeyboardChangedEventArgs : EventArgs
    {
        #region Class Data

        private mcKeyboard m_key;
        private Keys m_old;
        private Keys m_new;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the DayClickEventArgs class with default settings
        /// </summary>

        public KeyboardChangedEventArgs(mcKeyboard key, Keys oldKey, Keys newKey)
        {
            m_key = key;
            m_old = oldKey;
            m_new = newKey;
        }

        #endregion


        #region Properties

        public Keys NewKey
        {
            get
            {
                return m_new;
            }
        }

        public Keys OldKey
        {
            get
            {
                return m_old;
            }
        }

        public mcKeyboard Key
        {
            get
            {
                return m_key;
            }
        }


        #endregion
    }


    #endregion


    #region Designer

    // ControlDesigner

    public class MonthCalendarDesigner : System.Windows.Forms.Design.ControlDesigner
    {

        private DesignerActionListCollection actionLists;

        public MonthCalendarDesigner()
        {

        }

        [System.Obsolete]
        public override void OnSetComponentDefaults()
        {
            base.OnSetComponentDefaults();

        }

        // Use pull model to populate smart tag menu.
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(
                        new CalendarActionList(this.Component));
                }
                return actionLists;
            }
        }


        public override SelectionRules SelectionRules
        {
            get
            {
                // Remove all manual resizing of the control
                SelectionRules selectionRules = base.SelectionRules;
                selectionRules = SelectionRules.Visible | SelectionRules.AllSizeable | SelectionRules.Moveable;
                return selectionRules;
            }
        }

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);

            // Remove obsolete properties
            properties.Remove("BackColor");
            properties.Remove("Font");
            properties.Remove("BackgroundImage");
            properties.Remove("ForeColor");
            properties.Remove("Text");
            properties.Remove("RightToLeft");
            properties.Remove("ImeMode");
            properties.Remove("Padding");
            properties.Remove("BackgroundImageLayout");
        }

    }

    public class CalendarActionList : System.ComponentModel.Design.DesignerActionList
    {
        private MonthCalendar calendar;

        private DesignerActionUIService designerActionUISvc = null;

        //The constructor associates the control 
        //with the smart tag list.
        public CalendarActionList(IComponent component)
            : base(component)
        {
            this.calendar = component as MonthCalendar;

            // Cache a reference to DesignerActionUIService, so the
            // DesigneractionList can be refreshed.
            this.designerActionUISvc =
                GetService(typeof(DesignerActionUIService))
                as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of 
        // GetProperties enables undo and menu updates to work properly.
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(calendar)[propName];
            if (null == prop)
                throw new ArgumentException(
                     "Matching MonthCalendar property not found!",
                      propName);
            else
                return prop;
        }

        public DateItemCollection Dates
        {
            get
            {
                return calendar.Dates;
            }
        }

        public bool ShowHeader
        {
            get
            {
                return calendar.ShowHeader;
            }
            set
            {
                GetPropertyByName("ShowHeader").SetValue(calendar, value);
            }
        }

        public bool ShowFooter
        {
            get
            {
                return calendar.ShowFooter;
            }
            set
            {
                GetPropertyByName("ShowFooter").SetValue(calendar, value);
            }
        }

        public bool ShowWeekdays
        {
            get
            {
                return calendar.ShowWeekdays;
            }
            set
            {
                GetPropertyByName("ShowWeekdays").SetValue(calendar, value);
            }
        }

        public bool ShowWeeknumbers
        {
            get
            {
                return calendar.ShowWeeknumbers;
            }
            set
            {
                GetPropertyByName("ShowWeeknumbers").SetValue(calendar, value);
            }
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));
            items.Add(new DesignerActionHeaderItem("Information"));

            items.Add(new DesignerActionPropertyItem("Dates",
                             "Dates", "Appearance",
                             "Collection with formatted dates."));

            items.Add(new DesignerActionPropertyItem("ShowHeader",
                             "ShowHeader", "Appearance",
                             "Indicates wether the header should be visible."));

            items.Add(new DesignerActionPropertyItem("ShowWeekdays",
                             "ShowWeekdays", "Appearance",
                             "Indicates wether weekdays should be visible."));

            items.Add(new DesignerActionPropertyItem("ShowWeeknumbers",
                            "ShowWeeknumbers", "Appearance",
                             "Indicates wether the weeknumbers should be visible."));

            items.Add(new DesignerActionPropertyItem("ShowFooter",
                             "ShowFooter", "Appearance",
                             "Indicates wether the footer should be visible."));


            //Create entries for static Information section.
            StringBuilder version = new StringBuilder("Version: ");
            version.Append(calendar.Version);
            items.Add(new DesignerActionTextItem(version.ToString(), "Information"));

            return items;
        }
    }


    #endregion

}
