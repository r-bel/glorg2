using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CSharp;

using System.CodeDom.Compiler;
namespace GlorgIDE
{
	[Serializable()]
	public class GlorgProject
	{
		public string OutputName { get; set; }

		private List<GlorgClass> classes;
		private GlorgClass game_class;
		private List<string> references;
		private List<string> maps;
		public List<string> Maps { get { return maps; } }
		public GlorgClass GameClass { get { return game_class; } }
		public List<string> References { get { return references; } }
		public List<GlorgClass> Classes { get { return classes; } }
		public GlorgProject()
		{
			references = new List<string>();
			classes = new List<GlorgClass>();
			maps = new List<string>();
			game_class = new GlorgClass();
			GameClass.Inherits = "Glorg2.Game";
			GameClass.Name = "MyGame";
			OutputName = "MyGlorgGame";
			
		}
		public bool Compile()
		{
			List<string> code_files = new List<string>(classes.Count + 1);
			code_files.Add(GameClass.GenerateCode());

			foreach (var c in classes)
			{
				code_files.Add(c.GenerateCode());
			}

			var prov = new CSharpCodeProvider();
			var parameters = new CompilerParameters();
			parameters.GenerateExecutable = true;
			parameters.ReferencedAssemblies.AddRange(references.ToArray());
			parameters.MainClass = GameClass.Name;
			var results = prov.CompileAssemblyFromSource(parameters, code_files.ToArray());
			
			return results.Errors.Count == 0;

			
		}
	}
	[Serializable()]
	public class GlorgClass
	{
		public string Name {get; set; }
		public string Inherits { get; set; }
		private List<string> implements;
		public string Code { get; set; }
		public List<string> Implements { get { return implements; } }
		public string GenerateCode()
		{
			StringBuilder output = new StringBuilder();
			output.AppendLine("using System.Collections.Generic;");
			output.AppendLine("using System.Linq");
			output.AppendLine("using System;");
			output.AppendLine("using Glorg2;");
			output.AppendLine("using Glorg2.Scene;");
			output.AppendLine("using Glorg2.Graphics;");
			output.AppendLine("using Glorg2.Graphics.OpenGL;");
			output.AppendLine("using Glorg2.Graphics.OpenGL.Shaders;");
			output.AppendLine("using Glorg2.Resource;");
			output.AppendLine("using Glorg2.Physics;");

			output.Append("public class ");
			output.Append(Name);
			output.Append(" : ");
			output.Append(Inherits);
			if (implements.Count > 0)
			{
				for (int i = 0; i < implements.Count - 1; i++)
				{
					output.Append(implements[i]);
					output.Append(", ");
				}
				output.Append(implements[implements.Count - 1]);
			}
			output.AppendLine();
			output.AppendLine("{");
			output.AppendLine(Code);
			output.AppendLine("}");

			return output.ToString();
		}
	}
}
