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

namespace TX.Framework.WindowUI.Controls
{
	/// <summary>
	/// Summary description for ImageEditor.
	/// </summary>
	[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
	internal class AlignEditor : System.Drawing.Design.UITypeEditor, IDisposable   
	{
		
		#region private members
		
		private bool disposed;

		#endregion

		#region properties
		
		private IWindowsFormsEditorService wfes;
		private mcItemAlign m_selectedAlign = mcItemAlign.Center;
		private AlignControl m_alignCtrl;
	
		#endregion
		
		#region constructor

		public AlignEditor()
		{
			m_alignCtrl = new AlignControl(); 
		}

		#endregion

		#region Methods

		#endregion
		
		#region overrides

		public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			wfes = (IWindowsFormsEditorService)	provider.GetService(typeof(IWindowsFormsEditorService));
			if((wfes == null) || (context == null))
				return null ;
			
			m_alignCtrl.Default = (mcItemAlign)value;
			// add listner for event
			m_alignCtrl.AlignChanged+=new AlignEventHandler(m_alignCtrl_AlignChanged);
			
			m_selectedAlign = (mcItemAlign)value;

			// show the popup as a drop-down
			wfes.DropDownControl(m_alignCtrl) ;
			
			// return the selection (or the original value if none selected)
			return m_selectedAlign;
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
			return false;
		}

		#endregion

		#region EventHandlers

		private void m_alignCtrl_AlignChanged(object sender, AlignEventArgs e)
		{
			m_selectedAlign = e.Align; 
			
			//remove listner
			m_alignCtrl.AlignChanged-=new AlignEventHandler(m_alignCtrl_AlignChanged);
			
			// close the drop-dwon, we are done
			wfes.CloseDropDown();
		}

		#endregion

		#region IDisposable Members
		
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					m_alignCtrl.Dispose();
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
}
