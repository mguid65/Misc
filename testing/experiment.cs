using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;


/* experiment profiling performance of large switch case vs dictionary
   did not expect it to be faster,
   actually seems to be slightly slower, probably due to 
*/
public class Program
{
	public static Dictionary<string, string[]> BlacklistedDomainsDictOfArrays = new Dictionary<string, string[]>
	{
		{ "NES",			new string[] { "System Bus", "PRG ROM", "PALRAM", "CHR VROM", "Battery RAM", "FDS Side"} },
		{ "GB",				new string[] { "ROM", "System Bus", "OBP", "BGP", "BOOTROM" } },
		{ "GBC",			new string[] { "ROM", "System Bus", "OBP", "BGP", "BOOTROM" } },
		{ "SNES",			new string[] { "CARTROM", "APURAM", "CGRAM", "System Bus", "SGB CARTROM" } },
		{ "N64",			new string[] { "System Bus", "PI Register", "EEPROM", "ROM", "SI Register", "VI Register", "RI Register", "AI Register" } },
		{ "PCE",			new string[] { "ROM", "System Bus", "System Bus (21 bit)" } },
		{ "SGX",			new string[] { "ROM", "System Bus", "System Bus (21 bit)" } },
		{ "GBA",			new string[] { "OAM", "BIOS", "PALRAM", "ROM", "System Bus" } },
		{ "SMS",			new string[] { "System Bus", "ROM" } },
		{ "GG",				new string[] { "System Bus", "ROM" } },
		{ "SG",				new string[] { "System Bus", "ROM" } },
		{ "32X_INTERIM",	new string[] { "MD CART", "CRAM", "VSRAM", "SRAM", "BOOT ROM", "32X FB", "CD BOOT ROM", "S68K BUS", "M68K BUS" } },
		{ "GEN",			new string[] { "MD CART", "CRAM", "VSRAM", "SRAM", "BOOT ROM", "32X FB", "CD BOOT ROM", "S68K BUS", "M68K BUS" } },
		{ "PSX",			new string[] { "BiosROM", "PIOMem" } },
		{ "A26",			new string[] { "System Bus" } },
		{ "A78",			new string[] { "System Bus" } },
		{ "LYNX",			new string[] { "Save RAM", "Cart B", "Cart A" } },
		{ "WSWAN",          new string[] { "ROM" } },
		{ "Coleco",			new string[] { "System Bus"} },
		{ "VB",				new string[] { "ROM"} },
		{ "SAT",			new string[] { "Backup RAM", "Boot Rom", "Backup Cart", "VDP1 Framebuffer", "VDP2 CRam", "Sound Ram" } },
		{ "INTV",			new string[] { "Graphics ROM", "System ROM", "Executive Rom" } },
		{ "APPLEII",		new string[] { "System Bus" } },
		{ "C64",			new string[] { "System Bus", "1541 Bus" } },
		{ "PCECD",			new string[] {} },
		{ "TI83",			new string[] {} },
		{ "SGB",			new string[] {} },
		{ "DGB",			new string[] {} }
	};
	
	public static Dictionary<string, List<string>> BlacklistedDomainsDictOfLists = new Dictionary<string, List<string>>
	{
		{ "NES",			new List<string> { "System Bus", "PRG ROM", "PALRAM", "CHR VROM", "Battery RAM", "FDS Side" } },
		{ "GB",				new List<string> { "ROM", "System Bus", "OBP", "BGP", "BOOTROM" } },
		{ "GBC",			new List<string> { "ROM", "System Bus", "OBP", "BGP", "BOOTROM" } },
		{ "SNES",			new List<string> { "CARTROM", "APURAM", "CGRAM", "System Bus", "SGB CARTROM" } },
		{ "N64",			new List<string> { "System Bus", "PI Register", "EEPROM", "ROM", "SI Register", "VI Register", "RI Register", "AI Register" } },
		{ "PCE",			new List<string> { "ROM", "System Bus", "System Bus (21 bit)" } },
		{ "SGX",			new List<string> { "ROM", "System Bus", "System Bus (21 bit)" } },
		{ "GBA",			new List<string> { "OAM", "BIOS", "PALRAM", "ROM", "System Bus" } },
		{ "SMS",			new List<string> { "System Bus", "ROM" } },
		{ "GG",				new List<string> { "System Bus", "ROM" } },
		{ "SG",				new List<string> { "System Bus", "ROM" } },
		{ "32X_INTERIM",	new List<string> { "MD CART", "CRAM", "VSRAM", "SRAM", "BOOT ROM", "32X FB", "CD BOOT ROM", "S68K BUS", "M68K BUS" } },
		{ "GEN",			new List<string> { "MD CART", "CRAM", "VSRAM", "SRAM", "BOOT ROM", "32X FB", "CD BOOT ROM", "S68K BUS", "M68K BUS" } },
		{ "PSX",			new List<string> { "BiosROM", "PIOMem" } },
		{ "A26",			new List<string> { "System Bus" } },
		{ "A78",			new List<string> { "System Bus" } },
		{ "LYNX",			new List<string> { "Save RAM", "Cart B", "Cart A" } },
		{ "WSWAN",          new List<string> { "ROM" } },
		{ "Coleco",			new List<string> { "System Bus"} },
		{ "VB",				new List<string> { "ROM"} },
		{ "SAT",			new List<string> { "Backup RAM", "Boot Rom", "Backup Cart", "VDP1 Framebuffer", "VDP2 CRam", "Sound Ram" } },
		{ "INTV",			new List<string> { "Graphics ROM", "System ROM", "Executive Rom" } },
		{ "APPLEII",		new List<string> { "System Bus" } },
		{ "C64",			new List<string> { "System Bus", "1541 Bus" } },
		{ "PCECD",			new List<string> {} },
		{ "TI83",			new List<string> {} },
		{ "SGB",			new List<string> {} },
		{ "DGB",			new List<string> {} }
	};
	
