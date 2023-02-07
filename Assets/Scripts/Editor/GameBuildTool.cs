using System.Diagnostics;
using UnityEditor;
using UnityEditor.Build.Player;
using UnityEditor.EditorTools;
using UnityEngine;

namespace Tools
{
    public class GameBuildTool
    {
        [MenuItem("Tools/Build Client")]
        static void BuildClient()
        {
            string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "", "");
            string[] levels = new string[] {"Assets/Scene1.unity", "Assets/Scene2.unity"};

            // Build player.
            BuildPipeline.BuildPlayer(levels, path + "/Client.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);

            // Copy a file from the project folder to the build folder, alongside the built game.
            FileUtil.CopyFileOrDirectory("Assets/Templates/Readme.txt", path + "Readme.txt");

            // Run the game (Process class from System.Diagnostics).
            Process proc = new Process();
            proc.StartInfo.FileName = path + "/Client.exe";
            proc.Start();
        }
    }
}