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
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.ComponentModel;   
using System.ComponentModel;
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;

namespace TX.Framework.WindowUI.Controls
{

	
	/// <summary>
	/// A custom CollectionEditor for editing DateItemCollection
	/// </summary>
	public class DateItemCollectionEditor : CollectionEditor
	{
		#region private class member

		private MonthCalendar m_calendar;
		private ITypeDescriptorContext m_context;

		#endregion

		
		#region Constructor

		public DateItemCollectionEditor(Type type) : base(type)
		{
			
		}

		#endregion
		
		#region overrides
		
		protected override void DestroyInstance(object instance)
		{
			base.DestroyInstance (instance);
		
		}

		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			m_context = context;
			//MonthCalendar originalControl = (MonthCalendar) context.Instance;
			//m_calendar = originalControl;

			object returnObject = base.EditValue(context, provider, value);
			
			DateItemCollection collection = returnObject as DateItemCollection; 
			if (collection !=null)
			{
				collection.ModifiedEvent();
			}
			
			return returnObject;
		}
		

		protected override object CreateInstance(Type itemType)
		{
			object dateItem = base.CreateInstance(itemType);
			
			MonthCalendar originalControl = (MonthCalendar) m_context.Instance;
			m_calendar = originalControl;	
			
			((DateItem) dateItem).Date = DateTime.Today;
			((DateItem) dateItem).Calendar = m_calendar;
			return dateItem;
		}

		#endregion
	}

}
