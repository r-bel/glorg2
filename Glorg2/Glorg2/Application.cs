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
using System.Threading;

namespace Glorg2
{
	public abstract class Game
	{
		Thread RenderThread;
		Thread LogicThread;
		Thread PhysicsThread;
		Thread ResourceThread;
		
		Glorg2.Graphics.GraphicsDevice dev;

		Glorg2.Graphics.OpenGL.OpenGLContext res_ctx;

		System.Windows.Forms.Control target;
		
		volatile bool running;
		volatile bool provoke_render;
		volatile float fps;
		volatile float frame_time;
		volatile float total_time;
		Scene.Scene scene;
		Queue<Action> graphic_invoke;
		Queue<Action> physics_invoke;
		Queue<Action> logic_invoke;
		Queue<Action> resource_invoke;

		volatile ThreadReady threads_ready;

		public Scene.Scene Scene { get { return scene; } set { scene = value; } }

		/// <summary>
		/// Retrieves frames per second of the rendering thread
		/// Note that this value is a measurment of performance, and is calculated per frame and is therefore not statisticly reliable.
		/// </summary>
		public float FramesPerSecond { get { return fps; } }

		public System.Windows.Forms.Control Target { get { return target; } }

		/// <summary>
		/// Retrieves the game total running time in secods
		/// </summary>
		public float TotalTime { get { return total_time; } }

		public Graphics.GraphicsDevice Device { get { return dev; } }

		protected virtual void InternalSizeChanged()
		{
			System.Drawing.Size size;
			size = (System.Drawing.Size)target.Invoke(new Func<System.Drawing.Size>(delegate { return target.ClientSize; }));
			SizeChanged(size);
		}
		protected virtual void SizeChanged(System.Drawing.Size new_size)
		{
			dev.Viewport = new System.Drawing.Rectangle(0, 0, new_size.Width, new_size.Height);
			if (scene.Camera != null && scene.Camera is Glorg2.Scene.PerspectiveCamera)
			{
				(scene.Camera as Glorg2.Scene.PerspectiveCamera).Aspect = new_size.Width / (float)new_size.Height;
			}
		}

		private void StartInternal()
		{
			running = true;
			graphic_invoke = new Queue<Action>();
			physics_invoke = new Queue<Action>();
			logic_invoke = new Queue<Action>();
			resource_invoke = new Queue<Action>();
			scene = new Glorg2.Scene.Scene(this);
			RenderThread = new System.Threading.Thread(new System.Threading.ThreadStart(RenderLoop));
			RenderThread.Name = "Rendering thread";
			target.Show();
			target.HandleDestroyed += (sender, e) => { running = false; System.Windows.Forms.Application.Exit(); };
			//target.Resize += (sender, e) => GraphicInvoke(new Action(InternalSizeChanged));
			//GraphicInvoke(new Action(InternalSizeChanged));
			
			Scene.ParentNode.InternalPostSerialize();
		}
		public void Start(System.Windows.Forms.Control target)
		{
			this.target = target;
			ready = true;
			StartInternal();
			PhysicsThread = new Thread(new ThreadStart(PhysicsLoop));
			PhysicsThread.Name = "Physics thread";
			RenderThread.Start();
			PhysicsThread.Start();
			MainLoop();
		}
		public void Start()
		{
			target = new System.Windows.Forms.Form();
			target.Show();
			target.Update();
			ready = true;
			StartInternal();
			PhysicsThread = new Thread(new ThreadStart(PhysicsLoop));
			PhysicsThread.Name = "Physics thread";
			RenderThread.Start();
			PhysicsThread.Start();
            MainLoop();
		}
		public void StartAsync()
		{
			target = new System.Windows.Forms.Form();
			target.Show();
			target.Update();
			ready = true;
			StartInternal();
			LogicThread = new System.Threading.Thread(new System.Threading.ThreadStart(MainLoop));
			LogicThread.Name = "Logic thread";
			PhysicsThread = new Thread(new ThreadStart(PhysicsLoop));
			PhysicsThread.Name = "Physics thread";
			RenderThread.Start();
			LogicThread.Start();
			PhysicsThread.Start();
		}
		public void StartAsync(System.Windows.Forms.Control target)
		{
			this.target = target;
			LogicThread = new System.Threading.Thread(new System.Threading.ThreadStart(MainLoop));
			LogicThread.Name = "Logic thread";
			PhysicsThread = new Thread(new ThreadStart(PhysicsLoop));
			PhysicsThread.Name = "Physics thread";
			StartInternal();
			RenderThread.Start();
			LogicThread.Start();
			PhysicsThread.Start();
		}
		protected virtual void FrameStep(float time)
		{
		}
		protected virtual void InitializeGraphics()
		{
			
		}
		protected virtual void GraphicsClosing()
		{
		}

