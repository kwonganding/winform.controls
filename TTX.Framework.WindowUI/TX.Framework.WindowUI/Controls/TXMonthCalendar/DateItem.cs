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
using System.Windows;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Drawing;

namespace TX.Framework.WindowUI.Controls
{

    public enum mcDayInfoRecurrence { None = 0, Daily, Weekly, Monthly, Yearly }

    [TypeConverter(typeof(DateItemTypeConverter))]
    [DesignTimeVisible(false)]
    [ToolboxItem(false)]
    public class DateItem : IComponent
    {

        #region Private class members
        public event EventHandler Disposed;

        private DateTime m_date;
        private DateTime m_rangeDate;

        private bool disposed;
        private Color m_backColor1;
        private Color m_backColor2;
        private mcGradientMode m_gradientMode;
        private Color m_dateColor;
        private Color m_textColor;
        private string m_text;
        private int m_imageIndex;
        private Image m_image;
        private bool m_weekend;
        private bool m_enabled;
        private bool m_bolded;
        private ISite m_site;
        private MonthCalendar m_calendar;
        private mcDayInfoRecurrence m_pattern;
        private int m_index;
        private Image m_bgImage;
        private object m_tag;

        #endregion

        #region Constructor

        public DateItem()
        {
            m_imageIndex = -1;
            m_backColor1 = Color.FromArgb( 128, 255, 128 );
            m_backColor2 = Color.FromArgb( 192, 255, 192 );
            m_dateColor = Color.Empty;
            m_textColor = Color.Red;
            m_gradientMode = mcGradientMode.None;
            m_text = "";
            m_enabled = true;
            m_bgImage = null;
            m_pattern = mcDayInfoRecurrence.None;
            //
            this.RoomRateItem = new RoomRateItem(this);
        }

        #endregion

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {

                    if (m_image != null)
                        m_image.Dispose();
                    if (m_bgImage != null)
                        m_bgImage.Dispose();

                    //There is nothing to clean.
                    if (Disposed != null)
                        Disposed(this, EventArgs.Empty);
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

        #region properties

        [Browsable(false)]
        internal MonthCalendar Calendar
        {
            set
            {
                m_calendar = value;
            }
            get
            {
                return m_calendar;
            }
        }

        [Browsable(false)]
        public object Tag
        {
            set
            {
                if (value != m_tag)
                    m_tag = value;
            }
            get
            {
                return m_tag;
            }
        }

        [Browsable(false)]
        internal int Index
        {
            set
            {
                m_index = value;
            }
            get
            {
                return m_index;
            }
        }

        [Browsable(false)]
        public virtual ISite Site
        {
            get
            {
                return m_site;
            }
            set
            {
                m_site = value;
            }
        }

        [Description("indicates the range of the recurrence.")]
        [Category("Recurrence")]
        public DateTime Range
        {
            get
            {
                return m_rangeDate;
            }
            set
            {
                if (m_rangeDate != value)
                {
                    m_rangeDate = value;
                }
            }
        }

        [Description("indicates the recurrence of the info.")]
        [Category("Recurrence")]
        public mcDayInfoRecurrence Pattern
        {
            get
            {
                return m_pattern;
            }
            set
            {
                if (m_pattern != value)
                {
                    m_pattern = value;
                }
            }
        }

        [Description("The day for which the formatting applies.")]
        [Category("Ocurrence")]
        public DateTime Date
        {
            get
            {
                return m_date;
            }
            set
            {
                if (m_date != value)
                {
                    m_date = value;
                    m_rangeDate = m_date;
                }
            }
        }

        [Category("Color")]
        [Description("Background color assigned to this day.")]
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
                }
            }
        }

        [Category("Color")]
        [Description("Second background color when using a gradient.")]
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
                }
            }
        }

        [Category("Color")]
        [Description("Type of gradient used.")]
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
                }
            }
        }

        [Category("Behavior")]
        [Description("Indicates wether the date should be treated as a weekend.")]
        public bool Weekend
        {
            get
            {
                return m_weekend;
            }
            set
            {
                if (m_weekend != value)
                {
                    m_weekend = value;
                }
            }
        }

        [Category("Behavior")]
        [Description("Indicates wether the date is enabled i.e. selectable.")]
        public bool Enabled
        {
            get
            {
                return m_enabled;
            }
            set
            {
                if (m_enabled != value)
                {
                    m_enabled = value;
                }
            }
        }

        [Category("Appearance")]
        [Description("Indicates wether bold font should be used for the date.")]
        public bool BoldedDate
        {
            get
            {
                return m_bolded;
            }
            set
            {
                if (m_bolded != value)
                {
                    m_bolded = value;
                }
            }
        }


        [Category("Color")]
        [Description("Color used for date.")]
        public Color DateColor
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
                }
            }
        }

        [Category("Color")]
        [Description("Color used for text.")]
        public Color TextColor
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
                }
            }
        }

        [Category("Appearance")]
        [Description("Text to be displayed for day.")]
        public string Text
        {
            get
            {
                return this.RoomRateItem.Text;
            }
            set
            {
                if (m_text != value)
                {
                    m_text = value;
                }
            }
        }

        #region 扩展的房价、房控属性

        /// <summary>
        /// 获取房价（房控）信息
        /// </summary>
        /// <value>The room rate.</value>
        /// User:Ryan  CreateTime:2012-11-14 22:41.
        public RoomRateItem RoomRateItem { get; set; }

        #endregion


        [TypeConverter(typeof(ImageTypeConverter))]
        [Editor(typeof(TX.Framework.WindowUI.Controls.ImageMapEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Category("Appearance")]
        [Description("Index for the image assigned to this date.")]
        public int ImageListIndex
        {
            get
            {
                return m_imageIndex;
            }
            set
            {
                if (m_imageIndex != value)
                {
                    m_image = null;
                    m_imageIndex = value;
                }
            }
        }

        [Category("Appearance")]
        [Description("Image used as background.")]
        public Image BackgroundImage
        {
            get
            {
                return m_bgImage;
            }
            set
            {
                if (m_bgImage != value)
                {
                    m_bgImage = value;
                }
            }
        }

        [Category("Appearance")]
        [Description("Image assigned to this date.")]
        [Browsable(true)]
        public Image Image
        {
            get
            {
                if (m_image != null) return m_image;

                if ((GetImageList() != null) && (m_imageIndex != -1))
                {
                    try
                    {
                        return GetImageList().Images[m_imageIndex];
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                else return null;
            }
            set
            {
                m_image = value;
            }

        }


        internal ImageList GetImageList()
        {
            if (m_calendar != null)
                return m_calendar.ImageList;
            else
                return null;
        }

        #endregion

    }

    #region DateItemTypeConverter

    public class DateItemTypeConverter : TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            DateItem d;
            d = (DateItem)value;
            return d.Date.ToShortDateString();

        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {

            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);

        }

    }

    #endregion

}
