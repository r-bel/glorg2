/*
Copyright (C) 2010 Henning Moe

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{

	internal interface INodeReference
	{
		Scene Owner { get; set; }
		void Update();
	}
	[Serializable()]
	public struct NodeReference<T>
		where T : Node
	{
		public static readonly NodeReference<T> Empty = new NodeReference<T>();

		[NonSerialized()]
		T node;
		[NonSerialized()]
		internal Scene owner;

		Guid guid;

		public T Value { get { return node; } set { node = value; if (value == null) guid = Guid.Empty; else guid = value.Guid; } }

		public Scene Owner { get { return owner; } set { owner = value; } }

		public NodeReference(Scene owner)
		{
			guid = new Guid();
			node = null;
			this.owner = owner;
		}

		public NodeReference(T node)
		{
			guid = node.Guid;
			this.node = node;
			owner = node.owner;
		}

		private void Update()
		{
			Guid g = guid;
			if (g == Guid.Empty)
			{
				node = null;
			}
			else
			{
				node = (T)owner.items.Find(i => i.Guid == g);
				if (node == null)
					guid = Guid.Empty;
				else
					guid = node.Guid;
			}
		}

	}
}
