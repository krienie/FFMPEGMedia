// Copyright 1998-2018 Epic Games, Inc. All Rights Reserved.

using UnrealBuildTool;
using System.IO;

public class FFMPEGMedia : ModuleRules
{
	private string ThirdPartyPath
	{
		get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "../../ThirdParty/")); }
	}
	public void LoadFFmpeg(ReadOnlyTargetRules Target)
	{
		string LibrariesPath = Path.Combine(ThirdPartyPath, "ffmpeg-7.1", "lib");

		System.Console.WriteLine("... LibrariesPath -> " + LibrariesPath);

		PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "avcodec.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "avdevice.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "avfilter.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "avformat.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "avutil.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "postproc.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "swresample.lib"));
		PublicAdditionalLibraries.Add(Path.Combine(LibrariesPath, "swscale.lib"));

		string[] dlls = {"avcodec-61.dll","avdevice-61.dll", "avfilter-10.dll", "avformat-61.dll", "avutil-59.dll", "postproc-58.dll", "swresample-5.dll", "swscale-8.dll"};

		string BinariesPath = Path.Combine(ThirdPartyPath, "ffmpeg-7.1", "bin");
		foreach (string dll in dlls)
		{
			PublicDelayLoadDLLs.Add(dll);
			RuntimeDependencies.Add(Path.Combine("$(TargetOutputDir)", dll), Path.Combine(BinariesPath, dll));

			System.Console.WriteLine("... BinariesPath -> " + Path.Combine(BinariesPath, dll));
		}

		PublicIncludePaths.Add(Path.Combine(ThirdPartyPath, "ffmpeg-7.1", "include"));
	}

	public FFMPEGMedia(ReadOnlyTargetRules Target) : base(Target)
	{
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		bEnableExceptions = true;

		DynamicallyLoadedModuleNames.AddRange(
			new string[] {
				"Media",
			});

		PrivateDependencyModuleNames.AddRange(
			new string[] {
				"Core",
				"CoreUObject",
				"Engine",
				"MediaUtils",
				"RenderCore",
				"FFMPEGMediaFactory",
				"Projects",
			});
			
		if (Target.Platform == UnrealTargetPlatform.Android)
		{
			PrivateDependencyModuleNames.AddRange(
				new string[]
				{
					"ApplicationCore",
					"Launch"
				}
			);
		}

		PrivateIncludePathModuleNames.AddRange(
			new string[] {
				"Media",
			});

		PrivateIncludePaths.AddRange(
			new string[] {
				"FFMPEGMedia/Private",
				"FFMPEGMedia/Private/Player",
				"FFMPEGMedia/Private/FFMPEG",
			});

		LoadFFmpeg(Target);
	}
}