	public static List<string> LookupKeys = new List<string> {
		"NES",
		"GB",
		"GBC",
		"SNES",
		"N64",
		"PCE",
		"SGX",
		"GBA",
		"SMS",
		"GG",
		"SG",
		"32X_INTERIM",
		"GEN",
		"PSX",
		"A26",
		"A78",
		"LYNX",
		"WSWAN",
		"Coleco",
		"VB",
		"SAT",
		"INTV",
		"APPLEII",
		"C64",
		"PCECD",
		"TI83",
		"SGB",
		"DGB"
	};
	
	public static void Main()
	{
		Int64 TotalTicks = 0;
		//dict of arrays
		Console.WriteLine("Dictionary of Arrays\n");
		for (int i = 0; i < 25; i++) {
			Stopwatch sw = new Stopwatch();
			List<string> domainBlacklist = new List<string>();
			sw.Start();
			foreach(string key in LookupKeys) {
				domainBlacklist.AddRange(BlacklistedDomainsDictOfArrays[key]);
			}
			sw.Stop();
			TotalTicks += sw.ElapsedTicks;
			Console.WriteLine("Time: {0}ns \t{1}",sw.ElapsedTicks, domainBlacklist);
		}
		Console.WriteLine("AvgTime: {0}ns\n\n", TotalTicks / 25);
		
		TotalTicks = 0;
		
		//dict of lists
		Console.WriteLine("Dictionary of Lists\n");
		for (int i = 0; i < 25; i++) {
			Stopwatch sw = new Stopwatch();
			List<string> domainBlacklist = new List<string>();
			sw.Start();
			foreach(string key in LookupKeys) {
				domainBlacklist.AddRange(BlacklistedDomainsDictOfArrays[key]);
			}
			sw.Stop();
			TotalTicks += sw.ElapsedTicks;
			Console.WriteLine("Time: {0}ns \t{1}",sw.ElapsedTicks, domainBlacklist);
		}
		Console.WriteLine("AvgTime: {0}ns\n\n", TotalTicks / 25);
		
		TotalTicks = 0;
		
		//switch case with add
		Console.WriteLine("Switch Case with Add\n");
		for (int i = 0; i < 25; i++) {
			Stopwatch sw = new Stopwatch();
			List<string> domainBlacklist = new List<string>();
			sw.Start();
			foreach(string key in LookupKeys) {
				switch (key)
				{
					case "NES":     //Nintendo Entertainment system

						domainBlacklist.Add("System Bus");
						domainBlacklist.Add("PRG ROM");
						domainBlacklist.Add("PALRAM"); //Color Memory (Useless and disgusting)
						domainBlacklist.Add("CHR VROM"); //Cartridge
						domainBlacklist.Add("Battery RAM"); //Cartridge Save Data
						domainBlacklist.Add("FDS Side"); //ROM data for the FDS. Sadly uncorruptable.
						break;

					case "GB":      //Gameboy
					case "GBC":     //Gameboy Color
						domainBlacklist.Add("ROM"); //Cartridge
						domainBlacklist.Add("System Bus");
						domainBlacklist.Add("OBP"); //SGB dummy domain doesn't do anything in sameboy
						domainBlacklist.Add("BGP");  //SGB dummy domain doesn't do anything in sameboy
						domainBlacklist.Add("BOOTROM"); //Sameboy SGB Bootrom
						break;

					case "SNES":    //Super Nintendo

						domainBlacklist.Add("CARTROM"); //Cartridge
						domainBlacklist.Add("APURAM"); //SPC700 memory
						domainBlacklist.Add("CGRAM"); //Color Memory (Useless and disgusting)
						domainBlacklist.Add("System Bus"); // maxvalue is not representative of chip (goes ridiculously high)
						domainBlacklist.Add("SGB CARTROM"); // Supergameboy cartridge

						break;

					case "N64":     //Nintendo 64
						domainBlacklist.Add("System Bus");
						domainBlacklist.Add("PI Register");
						domainBlacklist.Add("EEPROM");
						domainBlacklist.Add("ROM");
						domainBlacklist.Add("SI Register");
						domainBlacklist.Add("VI Register");
						domainBlacklist.Add("RI Register");
						domainBlacklist.Add("AI Register");
						break;

					case "PCE":     //PC Engine / Turbo Grafx
					case "SGX":     //Super Grafx
						domainBlacklist.Add("ROM");
						domainBlacklist.Add("System Bus"); //BAD THINGS HAPPEN WITH THIS DOMAIN
						domainBlacklist.Add("System Bus (21 bit)");
						break;

					case "GBA":     //Gameboy Advance
						domainBlacklist.Add("OAM");
						domainBlacklist.Add("BIOS");
						domainBlacklist.Add("PALRAM");
						domainBlacklist.Add("ROM");
						domainBlacklist.Add("System Bus");
						break;

					case "SMS":     //Sega Master System
						domainBlacklist.Add("System Bus"); // the game cartridge appears to be on the system bus
						domainBlacklist.Add("ROM");
						break;

					case "GG":      //Sega GameGear
						domainBlacklist.Add("System Bus"); // the game cartridge appears to be on the system bus
						domainBlacklist.Add("ROM");
						break;

					case "SG":      //Sega SG-1000
						domainBlacklist.Add("System Bus");
						domainBlacklist.Add("ROM");
						break;

					case "32X_INTERIM":
					case "GEN":     //Sega Genesis and CD
						domainBlacklist.Add("MD CART");
						domainBlacklist.Add("CRAM"); //Color Ram
						domainBlacklist.Add("VSRAM"); //Vertical scroll ram. Do you like glitched scrolling? Have a dedicated domain...
						domainBlacklist.Add("SRAM"); //Save Ram
						domainBlacklist.Add("BOOT ROM"); //Genesis Boot Rom
						domainBlacklist.Add("32X FB"); //32X Sprinkles
						domainBlacklist.Add("CD BOOT ROM"); //Sega CD boot rom
						domainBlacklist.Add("S68K BUS");
						domainBlacklist.Add("M68K BUS");
						break;

					case "PSX":     //Sony Playstation 1
						domainBlacklist.Add("BiosROM");
						domainBlacklist.Add("PIOMem");
						break;

					case "A26":     //Atari 2600
						domainBlacklist.Add("System Bus");
						break;

					case "A78":     //Atari 7800
						domainBlacklist.Add("System Bus");
						break;

					case "LYNX":    //Atari Lynx
						domainBlacklist.Add("Save RAM");
						domainBlacklist.Add("Cart B");
						domainBlacklist.Add("Cart A");
						break;

					case "WSWAN":   //Wonderswan
						domainBlacklist.Add("ROM");
						break;

					case "Coleco":  //Colecovision
						domainBlacklist.Add("System Bus");
						break;

					case "VB":      //Virtualboy
						domainBlacklist.Add("ROM");
						break;

					case "SAT":     //Sega Saturn
						domainBlacklist.Add("Backup RAM");
						domainBlacklist.Add("Boot Rom");
						domainBlacklist.Add("Backup Cart");
						domainBlacklist.Add("VDP1 Framebuffer"); //Sprinkles
						domainBlacklist.Add("VDP2 CRam"); //VDP 2 color ram (pallettes)
						domainBlacklist.Add("Sound Ram"); //90% chance of killing the audio
						break;

					case "INTV": //Intellivision
						domainBlacklist.Add("Graphics ROM");
						domainBlacklist.Add("System ROM");
						domainBlacklist.Add("Executive Rom"); //??????
						break;

					case "APPLEII": //Apple II
						domainBlacklist.Add("System Bus");
						break;

					case "C64":     //Commodore 64
						domainBlacklist.Add("System Bus");
						domainBlacklist.Add("1541 Bus");
						break;

					case "PCECD":   //PC-Engine CD / Turbo Grafx CD
					case "TI83":    //Ti-83 Calculator
					case "SGB":     //Super Gameboy
					case "DGB":
						break;
				}
			}
			TotalTicks += sw.ElapsedTicks;
			Console.WriteLine("Time: {0}ns \t{1}",sw.ElapsedTicks, domainBlacklist);
		}
		Console.WriteLine("AvgTime: {0}ns\n\n", TotalTicks / 25);
		
		TotalTicks = 0;
		
		//switch case with AddRange
		Console.WriteLine("Switch Case with AddRange\n");
		for (int i = 0; i < 25; i++) {
			Stopwatch sw = new Stopwatch();
			List<string> domainBlacklist = new List<string>();
			sw.Start();
			foreach(string key in LookupKeys) {
				switch (key)
				{
					case "NES":     //Nintendo Entertainment system

						domainBlacklist.AddRange(new List<string> { "System Bus", "PRG ROM", "PALRAM", "CHR VROM", "Battery RAM", "FDS Side" });
						break;

					case "GB":      //Gameboy
					case "GBC":     //Gameboy Color
						domainBlacklist.AddRange(new List<string> { "ROM", "System Bus", "OBP", "BGP", "BOOTROM" });
						break;

					case "SNES":    //Super Nintendo
						domainBlacklist.AddRange(new List<string> { "CARTROM", "APURAM", "CGRAM", "System Bus", "SGB CARTROM" });
						break;

					case "N64":     //Nintendo 64
						domainBlacklist.AddRange(new List<string> { "System Bus", "PI Register", "EEPROM", "ROM", "SI Register", "VI Register", "RI Register", "AI Register" });
						break;

					case "PCE":     //PC Engine / Turbo Grafx
					case "SGX":     //Super Grafx
						domainBlacklist.AddRange(new List<string> { "ROM", "System Bus", "System Bus (21 bit)" });
						break;

					case "GBA":     //Gameboy Advance
						domainBlacklist.AddRange(new List<string> { "OAM", "BIOS", "PALRAM", "ROM", "System Bus" });
						break;

					case "SMS":     //Sega Master System
						domainBlacklist.AddRange(new List<string> { "System Bus", "ROM" });
						break;

					case "GG":      //Sega GameGear
						domainBlacklist.AddRange(new List<string> { "System Bus", "ROM" });
						break;

					case "SG":      //Sega SG-1000
						domainBlacklist.AddRange(new List<string> { "System Bus", "ROM" });
						break;

					case "32X_INTERIM":
					case "GEN":     //Sega Genesis and CD
						domainBlacklist.AddRange(new List<string> { "MD CART", "CRAM", "VSRAM", "SRAM", "BOOT ROM", "32X FB", "CD BOOT ROM", "S68K BUS", "M68K BUS" });
						break;

					case "PSX":     //Sony Playstation 1
						domainBlacklist.AddRange(new List<string> { "BiosROM", "PIOMem" });
						break;

					case "A26":     //Atari 2600
						domainBlacklist.Add("System Bus");
						break;

					case "A78":     //Atari 7800
						domainBlacklist.Add("System Bus");
						break;

					case "LYNX":    //Atari Lynx
						domainBlacklist.AddRange(new List<string> { "Save RAM", "Cart B", "Cart A" });
						break;

					case "WSWAN":   //Wonderswan
						domainBlacklist.Add("ROM");
						break;

					case "Coleco":  //Colecovision
						domainBlacklist.Add("System Bus");
						break;

					case "VB":      //Virtualboy
						domainBlacklist.Add("ROM");
						break;

					case "SAT":     //Sega Saturn
						domainBlacklist.AddRange(new List<string> { "Backup RAM", "Boot Rom", "Backup Cart", "VDP1 Framebuffer", "VDP2 CRam", "Sound Ram" });
						break;

					case "INTV": //Intellivision
						domainBlacklist.AddRange(new List<string> { "Graphics ROM", "System ROM", "Executive Rom" });
						break;

					case "APPLEII": //Apple II
						domainBlacklist.Add("System Bus");
						break;

					case "C64":     //Commodore 64
						domainBlacklist.AddRange(new List<string> { "System Bus", "1541 Bus" });
						break;

					case "PCECD":   //PC-Engine CD / Turbo Grafx CD
					case "TI83":    //Ti-83 Calculator
					case "SGB":     //Super Gameboy
					case "DGB":
						break;
				}
			}
			TotalTicks += sw.ElapsedTicks;
			Console.WriteLine("Time: {0}ns \t{1}",sw.ElapsedTicks, domainBlacklist);
		}
		Console.WriteLine("AvgTime: {0}ns\n\n", TotalTicks / 25);
	}
}
