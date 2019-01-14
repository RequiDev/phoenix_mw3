using Phoenix.CommandSystem;
using Phoenix.MemorySystem;
using Phoenix.ModernWarfare3.Enums;
using Phoenix.ModernWarfare3.Structs;
using Phoenix.Overlay;
using Phoenix.Structs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phoenix.Features
{
	internal class ESP
	{
		private static ProcessMemory Memory => Phoenix.Memory;
		private static OverlayWindow Overlay => Phoenix.Overlay;
		private static int Red;
		public static void Run()
		{
			if (Red == 0)
				Red = Overlay.Graphics.CreateBrush(Color.Red);

			if (!CommandHandler.GetParameter("esp", "active").Value.ToBool()) return;
			if (Phoenix.EntityList == null) return;
			if (Phoenix.EntityList.Players == null || Phoenix.EntityList.Players.Count < 1) return;
			var pLocal = Phoenix.EntityList.GetLocalPlayer();
			if (pLocal.ClientNum == -1) return;
			var localInfo = Phoenix.EntityList.GetLocalClient();

			foreach (var player in Phoenix.EntityList.Players)
			{
				var clientInfo = Phoenix.EntityList.Clients[player.ClientNum];
				if (player.IsValid == 0) continue;
				if (clientInfo.Team == localInfo.Team) continue;
				if ((player.IsAlive & 1) != 1) continue;

				float screen_x = 0, screen_y = 0;
				if (WorldToScreen(player.Origin, ref screen_x, ref screen_y))
				{
					float head_x = 0, head_y = 0;
					var head = player.Origin;
					head.Z += 60.0f;
					WorldToScreen(head, ref head_x, ref head_y);
					float draw_y = screen_y - head_y;

					if (draw_y < 6)
						draw_y = 6;

					// Calculate the width and height ratios for the boxes depending on the poses.
					float draw_x;
					if ((player.Flags & EntityFlags.EF_CROUCHED) == EntityFlags.EF_CROUCHED)
					{
						// Crouched.
						draw_y /= 1.5f;
						draw_x = draw_y / 1.5f;
					}
					else if ((player.Flags & EntityFlags.EF_PRONE) == EntityFlags.EF_PRONE)
					{
						// Proned.
						draw_y /= 3f;
						draw_x = draw_y * 2f;
					}
					else
					{
						// Standing.
						draw_x = draw_y / 2.75f;
					}

					float x = screen_x - (draw_x / 2),
						y = screen_y,
						w = draw_x,
						h = -draw_y;

					Overlay.Graphics.DrawCorneredBoxOutline((int)x, (int)y, (int)w, (int)h, Red); //psht
					//Overlay.Graphics.DrawCircle((int)screen_x, (int)screen_y, 10, Red, 1);
				}

					//Overlay.Graphics.DrawCorneredBoxOutline((int)(top.X - width), (int)top.Y, (int)(width * 2), (int)height, Red);
			}
		}

		public static bool WorldToScreen(Vector3D worldLocation, ref float screenX, ref float screenY)
		{
			ViewAngles View = Memory.Read<ViewAngles>(SignatureManager.GetViewAngle());
			RefDef Refdef = Memory.Read<RefDef>(SignatureManager.GetWorldToViewMatrix());
			Vector3D vTransform;

			Vector3D vLocal = worldLocation - View.Origin;
			vTransform.X = vLocal.Dot(Refdef.ViewAxis2);
			vTransform.Y = vLocal.Dot(Refdef.ViewAxis3);
			vTransform.Z = vLocal.Dot(Refdef.ViewAxis1);

			// Make sure the player is in front of us.
			if (vTransform.Z < 0.1)
				return false;

			screenX = (Overlay.Width / 2) * (1 - (vTransform.X / Refdef.FovX / vTransform.Z));
			screenY = (Overlay.Height / 2) * (1 - (vTransform.Y / Refdef.FovY / vTransform.Z));

			return true;
		}
	}
}
