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
using System.Collections;   

namespace TX.Framework.WindowUI.Controls
{

	/// <summary>
	/// Represents a collection of DateItem objects
	/// </summary>
	public class SelectedDatesCollection : ReadOnlyCollectionBase 
	{
		#region Class Data

		/// <summary>
		/// The Calendar that owns this DateItemCollection
		/// </summary>
		private MonthCalendar owner;

		#endregion
		

		#region Constructor
				
		public SelectedDatesCollection(MonthCalendar owner) : base()
		{
			if (owner == null)
				throw new ArgumentNullException("owner");
							
			this.owner = owner;
		}
			
		public SelectedDatesCollection(MonthCalendar owner, SelectedDatesCollection dates) : this(owner)
		{
			
			if (owner == null)
				throw new ArgumentNullException("owner");
		
			this.owner = owner;
			
			this.Add(dates);
		}

		#endregion

		#region Methods
		

		public void Add(DateTime value)
		{
			int index;
	
			index = this.IndexOf(value);
			if (index == -1)
				this.InnerList.Add(value);
			else
				this.InnerList[index] = value;
		}

		public void AddRange(DateTime[] dates)
		{
			if (dates == null)
				throw new ArgumentNullException("dates");
			
			for (int i=0; i<dates.Length; i++)
			{				
				this.Add(dates[i]);
			}
		}

		public void Add(SelectedDatesCollection dates)
		{
			if (dates == null)
				throw new ArgumentNullException("dates");
			
			for (int i=0; i<dates.Count; i++)
			{
				this.Add(dates[i]);
			}
		}
			
		public void Clear()
		{
			while (this.Count > 0)
			{
				this.RemoveAt(0);
			}
		}

		public bool Contains(DateTime date)
		{
			return (this.IndexOf(date) != -1);
		}

		public int IndexOf(DateTime date)
		{
							
			for (int i=0; i<this.Count; i++)
			{
				if (this[i] == date)
				{
					return i;
				}
			}

			return -1;
		}
			
		public void Remove(DateTime value)
		{
			
			this.InnerList.Remove(value);
		
		}
			
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		public void Move(DateTime value, int index)
		{
			if (index < 0)
			{
				index = 0;
			}
			else if (index > this.Count)
			{
				index = this.Count;
			}

			if (!this.Contains(value) || this.IndexOf(value) == index)
			{
				return;
			}

			this.InnerList.Remove(value);

			if (index > this.Count)
			{
				this.InnerList.Add(value);
			}
			else
			{
				this.InnerList.Insert(index, value);
			}

		}

		public void MoveToTop(DateTime value)
		{
			this.Move(value, 0);
		}


		public void MoveToBottom(DateTime value)
		{
			this.Move(value, this.Count);
		}

		#endregion

		#region Properties

		public virtual DateTime this[int index]
		{
			get
			{
				DateTime d = (DateTime)this.InnerList[index];
				return d;
			}
		}

		#endregion

	}

}