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
using System.Runtime.InteropServices;  
using System.Reflection;
using System.Windows.Forms;
using TX.Framework.WindowUI.Controls.TXMonthCalendar;  

namespace TX.Framework.WindowUI.Controls
{
	
	#region Delegates

	internal delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

	#endregion
	
	/// <summary>
	/// Summary description for GlobalHook.
	/// </summary>
	internal class GlobalHook : IDisposable
	{
				

		#region class members

		private bool disposed;
		private int m_keyboardHook;
 
		private HookProc m_keyboardHookProcedure;
		
		#endregion

		#region Events

		public event KeyEventHandler KeyUp;
		public event KeyEventHandler KeyDown;
		public event KeyPressEventHandler KeyPress;

		#endregion
		
		#region Constructor

		public GlobalHook()
		{

		}

		#endregion

		#region IDisposable Members
		
		protected virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
				{
					RemoveKeyboardHook();		
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
	

		#region public methods
		
		public void InstallKeyboardHook()
		{
			try
			{
				// install Keyboard hook 
				if(m_keyboardHook == 0)
				{
					m_keyboardHookProcedure = new HookProc(KeyboardHookProc);
                    m_keyboardHook = TXMonthCalendar.NativeMethods.SetWindowsHookEx(TXMonthCalendar.NativeMethods.WH_KEYBOARD_LL,
						m_keyboardHookProcedure, 
						Marshal.GetHINSTANCE(
						Assembly.GetExecutingAssembly().GetModules()[0]),
						0);
				}
			}
			catch(Exception)
			{

			}
		}

		public void RemoveKeyboardHook()
		{
			bool retKeyboard = true;
			
			try
			{

				if(m_keyboardHook != 0)
				{
					retKeyboard = NativeMethods.UnhookWindowsHookEx(m_keyboardHook);
					m_keyboardHook = 0;
				}
			}
			catch(Exception)
			{

			}
		}


		#endregion
		
		#region private methods

		private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
		{
			// it was ok and someone listens to events
			if ((nCode >= 0) && (KeyDown!=null || KeyUp!=null || KeyPress!=null))
			{
                TXMonthCalendar.NativeMethods.KeyboardHookStruct MyKeyboardHookStruct = (TXMonthCalendar.NativeMethods.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(TXMonthCalendar.NativeMethods.KeyboardHookStruct));
				// KeyDown
                if ((KeyDown != null) && (wParam == TXMonthCalendar.NativeMethods.WM_KEYDOWN || wParam == TXMonthCalendar.NativeMethods.WM_SYSKEYDOWN))
				{
					Keys keyData=(Keys)MyKeyboardHookStruct.vkCode;
					KeyEventArgs e = new KeyEventArgs(keyData);
					this.KeyDown(this, e);
				}
				
				// KeyPress
                if ((KeyPress != null) && (wParam == TXMonthCalendar.NativeMethods.WM_KEYDOWN))
				{
					byte[] keyState = new byte[256];
                    TXMonthCalendar.NativeMethods.GetKeyboardState(keyState);

					byte[] inBuffer= new byte[2];
                    if (TXMonthCalendar.NativeMethods.ToAscii(MyKeyboardHookStruct.vkCode,
						MyKeyboardHookStruct.scanCode,
						keyState,
						inBuffer,
						MyKeyboardHookStruct.flags)==1) 
					{
						KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
						KeyPress(this, e);
					}
				}
				
				// KeyUp
                if ((KeyUp != null) && (wParam == TXMonthCalendar.NativeMethods.WM_KEYUP || wParam == TXMonthCalendar.NativeMethods.WM_SYSKEYUP))
				{
					Keys keyData=(Keys)MyKeyboardHookStruct.vkCode;
					KeyEventArgs e = new KeyEventArgs(keyData);
					KeyUp(this, e);
				}

			}
            return TXMonthCalendar.NativeMethods.CallNextHookEx(m_keyboardHook, nCode, wParam, lParam); 
		}	


		#endregion
	}
}
