using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sandbox.ModAPI.Ingame;


namespace UraanNubGlobbler
{
	public class Program : IMyProgrammableBlock
	{

		IMyGridTerminalSystem GridTerminalSystem = null;

		#region CodeEditor
		IMyGridTerminalSystem self;

		void Main()
		{
			self = GridTerminalSystem;

			var light = self.GetBlockWithName ("Inventory Full Light");
			if (light == null)
				return;
			var l = new List<IMyTerminalBlock> ();
			self.GetBlocksOfType<IMyCargoContainer> (l);
		
			List<IMyCargoContainer> cargo = this.GetBlockOfType<IMyCargoContainer> ();

			if (All<IMyCargoContainer> (cargo, (c => c.GetInventory (0).IsFull))) {
				light.ApplyAction ("OnOff_On");
			} else {
				light.ApplyAction ("OnOff_Off");
			}

			var timer = (IMyTimerBlock)self.GetBlockWithName ("Timer Block") ;
			timer.ApplyAction ("Start");
					
		}


		public List<T> GetBlockOfType<T>() where T : IMyTerminalBlock
		{
			var l = new List<IMyTerminalBlock> ();
			self.GetBlocksOfType<T> (l);
			var nl = new List<T>();
			for (int i = 0; i < l.Count; i++)
				nl.Add ((T)l[i]);

			return nl;
		}

		public bool All<T>(List<T> l, Func<T,bool> test)
		{
			for (int i = 0; i < l.Count; i++) {
				if (!test (l [0])) {

					return false;
				}
			}
			return true;
		}

		#endregion


	}
}

