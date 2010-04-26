using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Glorg2
{
	public abstract class Game
	{
		System.Threading.Thread RenderThread;
		System.Threading.Thread SimulationThread;
		Glorg2.Graphics.GraphicsDevice dev;
		System.Windows.Forms.Control target;
		
		volatile bool running;
		volatile bool provoke_render;
		volatile float fps;
		volatile float frame_time;
		volatile float total_time;
		Scene.Scene scene;
		Queue<Action> graphic_invoke;

		public Scene.Scene Scene { get { return scene; } }

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
			RenderThread.Start();
			MainLoop();
		}
		public void Start()
		{
			target = new System.Windows.Forms.Form();
			target.Show();
			target.Update();
			ready = true;
			StartInternal();
			RenderThread.Start();
            MainLoop();
		}
		public void StartAsync()
		{
			target = new System.Windows.Forms.Form();
			target.Show();
			target.Update();
			ready = true;
			StartInternal();
			SimulationThread = new System.Threading.Thread(new System.Threading.ThreadStart(MainLoop));
			SimulationThread.Name = "Simulation thread";
			RenderThread.Start();
			SimulationThread.Start();
		}
		public void StartAsync(System.Windows.Forms.Control target)
		{
			this.target = target;
			SimulationThread = new System.Threading.Thread(new System.Threading.ThreadStart(MainLoop));
			SimulationThread.Name = "Simulation thread";
			StartInternal();
			RenderThread.Start();
			SimulationThread.Start();
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

		private void JoinThread(System.Threading.Thread th)
		{
			while (th.ThreadState == System.Threading.ThreadState.Running)
			{
				System.Windows.Forms.Application.DoEvents();
			}
		}
		public void Exit()
		{
			running = false;
			JoinThread(RenderThread);
			if (SimulationThread != null)
				JoinThread(SimulationThread);
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
		private void MainLoop()
		{
			long old_time;
			Init();
			old_time = System.Diagnostics.Stopwatch.GetTimestamp();
			target.Resize += (sender, e) => { GraphicInvoke(new Action(InternalSizeChanged));};
			while (running)
			{
				long new_time = System.Diagnostics.Stopwatch.GetTimestamp();
				frame_time = (new_time - old_time) / (float)System.Diagnostics.Stopwatch.Frequency;
				var res = scene.Resources.Janitorial();
				if (res.Count > 0)
				{
					GraphicInvoke(() =>
					{
						foreach (var r in res)
						{
							r.DoDispose();
						}
						lock (scene.Resources)
						{
							scene.Resources.Remove(res);
						}
					});
				}

				scene.sim_time += frame_time;
				FrameStep(frame_time);
				if (scene.camera.Value != null)
					scene.local_transform = scene.camera.Value.GetCameraTransform();
				else
					scene.local_transform = Matrix.Identity;						 
				scene.ParentNode.InternalProcess(frame_time);
				total_time += frame_time;
				provoke_render = true;
				System.Windows.Forms.Application.DoEvents();
				old_time = new_time;				

			}
			Closing();
		}

		private void InitLights()
		{
			foreach (var node in this.Scene.items)
			{
				var l = node as Scene.Light;
				if (l != null)
				{
					dev.ModelviewMatrix = l.absolute_transform;
					l.SetLight();
				}
			}
			dev.ModelviewMatrix = Matrix.Identity;
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

				// Wait until simulation thread has finished with one frame
				// or else it is not necessary to render the next frame (since nothing has happened)
				
				InitLights();
				while (!provoke_render && running)
				{
					System.Threading.Thread.Sleep(0);
				}
				
				Render(dev, frame_time, total_time);

				fps = 1 / time;
				provoke_render = false;
				old_time = new_time;
				Glorg2.Scene.Light.DisableAllLights();
			}
			/*while (!scene_disposed)
				System.Threading.Thread.Sleep(0);*/
			scene.Dispose();
			CleanupResources();
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
			scene.ParentNode.InternalRender(frame_time, dev);			
			dev.Present();
		}
	}
}
