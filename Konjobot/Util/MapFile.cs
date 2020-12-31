using KonjoBot.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace KonjoBot.Util
{
	public	class MapFile { 
	public MapFile(string Filename)
	{
		Location locationFromMapFile = this.GetLocationFromMapFile(Filename);
		this.Floor = locationFromMapFile.Z;
		this.myPicture = new Bitmap(Filename);
		this.Boundary = new Rectangle(locationFromMapFile.X, locationFromMapFile.Y, this.myPicture.Width, this.myPicture.Height);
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000E080 File Offset: 0x0000C280
	private Location GetLocationFromMapFile(string name)
	{
		string[] array = name.Split(new char[]
		{
				'_'
		}, StringSplitOptions.RemoveEmptyEntries);
		int x = int.Parse(array[2]);
		int y = int.Parse(array[3]);
		string text = array[4];
		string extension = Path.GetExtension(text);
		string s = text.Substring(0, text.Length - extension.Length);
		int z = int.Parse(s);
		return new Location(x, y, z);
	}

	// Token: 0x04000125 RID: 293
	public Rectangle Boundary;

	// Token: 0x04000126 RID: 294
	public int Floor;

	// Token: 0x04000127 RID: 295
	public Bitmap myPicture;
}
}
