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
		System.Drawing.Rectangle vp;
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
		private void StartInternal()
		{
			graphic_invoke = new Queue<Action>();
			scene = new Glorg2.Scene.Scene(this);
			RenderThread = new System.Threading.Thread(new System.Threading.ThreadStart(RenderLoop));
			running = true;
			target.Show();
			target.HandleDestroyed += (sender, e) => { running = false; System.Windows.Forms.Application.Exit(); };
			target.Resize += (sender, e) => vp = new System.Drawing.Rectangle(0, 0, target.ClientSize.Width, target.ClientSize.Height);
		}
		public void Start(System.Windows.Forms.Control target)
		{
			this.target = target;
			StartInternal();
			RenderThread.Start();
			MainLoop();
		}
		public void Start()
		{
			target = new System.Windows.Forms.Form();
			StartInternal();
			RenderThread.Start();
		}
		public void StartAsync()
		{
			target = new System.Windows.Forms.Form();
			StartInternal();
			SimulationThread = new System.Threading.Thread(new System.Threading.ThreadStart(MainLoop));
			RenderThread.Start();
			SimulationThread.Start();
		}
		public void StartAsync(System.Windows.Forms.Control target)
		{
			this.target = target;
			SimulationThread = new System.Threading.Thread(new System.Threading.ThreadStart(MainLoop));
			StartInternal();
			RenderThread.Start();
			SimulationThread.Start();
		}
		protected virtual void FrameStep(float time)
		{
		}
		protected virtual void GraphicsInit()
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

		public void Exit()
		{
			running = false;
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

		private void MainLoop()
		{
			long old_time;
			Init();
			old_time = System.Diagnostics.Stopwatch.GetTimestamp();
			while (running)
			{
				long new_time = System.Diagnostics.Stopwatch.GetTimestamp();
				frame_time = (new_time - old_time) / (float)System.Diagnostics.Stopwatch.Frequency;
				FrameStep(frame_time);
				scene.ParentNode.InternalProcess(frame_time);
				total_time += frame_time;
				provoke_render = true;
				System.Windows.Forms.Application.DoEvents();
				old_time = new_time;				
			}
			scene.Dispose();
			Closing();
		}
		private void RenderLoop()
		{
			long old_time = 0;
			IntPtr handle = (IntPtr)target.Invoke(new Func<IntPtr>(() => target.Handle));
			dev = new Glorg2.Graphics.GraphicsDevice(handle);
			GraphicsInit();
			old_time = System.Diagnostics.Stopwatch.GetTimestamp();
			target.Invoke(new Action(() => target.Size = new System.Drawing.Size(640, 480)));
			while (running)
			{
				long new_time = System.Diagnostics.Stopwatch.GetTimestamp();
				float time =  (new_time - old_time) / ((float)System.Diagnostics.Stopwatch.Frequency);
				dev.Viewport = vp;
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
				while (!provoke_render && running)
				{
					System.Threading.Thread.Sleep(0);
				}
				Render(dev, frame_time, total_time);

				fps = 1 / time;
				provoke_render = false;
				old_time = new_time;
			}
			GraphicsClosing();
			dev.Dispose();
		}

		protected virtual void Render(Glorg2.Graphics.GraphicsDevice dev, float frame_time, float total_time)
		{
			dev.Clear(Glorg2.Graphics.ClearFlags.Color | Glorg2.Graphics.ClearFlags.Depth, new Vector4(0, 0, .4f, 0));
			scene.ParentNode.InternalRender(frame_time, dev);			
			dev.Present();
		}
	}
}
