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


	#region Delegates

	public delegate void DateItemEventHandler(object sender, EventArgs e);

	#endregion	


	/// <summary>
	/// Represents a collection of DateItem objects
	/// </summary>
	public class DateItemCollection : CollectionBase 
	{
		#region Class Data

		/// <summary>
		/// The Calendar that owns this DateItemCollection
		/// </summary>
		private MonthCalendar owner;

		#endregion
		
		#region Events
		
		public event DateItemEventHandler DateItemModified;

		#endregion

		#region Constructor
				
		public DateItemCollection(MonthCalendar owner) : base()
		{
			if (owner == null)
				throw new ArgumentNullException("owner");
							
			this.owner = owner;
		}
			
		public DateItemCollection(MonthCalendar owner, DateItemCollection dateItems) : this(owner)
		{
			this.Add(dateItems);
		}

		#endregion

		#region Methods
		
		public void ModifiedEvent()
		{
			if (DateItemModified!=null)
				DateItemModified(this,new EventArgs());	
		}

		public void Add(DateItem value)
		{
			int index;
			if (value == null)
				throw new ArgumentNullException("value");
			
			if ((MonthCalendar)value.Calendar==null)
				value.Calendar = this.owner;

			index = this.IndexOf(value);
			if (index == -1)
				this.List.Add(value);
			else
				this.List[index] = value;
		}

		public void AddRange(DateItem[] dateItems)
		{
			if (dateItems == null)
				throw new ArgumentNullException("dateItems");
			
			for (int i=0; i<dateItems.Length; i++)
			{				
				dateItems[i].Calendar = owner;
				this.Add(dateItems[i]);
			}
		}

		public void Add(DateItemCollection dateItems)
		{
			if (dateItems == null)
				throw new ArgumentNullException("dateItems");
			
			for (int i=0; i<dateItems.Count; i++)
			{
				this.Add(dateItems[i]);
			}
		}
			
		public new void Clear()
		{
			while (this.Count > 0)
			{
				this.RemoveAt(0);
			}
		}

		public bool Contains(DateItem dateItem)
		{
			if (dateItem == null)
				throw new ArgumentNullException("dateItem");
			
			return (this.IndexOf(dateItem) != -1);
		}
		
		public int IndexOf(DateTime date)
		{
			DateItem[] d;

			d = DateInfo(date);
			if (d.Length>0)
				return d[0].Index;
			else
				return -1;
		}

		public DateItem[] DateInfo(DateTime dt)
		{
			DateItem[] ret = new DateItem[0];
			ret.Initialize(); 
			for (int i = 0;i<this.Count;i++)
			{
				if ( ((this[i].Date <= dt) && (this[i].Range >=dt)) )
				{
					switch (this[i].Pattern)
					{
						case mcDayInfoRecurrence.None:
						{
							if (this[i].Date.ToShortDateString()  == dt.ToShortDateString())
							{
								this[i].Index = i;
								ret = AddInfo(this[i],ret);
							}
							break;
						}

						case mcDayInfoRecurrence.Daily:
						{
							this[i].Index = i;
							ret = AddInfo(this[i],ret);
							break;
						}
						case mcDayInfoRecurrence.Weekly:
						{
							if ( (this[i].Date.DayOfWeek == dt.DayOfWeek) )
							{
								this[i].Index = i;
								ret = AddInfo(this[i],ret);
							}
							break;
						}
						case mcDayInfoRecurrence.Monthly:
						{
							if ( (this[i].Date.Day == dt.Day))
							{
								this[i].Index = i;																			
								ret = AddInfo(this[i],ret);
							}
							break;
						}
						case mcDayInfoRecurrence.Yearly:
						{
							if (this[i].Date.ToShortDateString().Substring(5) ==
                                dt.ToShortDateString().Substring(5))  
                       		{
								this[i].Index = i;
								ret = AddInfo(this[i],ret);
							}
							break;
						}
					}

				}
			}
			return ret;
		}

		public DateItem[] AddInfo(DateItem dt, DateItem[] old)
		{
			int l =  old.Length;
			int i;
			DateItem[] n = new DateItem[l+1];
			n.Initialize(); 
			for (i = 0;i<l;i++)
			{
				n[i] = old[i];
			}
			n[i] = dt;
			return n;
		}

		public int IndexOf(DateItem dateItem)
		{
			if (dateItem == null)
				throw new ArgumentNullException("dateItem");
							
			for (int i=0; i<this.Count; i++)
			{
				if (this[i] == dateItem)
				{
					return i;
				}
			}

			return -1;
		}
			
		public void Remove(DateItem value)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			
			this.List.Remove(value);
		
		}
			
		public new void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		public void Move(DateItem value, int index)
		{
			if (value == null)
				throw new ArgumentNullException("value");
			
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

			this.List.Remove(value);

			if (index > this.Count)
			{
				this.List.Add(value);
			}
			else
			{
				this.List.Insert(index, value);
			}

		}

		public void MoveToTop(DateItem value)
		{
			this.Move(value, 0);
		}


		public void MoveToBottom(DateItem value)
		{
			this.Move(value, this.Count);
		}

		#endregion

		#region Properties

		public virtual DateItem this[int index]
		{
			get
			{
				return this.List[index] as DateItem;
			}
		}

		#endregion

	}

}
