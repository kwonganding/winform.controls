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
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace TX.Framework.WindowUI.Controls
{
	
	public enum mcItemAlign {TopLeft = 0, TopRight, Center, BottomLeft, BottomRight,
						     LeftCenter, RightCenter, TopCenter, BottomCenter}
		
	#region Delegates
	
	public delegate void AlignEventHandler(object sender, AlignEventArgs e);
	
	#endregion

	
	/// <summary>
	/// Summary description for AlignControl.
	/// </summary>
	[ToolboxItem(false)]
	internal class AlignControl : System.Windows.Forms.Control
	{
		#region private class members
		
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		Rectangle m_topLeft = new Rectangle();
		Rectangle m_topRight = new Rectangle();
		Rectangle m_center = new Rectangle();
		Rectangle m_bottomLeft = new Rectangle();
		Rectangle m_bottomRight = new Rectangle();
		Rectangle m_leftCenter = new Rectangle();
		Rectangle m_rightCenter = new Rectangle();
		Rectangle m_topCenter = new Rectangle();
		Rectangle m_bottomCenter = new Rectangle();

		Region m_topLeftRgn;
		Region m_topRightRgn;
		Region m_centerRgn;
		Region m_bottomLeftRgn;
		Region m_bottomRightRgn;
		Region m_leftCenterRgn;
		Region m_rightCenterRgn;
		Region m_topCenterRgn;
		Region m_bottomCenterRgn;

		private mcItemAlign m_default;
		
		#endregion

		#region EventHandler
		
		public event AlignEventHandler AlignChanged;
	

		#endregion
		
		#region constructor

		public AlignControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			
			this.SetStyle(ControlStyles.DoubleBuffer, true);
			this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
			this.SetStyle(ControlStyles.UserPaint, true);

			// set min width and height
			this.Width = 75;
			this.Height = 75;		
		}

		#endregion

		#region Dispose

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
					
					m_topLeftRgn.Dispose();
					m_topRightRgn.Dispose();
					m_centerRgn.Dispose();
					m_bottomLeftRgn.Dispose();
					m_bottomRightRgn.Dispose();
					m_leftCenterRgn.Dispose();
					m_rightCenterRgn.Dispose();
					m_topCenterRgn.Dispose();
					m_bottomCenterRgn.Dispose();

				}
			}
			base.Dispose( disposing );
		}

		#endregion

		#region properties

		public mcItemAlign Default
		{
			get
			{
				return m_default;
			}
			set
			{
				m_default = value;
			}
		}
		
		#endregion

		#region overrides
		
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint (e);
		}
		
		
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground (e);
			
			SetupButtons();
			
			ControlPaint.DrawButton(e.Graphics,m_topLeft, (m_default != mcItemAlign.TopLeft) ? ButtonState.Normal : ButtonState.Checked);
			ControlPaint.DrawButton(e.Graphics,m_center,(m_default != mcItemAlign.Center) ? ButtonState.Normal : ButtonState.Checked);
			ControlPaint.DrawButton(e.Graphics,m_topRight,(m_default != mcItemAlign.TopRight) ? ButtonState.Normal : ButtonState.Checked);
			ControlPaint.DrawButton(e.Graphics,m_bottomLeft,(m_default != mcItemAlign.BottomLeft) ? ButtonState.Normal : ButtonState.Checked);
			ControlPaint.DrawButton(e.Graphics,m_bottomRight,(m_default != mcItemAlign.BottomRight) ? ButtonState.Normal : ButtonState.Checked);
			
			ControlPaint.DrawButton(e.Graphics,m_leftCenter,(m_default != mcItemAlign.LeftCenter) ? ButtonState.Normal : ButtonState.Checked);
			ControlPaint.DrawButton(e.Graphics,m_rightCenter,(m_default != mcItemAlign.RightCenter) ? ButtonState.Normal : ButtonState.Checked);
			ControlPaint.DrawButton(e.Graphics,m_topCenter,(m_default != mcItemAlign.TopCenter) ? ButtonState.Normal : ButtonState.Checked);
			ControlPaint.DrawButton(e.Graphics,m_bottomCenter,(m_default != mcItemAlign.BottomCenter) ? ButtonState.Normal : ButtonState.Checked);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown (e);
			
			Point p = new Point(e.X,e.Y); 
			mcItemAlign select = mcItemAlign.Center;
			bool hit = false;

			if (m_topLeftRgn.IsVisible(p))
			{
				select = mcItemAlign.TopLeft; 
				hit = true;
			}
			if (m_topRightRgn.IsVisible(p))
			{
				select = mcItemAlign.TopRight;
				hit = true;
			}
			if (m_bottomLeftRgn.IsVisible(p))
			{
				select = mcItemAlign.BottomLeft;
				hit = true;
			}
			if (m_bottomRightRgn.IsVisible(p))
			{
				select = mcItemAlign.BottomRight;
				hit = true;
			}
			if (m_centerRgn.IsVisible(p))
			{
				select = mcItemAlign.Center;
				hit = true;
			}
			if (m_leftCenterRgn.IsVisible(p))
			{	
				select = mcItemAlign.LeftCenter;
				hit = true;	
			}
			if (m_rightCenterRgn.IsVisible(p))
			{	
				select = mcItemAlign.RightCenter;
				hit = true;	
			}
			if (m_topCenterRgn.IsVisible(p))
			{	
				select = mcItemAlign.TopCenter;
				hit = true;	
			}
			if (m_bottomCenterRgn.IsVisible(p))
			{	
				select = mcItemAlign.BottomCenter;
				hit = true;	
			}
			
			if ((this.AlignChanged!=null) && (hit))
				this.AlignChanged(this,new AlignEventArgs(select));
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

		#region Methods

		private void SetupButtons()
		{
			int w;
			int h;;
			
			w = this.Width/3;
			h = this.Height/3;

			m_topLeft = new Rectangle(0,0,w,h);
			m_center = new Rectangle(w,h,System.Math.Max(this.Width-2*w,w),h);
			m_topRight = new Rectangle(this.Width-w,0,w,h);
			m_bottomLeft = new Rectangle(0,2*h,w,h);
			m_bottomRight = new Rectangle(this.Width-w,2*h,w,h);
			
			m_leftCenter = new Rectangle(0,h,w,h);
			m_rightCenter = new Rectangle(this.Width-w,h,w,h);
			m_topCenter = new Rectangle(w,0,System.Math.Max(this.Width-2*w,w),h);
			m_bottomCenter = new Rectangle(w,2*h,System.Math.Max(this.Width-2*w,w),h);

			m_topLeftRgn = new Region(m_topLeft);
			m_centerRgn = new Region(m_center);
			m_topRightRgn = new Region(m_topRight);
			m_bottomLeftRgn = new Region(m_bottomLeft);
			m_bottomRightRgn = new Region(m_bottomRight);
			m_leftCenterRgn = new Region(m_leftCenter);
			m_rightCenterRgn = new Region(m_rightCenter);
			m_topCenterRgn = new Region(m_topCenter);
			m_bottomCenterRgn = new Region(m_bottomCenter);

			
		}

		#endregion
	}
				
	#region AlignEventArgs
	
	public class AlignEventArgs : EventArgs
	{
		#region Class Data
			
		private mcItemAlign m_align;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the DayClickEventArgs class with default settings
		/// </summary>
		public AlignEventArgs()
		{
			m_align = mcItemAlign.Center;
		}

		public AlignEventArgs(mcItemAlign align)
		{
			this.m_align = align;
		}

		#endregion


		#region Properties

		public mcItemAlign Align
		{
			get
			{
				return this.m_align; 
			}
		}

		#endregion
	}


	#endregion

}