		protected virtual void Closing()
		{
		}
		protected virtual void Init()
		{
		}

		private void JoinThread(Thread th)
		{
			while (th.ThreadState == ThreadState.Running)
			{
				System.Windows.Forms.Application.DoEvents();
			}
		}
		public void Exit()
		{
			running = false;
			JoinThread(RenderThread);
			if (LogicThread != null)
				JoinThread(LogicThread);
		}
		/// <summary>
		/// Invokes an action in the rendering thread.
		/// </summary>
		/// <remarks>This action will not be executed instantly, rather the next time the rendering thread processes these events.</remarks>
		/// <param name="act">Action to perform.</param>
		public void GraphicInvoke(Action act)
		{
			lock (graphic_invoke)
			{
				graphic_invoke.Enqueue(act);
			}
		}
		public void ResourceInvoke(Action act)
		{
			lock (resource_invoke)
			{
				resource_invoke.Enqueue(act);
			}
		}
		public void PhysicsInvoke(Action act)
		{
			lock (physics_invoke)
			{
				physics_invoke.Enqueue(act);
			}
		}
		public void LogicInvoke(Action act)
		{
			lock (logic_invoke)
			{
				logic_invoke.Enqueue(act);
			}
		}
		private void DoJanitorial(IEnumerable<Resource.Resource> res)
		{
			foreach (var r in res)
			{
				r.DoDispose();
			}
			lock (scene.Resources)
			{
				scene.Resources.Remove(res);
			}
		}
		private void CleanupResources()
		{
			var r = scene.Resources.Janitorial();
			lock (scene.Resources)
			{
				scene.Resources.Remove(r);
			}
		}

		private void ResourceLoop()
		{
			/*System.Windows.Forms.NativeWindow render_offscreen = new System.Windows.Forms.NativeWindow()
			{

			};
			render_offscreen.CreateHandle(new System.Windows.Forms.CreateParams()
			{
				Caption = "Offsecreen resource loading window"
			});
			res_ctx = Graphics.OpenGL.OpenGLContext.GetContext();
			res_ctx.CreateContext(render_offscreen.Handle, IntPtr.Zero, dev.Context);*/
			threads_ready |= ThreadReady.ResourceThread;
			return;
			while (threads_ready != ThreadReady.All)
				Thread.Sleep(0);

			long old_time = System.Diagnostics.Stopwatch.GetTimestamp();
			while (running)
			{
				long new_time = System.Diagnostics.Stopwatch.GetTimestamp();
				frame_time = (new_time - old_time) / (float)System.Diagnostics.Stopwatch.Frequency;
				old_time = new_time;

				lock (resource_invoke)
				{
					while (resource_invoke.Count > 0)
					{
						var item = resource_invoke.Dequeue();
						item();
					}
				}

				var res = scene.Resources.Janitorial();
				if (res.Count > 0)
				{
					GraphicInvoke(new Action(delegate
					{
						foreach (var r in res)
						{
							r.DoDispose();
						}
						lock (scene.Resources.resources)
						{
							scene.Resources.Remove(res);
						}
					}));
				}
				System.Threading.Thread.Sleep(500);
			}
			//res_ctx.Dispose();
			//render_offscreen.DestroyHandle();
		}

