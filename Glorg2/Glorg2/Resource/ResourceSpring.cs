using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace Glorg2.Resource
{
	public class ResourceEntry
	{
		internal string name;	// Name of the resource
		internal ResourceDirectory parent;
		internal ResourceSpring owner;

		internal virtual void ToStream(BinaryWriter wr)
		{
			wr.Write(name);
			if (this is ResourceDirectory)
				wr.Write((int)0);
			else if (this is ResourceFile)
				wr.Write((int)1);
		}

		internal static ResourceEntry FromStream(BinaryReader rd, ResourceSpring owner)
		{
			string name = rd.ReadString();
			int type = rd.ReadInt32();
			switch (type)
			{
				case 0:
					return ResourceDirectory.DirFromStream(rd, owner);
				case 1:
					return ResourceFile.FileFromStream(rd, owner);
				default:
					throw new InvalidDataException("Unexpected entrytype");
			}
		}

		internal ResourceEntry(ResourceSpring spring, ResourceDirectory parent)
		{
			this.parent = parent;
			this.owner = spring;
		}
		internal ResourceEntry(ResourceSpring spring)
		{
			this.owner = spring;
		}


		public string Name { get { return name; } }
		public ResourceEntry Parent { get { return parent; } }
	}
	public class ResourceDirectory : ResourceEntry
	{
		internal List<ResourceEntry> entries;
		public System.Collections.ObjectModel.ReadOnlyCollection<ResourceEntry> Entries { get { return entries.AsReadOnly(); } }

		internal static ResourceDirectory DirFromStream(BinaryReader rd, ResourceSpring owner)
		{
			ResourceDirectory ret = new ResourceDirectory(owner);
			int count = rd.ReadInt32();
			for (int i = 0; i < count; i++)
			{
				var entry = ResourceEntry.FromStream(rd, owner);
				entry.parent = ret;
				ret.entries.Add(entry);
			}
			return ret;
		}

		internal override void ToStream(BinaryWriter wr)
		{
			base.ToStream(wr);
			wr.Write(entries.Count);
			foreach (var ent in entries)
				ent.ToStream(wr);
		}

		public ResourceDirectory(ResourceSpring spring, ResourceDirectory parent)
			: base(spring, parent)
		{
		}
		public ResourceDirectory(ResourceSpring spring)
			: base(spring, null)
		{
		}

	}
	public class ResourceFile : ResourceEntry
	{
		internal string handler;	// Handler describing which importer to use
		internal long length;		// Entry size in bytes
		internal long offset;		// Absolute file offset

		internal string source_file;

		public string Handler { get { return handler; } }
		public long Length { get { return length; } }
		public long Offset { get { return offset; } }

		public static ResourceFile FileFromStream(BinaryReader rd, ResourceSpring owner)
		{
			ResourceFile file = new ResourceFile(owner);
			file.handler = rd.ReadString();
			file.length = rd.ReadInt64();
			file.offset = rd.ReadInt64();
			return file;
		}

		internal override void ToStream(BinaryWriter wr)
		{
			wr.Write(handler);
			wr.Write(length);
			wr.Write(offset);
		}

		internal long WriteFile(BinaryWriter dst)
		{
			byte[] buffer = new byte[8192];
			long total = 0;
			using (var fs = new System.IO.FileStream(source_file, FileMode.Open, FileAccess.Read))
			{
				int count = 0;
				while ((count = fs.Read(buffer, 0, buffer.Length)) > 0)
				{
					dst.Write(buffer, 0, count);
					total += count;
				}
			}
			return total;
		}

		internal ResourceFile(ResourceSpring owner, ResourceDirectory parent)
			: base(owner, parent)
		{
		}

		internal ResourceFile(ResourceSpring owner)
			: base(owner)
		{
		}

		public System.IO.Stream GetStream()
		{
			return new SubStream(owner.src, offset, length);
		}
	}

	public class ResourceSpring : IDisposable
	{
		internal System.IO.FileStream src;
		ResourceDirectory root;

		public ResourceDirectory Root { get { return root; } }
		public ResourceSpring(string file)
		{
			root = new ResourceDirectory(this);
			src = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read);
		}
		internal ResourceSpring()
		{
			root = new ResourceDirectory(this);
		}

		private void Load(string filename)
		{
		}

		public static void BuildSpring(System.IO.DirectoryInfo directory, string output)
		{
			ResourceSpring spring = new ResourceSpring();
			BuildDirectory(directory, spring.root, spring);

		}
		public static void BuildDirectory(DirectoryInfo dir, ResourceDirectory parent, ResourceSpring target)
		{
			var dirs = dir.GetDirectories();
			foreach (var d in dirs)
			{
				ResourceDirectory new_dir = new ResourceDirectory(target);
				new_dir.name = d.Name;
				new_dir.parent = parent;
				parent.entries.Add(new_dir);
				BuildDirectory(d, new_dir, target);
			}
			var files = dir.GetFiles();
			foreach (var file in files)
				BuildFile(file, parent, target);
		}
		public static void BuildFile(FileInfo file, ResourceDirectory parent, ResourceSpring target)
		{
			ResourceFile new_file = new ResourceFile(target);
			new_file.source_file = file.FullName;
			new_file.parent = parent;
			new_file.name = file.Name;
			parent.entries.Add(new_file);
		}

		public void Close()
		{
			if(src != null)
			{
				src.Close();
				src = null;
			}
		}

		public void Dispose()
		{
			Close();
			GC.SuppressFinalize(this);
		}
		~ResourceSpring()
		{
			Close();
		}


	}
}
