using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2.Scene
{
	[Serializable()]
    public class WorldSpawn : Node
    {
        string title;
        string creator;
        string comments;

        public string Title { get { return title; } set { title = value; } }
        public string Creator { get { return creator; } set { creator = value; } }
        public string Comments { get { return comments; } set { comments = value; } }

        public WorldSpawn()
            : base()
        {
            title = "";
            creator = "";
            comments = "";
        }
        
    }
}