		private void PhysicsLoop()
		{
			threads_ready |= ThreadReady.PhysicsThread;
			while (threads_ready != ThreadReady.All)
				Thread.Sleep(0);

			long old_time = System.Diagnostics.Stopwatch.GetTimestamp();
			return;
			while (running)
			{
				long new_time = System.Diagnostics.Stopwatch.GetTimestamp();
				frame_time = (new_time - old_time) / (float)System.Diagnostics.Stopwatch.Frequency;
				old_time = new_time;
				lock (physics_invoke)
				{
					while (physics_invoke.Count > 0)
					{
						var item = physics_invoke.Dequeue();
						item();
					}
				}
				if (scene.physics.Count == 0)
					System.Threading.Thread.Sleep(50);
				else
					foreach (var obj in scene.physics)
					{
						obj.SimulationStep(frame_time);
					}
				
			
			}
		}
		volatile bool sort_render_list;
		public void SortRenderingList()
		{
			sort_render_list = true;
		}
		private void MainLoop()
		{
#if DEBUG
			Glorg2.Debugging.Debug.LogEnabled = true;
#endif
			long old_time;
			Init();
			old_time = System.Diagnostics.Stopwatch.GetTimestamp();
			target.Resize += (sender, e) => { GraphicInvoke(new Action(InternalSizeChanged));};

			threads_ready |= ThreadReady.MainThread;

			while (threads_ready != ThreadReady.All)
			{
				System.Windows.Forms.Application.DoEvents();
				Thread.Sleep(0);
			}


			while (running)
			{
				long new_time = System.Diagnostics.Stopwatch.GetTimestamp();
				frame_time = (new_time - old_time) / (float)System.Diagnostics.Stopwatch.Frequency;


				lock (logic_invoke)
				{
					while (logic_invoke.Count > 0)
					{
						var item = logic_invoke.Dequeue();
						item();
					}
				}
				if (scene.Camera != null)
				{
					if (!scene.Camera.absolute_transform.Invert(out scene.camera_mat))
						throw new Exception("WTF! Invert failed!");
					
					scene.sim_time += frame_time;
				}
				else
					scene.camera_mat = Matrix.Identity;
				FrameStep(frame_time);

				scene.local_transform = Matrix.Identity;

				scene.ParentNode.InternalProcess(frame_time);
				total_time += frame_time;
				provoke_render = true;
				System.Threading.Thread.Sleep(0);
				old_time = new_time;				

			}
			Closing();
		}

		volatile bool ready;
		private void RenderLoop()
		{
			long old_time = 0;
			// Wait for control to recieve a handle
			target.HandleCreated += (sender, e) => ready = true;

			while (!ready)
			{
				IntPtr h = (IntPtr)target.Invoke(new Func<IntPtr>(() => target.Handle));
				if (h != IntPtr.Zero)
					ready = true;
				System.Threading.Thread.Sleep(0);
			}
			IntPtr handle = (IntPtr)target.Invoke(new Func<IntPtr>(() => target.Handle));
			dev = new Glorg2.Graphics.GraphicsDevice(handle);

			ResourceThread = new Thread(new ThreadStart(ResourceLoop));
			ResourceThread.Name = "Resource thread";
			ResourceThread.Start();

			threads_ready |= ThreadReady.RenderThread;
			while (threads_ready != ThreadReady.All)
				Thread.Sleep(0);


			InitializeGraphics();
			old_time = System.Diagnostics.Stopwatch.GetTimestamp();

			dev.State.MultiSample = true;
			while (running)
			{
				long new_time = System.Diagnostics.Stopwatch.GetTimestamp();
				float time =  (new_time - old_time) / ((float)System.Diagnostics.Stopwatch.Frequency);
				lock (graphic_invoke)
				{
					while (graphic_invoke.Count > 0)
					{
						var act = graphic_invoke.Dequeue();
						if (act != null)
							act();
					}
				}
				if (sort_render_list)
				{
					
				}
				var res = scene.Resources.Janitorial();
				if (res.Count > 0)
				{
					foreach (var item in res)
					{
						item.Dispose();	
					}
					lock (scene.Resources.resources)
					{
						scene.Resources.Remove(res);
					}
				}
				// Wait until simulation thread has finished with one frame
				// or else it is not necessary to render the next frame (since nothing has happened)
				while (!provoke_render && running)
				{
					System.Threading.Thread.Sleep(0);
				}
				
				Render(dev, frame_time, total_time);
				//System.Threading.Thread.Sleep(0);
				fps = 1 / time;
				provoke_render = false;
				old_time = new_time;
			}

			scene.Dispose();
			CleanupResources();
			CleanupResources(); // Do this again to ensure that resources which are referenced by other resources are freed
			GraphicsClosing();
			scene.GraphicsDispose();
			dev.Dispose();
		}

		protected virtual void Render(Glorg2.Graphics.GraphicsDevice dev, float frame_time, float total_time)
		{
			if(scene.Background.w > 0)
				dev.Clear(Glorg2.Graphics.ClearFlags.Color | Glorg2.Graphics.ClearFlags.Depth, scene.Background);

			if (scene.Camera != null)
				dev.ProjectionMatrix = scene.Camera.GetProjectionMatrix();

			lock (scene.renderables)
			{
				foreach (var n in scene.renderables)
					(n as Scene.Node).InternalRender(frame_time, dev);
			}
			dev.Present();
		}
	}
	[System.Flags()]
	public enum ThreadReady
	{
		MainThread = 1,
		RenderThread = 2,
		ResourceThread = 4,
		PhysicsThread = 8,
		All = MainThread | RenderThread | ResourceThread | PhysicsThread

	}
}
